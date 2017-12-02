using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    // We need a way to determine when an event has been triggered.
    // Easiest way is to use a Process System, which can check once
    // per update if any events should be triggered.
    public class InputEventTriggerSystem : ISystem, IProcessSystem
    {
        public void Process(ECSData ecsData)
        {
            var pool = ECSCore.eventPool;

            // Trigger events if their conditions are met.
            if (Input.RightMouseClicked())
                pool.Trigger<Event_OnRightMouseClick>(ecsData);
            if (Input.RightMousePressed())
                pool.Trigger<Event_OnRightMousePress>(ecsData);
            if (Input.LeftMouseClicked())
                pool.Trigger<Event_OnLeftMouseClick>(ecsData);
            if (Input.LeftMousePressed())
                pool.Trigger<Event_OnLeftMousePress>(ecsData);
            if (Input.DeltaMousePos != Vector2.Zero)
                pool.Trigger<Event_OnMouseMove>(ecsData, Input.DeltaMousePos);
        }
    }
}
