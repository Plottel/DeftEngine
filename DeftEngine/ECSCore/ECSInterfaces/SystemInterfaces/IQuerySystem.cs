using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Indicates a system has a custom query.
    /// Default queries are:
    /// - All entities with a specific component.
    /// Custom queries include those asking for more than one component
    /// </summary>
    public interface IQuerySystem
    {
        bool Query(Entity e);
    }
}
