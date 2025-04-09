using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal struct Sprite
    {
        public Texture2D texture;
        public Rectangle? sourceRect;

        public int Width => sourceRect == null ? texture.Width : sourceRect.Value.Width;
        public int Height => sourceRect == null ? texture.Height : sourceRect.Value.Height;
        public Rectangle Bounds => sourceRect == null ? texture.Bounds : sourceRect.Value;

        public Sprite(Texture2D texture, Rectangle? sourceRect = null)
        {
            this.texture = texture;
            this.sourceRect = sourceRect;
        }

        public static implicit operator Sprite(Texture2D texture)
        {
            return new Sprite(texture);
        }
    }
}
