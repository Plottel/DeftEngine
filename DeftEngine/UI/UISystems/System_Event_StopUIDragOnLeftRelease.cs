using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Event_StopUIDragOnLeftRelease : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnLeftMouseClick>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var entities = ECSCore.pool.GetEntities<Component_UI_Drag>();

            for (int i = entities.Count - 1; i >= 0; --i)
                entities[i].Remove<Component_UI_Drag>();
        }
    }
}
