using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_StartUIResizeOnLeftPress : ISystem, IEventSystem, IQuerySystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnLeftMousePress>(this);

        public bool Query(Entity e)
            => e.Has<Component_UI_Selected>() && e.Has<Component_UI_Resizable>();

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var entities = ECSCore.pool.Query(this);
            Rectangle[] entAnchorBoxes;
            Vector2 mousePos = Input.MousePos;
            AnchorPoint anchorPoint = AnchorPoint.None;

            bool clickedOnPoint = false;

            // Loop through Selected and Resizable entity.
            // Figure out if we're clicking on one of the resize anchor points.
            // If we are ->
            //  Store the anchor point
            //  Add Resize components to each entity with the stored anchor point value.
            foreach (var e in entities)
            {
                entAnchorBoxes = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint()).Get8ExternalBorderBoxes(7);

                for (int i = 0; i < 8; ++i)
                {
                    if (entAnchorBoxes[i].Contains(Input.MousePos))
                    {
                        anchorPoint = (AnchorPoint)i;
                        clickedOnPoint = true;
                        break;
                    }
                }

                if (clickedOnPoint)
                    break;
            }

            // Add resize components
            if (anchorPoint != AnchorPoint.None)
            {
                foreach (var e in entities)
                    e.Add(new Component_UI_Resize { anchorPoint = anchorPoint });
            }
        }
    }
}
