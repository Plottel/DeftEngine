using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class SetVelocityActionSystem : IEntitySystem, IActionSystem
    {
        public Queue<Action_SetVelocity> actions = new Queue<Action_SetVelocity>();

        public Type GetActionType()
            => typeof(Action_SetVelocity);

        public IEntityQuery GetQuery()
            => EntityPool.QUERY_NO_ENTITIES;

        public void EnqueueAction(DeftAction action)
        {
            Debug.Assert(action is Action_SetVelocity, "Wrong action type");
            actions.Enqueue(action as Action_SetVelocity);
        }

        public void Process(ECSData ecsData)
        {
            while (actions.Count > 0)
                OnAction(actions.Dequeue());
        }

        public void OnAction(DeftAction action)
        {
            Debug.Assert(action is Action_SetVelocity, "Wrong action type");

            var setVel = action as Action_SetVelocity;

            if (setVel.actor != null)
                setVel.actor.Get<VelocityComponent>().velocity = setVel.newVelocity;            
        }
    }
}
