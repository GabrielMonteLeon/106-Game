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
        private Rectangle transform;
        private Texture2D texture;
        public Rectangle Transform
        {
            get
            {
                return transform;
            }
            set
            {
                transform = value;
            }
        }

        public Point Position
        {
            get
            {
                return transform.Location;
            }
            set
            {
                transform.Location = value;
            }
        }

        public Texture2D Texture => texture;

        public GameObject(Rectangle position, Texture2D texture)
        {
            this.transform = position;
            this.texture = texture;
        }

        public virtual void update(GameTime gameTime)
        {
            //default gameobject update is empty for now
        }

        public virtual void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {
            Rectangle screenSpacePos = Transform;
            screenSpacePos.Offset(-camPosition);
            spriteBatch.Draw(texture, screenSpacePos, Color.White);
        }
    }
}
