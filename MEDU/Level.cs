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
        public Level(List<Platform> platforms, Vector2 playerStartPos, Rectangle endTrigger)
        {
            this.platforms = platforms;
            this.playerStartPos = playerStartPos;
            this.endTrigger = endTrigger;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset, bool debug = false)
        {

        }

        public string GetData()
        {
            string data = "";
            data += $"{platforms.Count} total platforms:";
            foreach(Platform platform in platforms)
            {
                data += $"\n  Platform at {platform.Position.Location} with size {platform.Position.Size}";
            }
            data += $"\nPlayer Starting Pos: {playerStartPos}";
            data += $"\nEnd Trigger: {endTrigger}";
            return data;
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
            Vector2 startPos = new Vector2(-1, -1);
            Rectangle endTrigger = new Rectangle(-1, -1, -1, -1);

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
                        case 2: //player start
                            startPos = new Vector2(x, y);
                            break;
                        case 3: //platform
                            if (currentPlatformStart == -1)
                                currentPlatformStart = x;
                            break;
                        case 4: //level end
                            endTrigger = new Rectangle(x, y, 1, 1);
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine($"Warning: Found invalid tile {data[dataIndex]} at coordinate ({x}, {y}).");
                            break;
                    }
                }
            }
            if (startPos.X < 0)
                System.Diagnostics.Debug.WriteLine("Warning: Start Pos not defined");
            if (endTrigger.X < 0)
                System.Diagnostics.Debug.WriteLine("Warning: End Trigger not defined");
            return new Level(platforms, startPos, endTrigger);
        }
    }
}
