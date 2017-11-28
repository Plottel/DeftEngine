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
        void Process(ECSData ecsData);

        /// <summary>
        /// Determines whether or not an event system should process this update.
        /// // TODO: Currently brute forcing all event systems.
        /// // Need to build an "Event.. System" so we only check IEventSystems when an incoming event
        /// // arrives - like messaging.
        /// </summary>
        bool ShouldProcess(ECSData ecsData);
    }
}
