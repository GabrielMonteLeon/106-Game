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
        private Player Player;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void GoToLevel(int level)
        {

        }

        public void GoToMenu()
        {

        }

        public void HandleCollision()
        {
            List<GameObject> objects = new List<GameObject>();
            //leaving the list like that for now to be changed once level has the field
            foreach(GameObject gObject in objects)
            {
                if(gObject is Platform)
                {
                    Platform platform = (Platform)gObject;
                    if (Player.Position.Intersects(platform.Position))
                    {
                        if (!platform.IsSafe)
                        {
                            //implement code for player dying
                        }


                        //move player and camera based on collision here
                        Rectangle playerRect = Player.Position;
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


                        //at this point, all horizontal collisions should be resolved, so there's no need for a width/height check
                        if (overlap.Height == 0)
                            continue;

                        //if above the obstacle, move up. otherwise, move down
                        if (playerRect.Y < platform.Position.Y)
                            playerRect.Y -= overlap.Height;
                        else
                            playerRect.Y += overlap.Height;
                        //playerVelocity.Y = 0; UPDATE THIS WHEN PLAYER CLASS IS UPDATED
                        Player.Position = playerRect;

                    }
                }
            }
        }


        //    //resolve horizontally
        //    foreach (Rectangle intersection in intersections)
        //    {
        //        Rectangle overlap = Rectangle.Intersect(intersection, playerRect);

        //        //Resolve horizontally only if the overlap's width is less than its height
        //        //if the overlap is a square, prioritize horizontal resolution
        //        if (overlap.Width > overlap.Height || overlap.Width == 0)
        //            continue;

        //        //if to the left of the obstacle, move left. otherwise, move right
        //        if (playerRect.X < intersection.X)
        //            playerRect.X -= overlap.Width;
        //        else
        //            playerRect.X += overlap.Width;
        //    }

        //    //resolve vertically
        //    foreach (Rectangle intersection in intersections)
        //    {
        //        Rectangle overlap = Rectangle.Intersect(intersection, playerRect);

        //        //at this point, all horizontal collisions should be resolved, so there's no need for a width/height check
        //        if (overlap.Height == 0)
        //            continue;

        //        //if above the obstacle, move up. otherwise, move down
        //        if (playerRect.Y < intersection.Y)
        //            playerRect.Y -= overlap.Height;
        //        else
        //            playerRect.Y += overlap.Height;
        //        playerVelocity.Y = 0;
        //    }
        //    playerPosition = playerRect.Location.ToVector2();
        //}
    }
}
