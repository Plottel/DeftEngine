using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public interface IActionSystem
    {
        // To fetch actions, query the Action Pool.
        void ProcessActions();
    }
}
