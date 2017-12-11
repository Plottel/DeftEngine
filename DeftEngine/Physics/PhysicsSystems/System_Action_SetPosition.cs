using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class System_Action_SetPosition : ISystem, IActionSystem
    {
        public void ProcessActions()
        {
            // Fetch the actions to process.
            var actions = ECSCore.actionPool.GetActions<Action_SetPosition>();

            // Process the actions.
            foreach (var action in actions)
            {
                // Check nothing went wrong.
                Debug.Assert(action.actor.Has<Component_Spatial>());

                // Move the entity.
                action.actor.Get<Component_Spatial>().pos = action.newPosition;
            }
        }
    }
}
