using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class ActionPool
    {
        public Dictionary<Type, IActionSystem> systemMap =
            new Dictionary<Type, IActionSystem>();

        public void SubscribeTo<T>(IActionSystem subscriber) where T : DeftAction
        {
            var actionType = typeof(T);
            systemMap[actionType] = subscriber;
        }

        public void SubscribeTo(Type actionType, IActionSystem subscriber)
        {
            Debug.Assert(typeof(DeftAction).IsAssignableFrom(actionType));
            systemMap[actionType] = subscriber;
        }

        public void AddAction(DeftAction action)
        {
            var actionType = action.GetType();

            if (systemMap.ContainsKey(actionType))
                systemMap[actionType].EnqueueAction(action);
        }
    }
}
