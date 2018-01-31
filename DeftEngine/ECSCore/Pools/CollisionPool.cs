using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public enum CollisionState
    {
        Start,
        Colliding,
        Stop
    }

    public class CollisionPool
    {
        public Dictionary<Entity, Dictionary<CollisionState, List<Collision>>> collisions
            = new Dictionary<Entity, Dictionary<CollisionState, List<Collision>>>();

        public int NumStartCollisions
        {
            get
            {
                int result = 0;
                foreach (var collisionMap in collisions.Values)
                    result += collisionMap[CollisionState.Start].Count;
                return result;
            }
        }

        public int NumCollidingCollisions
        {
            get
            {
                int result = 0;
                foreach (var collisionMap in collisions.Values)
                    result += collisionMap[CollisionState.Colliding].Count;
                return result;
            }
        }

        public int NumStopCollisions
        {
            get
            {
                int result = 0;
                foreach (var collisionMap in collisions.Values)
                    result += collisionMap[CollisionState.Stop].Count;
                return result;
            }
        }

        public void RegisterCollision(Entity entity, Entity collidedWith)
        {
            // New entity, set up collision map.
            if (!collisions.ContainsKey(entity))
            {
                var collisionMap = new Dictionary<CollisionState, List<Collision>>();
                collisionMap[CollisionState.Start] = new List<Collision>();
                collisionMap[CollisionState.Colliding] = new List<Collision>();
                collisionMap[CollisionState.Stop] = new List<Collision>();

                collisions[entity] = collisionMap;
            }

            // Fetch collision data, useful...
            var collisionsColliding = collisions[entity][CollisionState.Colliding];
            var collisionsStart = collisions[entity][CollisionState.Start];

            // Collision already in the Colliding list, not a new collision.
            if (collisionsColliding.Find(collision => collision.entity == collidedWith) != null)
                return;

            var theStartCollision = collisionsStart.Find(collision => collision.entity == collidedWith);

            // Started colliding last frame. Remove from Start list, add to Colliding list.
            if (theStartCollision != null)
            {
                collisionsStart.Remove(theStartCollision);
                collisionsColliding.Add(theStartCollision);
                return;
            }

            // Not in the Start or Colliding list, must be a new collision.
            collisionsStart.Add(new Collision { entity = collidedWith });
        }         
        
        public List<Collision> GetCollisions(Entity entity, CollisionState state)
        {
            Debug.Assert(collisions.ContainsKey(entity));
            return collisions[entity][state];
        }

        public bool HasCollision(Entity entity)
        {
            if (!collisions.ContainsKey(entity)) return false;

            return collisions[entity][CollisionState.Start].Count != 0 ||
                collisions[entity][CollisionState.Colliding].Count != 0;
        }
    }
}
