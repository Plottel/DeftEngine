using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DeftEngine
{
    public class NeuralNetComponent : IComponent
    {
        public NeuralNet net = new NeuralNet();

        public void Serialize(BinaryWriter writer) { }
        public void Deserialize(BinaryReader reader) { }
        public IComponent Copy() { return default(IComponent); }
    }
}
