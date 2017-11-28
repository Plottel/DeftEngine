using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public interface IEntityQuery
    {
        bool Query(Entity e);
    }
}
