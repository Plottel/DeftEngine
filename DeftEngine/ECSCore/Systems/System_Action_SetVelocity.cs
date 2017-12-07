using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class System_Action_SetVelocity : ISystem, IActionSystem
    {
        public void ProcessActions()
        {
            var actions = ECSCore.actionPool.GetActions<Action_SetVelocity>();

            foreach (var action in actions)
                action.actor.Get<Component_Velocity>().velocity = action.newVelocity;
        }

        public void Process(ECSData ecsData) { }
    }
}
