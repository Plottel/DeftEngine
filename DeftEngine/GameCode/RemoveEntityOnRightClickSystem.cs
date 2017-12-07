using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class RemoveEntityOnRightClickSystem : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool pool)
            => pool.SubscribeTo<Event_OnRightMouseClick>(this);

        public void OnEvent(ECSData ecsData, DeftEvent theEvent, params object[] args)
        {
            var e = EntityAtMousePos;

            if (e != null)
                ecsData.pool.RemoveEntity(e);
        }

        private Entity EntityAtMousePos
        {
            get
            {
                Component_Spatial s;

                foreach (var e in ECSCore.pool.GetEntities<Component_Spatial>())
                {
                    s = e.Get<Component_Spatial>();
                    if (s.Bounds.Contains(Input.MousePos))
                        return e;
                }
                return null;
            }
        }

    }
}
