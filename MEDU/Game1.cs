using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MEDU
{
    enum MenuState
    {
        Menu,
        LevelSelect,
        PreLevel,
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
        private KeyboardState prevkb;
        private int coinCount;
        private Texture2D coinTexture;
        private Texture2D clockTexture;


        //menu fields
        private Rectangle Start;
        private Rectangle End;
        private Vector2 cameraPosition;
        private Texture2D start_texture;
        private Texture2D end_texture;
        private MouseState pms;
        private Texture2D title;
        private Rectangle titleRect;
        private Texture2D background;
        private Rectangle backgroundRect;

        //level select fields
        private Rectangle[] levelSelection;
        private Texture2D[] levelSelectTextures;
        private Rectangle select;
        private int selectedLevel;

        // font
        private SpriteFont font;
        private SpriteFont descriptionFont;
        private SpriteFont byteBounce;

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
            End = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height/2 + 50,150, 150);
            titleRect = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 150, GraphicsDevice.Viewport.Height / 2 - 200, 300, 300);
            cameraCenterOffset = new Point(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            menuState = MenuState.Menu;
            player = new Player(new Rectangle(10,10,Level.TILESIZE,Level.TILESIZE*2), Content.Load<Texture2D>("CharacterRight"));
            select = new Rectangle(
                _graphics.PreferredBackBufferWidth / 2 - 35,
                _graphics.PreferredBackBufferHeight - 75,
                70,
                70);
            selectedLevel = -1;
            timer = 0;
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            start_texture = Content.Load<Texture2D>("Start");
            end_texture = Content.Load<Texture2D>("End");
            Level.LoadAssets(Content);
            levels = new Level[] { 
                Level.LoadLevelFromFile("Content/level1.level"), 
                Level.LoadLevelFromFile("Content/level2.level"),
                Level.LoadLevelFromFile("Content/level3.level")};
            levelSelection = new Rectangle[levels.Length];
            levelSelectTextures = new Texture2D[levels.Length];
            for (int i = 0; i < levelSelection.Length; i++)
            {
                levels[i].Completed = false;
                levelSelection[i] = new Rectangle(
                    i * _graphics.PreferredBackBufferWidth / levelSelection.Length + 25,
                    150,
                    150,
                    150);
                // TODO: replace texture with something that depicts the level
                levelSelectTextures[i] = Content.Load<Texture2D>($"LS{i+1}");
            }

            coinTexture = Content.Load<Texture2D>("coin");
            clockTexture = Content.Load<Texture2D>("Clock");


            title = Content.Load<Texture2D>("Title");
            background = Content.Load<Texture2D>("background");

            byteBounce = Content.Load<SpriteFont>("ByteBounce");
            descriptionFont = byteBounce;

            font = Content.Load<SpriteFont>("spritefont");
            descriptionFont = Content.Load<SpriteFont>("ByteBounce");


            //System.Diagnostics.Debug.WriteLine(Level.LoadLevelFromFile("Content/test level.level").GetData());
        }

        protected override void Update(GameTime gameTime)
        {
            //LevelTest.Update(gameTime);
            //return;

            //mouse position
            MouseState ms = Mouse.GetState();
            KeyboardState kb = Keyboard.GetState();

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
                    for (int i = 0; i < levelSelection.Length; i++)
                    {
                        if (levelSelection[i].Contains(ms.Position) && singleLeftClick(ms))
                        {
                            selectedLevel = i;
                        }
                    }
                    if (select.Contains(ms.Position) && singleLeftClick(ms) && selectedLevel != -1)
                    {
                        menuState = MenuState.PreLevel;
                    }
                    break;

                case (MenuState.PreLevel):
                    if (singleLeftClick(ms))
                    {
                        GoToLevel(selectedLevel);
                    }
                    break;

                case (MenuState.Level):
                    timer += gameTime.ElapsedGameTime.TotalSeconds;
                    player.update(gameTime);
                    HandlePhysics(gameTime);
                    CheckIfPlayerOutofBounds(player);
                    cameraPosition = (player.Transform.Center - cameraCenterOffset).ToVector2();
                    if (currentLevel.Goal == Level.EndGoal.speed && timer >= currentLevel.LevelTimer)
                    {
                        player.IsAlive = false;
                    }
                    if (!player.IsAlive)
                        menuState = MenuState.LevelFailed;
                    else if (player.Transform.Intersects(currentLevel.EndTrigger))
                    {
                        currentLevel.Completed = true;
                        menuState = MenuState.LevelComplete;
                    }
                    else if (kb.IsKeyDown(Keys.Escape) && prevkb.IsKeyUp(Keys.Escape))
                        menuState = MenuState.Pause;
                    break;

                case (MenuState.Pause):
                    if (kb.IsKeyDown(Keys.R) && prevkb.IsKeyUp(Keys.R))
                        menuState = MenuState.Level;
                    if (kb.IsKeyDown(Keys.Escape) && prevkb.IsKeyUp(Keys.Escape))
                        menuState = MenuState.Menu;
                        break;

                case (MenuState.LevelComplete):
                    if (singleLeftClick(ms))
                    {
                        menuState = MenuState.LevelSelect;
                        selectedLevel = -1;
                    }
                    break;

                case (MenuState.LevelFailed):
                    if (End.Contains(ms.Position) && singleLeftClick(ms))
                    {
                        GoToLevel(levelNum);
                    }
                    break;
            }
            prevMsState = ms;
            prevkb = kb;
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
                    _spriteBatch.Draw(background, backgroundRect, Color.White);
                    _spriteBatch.Draw(title, titleRect, Color.White);
                    _spriteBatch.Draw(start_texture,Start, Color.White);
                    break;

                case (MenuState.LevelSelect):
                    _spriteBatch.DrawString(font, "LEVEL SELECTION", new Vector2(180, 20), Color.White);
                    for (int i = 0; i < levelSelection.Length; i++)
                    {
                        Color color = Color.White;
                        if (levels[i].Completed)
                            color = Color.Gray;
                        if (levelSelection[i].Contains(Mouse.GetState().Position))
                            color = Color.LightYellow;
                        if (selectedLevel == i)
                            color = Color.LightBlue;
                        _spriteBatch.Draw(levelSelectTextures[i], levelSelection[i], color);
                    }
                    _spriteBatch.Draw(start_texture, select, Color.White);
                    break;

                case (MenuState.PreLevel):
                    _spriteBatch.DrawString(font, "INSERT JOKE HERE", new Vector2(200, _graphics.PreferredBackBufferHeight / 2 - 50), Color.White);
                    break;

                case (MenuState.Level):
                    _spriteBatch.Draw(background, backgroundRect, Color.White);
                    currentLevel.Draw(_spriteBatch, cameraPosition);
                    // timer
                    _spriteBatch.Draw(clockTexture, new Rectangle(10, 25, 20, 20), Color.White);
                    String time = String.Format("{0:0.00}", timer);
                    if (currentLevel.Goal == Level.EndGoal.speed)
                    {
                        time = String.Format("{0:0.00}", currentLevel.LevelTimer-timer);
                    }
                    player.draw(_spriteBatch, cameraPosition);
                    // coin count
                    _spriteBatch.Draw(coinTexture, new Rectangle(10, 8, 20, 20), Color.White);
                    _spriteBatch.DrawString(byteBounce,
                        $"{coinCount}/{currentLevel.Coins.Count}",
                        new Vector2(32, 8),
                        Color.Yellow);
                    _spriteBatch.DrawString(byteBounce,
                        time,
                        new Vector2(32, 25),
                        Color.Yellow);
                    _spriteBatch.DrawString(byteBounce,
                        "press escape to pause game", 
                        new Vector2(10, _graphics.PreferredBackBufferHeight - 20), 
                        Color.White);
                    break;

                case (MenuState.Pause):
                    _spriteBatch.DrawString(font, "GAME PAUSED", new Vector2(_graphics.PreferredBackBufferWidth/2 - 170, _graphics.PreferredBackBufferHeight / 2 - 50), Color.White);
                    _spriteBatch.DrawString(descriptionFont,
                        "press 'r' to resume.",
                        new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2 + 20),
                        Color.White);
                    _spriteBatch.DrawString(descriptionFont,
                        "press escape to go to menu.",
                        new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2 + 40),
                        Color.White);
                    break;

                case (MenuState.LevelComplete):
                    _spriteBatch.DrawString(font, "LEVEL COMPLETE", new Vector2(_graphics.PreferredBackBufferWidth/2 - 200, _graphics.PreferredBackBufferHeight / 2 - 50), Color.White);
                    _spriteBatch.DrawString(byteBounce,
                        "click to continue",
                        new Vector2(_graphics.PreferredBackBufferWidth/2 - 90, _graphics.PreferredBackBufferHeight/2 + 20),
                        Color.White);
                    selectedLevel = -1;
                    break;

                case (MenuState.LevelFailed):

                    _spriteBatch.Draw(background, backgroundRect, Color.Red);
                    _spriteBatch.Draw(end_texture, End, Color.DarkGray);
                    ResetCoins();
                    break;
            }

            _spriteBatch.End();



            base.Draw(gameTime);
        }
        

        public void GoToLevel(int level)
        {
            currentLevel = levels[level];
            ResetCoins();
            levelNum = level;
            player.Reset(currentLevel.PlayerStartPos);
            timer = 0;
            menuState = MenuState.Level;
            //add modifiers
            switch(level)
            {
                case 0:
                case 2:
                    player.ExtraJumps = 0;
                    break;
                case 1:
                    player.ExtraJumps = 1;
                    break;
            }
        }

        public void ResetCoins()
        {
            coinCount = 0;
            foreach(Coin coin in currentLevel.Coins)
            {
                coin.reset();
            }
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
            List<Coin> coins = currentLevel.Coins;
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
            foreach (Coin coin in coins)
            {
                if (!coin.Collected&&coin.Transform.Intersects(player.Transform))
                {
                    if (currentLevel.Goal == Level.EndGoal.noCoin)
                    {
                        player.IsAlive = false;
                        return;
                    }
                    coin.collect();
                    coinCount++;
                    //add other implementation here
                }
            }
        }

        public bool singleLeftClick(MouseState ms)
        {
            return ms.LeftButton == ButtonState.Pressed && prevMsState.LeftButton != ButtonState.Pressed;
        }
    }
}
