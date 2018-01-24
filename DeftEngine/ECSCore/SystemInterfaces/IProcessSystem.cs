using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Indicates that a System will be run once per update.
    /// </summary>
    public interface IProcessSystem
    {
        void Process();
    }
}
