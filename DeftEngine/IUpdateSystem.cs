using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Indicates that an EntitySystem should be run once per update.
    /// </summary>
    public interface IUpdateSystem
    {
        void Process(ECSData ecsData);
    }
}
