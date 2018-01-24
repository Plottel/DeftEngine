using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class System_Action_MoveTo : ISystem, IActionSystem
    {
        public void ProcessActions()
        {
            var actions = ECSCore.actionPool.GetActions<Action_MoveTo>();

            foreach (var action in actions)
                action.actor.pos = action.newPosition;
        }
    }
}
