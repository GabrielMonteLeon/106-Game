﻿using Microsoft.VisualBasic;
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
        private int level;
        private Level currentLevel;
        private Player player;
        private MenuState menuState;

        //menu fields
        private Rectangle Start;
        private Rectangle End;
        private Vector2 cameraPosition;
        private Texture2D start_texture;
        private Texture2D end_texture;
        private MouseState pms;


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
            menuState = MenuState.Menu;
            player = new Player(new Rectangle(10,10,100,100), Content.Load<Texture2D>("CLEFT"));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            start_texture = Content.Load<Texture2D>("Start");
            end_texture = Content.Load<Texture2D>("End");
            Level.LoadAssets(Content);
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
                    if (Start.Contains(ms.Position) && ms.LeftButton == ButtonState.Pressed)
                    {
                        menuState = MenuState.Level;
                    }
                    break;
                case (MenuState.Level):
                    CheckIfPlayerOutofBounds(player);
                    cameraPosition = new Vector2(player.Position.X, player.Position.Y);
                    if (!player.isAlive)
                    {
                        menuState = MenuState.LevelFailed;
                    }
                    player.update();
                    break;
                case (MenuState.LevelFailed):
                    if (End.Contains(ms.Position) && ms.LeftButton == ButtonState.Pressed)
                    {
                        menuState = MenuState.Menu;
                    }
                    break;
            }
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
                case (MenuState.Level):
                    player.draw(_spriteBatch, new Vector2(0,0));
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

        }

        public void GoToMenu()
        {

        }
        
        /// <summary>
        /// Makes character dead if they run out of bounds
        /// </summary>
        private void CheckIfPlayerOutofBounds(Player player)
        {
            Rectangle position = player.Position;
            if(position.Y > currentLevel.DeathPlaneY)
            {
                player.isAlive = false;
            }
        }

        public void HandleCollision()
        {
            List<Platform> objects = currentLevel.Platforms;
            foreach (Platform platform in objects)
            {
                if (player.Position.Intersects(platform.Position))
                {
                    if (!platform.IsSafe)
                    {
                        //implement code for player dying
                        player.isAlive = false;
                    }


                    //move player and camera based on collision here
                    Rectangle playerRect = player.Position;
                    Rectangle overlap = Rectangle.Intersect(platform.Position, playerRect);

                    //Resolve horizontally only if the overlap's width is less than its height
                    //if the overlap is a square, prioritize horizontal resolution
                    if (overlap.Width > overlap.Height || overlap.Width == 0)
                        continue;

                    //if to the left of the obstacle, move left. otherwise, move right
                    if (playerRect.X < platform.Position.X)
                        playerRect.X -= overlap.Width;
                    else
                        playerRect.X += overlap.Width;

                    player.Position = playerRect;
                    //at this point, all horizontal collisions should be resolved, so there's no need for a width/height check
                    if (player.Position.Intersects(platform.Position))
                    {
                        if (overlap.Height == 0)
                            continue;

                        //if above the obstacle, move up. otherwise, move down
                        if (playerRect.Y < platform.Position.Y)
                        {
                            if (platform.PassThrough)
                                playerRect.Y += overlap.Height;
                            else
                                playerRect.Y -= overlap.Height;
                        }
                        else
                            playerRect.Y += overlap.Height;


                        player.JumpVelocity = 0;
                        player.Position = playerRect;
                    }


                }
            }
        }
    }
}
