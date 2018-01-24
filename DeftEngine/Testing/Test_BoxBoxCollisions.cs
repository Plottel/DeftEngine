using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DeftEngine;
using Microsoft.Xna.Framework;

namespace DeftEngineTests
{
    [TestFixture]
    public class Test_BoxBoxCollisions
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
            e1.pos = new Vector2(50, 50);
            e1.size = new Vector2(50, 50);
            e1.Add<Component_Collision_Box>();
            e1.Get<Component_Collision_Box>().offset = Vector2.Zero;
            e1.Get<Component_Collision_Box>().size = e1.size;

            e2 = new Entity();
            e2.pos = new Vector2(50, 50);
            e2.size = new Vector2(50, 50);
            e2.Add<Component_Collision_Box>();
            e2.Get<Component_Collision_Box>().offset = Vector2.Zero;
            e2.Get<Component_Collision_Box>().size = e2.size;
        }

        [Test]
        public void SamePosition()
        {
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void OnBorders()
        {
            Vector2 originalPos = e1.pos;

            e1.pos.X += e1.size.X;
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.pos = originalPos;
            e1.pos.X -= e1.size.X;
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.pos = originalPos;
            e1.pos.Y += e1.size.Y;
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
            e1.pos = originalPos;
            e1.pos.Y -= e1.size.Y;
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void SeparateByOne()
        {
            Vector2 originalPos = e1.pos;

            e1.pos.X += e1.size.X + 1;
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
            e1.pos = originalPos;
            e1.pos.X -= e1.size.X + 1;
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
            e1.pos = originalPos;
            e1.pos.Y += e1.size.Y + 1;
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
            e1.pos = originalPos;
            e1.pos.Y -= e1.size.Y + 1;
            Assert.IsFalse(Collisions.EntitiesCollide(e1, e2));
        }

        [Test]
        public void SmallBoxCompletelyInsideBigBox()
        {
            e2.size = new Vector2(5, 5);
            e2.Get<Component_Collision_Box>().size = new Vector2(5, 5);
            e2.pos += e1.size / 2;
            Assert.IsTrue(Collisions.EntitiesCollide(e1, e2));
        }

    }
}
