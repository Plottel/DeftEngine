using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class Query_KillMeComponent : IEntityQuery
    {
        public bool Query(Entity e)
            => e.Has<KillMeComponent>();
    }

    public class KillMeSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => new Query_KillMeComponent();

        public void Process (ECSData ecsData)
        {
            var entities = ecsData.pool.GetEntities<KillMeComponent>();

            for (int i = entities.Count - 1; i >= 0; --i)
                ecsData.pool.RemoveEntity(entities[i]);
        }
    }
}
