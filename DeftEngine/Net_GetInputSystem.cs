using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Net_GetInputSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_ALL_ENTITIES;

        public void Process(ECSData ecsData)
        {
            var fighters = ecsData.pool.GetEntities<FighterComponent>();
            var bullets = ecsData.pool.GetEntities<BulletComponent>();

            var f1 = fighters[0];
            var f2 = fighters[1];

            // Fighter 1
            var n1 = fighters[0].Get<NeuralNetComponent>();

            n1.net.inputs.Clear();

            var toOther = f2.Get<SpatialComponent>().pos - f1.Get<SpatialComponent>().pos;

            bool hasBullet = false;

            foreach (var bullet in bullets)
            {
                if (bullet.Get<BulletComponent>().shooter == f1)
                {
                    hasBullet = true;
                    break;
                }
            }

            n1.net.inputs.Add(Convert.ToInt32(hasBullet));
            n1.net.inputs.Add(toOther.X);
            n1.net.inputs.Add(toOther.Y);
            n1.net.inputs.Add(toOther.Length());

            // Fighter 2
            var n2 = fighters[1].Get<NeuralNetComponent>();

            toOther = toOther * -1;

            n2.net.inputs.Clear();

            hasBullet = false;

            foreach (var bullet in bullets)
            {
                if (bullet.Get<BulletComponent>().shooter == f2)
                {
                    hasBullet = true;
                    break;
                }
            }

            n2.net.inputs.Add(Convert.ToInt32(hasBullet));
            n2.net.inputs.Add(toOther.X);
            n2.net.inputs.Add(toOther.Y);
            n2.net.inputs.Add(toOther.Length());
        }
    }
}
