using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DeftEngine
{
    public interface IComponent
    {
        void Serialize(BinaryWriter writer);
        void Deserialize(BinaryReader reader);
        IComponent Copy();
    }
}
