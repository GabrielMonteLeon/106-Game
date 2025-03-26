using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    enum SpriteState
    {
        Idle,
        Jump,
        Walk
    }
    internal class Player : GameObject
    {
        // fields
        private double animationTimer;
        private bool alive;
        private int extraJumps;
        private int currentJumps;
        private bool facingRight;
        private SpriteState spriteState;
        private KeyboardState prevKb;

        private Vector2 playerVelocity;

        // edit these to adjust speeds
        private float playerspeedX;
        private float initialJumpVelocity;
        private float gravity;


        // properties
        public bool IsAlive { get => alive; set => alive = value; }
        public int ExtraJumps { get => extraJumps; set => extraJumps = value; }

        public Vector2 PlayerVelocity { get => playerVelocity; set => playerVelocity = value; }
        public bool IsOnGround { get; set; }

        // constructor
        public Player(Rectangle position, Texture2D texture) 
            : base(position, texture)
        {
            alive = true;
            facingRight = true;
            spriteState = SpriteState.Idle;
            extraJumps = 0;
            currentJumps = 0;
            playerVelocity = new Vector2(0, 0);

            // edit these values to adjust speed
            playerspeedX = 600;
            initialJumpVelocity = -900;
            gravity = 2000;
        }

        public void Reset(Point position)
        {
            this.Position = position;
            alive = true;
            facingRight = true;
            playerVelocity = Vector2.Zero;
        }

        public override void update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            
            //DOUBLE JUMP TESTER REMOVE IN FINAL VERSION
            if (kb.IsKeyDown(Keys.J))
            {
                extraJumps=1;
            }
            if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
            {
                playerVelocity.X = -playerspeedX;
                facingRight = false;
            }
            else if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
            {
                playerVelocity.X = playerspeedX;
                facingRight = true;
            }
            else
            {
                playerVelocity.X = 0;
            }
            if (IsOnGround)
                currentJumps = 0;
            if(kb.IsKeyDown(Keys.Space) && prevKb.IsKeyUp(Keys.Space))
            {
                if (IsOnGround)
                {
                    playerVelocity.Y = initialJumpVelocity;
                    IsOnGround = false;
                }
                else if (extraJumps>currentJumps)
                {
                    currentJumps++;
                    playerVelocity.Y = initialJumpVelocity;
                    IsOnGround = false;
                }
            }

            UpdatePosition((float)gameTime.ElapsedGameTime.TotalSeconds);
            // tracks kb state for next frame
            prevKb = kb;
        }

        public override void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {
            //determine sprite state
            if (!IsOnGround)
                spriteState = SpriteState.Jump;
            else if (playerVelocity.X != 0)
                spriteState = SpriteState.Walk;
            else
                spriteState = SpriteState.Idle;
            base.draw(spriteBatch, camPosition);
        }
        /// <summary>
        /// updates player position based on velocity
        /// </summary>
        public void UpdatePosition(float deltaTime)
        {
            Rectangle pos = Transform;
            playerVelocity.Y += gravity * deltaTime;
            pos.Location += (playerVelocity * deltaTime).ToPoint();
            this.Transform = pos;
        }
    }
}
