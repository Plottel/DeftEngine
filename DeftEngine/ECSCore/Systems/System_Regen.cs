using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    // System_Action_Query_Regen
    public class System_Regen : ISystem, IProcessSystem, IQuerySystem
    {
        public bool Query(Entity e)
            => e.Has<Component_Health>() && e.Has<Component_Regen>();      

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query(this);
            Component_Health hp;
            Component_Regen regen;

            foreach (var e in entities)
            {
                hp = e.Get<Component_Health>();
                regen = e.Get<Component_Regen>();

                hp.health += regen.regenAmount;
            }
        }
    }
}
