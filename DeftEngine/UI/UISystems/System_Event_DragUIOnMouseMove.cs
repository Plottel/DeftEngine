using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_DragUIOnMouseMove : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnMouseMove>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var entities = ECSCore.pool.GetEntities<Component_UI_Drag>();
            var deltaMousePos = Input.DeltaMousePos;

            foreach (var e in entities)
                e.MoveBy(deltaMousePos);
        }
    }
}
