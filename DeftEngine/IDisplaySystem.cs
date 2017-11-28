using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Indicates an EntitySystem reponsible for rendering.
    /// Ensures this system is called with render code, not gameplay logic.
    /// </summary>
    public interface IDisplaySystem
    {
        void Process(ECSData ecsData);
    }
}
