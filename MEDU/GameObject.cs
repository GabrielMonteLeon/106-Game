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
        private Sprite sprite;
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

        public Texture2D Texture => sprite.texture;
        public Sprite Sprite => sprite;

        public GameObject(Rectangle position, Sprite sprite)
        {
            this.transform = position;
            this.sprite = sprite;
        }

        public virtual void update(GameTime gameTime)
        {
            //default gameobject update is empty for now
        }

        public virtual void draw(SpriteBatch spriteBatch, Vector2 camPosition)
        {
            Rectangle screenSpacePos = Transform;
            screenSpacePos.Offset(-camPosition);
            spriteBatch.Draw(Sprite.texture, screenSpacePos, Sprite.sourceRect, Color.White);
        }
    }
}
