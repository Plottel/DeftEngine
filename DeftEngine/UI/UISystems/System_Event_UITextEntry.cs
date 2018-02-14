using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_UITextEntry : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
        {
            eventPool.SubscribeTo<Event_OnTextEntry>(this);
        }

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            string text = (string)args[0];

            if (DeftUI.focus == null)
                return;

            if (DeftUI.focus.GetType().IsSubclassOf(typeof(TextBox)) &&
                (text == "BACKSPACE" || text == "DELETE"))
            {
                    var textBox = DeftUI.focus as TextBox;
                    textBox.ApplyTextOpCode(text);
            }
            else
                DeftUI.focus.OnTextEntry(text);

        }
    }
}
