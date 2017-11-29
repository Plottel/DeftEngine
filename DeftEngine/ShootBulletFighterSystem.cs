using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class Query_FighterComponent : IEntityQuery
    {
        public bool Query(Entity e)
           => e.Has<FighterComponent>();
    }

    public class ShootBulletFighterSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => new Query_FighterComponent();

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query<Query_FighterComponent>();

            if (Input.KeyTyped(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                foreach (var e in entities)
                {
                    var shootBulletAction = new Action_ShootBullet();
                    shootBulletAction.actor = e;
                    shootBulletAction.pos = e.Get<SpatialComponent>().MidVector - new Microsoft.Xna.Framework.Vector2(5, 5);
                    shootBulletAction.dir = Microsoft.Xna.Framework.Vector2.Normalize(Input.MousePos - e.Get<SpatialComponent>().pos);
                    shootBulletAction.speed = 2f;

                    ECSCore.actionPool.AddAction(shootBulletAction);
                }
            }

        }
    }
}
