using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class EventPool
    {
        public Dictionary<Type, DeftEvent> events = 
            new Dictionary<Type, DeftEvent>();

        public void AddEvent<T>() where T : DeftEvent
        {
            var eType = typeof(T);
            events[eType] = (T)Activator.CreateInstance(eType);
        }

        public void SubscribeTo<T>(IEventSystem subscriber) where T : DeftEvent
        {
            var eType = typeof(T);

            if (events.ContainsKey(eType))
                events[eType].listeners.Add(subscriber);
        }

        public void Trigger<T>(ECSData ecsData, params object[] args) where T : DeftEvent
        {
            var eType = typeof(T);

            if (events.ContainsKey(eType))
                events[eType].Trigger(ecsData, args);
        }
    }
}
