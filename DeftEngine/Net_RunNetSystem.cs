using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class Net_RunNetSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_ALL_ENTITIES;

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.GetEntities<NeuralNetComponent>();

            NeuralNetComponent net;

            foreach (var e in entities)
            {
                net = e.Get<NeuralNetComponent>();
                net.net.Run();
            }
        }
    }
}
