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
        Vector2 velocity;
        public Player(Rectangle position, Texture2D texture) : base(position, texture)
        {
            //empty for now so it can be fully implemented later
        }
    }
}
