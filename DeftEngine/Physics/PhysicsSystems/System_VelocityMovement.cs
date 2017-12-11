using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Process_VelocityMovement : ISystem, IQuerySystem, IProcessSystem
    {
        public bool Query(Entity e)
            => e.Has<Component_Spatial>() && e.Has<Component_Velocity>();

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query(this);
            Component_Velocity velComp;

            foreach (var e in entities)
            {
                velComp = e.Get<Component_Velocity>();
                e.Get<Component_Spatial>().pos += velComp.velocity * velComp.speed;
            }                
        }
    }
}
