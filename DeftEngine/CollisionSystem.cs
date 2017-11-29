using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class CollisionSystem : IEntitySystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_ALL_ENTITIES;

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.GetEntities();

            foreach (var e1 in entities)
            {
                foreach (var e2 in entities)
                {
                    if (e1 == e2)
                        continue;

                    if (e1.Has<BulletComponent>() && e2.Has<BulletComponent>())
                        continue;

                    if (e1.Has<FighterComponent>() && e2.Has<FighterComponent>())
                        continue;

                    Entity bullet;
                    Entity fighter;

                    if (e1.Has<BulletComponent>())
                    {
                        bullet = e1;
                        fighter = e2;
                    }
                    else
                    {
                        bullet = e2;
                        fighter = e1;
                    }

                    bool areColliding = (e1.Get<SpatialComponent>().Bounds.Intersects(e2.Get<SpatialComponent>().Bounds));
                    if (areColliding)
                    {
                        if (fighter != bullet.Get<BulletComponent>().shooter)
                        {
                            --fighter.Get<FighterComponent>().health;
                            bullet.AddComponent<KillMeComponent>();
                        }
                    }

                    // The only collision we care about is.
                    // Bullet -> Entity that DID NOT shoot it
                }
            }
        }
    }
}
