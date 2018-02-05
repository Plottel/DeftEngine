using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_ResizeUIOnMouseMove : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnMouseMove>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var entities = ECSCore.pool.GetEntities<Component_UI_Resize>();
            AnchorPoint anchor = AnchorPoint.None;

            var delta = Input.DeltaMousePos;
            var dx = delta.X;
            var dy = delta.Y;

            // Assume a universal anchor point. Can only be resizing in one direction at a time, surely.
            // TODO: Investigate if this can break somehow

            foreach (var e in entities)
            {
                anchor = e.Get<Component_UI_Resize>().anchorPoint;

                if (anchor == AnchorPoint.Left)
                {
                    e.MoveByX(dx);
                    e.Size += new Vector2(-dx, 0);
                }
                else if (anchor == AnchorPoint.Top)
                {
                    e.MoveByY(dy);
                    e.Size += new Vector2(0, -dy);
                }
                else if (anchor == AnchorPoint.Right)
                    e.Size += new Vector2(dx, 0);
                else if (anchor == AnchorPoint.Bot)
                    e.Size += new Vector2(0, dy);
                else if (anchor == AnchorPoint.TopLeft)
                {
                    e.MoveBy(delta);
                    e.Size += -delta;
                }
                else if (anchor == AnchorPoint.TopRight)
                {
                    e.MoveByY(dy);
                    e.Size += new Vector2(dx, -dy);
                }
                else if (anchor == AnchorPoint.BotLeft)
                {
                    e.MoveByX(dx);
                    e.Size += new Vector2(-dx, dy);
                }
                else if (anchor == AnchorPoint.BotRight)
                {
                    e.Size += delta;
                }
            }
        }

        //private void HandleResize()
        //{
        //    var dx = Input.DeltaMousePos.X;
        //    var dy = Input.DeltaMousePos.Y;

        //    if (_selectedResizeRectIndex == 0)           // Left
        //    {
        //        _editing.MoveByX(dx);
        //        _editing.ResizeByX(-dx);
        //    }
        //    else if (_selectedResizeRectIndex == 1)      // Top
        //    {
        //        _editing.MoveByY(dy);
        //        _editing.ResizeByY(-dy);
        //    }
        //    else if (_selectedResizeRectIndex == 2)      // Right
        //    {
        //        _editing.ResizeByX(dx);
        //    }
        //    else if (_selectedResizeRectIndex == 3)      // Bottom
        //    {
        //        _editing.ResizeByY(dy);
        //    }
        //}
    }
}
