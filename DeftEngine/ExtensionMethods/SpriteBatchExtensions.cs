using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public static class SpriteBatchExtensions
    {
        /// <summary>
        /// Draws an entire Texture. Rescales the texture according to passed in size Vector.
        /// </summary>
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, float rotation, Vector2 size)
        {
            var scale = new Vector2(size.X / texture.Width, size.Y / texture.Height);


            var midVec = new Vector2((texture.Width * scale.X) / 2, (texture.Height * scale.Y) / 2);
            var unscaledMidVec = new Vector2(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(
                texture,                                            // Texture
                pos + midVec,                                       // Top left point to draw
                null,                                               // Subsection of texture to render
                Color.White,                                        // Color mask   
                MathHelper.ToRadians(rotation),                     // Rotation in radians
                unscaledMidVec,                                     // Rotation anchor point (center by default)
                scale,                                              // Scale
                SpriteEffects.None,                                 // NOTE: Not used, bound by MonoGame/XNA function calls
                0                                                   // Layer depth (default 0)
            );
        }

        public static void DrawBox(this SpriteBatch spriteBatch, Box box, Color color)
        {
            Vector2[] corners = box.Corners;

            spriteBatch.DrawLine(corners[0], corners[1], color, 1);
            spriteBatch.DrawLine(corners[1], corners[2], color, 1);
            spriteBatch.DrawLine(corners[2], corners[3], color, 1);
            spriteBatch.DrawLine(corners[3], corners[0], color, 1);

            spriteBatch.DrawPoint(box.Center, color, 5);
        }

        public static void DrawString(this SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
        {
            spriteBatch.DrawString(Assets.GetFont("GadgetFont12"), text, pos, color);
        }
    }
}
