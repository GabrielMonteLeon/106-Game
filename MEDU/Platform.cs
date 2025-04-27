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
        private Sprite leftTexture;
        private Sprite rightTexture;

        public bool PassThrough => passThrough;
        public bool IsSafe => isSafe;

        public Platform(Rectangle position, Sprite midTexture, Sprite leftTexture, Sprite rightTexture, bool passThrough, bool isSafe) : base(position, midTexture)
        {
            this.passThrough = passThrough;
            this.isSafe = isSafe;
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
        }

        public override void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {
            Rectangle screenPos = Transform;
            screenPos.Offset(-camPosition);
            //assuming platform is 1 unit tall and an integer number of units wide
            if (screenPos.Width == screenPos.Height)
            {
                //Render half of left and right textures
                int textureSize = leftTexture.Width;

                Rectangle leftSourceRect = leftTexture.Bounds;
                leftSourceRect.Width /= 2;

                Rectangle rightSourceRect = rightTexture.Bounds;
                rightSourceRect.Width /= 2;
                rightSourceRect.X += rightSourceRect.Width;

                Rectangle leftDestRect = screenPos;
                leftDestRect.Width /= 2;
                Rectangle rightDestRect = leftDestRect;
                rightDestRect.X += rightDestRect.Width;
                spriteBatch.Draw(leftTexture.texture, leftDestRect, leftSourceRect, Color.White);
                spriteBatch.Draw(rightTexture.texture, rightDestRect, rightSourceRect, Color.White);
            }
            else
            {
                Rectangle destination = screenPos;
                destination.Width = destination.Height;

                spriteBatch.Draw(leftTexture.texture, destination, leftTexture.sourceRect, Color.White);
                destination.X += destination.Width;
                for (int i = 1; i < screenPos.Width / screenPos.Height - 1; i++)
                {
                    spriteBatch.Draw(Sprite.texture, destination, Sprite.sourceRect, Color.White);
                    destination.X += destination.Width;
                }
                spriteBatch.Draw(rightTexture.texture, destination, rightTexture.sourceRect, Color.White);
            }
        }
    }
}
