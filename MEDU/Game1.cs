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
        private Player player;
        private MenuState menuState;

        //menu fields
        private Rectangle Start;
        private Rectangle End;
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
            // TODO: Add your initialization logic here

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


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //mouse position + rectangle
            MouseState ms = Mouse.GetState();
            Rectangle msr = new Rectangle(ms.X,ms.Y,1,1);   

            //Basic finite for going through basic game
            switch (menuState)
            {
                case (MenuState.Menu):
                    if (msr.Intersects(Start) && ms.LeftButton == ButtonState.Pressed)
                    {
                        menuState = MenuState.Level;
                    }
                    break;
                case (MenuState.Level):
                    PlayerOutofBounds(player);
                    if (!player.isAlive)
                    {
                        menuState = MenuState.LevelFailed;
                    }
                    break;
                case (MenuState.LevelFailed):
                    if (msr.Intersects(End) && ms.LeftButton == ButtonState.Pressed)
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

        private void PlayerOutofBounds(Player player)
        {
            Rectangle position = player.Position;
            if((position.X + position.Width) < 0)
            {
                player.isAlive = false;
            }
            if((position.Y > GraphicsDevice.Viewport.Height))
            {
                player.isAlive = false;
            }
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
}
