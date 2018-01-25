using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Action_MoveBy : ISystem, IActionSystem
    {
        public void ProcessActions()
        {
            var actions = ECSCore.actionPool.GetActions<Action_MoveBy>();

            foreach (var action in actions)
                action.actor.pos += action.deltaPos;

            foreach (var action in actions)
                Collisions.SyncColliders(action.actor);
        }
    }
}
