using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_SelectUIFocus : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
        {
            eventPool.SubscribeTo<Event_OnLeftMousePress>(this);
        }

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            Gadget focus = DeftUI.focus;

            if (focus == null)
                SelectNewFocusGadget();
            else
            {
                // Already have an active gadget, only make a new selection IF:
                //      If within borders, we're not within the borders of a higher layer element
                //      If not within borders,
                //          If resizable, mouse not within resize boxes
                if (focus.Bounds.Contains(Input.MousePos))
                {
                    if (!IsHighestLayerGadgetAtPos(focus.layer, Input.MousePos))
                        SelectNewFocusGadget();
                }
                else // Mouse not within focus borders
                {
                    if (!focus.isResizable)
                        SelectNewFocusGadget();
                    else if (focus.Bounds.GetBoxAnchorPointAtPos(Input.MousePos, 7) == AnchorPoint.None)
                        SelectNewFocusGadget();
                }
            }
        }

        private void SelectNewFocusGadget()
        {
            if (!DeftUI.IsEmpty)
                return;

            var mousePos = Input.MousePos;
            // Select first gadget that overlaps mouse position.
            foreach (var g in DeftUI.FrontToBackGadgets)
            {
                if (g.Bounds.Contains(mousePos))
                {
                    DeftUI.focus = g;
                    return;
                }
            }
        }

        private bool IsHighestLayerGadgetAtPos(int layer, Vector2 pos)
        {
            foreach (var g in DeftUI.Gadgets)
            {
                if (g.layer > layer && g.Bounds.Contains(pos))
                    return false;
            }
            return true;
        }
    }
}
