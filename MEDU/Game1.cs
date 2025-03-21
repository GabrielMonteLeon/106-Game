using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

            MouseState ms = Mouse.GetState();
            Rectangle msr = new Rectangle(ms.X,ms.Y,1,1);   

            switch (menuState)
            {
                case (MenuState.Menu):
                    if (msr.Intersects(Start))
                    {
                        menuState = MenuState.Level;
                    }
                    break;
                case (MenuState.Level):
                    if (!player.isAlive)
                    {
                        menuState = MenuState.LevelFailed;
                    }
                    break;
                case (MenuState.LevelFailed):
                    if (msr.Intersects(End))
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

        public void HandleCollision()
        {

        }
    }
}
