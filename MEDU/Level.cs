using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private Point playerStartPos;
        private Rectangle endTrigger;
        private int deathPlaneY;

        /// <summary>
        /// List of all platforms in the level.
        /// </summary>
        public List<Platform> Platforms => platforms;
        /// <summary>
        /// Location where player spawns.
        /// </summary>
        public Point PlayerStartPos => playerStartPos;
        /// <summary>
        /// When the player reaches this area, the level ends.
        /// </summary>
        public Rectangle EndTrigger => endTrigger;
        /// <summary>
        /// When the player goes below this point, they die.
        /// </summary>
        public int DeathPlaneY => deathPlaneY;

        public const int TILESIZE = 32;

        //Assets
        private static Texture2D[] sprites;
        private enum SpriteID { Pixel, CloudLeft, CloudMid, CloudRight, SolidLeft, SolidMid, SolidRight, DangerLeft, DangerMid, DangerRight, Flag }

        /// <summary>
        /// Creates a new level from specific data. 
        /// </summary>
        public Level(List<Platform> platforms, Point playerStartPos, Rectangle endTrigger, int deathPlaneY)
        {
            this.platforms = platforms;
            this.playerStartPos = playerStartPos;
            this.endTrigger = endTrigger;
            this.deathPlaneY = deathPlaneY;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset, bool debug = false)
        {
            foreach (Platform platform in platforms)
                platform.draw(spriteBatch, cameraOffset);

            Rectangle screenSpaceEnd = endTrigger;
            screenSpaceEnd.Offset(-cameraOffset);
            spriteBatch.Draw(sprites[(int)SpriteID.Flag], screenSpaceEnd, Color.Green);

            if (!debug)
                return;
            spriteBatch.Draw(sprites[0], new Rectangle(PlayerStartPos - cameraOffset.ToPoint(), new Point(TILESIZE)), Color.Blue);
        }

        public string GetData()
        {
            string data = "";
            data += $"{platforms.Count} total platforms:";
            foreach (Platform platform in platforms)
            {
                data += $"\n  Platform at {platform.Transform.Location} with size {platform.Transform.Size}";
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
            Point startPos = new Point(-1, -1);
            Rectangle endTrigger = new Rectangle(-1, -1, -1, -1);

            for (int y = 0; y < height; y++)
            {
                //all platforms are assumed to be 1 unit tall
                int currentPlatformStart = -1;
                int currentPlatformType = -1;
                for (int x = 0; x < width; x++)
                {
                    int dataIndex = x * height + y;
                    switch (data[dataIndex])
                    {
                        case 0: //nothing
                            if (currentPlatformStart != -1)
                            {
                                CreatePlatform(currentPlatformStart, x, y, currentPlatformType);
                                currentPlatformStart = -1;
                                currentPlatformType = -1;
                            }
                            break;
                        case 2: //player start
                            startPos = new Point(x * TILESIZE, y * TILESIZE);
                            break;
                        case 3: //passthrough platform
                            if (currentPlatformStart == -1) //if a platform is not yet being constructed
                            {
                                currentPlatformStart = x;
                                currentPlatformType = 0;
                            }
                            else if(currentPlatformType != 0) //if the platform being constructed is a different type
                            {
                                CreatePlatform(currentPlatformStart, x, y, currentPlatformType);
                                currentPlatformStart = x;
                                currentPlatformType = 0;
                            }
                            break;
                        case 5: //solid platform
                            if (currentPlatformStart == -1) //if a platform is not yet being constructed
                            {
                                currentPlatformStart = x;
                                currentPlatformType = 1;
                            }
                            else if (currentPlatformType != 1) //if the platform being constructed is a different type
                            {
                                CreatePlatform(currentPlatformStart, x, y, currentPlatformType);
                                currentPlatformStart = x;
                                currentPlatformType = 1;
                            }
                            break;
                        case 1: //dangerous platform
                            if (currentPlatformStart == -1) //if a platform is not yet being constructed
                            {
                                currentPlatformStart = x;
                                currentPlatformType = 2;
                            }
                            else if (currentPlatformType != 2) //if the platform being constructed is a different type
                            {
                                CreatePlatform(currentPlatformStart, x, y, currentPlatformType);
                                currentPlatformStart = x;
                                currentPlatformType = 2;
                            }
                            break;
                        case 4: //level end
                            endTrigger = new Rectangle(x * TILESIZE, y * TILESIZE, TILESIZE, TILESIZE);
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
            return new Level(platforms, startPos, endTrigger, height * TILESIZE);

            //I might refactor this in the future since it feels a bit janky, but it works for now
            void CreatePlatform(int startX, int endX, int y, int platformType)
            {
                int baseSpriteIndex = (int)SpriteID.CloudLeft;
                int typeOffset = platformType * 3;
                bool passThrough = platformType switch
                {
                    0 => true, //normal passthrough
                    1 => false, //normal solid
                    2 => false, //dangerous
                    _ => true
                };
                bool isSafe = platformType switch
                {
                    0 => true, //normal passthrough
                    1 => true, //normal solid
                    2 => false, //dangerous
                    _ => true
                };
                platforms.Add(new Platform(
                    new Rectangle(startX * TILESIZE, y * TILESIZE, (endX - startX) * TILESIZE, TILESIZE),
                    sprites[baseSpriteIndex + typeOffset + 1], sprites[baseSpriteIndex + typeOffset], sprites[baseSpriteIndex + typeOffset + 2], passThrough, isSafe));
            }
        }

        /// <summary>
        /// Loads the sprite assets needed for level creation and stores them.
        /// </summary>
        public static void LoadAssets(ContentManager content)
        {
            sprites = new Texture2D[11];
            sprites[(int)SpriteID.Pixel]       = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.CloudLeft]   = content.Load<Texture2D>("CloudLeft");
            sprites[(int)SpriteID.CloudMid]    = content.Load<Texture2D>("CloudMid");
            sprites[(int)SpriteID.CloudRight]  = content.Load<Texture2D>("CloudRight");
            sprites[(int)SpriteID.SolidLeft]   = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.SolidMid]    = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.SolidRight]  = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.DangerLeft]  = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.DangerMid]   = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.DangerRight] = content.Load<Texture2D>("pixel");
            sprites[(int)SpriteID.Flag]        = content.Load<Texture2D>("pixel");
        }
    }
}
