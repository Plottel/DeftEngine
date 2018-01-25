using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace DeftEngine
{
    public static class Collisions
    {
        private delegate bool CollisionTest(Entity e1, Entity e2);

        private static Dictionary<Type, Dictionary<Type, CollisionTest>> _collisionMethods =
            new Dictionary<Type, Dictionary<Type, CollisionTest>>();

        public static void Setup()
        {
            Type box = typeof(Component_Collision_Box);
            Type circle = typeof(Component_Collision_Circle);

            _collisionMethods[box] = new Dictionary<Type, CollisionTest>();
            _collisionMethods[circle] = new Dictionary<Type, CollisionTest>();

            _collisionMethods[box][box] = TestBoxBox;
            _collisionMethods[box][circle] = TestBoxCircle;

            _collisionMethods[circle][circle] = TestCircleCircle;
            _collisionMethods[circle][box] = TestCircleBox;
        }

        public static void SyncColliders(Entity e)
        {
            foreach (var collider in e.Colliders)
                SyncCollider(e, collider);
        }

        // TODO: Check if doing this manually is ACTUALLY faster than just using IColliderComponent.SetDefault()
        // This method has additional type checking but less assignments.
        private static void SyncCollider(Entity e, IColliderComponent collider)
        {
            // Figure out which type it is, sync accordingly.
            if (collider is Component_Collision_Box)
            {
                var box = collider as Component_Collision_Box;
                box.bounds.Location = (e.pos + box.offsetEntityPos).ToPoint();
            }
            else if (collider is Component_Collision_Circle)
            {
                var circle = collider as Component_Collision_Circle;
                circle.bounds.Center = e.MidPt + circle.offsetEntityMid.ToPoint();
            }
        }

        public static bool EntitiesCollide(Entity e1, Entity e2)
        {
            Type c1Type;
            Type c2Type;

            foreach (var collider1 in e1.Colliders)
            {
                c1Type = collider1.GetType();

                foreach (var collider2 in e2.Colliders)
                {
                    c2Type = collider2.GetType();

                    if (_collisionMethods[c1Type][c2Type](e1, e2))
                        return true;
                }
            }

            return false;
        }

        private static bool TestBoxBox(Entity e1, Entity e2)
        {
            var box1 = e1.Get<Component_Collision_Box>();
            var box2 = e2.Get<Component_Collision_Box>();

            Vector2 box1Min = box1.bounds.TopLeft();
            Vector2 box1Max = box1.bounds.BottomRight();

            Vector2 box2Min = box2.bounds.TopLeft();
            Vector2 box2Max = box2.bounds.BottomRight();

            if (box1Max.X < box2Min.X || box1Min.X > box2Max.X) return false;
            if (box1Max.Y < box2Min.Y || box1Min.Y > box2Max.Y) return false;
            return true; // Colliding.
        }

        private static bool TestCircleCircle(Entity e1, Entity e2)
        {
            var circle1 = e1.Get<Component_Collision_Circle>();
            var circle2 = e2.Get<Component_Collision_Circle>();

            float radiusSum = circle1.radius + circle2.radius;
            float sqDistApart = Vector2.DistanceSquared(circle1.bounds.Center, circle2.bounds.Center);

            return sqDistApart <= radiusSum * radiusSum;
        }

        private static bool TestBoxCircle(Entity e1, Entity e2)
        {
            var box = e1.Get<Component_Collision_Box>();
            var circle = e2.Get<Component_Collision_Circle>();

            Vector2 circleMid = circle.bounds.Center;
            float sqRadius = circle.radius * circle.radius;

            return box.bounds.ToRectangleF().SquaredDistanceTo(circleMid) <= sqRadius;
        }

        private static bool TestCircleBox(Entity e1, Entity e2) => TestBoxCircle(e2, e1);
    }
}
