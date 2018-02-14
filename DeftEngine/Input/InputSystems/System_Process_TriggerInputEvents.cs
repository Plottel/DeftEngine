using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeftEngine
{
    // We need a way to determine when an event has been triggered.
    // Easiest way is to use a Process System, which can check once
    // per update if any events should be triggered.
    public class System_Process_TriggerInputEvents : ISystem, IProcessSystem
    {
        public void Process()
        {
            var pool = ECSCore.eventPool;

            bool mouseMoved = Input.DeltaMousePos != Vector2.Zero;

            // Trigger events if their conditions are met.
            if (Input.RightMouseClicked())
                pool.Trigger<Event_OnRightMouseClick>();
            if (Input.RightMousePressed())
                pool.Trigger<Event_OnRightMousePress>();
            if (Input.LeftMouseClicked())
                pool.Trigger<Event_OnLeftMouseClick>();
            if (Input.LeftMousePressed())
                pool.Trigger<Event_OnLeftMousePress>();

            if (mouseMoved)
            {
                pool.Trigger<Event_OnMouseMove>(Input.DeltaMousePos);

                if (Input.LeftMouseDown())
                    pool.Trigger<Event_OnLeftMouseDrag>(Input.DeltaMousePos);
                if (Input.RightMouseDown())
                    pool.Trigger<Event_OnRightMouseDrag>(Input.DeltaMousePos);
            }

            if (Input.InputString != "")
                pool.Trigger<Event_OnTextEntry>(Input.PumpInputString());
            if (Input.KeyTyped(Keys.Back))
                pool.Trigger<Event_OnTextEntry>("BACKSPACE");
            if (Input.KeyTyped(Keys.Delete))
                pool.Trigger<Event_OnTextEntry>("DELETE");
        }
    }
}
