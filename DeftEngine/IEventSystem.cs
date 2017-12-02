using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Indicates an EntitySystem will only be run when a certain event triggers.
    /// Implementations are required to provide details of the event they respond to.
    /// </summary>
    public interface IEventSystem
    {
        void SubscribeToEvents(EventPool pool);
        void OnEvent(ECSData ecsData, DeftEvent theEvent, params object[] args);
    }
}
