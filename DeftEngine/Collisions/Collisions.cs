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
            Type aaBox = typeof(Component_Collision_AABox);
            Type circle = typeof(Component_Collision_Circle);
            Type box = typeof(Component_Collision_Box);

            _collisionMethods[aaBox] = new Dictionary<Type, CollisionTest>();
            _collisionMethods[circle] = new Dictionary<Type, CollisionTest>();
            _collisionMethods[box] = new Dictionary<Type, CollisionTest>();

            _collisionMethods[aaBox][aaBox] = TestAABoxAABox;
            _collisionMethods[aaBox][circle] = TestAABoxCircle;
            _collisionMethods[aaBox][box] = TestAABoxBox;

            _collisionMethods[circle][circle] = TestCircleCircle;
            _collisionMethods[circle][aaBox] = TestCircleAABox;
            _collisionMethods[circle][box] = TestCircleBox;

            _collisionMethods[box][box] = TestBoxBox;
            _collisionMethods[box][aaBox] = TestBoxAABox;
            _collisionMethods[box][circle] = TestBoxCircle;
        }

        public static void SyncColliders(Entity e)
        {
            foreach (var collider in e.Colliders)
                collider.Sync(e);
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

        private static bool TestAABoxAABox(Entity e1, Entity e2)
        {
            var box1 = e1.Get<Component_Collision_AABox>();
            var box2 = e2.Get<Component_Collision_AABox>();

            Vector2 box1Min = box1.bounds.TopLeft();
            Vector2 box1Max = box1.bounds.BottomRight();

            Vector2 box2Min = box2.bounds.TopLeft();
            Vector2 box2Max = box2.bounds.BottomRight();

            if (box1Max.X < box2Min.X || box1Min.X > box2Max.X) return false;
            if (box1Max.Y < box2Min.Y || box1Min.Y > box2Max.Y) return false;
            return true; // Colliding.
        }

        private static bool TestAABoxCircle(Entity e1, Entity e2)
        {
            var box = e1.Get<Component_Collision_AABox>();
            var circle = e2.Get<Component_Collision_Circle>();

            Vector2 circleMid = circle.bounds.Center;
            float sqRadius = circle.bounds.Radius * circle.bounds.Radius;

            return box.bounds.ToRectangleF().SquaredDistanceTo(circleMid) <= sqRadius;
        }

        private static bool TestCircleCircle(Entity e1, Entity e2)
        {
            var circle1 = e1.Get<Component_Collision_Circle>();
            var circle2 = e2.Get<Component_Collision_Circle>();

            float radiusSum = circle1.bounds.Radius + circle2.bounds.Radius;
            float sqDistApart = Vector2.DistanceSquared(circle1.bounds.Center, circle2.bounds.Center);

            return sqDistApart <= radiusSum * radiusSum;
        }

        private static bool TestCircleBox(Entity e1, Entity e2)
        {
            var circle = e1.Get<Component_Collision_Circle>();
            var box = e2.Get<Component_Collision_Box>();

            Vector2 circleMid = circle.bounds.Center;
            float sqRadius = circle.bounds.Radius * circle.bounds.Radius;

            Vector2 closestPt = box.box.ClosestPointTo(circleMid);

            return Vector2.DistanceSquared(closestPt, circleMid) <= sqRadius;
        }

        // TODO: SLOW. Massive point conversion, should be able to take advantage of 1 box being Axis-Aligned.
        private static bool TestAABoxBox(Entity e1, Entity e2)
        {
            var aaBox = e1.Get<Component_Collision_AABox>().bounds;
            var box = e2.Get<Component_Collision_Box>().box;

            Vector2[] aaBoxCorners = aaBox.GetVectorCorners();
            Vector2[] boxCorners = box.Corners;

            Vector2[] axes = { box.LocalXAxis, box.LocalYAxis, new Vector2(1, 0), new Vector2(0, 1) };

            foreach (Vector2 axis in axes)
            {
                if (!BoxesCollideOnAxis(aaBoxCorners, boxCorners, axis))
                    return false;
            }

            // All of the axes are colliding, collision.
            return true;
        }

        private static bool TestBoxBox(Entity e1, Entity e2)
        {
            var box1 = e1.Get<Component_Collision_Box>().box;
            var box2 = e2.Get<Component_Collision_Box>().box;

            Vector2[] box1Corners = box1.Corners;
            Vector2[] box2Corners = box2.Corners;

            Vector2[] axes = { box1.LocalXAxis, box1.LocalYAxis, box2.LocalXAxis, box2.LocalYAxis };

            foreach (Vector2 axis in axes)
            {
                if (!BoxesCollideOnAxis(box1Corners, box2Corners, axis))
                    return false;
            }

            // All of the axes are colliding, collision.
            return true;
        }

        private static bool TestCircleAABox(Entity e1, Entity e2) => TestAABoxCircle(e2, e1);
        private static bool TestBoxAABox(Entity e1, Entity e2) => TestAABoxBox(e2, e1);
        private static bool TestBoxCircle(Entity e1, Entity e2) => TestCircleBox(e2, e1);

        private static bool BoxesCollideOnAxis(Vector2[] box1Corners, Vector2[] box2Corners, Vector2 axis)
        {
            float[] box1Projections = new float[4];
            float[] box2Projections = new float[4];

            // Project all 8 corners to generate 8 scalars.
            for (int i = 0; i < 4; ++i)
            {
                box1Projections[i] = Vector2.Dot(box1Corners[i], axis);
                box2Projections[i] = Vector2.Dot(box2Corners[i], axis);
            }

            // Fetch box1Min/Max and box2Min/Max
            float box1Min = box1Projections.Min();
            float box1Max = box1Projections.Max();
            float box2Min = box2Projections.Min();
            float box2Max = box2Projections.Max();

            return (box2Min <= box1Max && box2Max >= box1Max) ||
                                (box1Min <= box2Max && box1Max >= box2Max);
        }

    }
}
