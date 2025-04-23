using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal class Coin : GameObject
    {
        private bool collected;
        public bool Collected => collected;
        public Coin(Rectangle position, Sprite texture) : base(position, texture)
        {
            collected = false;        
        }
        public void collect() 
        {
            collected = true; 
            //nothing else here for now but could add more later
        }
        public void reset()
        {
            collected = false;
            //nothing else here for now but could add more later
        }
        public override void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {
            if (!collected)
            {
                Rectangle screenSpacePos = Transform;
                screenSpacePos.Offset(-camPosition);
                spriteBatch.Draw(Sprite.texture, screenSpacePos, Sprite.sourceRect, Color.White);
            }
        }
    }
}
