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
        idle,
        jump,
        walk
    }
    internal class Player : GameObject
    {
        // fields
        private Double animationTimer;
        private bool isAlive;

        private bool facingRight;
        private SpriteState spriteState;
        private KeyboardState prevKb;

        private Vector2 playerVelocity;
        private float gravity;
        private float jumpVelocity;

        // edit these to adjust speeds
        private float initialJumpVelocity;
        private float playerspeedX;


        // properties
        public bool Alive { get => isAlive; set => isAlive = value; }
        public Vector2 PlayerVelocity { get => playerVelocity; }
        
        // constructor
        public Player(Rectangle position, Texture2D texture) 
            : base(position, texture)
        {
            facingRight = true;
            spriteState = SpriteState.idle;

            playerVelocity = new Vector2(0, 0);
            gravity = 9.81f;
            jumpVelocity = 0;

            // edit these values to adjust speed
            initialJumpVelocity = 10;
            playerspeedX = 2;
        }
        public override void update()
        {
            KeyboardState kb = Keyboard.GetState();

            switch (spriteState)
            {
                case SpriteState.idle:
                    // walk left
                    if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
                    {
                        spriteState = SpriteState.walk;
                        facingRight = false;
                    }
                    // walk right
                    if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
                    {
                        spriteState = SpriteState.walk;
                        facingRight = true;
                    }
                    // jump (single press)
                    if (kb.IsKeyDown(Keys.Space) && prevKb.IsKeyUp(Keys.Space))
                    {
                        spriteState = SpriteState.jump;
                        jumpVelocity = initialJumpVelocity;
                    }
                    break;

                case SpriteState.jump:
                    jumpVelocity -= gravity;
                    playerVelocity.Y = jumpVelocity;

                    // when player lands on the ground
                    if (jumpVelocity <= -initialJumpVelocity)
                    {
                        spriteState = SpriteState.idle;
                    }
                    break;

                case SpriteState.walk:
                    playerVelocity.X = playerspeedX;
                    
                    // idle
                    if (kb.IsKeyUp(Keys.A) && kb.IsKeyUp(Keys.Left) && kb.IsKeyUp(Keys.D) && kb.IsKeyUp(Keys.Right))
                    {
                        spriteState = SpriteState.idle;
                    }
                    // jump (single press only)
                    if (kb.IsKeyDown(Keys.Space) && prevKb.IsKeyUp(Keys.Space))
                    {
                        spriteState = SpriteState.jump;
                        jumpVelocity = initialJumpVelocity;
                    }
                    break;
            }

            // tracks kb state for next frame
            prevKb = kb;
        }
    }
}
