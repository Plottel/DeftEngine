using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DeftEngine;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace DeftEngineTesting
{
    [TestFixture]
    public class Test_AABoxAABoxCollisions
    {
        Entity e1;
        Entity e2;
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

            e1 = new Entity();
            e1.MoveTo(new Vector2(50, 50));
            e1.Size = new Vector2(50, 50);
            e1.Add<Component_Collision_AABox>();

            e2 = new Entity();
            e2.MoveTo(new Vector2(50, 50));
            e2.Size = new Vector2(50, 50);
            e2.Add<Component_Collision_AABox>();
        }

        [Test]
        public void SamePosition()
        {
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void OnBorders()
        {
            Vector2 originalPos = e1.Pos;

            e1.MoveByX(e1.Size.X);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveByX(-e1.Size.X);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveByY(e1.Size.Y);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveByY(-e1.Size.Y);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void OnCorners()
        {
            Vector2 originalPos = e1.Pos;

            e1.MoveBy(e1.Size);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveBy(-e1.Size);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveBy(new Vector2(e1.Size.X, -e1.Size.Y));
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveBy(new Vector2(-e1.Size.X, e1.Size.Y));
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void SeparateByOne()
        {
            Vector2 originalPos = e1.Pos;

            e1.MoveByX(e1.Size.X + 1);
            Collisions.SyncColliders(e1);
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveByX(-(e1.Size.X + 1));
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveByY(e1.Size.Y + 1);
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
            e1.MoveTo(originalPos);
            e1.MoveByY(-(e1.Size.Y + 1));
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void SmallBoxCompletelyInsideBigBox()
        {
            e2.Size = new Vector2(5, 5);
            e2.Get<Component_Collision_AABox>()._offsetSize = new Vector2(5, 5);
            e2.MoveBy(e1.Size / 2);
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }
    }
}
