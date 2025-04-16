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
        private Texture2D title;
        private Rectangle titleRect;

        //level select fields
        private Rectangle[] levelSelection;
        private Texture2D[] levelSelectTextures;
        private Rectangle select;

        // level complete
        private SpriteFont font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Start = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 + 50, 150, 150);
            End = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 75,150, 150);
            titleRect = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 200, GraphicsDevice.Viewport.Height / 3 - 100, 400, 200);
            cameraCenterOffset = new Point(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            menuState = MenuState.Menu;
            player = new Player(new Rectangle(10,10,Level.TILESIZE,Level.TILESIZE*2), Content.Load<Texture2D>("CharacterRight"));
            select = new Rectangle(
                _graphics.PreferredBackBufferWidth / 2 - 35,
                _graphics.PreferredBackBufferHeight - 75,
                70,
                70);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            start_texture = Content.Load<Texture2D>("Start");
            end_texture = Content.Load<Texture2D>("End");
            Level.LoadAssets(Content);
            levels = new Level[] { Level.LoadLevelFromFile("Content/Main Level.level") };
            levelSelection = new Rectangle[levels.Length];
            levelSelectTextures = new Texture2D[levels.Length];
            for (int i = 0; i < levelSelection.Length; i++)
            {
                levels[i].Completed = false;
                levelSelection[i] = new Rectangle(
                    i * _graphics.PreferredBackBufferWidth / levelSelection.Length + 25,
                    70,
                    _graphics.PreferredBackBufferWidth / levelSelection.Length - 50,
                    _graphics.PreferredBackBufferHeight - 150);
                // TODO: replace texture with something that depicts the level
                levelSelectTextures[i] = Content.Load<Texture2D>("pixel");
            }
            font = Content.Load<SpriteFont>("spritefont");
            title = Content.Load<Texture2D>("TitleCard");

            //System.Diagnostics.Debug.WriteLine(Level.LoadLevelFromFile("Content/test level.level").GetData());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //LevelTest.Update(gameTime);
            //return;

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
                    HandlePhysics(gameTime);
                    CheckIfPlayerOutofBounds(player);
                    cameraPosition = (player.Transform.Center - cameraCenterOffset).ToVector2();
                    if (!player.IsAlive)
                        menuState = MenuState.LevelFailed;
                    if (player.Transform.Intersects(currentLevel.EndTrigger))
                    {
                        currentLevel.Completed = true;
                        menuState = MenuState.LevelComplete;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                        menuState = MenuState.Pause;
                    break;
                case (MenuState.Pause):
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                        menuState = MenuState.Level; 
                    break;
                case (MenuState.LevelComplete):
                    if (singleLeftClick(ms))
                    {
                        menuState = MenuState.Menu;
                    }
                    break;
                case (MenuState.LevelFailed):
                    if (End.Contains(ms.Position) && singleLeftClick(ms))
                    {
                        menuState = MenuState.LevelSelect;
                    }
                    break;
            }
            prevMsState = ms;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //LevelTest.Draw(_spriteBatch);
            //return;

            _spriteBatch.Begin();
            //temporary menu demo draws
            switch (menuState)
            {
                case (MenuState.Menu):
                    _spriteBatch.Draw(title, titleRect, Color.White);
                    _spriteBatch.Draw(start_texture,Start, Color.White);
                    break;
                case (MenuState.LevelSelect):
                    _spriteBatch.DrawString(font, "LEVEL SELECTION", new Vector2(200, 10), Color.White);
                    for (int i = 0; i < levelSelection.Length; i++)
                    {
                        Color color = Color.White;
                        if (levels[i].Completed)
                            color = Color.Gray;
                        _spriteBatch.Draw(levelSelectTextures[i], levelSelection[i], color);
                    }
                    _spriteBatch.Draw(start_texture, select, Color.White);
                    break;
                case (MenuState.Level):
                    currentLevel.Draw(_spriteBatch, cameraPosition);
                    player.draw(_spriteBatch, cameraPosition);
                    break;
                case (MenuState.Pause):
                    _spriteBatch.DrawString(font, "GAME PAUSED", new Vector2(230, _graphics.PreferredBackBufferHeight / 2 - 50), Color.White);
                    break;
                case (MenuState.LevelComplete):
                    _spriteBatch.DrawString(font, "LEVEL COMPLETE", new Vector2(200, _graphics.PreferredBackBufferHeight / 2 - 50), Color.White);
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

        public void HandlePhysics(GameTime gameTime)
        {
            Rectangle oldPlayerTransform = player.Transform;
            Rectangle newPlayerTransform = player.Transform;
            //if we don't detect ground collision, IsOnGround will default to false
            player.IsOnGround = false;
            player.IsOnLeftWall = false;
            player.IsOnRightWall = false;
            //Move player with velocity
            newPlayerTransform.Offset(player.PlayerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //Check collisions
            List<Platform> objects = currentLevel.Platforms;
            List<Platform> intersections = new List<Platform>();
            foreach (Platform platform in objects)
            {
                //Ignore platforms not being collided with
                if (!newPlayerTransform.Intersects(platform.Transform))
                    continue;

                //If the platform is deadly, kill the player. Any further collisions are irrelevant.
                if (!platform.IsSafe)
                {
                    //implement code for player dying
                    player.IsAlive = false;
                    return;
                }

                //Record the intersection to process it later.
                intersections.Add(platform);
            }

            //resolve horizontally first
            for(int i = intersections.Count - 1; i >= 0; i--)
            {
                Platform platform = intersections[i];
                //pass-through platforms have no horizontal collision
                if (platform.PassThrough)
                {
                    continue;
                }
                else
                {
                    Rectangle overlap = Rectangle.Intersect(platform.Transform, newPlayerTransform);

                    //if there's no overlap, there's no more intersection!
                    //(this would be pretty common for horizontal collisions since each row of tiles have their own collision box)
                    if (overlap.Width == 0)
                    {
                        intersections.RemoveAt(i);
                        continue;
                    }

                    //Resolve horizontally only if the overlap's width is less than its height
                    //if the overlap is a square, prioritize horizontal resolution
                    if (overlap.Width > overlap.Height)
                    {
                        continue;
                    }

                    //if to the left of the obstacle, move left. otherwise, move right
                    if (newPlayerTransform.X < platform.Transform.X)
                    {
                        newPlayerTransform.X -= overlap.Width;
                    }
                    else
                    {
                        newPlayerTransform.X += overlap.Width;
                    }
                    intersections.RemoveAt(i);
                }
            }

            //resolve vertically
            for (int i = intersections.Count - 1; i >= 0; i--)
            {
                Platform platform = intersections[i];
                Rectangle overlap = Rectangle.Intersect(platform.Transform, newPlayerTransform);

                if (overlap.Height == 0)
                    continue;

                if (platform.PassThrough)
                {
                    //ignore pass-through collision when moving up
                    if (player.PlayerVelocity.Y < 0)
                        continue;
                    //collide if the player was above the platform (with leeway)
                    if (oldPlayerTransform.Bottom < platform.Transform.Top + 5)
                    {
                        //keep the player slightly inside the platform so future collision checks work correctly
                        newPlayerTransform.Y = platform.Transform.Top - newPlayerTransform.Height + 1;
                        player.PlayerVelocity = new Vector2(player.PlayerVelocity.X, 0);
                        player.IsOnGround = true;
                    }
                }

                else
                {
                    //if above the obstacle, move up. otherwise, move down
                    if (newPlayerTransform.Y < platform.Transform.Y)
                    {
                        //keep the player slightly inside the platform so future collision checks work correctly
                        newPlayerTransform.Y = platform.Transform.Top - newPlayerTransform.Height + 1;

                        //only hit the floor if moving down (or staying still)
                        if (player.PlayerVelocity.Y >= 0)
                        {
                            player.PlayerVelocity = new Vector2(player.PlayerVelocity.X, 0);
                            player.IsOnGround = true;
                        }
                    }
                    else
                    {
                        newPlayerTransform.Y += overlap.Height;

                        //only hit the ceiling if moving up (or staying still)
                        if(player.PlayerVelocity.Y <= 0)
                            player.PlayerVelocity = new Vector2(player.PlayerVelocity.X, 0);
                    }
                }
            }

            player.Transform = newPlayerTransform;
        }

        //private void ResolveCollisions()
        //{
        //    Rectangle playerRect = GetPlayerRect();
        //    //find all intersections
        //    List<Rectangle> intersections = new List<Rectangle>();
        //    foreach (Rectangle obstacle in obstacleRects)
        //    {
        //        if (playerRect.Intersects(obstacle))
        //            intersections.Add(obstacle);
        //    }

            ////resolve horizontally
            //foreach (Rectangle intersection in intersections)
            //{
            //    Rectangle overlap = Rectangle.Intersect(intersection, playerRect);

            //    //Resolve horizontally only if the overlap's width is less than its height
            //    //if the overlap is a square, prioritize horizontal resolution
            //    if (overlap.Width > overlap.Height || overlap.Width == 0)
            //        continue;

            //    //if to the left of the obstacle, move left. otherwise, move right
            //    if (playerRect.X<intersection.X)
            //        playerRect.X -= overlap.Width;
            //    else
            //        playerRect.X += overlap.Width;
            //}

            //resolve vertically
            //foreach (Rectangle intersection in intersections)
            //{
            //    Rectangle overlap = Rectangle.Intersect(intersection, playerRect);

            //    //at this point, all horizontal collisions should be resolved, so there's no need for a width/height check
            //    if (overlap.Height == 0)
            //        continue;

            //    //if above the obstacle, move up. otherwise, move down
            //    if (playerRect.Y<intersection.Y)
            //        playerRect.Y -= overlap.Height;
            //    else
            //        playerRect.Y += overlap.Height;
            //    playerVelocity.Y = 0;
            //}
        //    playerPosition = playerRect.Location.ToVector2();
        //}

        public bool singleLeftClick(MouseState ms)
        {
            return ms.LeftButton == ButtonState.Pressed && prevMsState.LeftButton != ButtonState.Pressed;
        }
    }
}
