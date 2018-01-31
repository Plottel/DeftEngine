using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public static class IOExtensions
    {
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
