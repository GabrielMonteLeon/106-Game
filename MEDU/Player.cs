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
        private float coyoteTimer;
        private int extraJumps;
        private int currentJumps;
        private int extraWallJumps;
        private int currentWallJumps;
        private bool facingRight;
        private SpriteState spriteState;
        private KeyboardState prevKb;

        private Vector2 playerVelocity;

        // edit these to adjust speeds
        private const float PLAYERSPEED = Level.TILESIZE * 15;
        private const float PLAYERACCEL = Level.TILESIZE * 300;
        private const float INITIALJUMPVEL = Level.TILESIZE * -28;
        private const float GRAVITY = Level.TILESIZE * 80;
        private const float COYOTETIME = 0.1f;


        // properties
        public bool IsAlive { get => alive; set => alive = value; }
        public int ExtraJumps { get => extraJumps; set => extraJumps = value; }
        public int ExtraWallJumps { get => extraWallJumps; set => extraWallJumps = value; }

        /// <summary>
        /// Used by the physics function to move the player and handle collision
        /// The player's position is NOT updated in the player class.
        /// </summary>
        public Vector2 PlayerVelocity { get => playerVelocity; set => playerVelocity = value; }
        public bool IsOnGround { get; set; }
        public Platform.PlatformType SpecialTerrainType { get; set; }
        public bool IsOnRightWall { get; set; }
        public bool IsOnLeftWall { get; set; }



        // constructor
        public Player(Rectangle position, Sprite texture) 
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
        }

        public void Reset(Point position)
        {
            position.Y -= Transform.Height;
            this.Position = position;
            alive = true;
            facingRight = true;
            playerVelocity = Vector2.Zero;
            extraJumps = 0;
            currentJumps = 0;
            extraWallJumps = 0;
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

            float accelValue = PLAYERACCEL * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (SpecialTerrainType == Platform.PlatformType.Ice)
                accelValue *= 0.2f;
            if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left)) //walking left
            {
                playerVelocity.X -= accelValue;
                if (playerVelocity.X < -PLAYERSPEED)
                    playerVelocity.X = -PLAYERSPEED;
                facingRight = false;
            }
            else if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right)) //walking right
            {
                playerVelocity.X += accelValue;
                if (playerVelocity.X > PLAYERSPEED)
                    playerVelocity.X = PLAYERSPEED;
                facingRight = true;
            }
            else //idling
            {
                accelValue *= 0.5f;
                if(playerVelocity.X > 0)
                {
                    playerVelocity.X -= accelValue;
                    if (playerVelocity.X < 0)
                        playerVelocity.X = 0;
                }
                else if(playerVelocity.X < 0)
                {
                    playerVelocity.X += accelValue;
                    if (playerVelocity.X > 0)
                        playerVelocity.X = 0;
                }
            }
            if (IsOnGround)
            {
                currentJumps = 0;
                currentWallJumps = 0;
                coyoteTimer = COYOTETIME;
            }
            else
            {
                coyoteTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kb.IsKeyDown(Keys.Space) && prevKb.IsKeyUp(Keys.Space))
            {
                if (coyoteTimer > 0)
                {
                    playerVelocity.Y = INITIALJUMPVEL;
                    if (SpecialTerrainType == Platform.PlatformType.Mud)
                        playerVelocity.Y *= 0.7f;
                    IsOnGround = false;
                    coyoteTimer = 0;
                }
                else if (extraJumps>currentJumps)
                {
                    currentJumps++;
                    playerVelocity.Y = INITIALJUMPVEL;
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
            playerVelocity.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            switch (facingRight)
            {
                case true:
                    base.draw(spriteBatch, camPosition);
                    break;
                case false:
                    Rectangle screenSpacePos = Transform;
                    screenSpacePos.Offset(-camPosition);
                    spriteBatch.Draw(Sprite.texture, screenSpacePos, Sprite.sourceRect, Color.White, 0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0f);
                    break;
            }
        }
    }
}
