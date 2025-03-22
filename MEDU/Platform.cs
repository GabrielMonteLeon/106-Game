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
            Rectangle screenPos = Position;
            screenPos.Offset(-camPosition);
            //assuming platform is 1 unit tall and an integer number of units wide
            if (screenPos.Width == screenPos.Height)
            {
                //Render half of left and right textures
                int textureSize = leftTexture.Width;

                Rectangle leftSourceRect = new Rectangle(0, 0, leftTexture.Width / 2, leftTexture.Height);
                Rectangle rightSourceRect = leftSourceRect;
                rightSourceRect.X += rightSourceRect.Width;

                Rectangle leftDestRect = screenPos;
                leftDestRect.Width /= 2;
                Rectangle rightDestRect = leftDestRect;
                rightDestRect.X += rightDestRect.Width;
                spriteBatch.Draw(leftTexture, leftDestRect, leftSourceRect, Color.White);
                spriteBatch.Draw(rightTexture, rightDestRect, rightSourceRect, Color.White);
            }
            else
            {
                Rectangle destination = screenPos;
                destination.Width = destination.Height;
                spriteBatch.Draw(leftTexture, destination, Color.White);
                destination.X += destination.Width;
                for (int i = 1; i < screenPos.Width / screenPos.Height - 1; i++)
                {
                    spriteBatch.Draw(Texture, destination, Color.White);
                    destination.X += destination.Width;
                }
                spriteBatch.Draw(rightTexture, destination, Color.White);
            }
        }
    }
}
