using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal class Platform
    {
        private bool passThrough;
        private bool isSafe;

        public bool PassThrough => passThrough;
        public bool IsSafe => isSafe;

        public Platform(Rectangle position, Texture2D texture, bool passThrough, bool isSafe) : base(position, texture)
        {
            this.passThrough = passThrough;
            this.isSafe = isSafe;
        }
    }
}
