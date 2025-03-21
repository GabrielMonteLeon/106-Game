using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal class Platform : GameObject
    {
        private bool passThrough;
        private bool isSafe;
        private Texture2D leftTexture;
        private Texture2D rightTexture;

        public bool PassThrough => passThrough;
        public bool IsSafe => isSafe;

        public Platform(Rectangle position, Texture2D midTexture, Texture2D leftTexture, Texture2D rightTexture, bool passThrough, bool isSafe) : base(position, midTexture)
        {
            this.passThrough = passThrough;
            this.isSafe = isSafe;
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
        }

        public override void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {

        }
    }
}
