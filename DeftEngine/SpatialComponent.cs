using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class SpatialComponent : IComponent
    {
        public Vector2 pos;
        public Vector2 size;
        public float rotation;

        public Rectangle Bounds
        {
            get { return new Rectangle(pos.ToPoint(), size.ToPoint()); }
        }

        public Point MidPt
        {
            get { return new Point((int)pos.X + (int)size.X / 2, (int)pos.Y + (int)size.Y / 2); }
        }

        public Vector2 MidVector
        {
            get { return new Vector2(pos.X + size.X / 2, pos.Y + size.Y / 2); }
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVector2(pos);
            writer.WriteVector2(size);
            writer.Write(rotation);
        }

        public void Deserialize(BinaryReader reader)
        {
            pos = reader.ReadVector2();
            size = reader.ReadVector2();
            rotation = reader.ReadSingle();
        }

        public IComponent Copy()
        {
            return default(SpatialComponent);
        }
    }
}
