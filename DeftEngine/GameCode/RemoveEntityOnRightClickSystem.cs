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
                SpatialComponent s;

                foreach (var e in ECSCore.pool.GetEntities<SpatialComponent>())
                {
                    s = e.Get<SpatialComponent>();
                    if (s.Bounds.Contains(Input.MousePos))
                        return e;
                }
                return null;
            }
        }

    }
}
