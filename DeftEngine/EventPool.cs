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

        public void AddEvent(Type eventType)
        {
            System.Diagnostics.Debug.Assert(typeof(DeftEvent).IsAssignableFrom(eventType));
            events[eventType] = (DeftEvent)Activator.CreateInstance(eventType);
        }

        public void SubscribeTo<T>(IEventSystem subscriber) where T : DeftEvent
        {
            var eType = typeof(T);

            if (events.ContainsKey(eType))
                events[eType].listeners.Add(subscriber);
        }

        public void SubscribeToAllEvents(IEventSystem subscriber)
        {
            foreach (var deftEvent in events.Values)
                deftEvent.listeners.Add(subscriber);
        }

        public void Trigger<T>(ECSData ecsData, params object[] args) where T : DeftEvent
        {
            var eType = typeof(T);

            if (events.ContainsKey(eType))
                events[eType].Trigger(ecsData, args);
        }
    }
}
