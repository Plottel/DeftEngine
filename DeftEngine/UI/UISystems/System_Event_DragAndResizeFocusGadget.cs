using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_DragAndResizeFocusGadget : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
        {
            eventPool.SubscribeTo<Event_OnLeftMouseDrag>(this);
        }

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            Gadget focus = DeftUI.focus;

            if (focus == null)
                return;

            if (DeftUI.focusState == UIFocusState.Dragging)
                Drag(focus);
            else if (DeftUI.focusState == UIFocusState.Resizing)
                Resize(focus, DeftUI.focusResizeAnchor);
        }

        private void Resize(Gadget g, AnchorPoint anchor)
        {
            Vector2 delta = Input.DeltaMousePos;
            float dx = delta.X;
            float dy = delta.Y;

            if (anchor == AnchorPoint.Left)
            {
                g.X += dx;
                g.Size += new Vector2(-dx, 0);
            }
            else if (anchor == AnchorPoint.Top)
            {
                g.Y += dy;
                g.Size += new Vector2(0, -dy);
            }
            else if (anchor == AnchorPoint.Right)
                g.Size += new Vector2(dx, 0);
            else if (anchor == AnchorPoint.Bot)
                g.Size += new Vector2(0, dy);
            else if (anchor == AnchorPoint.TopLeft)
            {
                g.Pos += delta;
                g.Size += -delta;
            }
            else if (anchor == AnchorPoint.TopRight)
            {
                g.Y += dy;
                g.Size += new Vector2(dx, -dy);
            }
            else if (anchor == AnchorPoint.BotLeft)
            {
                g.X += dx;
                g.Size += new Vector2(-dx, dy);
            }
            else if (anchor == AnchorPoint.BotRight)
            {
                g.Size += delta;
            }
        }

        private void Drag(Gadget g)
        {
            g.Pos += Input.DeltaMousePos;
        }
    }
}
