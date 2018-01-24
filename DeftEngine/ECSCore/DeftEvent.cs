using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class DeftEvent
    {
        public List<IEventSystem> listeners = new List<IEventSystem>();

        public void Trigger(params object[] args)
        {
            foreach (var listener in listeners)
                listener.OnEvent(this, args);
        }
    }
}