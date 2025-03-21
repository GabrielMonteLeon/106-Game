using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal class Player : GameObject
    {

        public bool isAlive;
        
        private Vector2 velocity;
        private Double animationTimer;
        private bool facingRight;
        private enum SpriteState
        {
            idle,
            jump,
            walk
        }
        public Player(Rectangle position, Texture2D texture) : base(position, texture)
        {
            velocity = new Vector2(0, 0);
            facingRight = true;
        }
        public override void update()
        {

        }
    }
}
