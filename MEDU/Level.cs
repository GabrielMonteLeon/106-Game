using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Creates a new level from specific data. 
        /// </summary>
        /// <param name="platforms"></param>
        /// <param name="endTrigger"></param>
        public Level(List<Platform> platforms, Vector2 playerStartPos, Rectangle endTrigger)
        {
            this.platforms = platforms;
            this.playerStartPos = playerStartPos;
            this.endTrigger = endTrigger;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset, bool debug = false)
        {

        }

        public static Level LoadLevelFromFile(string filePath)
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(filePath));
            byte width = reader.ReadByte();
            byte height = reader.ReadByte();
            byte[] data = reader.ReadBytes(width * height);
            if (data.Length < width * height)
                throw new InvalidDataException($"The level data at {filePath} is invalid.");
            reader.Close();

            List<Platform> platforms = new List<Platform>();
            Vector2 startPos;
            Rectangle endTrigger;

            for (int y = 0; y < height; y++)
            {
                //all platforms are assumed to be 1 unit tall
                int currentPlatformStart = -1;
                for(int x = 0; x < width; x++)
                {
                    int dataIndex = x * height + y;
                    switch(data[dataIndex])
                    {
                        case 0: //nothing
                            if(currentPlatformStart != -1)
                            {
                                platforms.Add(new Platform(
                                    new Rectangle(currentPlatformStart, y, x - currentPlatformStart, 1),
                                    null!, true, true
                                    ));
                                currentPlatformStart = -1;
                            }
                            break;
                    }
                }
            }
        }
    }
}
