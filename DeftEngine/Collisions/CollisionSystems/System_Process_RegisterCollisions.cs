using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public class System_Process_RegisterCollisions : ISystem, IProcessSystem
    {
        public void Process()
        {
            var entities = ECSCore.pool.GetEntities();
            var collisionPool = ECSCore.collisionPool;

            foreach (var e1 in entities)
            {
                foreach (var e2 in entities)
                {
                    if (e1 == e2) continue;

                    if (Collisions.EntitiesCollide(e1, e2))
                    {
                        collisionPool.RegisterCollision(e1, e2);
                        collisionPool.RegisterCollision(e2, e1);
                    }
                }
            }
        }
    }
}
