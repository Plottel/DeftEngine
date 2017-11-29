using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class InputEventTriggerSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_NO_ENTITIES;

        public void Process(ECSData ecsData)
        {
            if (Input.LeftMouseClicked())
                ECSCore.eventPool.Trigger<Event_OnLeftMouseClick>(ecsData);
        }
    }
}
