using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal abstract class GameObject
    {
        //fields
        private Rectangle position;
        private Texture2D texture;

        public GameObject(Rectangle position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }
        public void update()
        {
            //default gameobject update is empty for now
        }
        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {
            spriteBatch.Draw(texture, new Vector2(position.X-camPosition.X, position.Y-camPosition.Y), position, Color.White);
        }
    }
}
