using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class Query_SpatialAndVelocityComponents : IEntityQuery
    {
        public bool Query(Entity e)
            => e.Has<SpatialComponent>() && e.Has<VelocityComponent>();

    }

    public class VelocitySystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => new Query_SpatialAndVelocityComponents();

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query<Query_SpatialAndVelocityComponents>();

            SpatialComponent spatial;
            VelocityComponent velComp;

            foreach (var e in entities)
            {
                spatial = e.Get<SpatialComponent>();
                velComp = e.Get<VelocityComponent>();

                spatial.pos += velComp.velocity * velComp.speed;
            }
        }
    }
}
