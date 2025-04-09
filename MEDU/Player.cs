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
        private int extraWallJumps;
        private int currentWallJumps;
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
        public int ExtraWallJumps { get => extraWallJumps; set => extraWallJumps = value; }

        public Vector2 PlayerVelocity { get => playerVelocity; set => playerVelocity = value; }
        public bool IsOnGround { get; set; }
        public bool IsOnRightWall { get; set; }
        public bool IsOnLeftWall { get; set; }



        // constructor
        public Player(Rectangle position, Texture2D texture) 
            : base(position, texture)
        {
            alive = true;
            facingRight = true;
            spriteState = SpriteState.Idle;
            extraJumps = 0;
            currentJumps = 0;
            extraWallJumps = 0;
            currentWallJumps = 0;
            playerVelocity = new Vector2(0, 0);

            // edit these values to adjust speed
            playerspeedX = 600;
            initialJumpVelocity = -1200;
            gravity = 2500;
        }

        public void Reset(Point position)
        {
            position.Y -= Transform.Height;
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
            //Wall Jump Tester
            if (kb.IsKeyDown(Keys.L))
            {
                extraWallJumps = 1;
            }
            //relatively infinite jumps 
            if (kb.IsKeyDown(Keys.O))
            {
                extraJumps = 10000000;
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
                float adjust;
                switch (playerspeedX)
                {
                    case > 0:
                        adjust = playerVelocity.X - 100;
                        playerVelocity.X = adjust;
                        if (playerVelocity.X < 0)
                        {
                            playerVelocity.X = 0;
                        }
                        break;
                    case < 0:
                        adjust = playerVelocity.X+100;
                        playerVelocity.X = adjust;
                        if (playerVelocity.X > 0)
                        {
                            playerVelocity.X = 0;
                        }
                        break;
                }
            }
            if (IsOnGround)
            {
                currentJumps = 0;
                currentWallJumps = 0;
            }
            if (kb.IsKeyDown(Keys.Space) && prevKb.IsKeyUp(Keys.Space))
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
                else if (extraWallJumps > currentWallJumps)
                {
                    float wallBounceVelocity = 800;
                    if (IsOnRightWall)
                    {
                        playerVelocity.Y = -wallBounceVelocity;
                        playerVelocity.X = 5*-wallBounceVelocity;
                        currentWallJumps--;

                    }
                    else if (IsOnLeftWall)
                    {
                        playerVelocity.Y = -wallBounceVelocity;
                        playerVelocity.X = 5*wallBounceVelocity;
                        currentWallJumps--;
                    }
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
