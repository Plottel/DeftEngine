using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_UpdateUIFocusState : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
        {
            eventPool.SubscribeTo<Event_OnLeftMousePress>(this);
            eventPool.SubscribeTo<Event_OnLeftMouseClick>(this);
        }

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            if (DeftUI.focus == null)
                return;

            var eventType = theEvent.GetType();

            if (eventType == typeof(Event_OnLeftMouseClick))
                DeftUI.focusState = UIFocusState.None;
            else if (eventType == typeof(Event_OnLeftMousePress))
            {
                if (DeftUI.focus.Bounds.Contains(Input.MousePos))
                    DeftUI.focusState = UIFocusState.Dragging;
                else
                {
                    var anchor = DeftUI.focus.Bounds.GetBoxAnchorPointAtPos(Input.MousePos, 15);

                    if (anchor != AnchorPoint.None)
                    {
                        DeftUI.focusState = UIFocusState.Resizing;
                        DeftUI.focusResizeAnchor = anchor;
                    }                        
                }                    
            }
        }
    }
}
