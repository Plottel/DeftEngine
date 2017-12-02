using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Regen : ISystem, IProcessSystem, IQuerySystem
    {
        public bool Query(Entity e)
            => e.Has<HealthComponent>() && e.Has<RegenComponent>();      

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query(this);
            HealthComponent hp;
            RegenComponent regen;

            foreach (var e in entities)
            {
                hp = e.Get<HealthComponent>();
                regen = e.Get<RegenComponent>();

                hp.health += regen.regenAmount;
            }
        }
    }
}
