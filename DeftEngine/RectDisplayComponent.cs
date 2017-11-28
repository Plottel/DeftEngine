using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class RectDisplayComponent : IComponent
    {
        public Color color;

        public void Serialize(BinaryWriter writer)
        {
            writer.WriteColor(color);
        }

        public void Deserialize(BinaryReader reader)
        {
            color = reader.ReadColor();
        }

        public IComponent Copy()
        {
            return default(RectDisplayComponent);
        }
    }
}
