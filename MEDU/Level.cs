using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    internal class Level
    {
        //fields
        private List<Platform> platforms;
        private Vector2 playerStartPos;
        private Rectangle endTrigger;

        public List<Platform> Platforms => platforms;
        public Vector2 PlayerStartPos => playerStartPos;
        public Rectangle EndTrigger => endTrigger;

        public Level(List<Platform> platforms, Vector2 playerStartPos, Rectangle endTrigger)
        {
            this.platforms = platforms;
            this.playerStartPos = playerStartPos;
            this.endTrigger = endTrigger;
        }
    }
}
