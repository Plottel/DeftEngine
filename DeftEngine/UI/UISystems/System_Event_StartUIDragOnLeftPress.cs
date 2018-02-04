using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_StartUIDragOnLeftPress : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnLeftMousePress>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var entities = ECSCore.pool.GetEntities<Component_UI_Draggable>();
            Vector2 mousePos = Input.MousePos;
            Rectangle uiBounds;

            foreach (var e in entities)
            {
                uiBounds = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint());

                if (uiBounds.Contains(mousePos))
                    e.Add(new Component_UI_Drag { dragStartPos = e.Pos });
            }
        }
    }
}
