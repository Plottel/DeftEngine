using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class ShootBulletActionSystem : IEntitySystem, IActionSystem
    {
        public Queue<Action_ShootBullet> actions = new Queue<Action_ShootBullet>();

        public Type GetActionType()
            => typeof(Action_ShootBullet);

        public IEntityQuery GetQuery()
            => EntityPool.QUERY_NO_ENTITIES;

        public void EnqueueAction(DeftAction action)
        {
            Debug.Assert(action is Action_ShootBullet, "Wrong action type");
            actions.Enqueue(action as Action_ShootBullet);
        }

        public void Process(ECSData ecsData)
        {
            while (actions.Count > 0)
                OnAction(actions.Dequeue());
        }

        public void OnAction(DeftAction action)
        {
            Debug.Assert(action is Action_ShootBullet, "Wrong action type");

            var shootBullet = action as Action_ShootBullet;

            var bullets = ECSCore.pool.GetEntities<BulletComponent>();

            bool canShoot = true;

            foreach (var bullet in bullets)
            {
                if (bullet.Get<BulletComponent>().shooter == shootBullet.actor)
                {
                    canShoot = false;
                    break;
                }
            }
            
            if (canShoot)
                EntityFactory.Bullet(shootBullet.pos, shootBullet.dir, shootBullet.speed, shootBullet.actor);             
        }
    } 
}
