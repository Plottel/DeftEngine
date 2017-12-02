using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    // When an event is triggered, we need to tell the engine what should happen.
    // This is what Event Systems are for.
    // 1. Subscribe the Event System to a number of events (typically 1)
    // 2. Define an OnEvent method. This is called when an Event is triggered.
    public class TestInputEventsSystem : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool pool)
            => pool.SubscribeToAllEvents(this);

        public void OnEvent(ECSData data, DeftEvent theEvent, params object[] args)
        {
            // Best practice is 1 Event Subscription per System.
            // This sytem is bad and just for demo.
            var eventType = theEvent.GetType();

            if (eventType == typeof(Event_OnLeftMouseClick))
                Console.WriteLine("Left Mouse Clicked!");
            else if (eventType == typeof(Event_OnRightMouseClick))
                Console.WriteLine("Right Mouse Clicked!");
            // etc.....
        }
    }
}
