using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_SelectUIOnLeftPress : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnLeftMousePress>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var entities = ECSCore.pool.GetEntities<Component_UI_Selectable>();
            Component_UI_Selectable selectComp;
            Rectangle uiBounds;
            Vector2 mousePos = Input.MousePos;

            foreach (var e in entities)
            {
                selectComp = e.Get<Component_UI_Selectable>();
                selectComp.isSelected = false;
            }

            foreach (var e in entities)
            {
                selectComp = e.Get<Component_UI_Selectable>();
                uiBounds = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint());

                if (uiBounds.Contains(Input.MousePos))
                    selectComp.isSelected = true;
            }

        }
    }
}
