using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Action_TrackMovedEntities : ISystem, IActionSystem
    {
        // TODO: Eliminate this situation:
        // "Remove Component because it moved last frame" -> "Immediately re-add because it also moved this frame"
        public void ProcessActions()
        {
            // Clear previous moved entities
            var entities = ECSCore.pool.GetEntities<Component_Moved>();

            for (int i = 0; i < entities.Count; ++i)
                entities[i].Remove<Component_Moved>();

            // Populate newly moved entities
            var actions = ECSCore.actionPool.GetActions<Action_MoveTo>();
            Entity actor;

            foreach (var a in actions)
            {
                actor = a.actor;

                if (actor.pos != a.newPosition)
                    actor.Add<Component_Moved>();
            }
        }
    }
}
