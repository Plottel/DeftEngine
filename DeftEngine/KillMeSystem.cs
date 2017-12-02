using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class KillMeSystem : ISystem, IProcessSystem
    {
        public void Process (ECSData ecsData)
        {
            var entities = ecsData.pool.GetEntities<KillMeComponent>();

            for (int i = entities.Count - 1; i >= 0; --i)
                ecsData.pool.RemoveEntity(entities[i]);
        }
    }
}
