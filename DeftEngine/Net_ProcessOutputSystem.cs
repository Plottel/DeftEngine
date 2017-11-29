using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Net_ProcessOutputSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_ALL_ENTITIES;

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.GetEntities<NeuralNetComponent>();

            foreach (var e in entities)
            {
                var outputs = e.Get<NeuralNetComponent>().net.outputs;

                var newDirection = new Vector2(outputs[0] * 2 - 1, outputs[1] * 2 - 1);

                var tryToShoot = outputs[2] > 0.5f;

                var moveAction = new Action_SetVelocity();
                moveAction.newVelocity = newDirection;
                moveAction.actor = e;

                ECSCore.actionPool.AddAction(moveAction);

                if (tryToShoot)
                {
                    var shootAction = new Action_ShootBullet();
                    shootAction.actor = e;
                    shootAction.dir = e.Get<VelocityComponent>().velocity;
                    shootAction.speed = SimParams.BULLET_SPEED;

                    ECSCore.actionPool.AddAction(shootAction);
                }
            }
        }
    }
}
