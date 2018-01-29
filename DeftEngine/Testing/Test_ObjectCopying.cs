using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeftEngine;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace DeftEngineTesting
{
    [TestFixture]
    public class Test_ObjectCopying
    {
        // o = Original
        // c = Copy

        [Test]
        public void TestCopyVector2()
        {
            Vector2 original = new Vector2(100, 200);
            Vector2 copy = (Vector2)Serializer.CopyObject(original);

            Assert.IsTrue(copy != null && original.X == copy.X && original.Y == copy.Y);
        }

        [Test]
        public void TestCopyRectangle()
        {
            Rectangle o = new Rectangle(100, 200, 300, 400);
            Rectangle c = (Rectangle)Serializer.CopyObject(o);

            Assert.IsTrue(c != null &&
                o.X == c.X &&
                o.Y == c.Y &&
                o.Width == c.Width &&
                o.Height == c.Height);
        }

        [Test]
        public void TestCopyComponent_Display_Box()
        {
            Component_Display_Box o = new Component_Display_Box { color = Color.Red };
            Component_Display_Box c = (Component_Display_Box)Serializer.CopyObject(o);

            Assert.IsTrue(c != null && c.color == o.color);
        }
    }
}
