using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeftEngine;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace DeftEngineTesting
{
    [TestFixture]
    public class Test_AABoxCircleCollisions
    {
        Entity box;
        Entity circle;
        bool needsInit = true;

        [SetUp]
        public void Setup()
        {
            if (needsInit)
            {
                ECSCore.Setup();
                ECSCore.Start();
                needsInit = false;
            }

            box = new Entity();
            box.MoveTo(new Vector2(50, 50));
            box.Size = new Vector2(50, 50);
            box.Add<Component_Collision_AABox>();

            circle = new Entity();
            circle.MoveTo(new Vector2(50, 50));
            circle.Size = new Vector2(50, 50);
            circle.Add<Component_Collision_AABox>();
        }

        [Test]
        public void SamePosition()
        {
            Assert.IsTrue(Collisions.EntitiesCollide(box, circle));
        }

        [Test]
        public void OnBorder()
        {
            circle.MoveBy(box.Size);
            Assert.IsTrue(Collisions.EntitiesCollide(box, circle));
        }

        [Test]
        public void SeparateByOne()
        {
            circle.MoveBy(box.Size + new Vector2(1, 1));
            Assert.IsFalse(Collisions.EntitiesCollide(box, circle));
        }

        [Test]
        public void BoxInsideCircle()
        {
            box.Size = new Vector2(2, 2);
            box.MoveBy(new Vector2(10, 10));
            Assert.IsTrue(Collisions.EntitiesCollide(box, circle));
        }

        [Test]
        public void CircleInsideBox()
        {
            circle.Size = new Vector2(2, 2);
            circle.MoveBy(new Vector2(10, 10));
            Assert.IsTrue(Collisions.EntitiesCollide(box, circle));
        }
    }
}
