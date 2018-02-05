using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Event_StopUIResizeOnLeftRelease : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnLeftMouseClick>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var oldResizeEntities = ECSCore.pool.GetEntities<Component_UI_Resize>();

            // Clear old resize entities.
            for (int i = oldResizeEntities.Count - 1; i >= 0; --i)
                oldResizeEntities[i].Remove<Component_UI_Resize>();
        }
    }
}
