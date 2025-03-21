using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDU
{
    /// <summary>
    /// Class for testing level data
    /// To use this, simply call LevelTest.Update and LevelTest.Draw from Game1.Update and Game1.Draw
    /// </summary>
    internal static class LevelTest
    {
        public static Level loadedLevel;
        public static Vector2 cameraPos;

        private static KeyboardState prevKBState;
        private static float cameraSpeed = 400;

        public static void LoadLevel(string filePath)
        {
            loadedLevel = Level.LoadLevelFromFile(filePath);
            cameraPos = loadedLevel.PlayerStartPos;
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.D1) && prevKBState.IsKeyUp(Keys.D1))
                LoadLevel("Content/test level.level");

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kb.IsKeyDown(Keys.Up) && prevKBState.IsKeyUp(Keys.Up))
                cameraSpeed *= 1.25f;
            if (kb.IsKeyDown(Keys.Down) && prevKBState.IsKeyUp(Keys.Down))
                cameraSpeed *= 0.8f;

            if (kb.IsKeyDown(Keys.W))
                cameraPos.Y -= cameraSpeed * deltaTime;
            if (kb.IsKeyDown(Keys.S))
                cameraPos.Y += cameraSpeed * deltaTime;
            if (kb.IsKeyDown(Keys.A))
                cameraPos.X -= cameraSpeed * deltaTime;
            if (kb.IsKeyDown(Keys.D))
                cameraPos.X += cameraSpeed * deltaTime;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            loadedLevel.Draw(spriteBatch, cameraPos, true);
        }
    }
}
