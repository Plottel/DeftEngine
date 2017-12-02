using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Velocity : ISystem, IQuerySystem, IProcessSystem
    {
        public bool Query(Entity e)
            => e.Has<SpatialComponent>() && e.Has<VelocityComponent>();

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query(this);
            VelocityComponent velComp;

            foreach (var e in entities)
            {
                velComp = e.Get<VelocityComponent>();
                e.Get<SpatialComponent>().pos += velComp.velocity * velComp.speed;
            }                
        }
    }
}
