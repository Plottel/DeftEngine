using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public static class ExtensionMethods
    {
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, float rotation, Vector2 scale)
        {
            var midVec = new Vector2((texture.Width * scale.X) / 2, (texture.Height * scale.Y) / 2);
            var unscaledMidVec = new Vector2(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(
                texture,                                            // Texture
                pos + midVec,                                       // Top left point to draw
                null,                                               // Subsection of texture to render
                Color.White,                                        // Color mask   
                MathHelper.ToRadians(rotation),                     // Rotation in degrees
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

            spriteBatch.DrawPoint(box.center, color, 5);
        }

        public static Rectangle GetInflated(this Rectangle rectangle, int horizontalAmount, int verticalAmount)
        {
            Rectangle inflated = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            inflated.Inflate(horizontalAmount, verticalAmount);

            return inflated;
        }

        public static Rectangle GetInflated(this Rectangle rectangle, float horizontalAmount, float verticalAmount)
        {
            Rectangle inflated = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            inflated.Inflate(horizontalAmount, verticalAmount);

            return inflated;
        }

        public static Vector2 TopLeft(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Left, rectangle.Top);
        }

        public static Vector2 BottomRight(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Right, rectangle.Bottom);
        }

        public static Vector2 Add(this Vector2 vec, int other)
        {
            vec.X += other;
            vec.Y += other;
            return vec;
        }

        public static Vector2 AddX(this Vector2 vec, int other)
        {
            vec.X += other;
            return vec;
        }

        public static Vector2 AddY(this Vector2 vec, int other)
        {
            vec.Y += other;
            return vec;
        }

        public static int Col(this Point pt)
        {
            return pt.X;
        }

        public static int Row(this Point pt)
        {
            return pt.Y;
        }

        public static void WriteVector2(this BinaryWriter writer, Vector2 v)
        {
            writer.Write(v.X);
            writer.Write(v.Y);
        }

        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }

        public static void WriteRectangle(this BinaryWriter writer, Rectangle r)
        {
            writer.Write(r.X);
            writer.Write(r.Y);
            writer.Write(r.Width);
            writer.Write(r.Height);
        }

        public static Rectangle ReadRectangle(this BinaryReader reader)
        {
            return new Rectangle
            (
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32()
            );
        }

        public static void WriteColor(this BinaryWriter writer, Color c)
            => writer.Write(c.PackedValue);

        public static Color ReadColor(this BinaryReader reader)
            => new Color(reader.ReadUInt32());
    }
}
