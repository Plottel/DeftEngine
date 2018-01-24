using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Process_RegisterNewStopCollisions : ISystem, IProcessSystem
    {
        /// <summary>
        /// Checks all "Colliding" collisions to see if they are still colliding.
        /// If not, they are removed from the "Colliding" collision list and added to the "Stop" collision list.
        /// </summary>
        public void Process()
        {
            var collisions = ECSCore.collisionPool.collisions;
            Entity e;
            Collision collision;
            List<Collision> collidingCollisions;
            List<Collision> stopCollisions;

            foreach (var collisionMap in collisions)
            {
                e = collisionMap.Key;
                collidingCollisions = collisionMap.Value[CollisionState.Colliding];
                stopCollisions = collisionMap.Value[CollisionState.Stop];

                for (int i = collidingCollisions.Count - 1; i >= 0; --i)
                {
                    collision = collidingCollisions[i];

                    if (!Collisions.EntitiesCollide(e, collision.entity))
                    {
                        collidingCollisions.RemoveAt(i);
                        stopCollisions.Add(collision);
                    }
                }
            }
        }
    }
}
