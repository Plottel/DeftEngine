using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class SetVelocityActionSystem : ISystem, IActionSystem
    {
        public void ProcessActions()
        {
            var actions = ECSCore.actionPool.GetActions<Action_SetVelocity>();

            foreach (var action in actions)
                action.actor.Get<VelocityComponent>().velocity = action.newVelocity;
        }

        public void Process(ECSData ecsData) { }
    }
}
