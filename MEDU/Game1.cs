using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MEDU
{
    enum MenuState
    {
        Menu,
        LevelSelect,
        Level,
        LevelFailed,
        LevelComplete,
        Pause,
    }
    public class Game1 : Game
    {
        //field
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private double timer;
        private int levelNum;
        private Level currentLevel;
        private Level[] levels;
        private Player player;
        private MenuState menuState;
        private Point cameraCenterOffset;
        private MouseState prevMsState;


        //menu fields
        private Rectangle Start;
        private Rectangle End;
        private Vector2 cameraPosition;
        private Texture2D start_texture;
        private Texture2D end_texture;
        private MouseState pms;

        //level select fields
        private Rectangle[] levelSelection;
        private Texture2D[] levelSelectTextures;
        private Rectangle select;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Start = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 - 50, 100, 100);
            End = new Rectangle(Start.X,Start.Y, 100, 100);
            cameraCenterOffset = new Point(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            menuState = MenuState.Menu;
            player = new Player(new Rectangle(10,10,100,100), Content.Load<Texture2D>("TempAvatar"));
            select = new Rectangle(
                _graphics.PreferredBackBufferWidth / 2 - 25,
                _graphics.PreferredBackBufferHeight - 75,
                50,
                50);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            start_texture = Content.Load<Texture2D>("Start");
            end_texture = Content.Load<Texture2D>("End");
            Level.LoadAssets(Content);
            levels = new Level[] { Level.LoadLevelFromFile("Content/test level.level") };
            levelSelection = new Rectangle[levels.Length];
            levelSelectTextures = new Texture2D[levels.Length];
            for (int i = 0; i < levelSelection.Length; i++)
            {
                levelSelection[i] = new Rectangle(
                    i * _graphics.PreferredBackBufferWidth / levelSelection.Length,
                    0,
                    _graphics.PreferredBackBufferWidth / levelSelection.Length,
                    _graphics.PreferredBackBufferHeight - 100);
                // TODO: replace texture with something that depicts the level
                levelSelectTextures[i] = Content.Load<Texture2D>("Start");
            }
            
            //System.Diagnostics.Debug.WriteLine(Level.LoadLevelFromFile("Content/test level.level").GetData());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //mouse position
            MouseState ms = Mouse.GetState();

            //Basic finite state machine for going through basic game
            switch (menuState)
            {
                case (MenuState.Menu):
                    if (Start.Contains(ms.Position) && singleLeftClick(ms))
                    {
                        menuState = MenuState.LevelSelect;
                    }
                    break;
                case (MenuState.LevelSelect):
                    int selectedLevel = 0;
                    for (int i = 0; i < levelSelection.Length; i++)
                    {
                        if (levelSelection[i].Contains(ms.Position) && singleLeftClick(ms))
                        {
                            selectedLevel = i;
                        }
                    }
                    if (select.Contains(ms.Position) && singleLeftClick(ms))
                    {
                        GoToLevel(selectedLevel);
                        menuState = MenuState.Level;
                    }
                    break;
                case (MenuState.Level):
                    player.update(gameTime);
                    HandleCollision();
                    CheckIfPlayerOutofBounds(player);
                    cameraPosition = (player.Transform.Location - cameraCenterOffset).ToVector2();
                    if (!player.IsAlive)
                    {
                        menuState = MenuState.LevelFailed;
                    }
                    if (player.Transform.Intersects(currentLevel.EndTrigger))
                        menuState = MenuState.Menu;
                    break;
                case (MenuState.LevelFailed):
                    if (End.Contains(ms.Position) && singleLeftClick(ms))
                    {
                        menuState = MenuState.Menu;
                    }
                    break;
            }
            prevMsState = ms;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //temporary menu demo draws
            switch (menuState)
            {
                case (MenuState.Menu):
                    _spriteBatch.Draw(start_texture,Start, Color.White);
                    break;
                case (MenuState.LevelSelect):
                    for (int i = 0; i < levelSelection.Length; i++)
                    {
                        _spriteBatch.Draw(levelSelectTextures[i], levelSelection[i], Color.White);
                    }
                    _spriteBatch.Draw(start_texture, select, Color.White);
                    break;
                case (MenuState.Level):
                    currentLevel.Draw(_spriteBatch, cameraPosition);
                    player.draw(_spriteBatch, cameraPosition);
                    break;
                case (MenuState.LevelFailed):
                    _spriteBatch.Draw(end_texture, End, Color.White);
                    break;
            }

            _spriteBatch.End();



            base.Draw(gameTime);
        }
        

        public void GoToLevel(int level)
        {
            currentLevel = levels[level];
            levelNum = level;
            player.Reset(currentLevel.PlayerStartPos);
        }

        public void GoToMenu()
        {

        }
        
        /// <summary>
        /// Makes character dead if they run out of bounds
        /// </summary>
        private void CheckIfPlayerOutofBounds(Player player)
        {
            Rectangle position = player.Transform;
            if(position.Y > currentLevel.DeathPlaneY)
            {
                player.IsAlive = false;
            }
        }

        public void HandleCollision()
        {
            List<Platform> objects = currentLevel.Platforms;
            Rectangle playerRect = player.Transform;
            //if we don't detect ground collision, IsOnGround will default to false
            player.IsOnGround = false;
            foreach (Platform platform in objects)
            {
                if (!playerRect.Intersects(platform.Transform))
                    continue;

                Rectangle overlap = Rectangle.Intersect(platform.Transform, playerRect);

                if (!platform.IsSafe)
                {
                    //implement code for player dying
                    player.IsAlive = false;
                }

                //for pass-through platforms, only make sure the player doesn't fall through the top of it
                if (platform.PassThrough)
                {
                    //if not falling downwards, don't worry about collision
                    if (player.PlayerVelocity.Y < 0)
                        break;
                    //only collide if player's center is above the platform
                    else if (playerRect.Center.Y < platform.Transform.Top)
                    {
                        //keep the player slightly inside the platform so future collision checks work correctly
                        playerRect.Y = platform.Transform.Top - playerRect.Height + 1;
                        player.PlayerVelocity = new Vector2(player.PlayerVelocity.X, 0);
                        player.IsOnGround = true;
                    }
                }
                //all platforms are currently passthrough, so this code is never run
                else
                {
                    ////Resolve horizontally only if the overlap's width is less than its height
                    ////if the overlap is a square, prioritize horizontal resolution
                    //if (overlap.Width > overlap.Height || overlap.Width == 0)
                    //    continue;

                    ////if to the left of the obstacle, move left. otherwise, move right
                    //if (playerRect.X < platform.Position.X)
                    //    playerRect.X -= overlap.Width;
                    //else
                    //    playerRect.X += overlap.Width;

                    //player.Position = playerRect;
                    ////at this point, all horizontal collisions should be resolved, so there's no need for a width/height check
                    //if (player.Position.Intersects(platform.Position))
                    //{
                    //    if (overlap.Height == 0)
                    //        continue;

                    //    //if above the obstacle, move up. otherwise, move down
                    //    if (playerRect.Y < platform.Position.Y)
                    //    {
                    //        if (platform.PassThrough)
                    //            playerRect.Y += overlap.Height;
                    //        else
                    //            playerRect.Y -= overlap.Height;
                    //    }
                    //    else
                    //        playerRect.Y += overlap.Height;


                    //    player.JumpVelocity = 0;
                    //    player.Position = playerRect;
                    //}
                }
            }
            player.Transform = playerRect;
        }

        public bool singleLeftClick(MouseState ms)
        {
            return ms.LeftButton == ButtonState.Pressed && prevMsState.LeftButton != ButtonState.Pressed;
        }
    }
}
