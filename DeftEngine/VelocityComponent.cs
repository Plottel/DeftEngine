using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class VelocityComponent : IComponent
    {
        public Vector2 velocity;
        public float speed;

        public void Serialize(BinaryWriter writer) { }
        public void Deserialize(BinaryReader reader) { }
        public IComponent Copy() { return default(IComponent); }
    }
}
