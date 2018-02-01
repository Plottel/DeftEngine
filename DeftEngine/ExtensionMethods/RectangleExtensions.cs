using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace DeftEngine
{
    public static class RectangleExtensions
    {
        public static Rectangle GetInflated(this Rectangle rectangle, int horizontalAmount, int verticalAmount)
        {
            Rectangle inflated = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            inflated.Inflate(horizontalAmount, verticalAmount);

            return inflated;
        }

        public static Rectangle GetInflated(this Rectangle rectangle, float horizontalAmount, float verticalAmount)
        {
            Rectangle inflated = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            inflated.Inflate(horizontalAmount, verticalAmount);

            return inflated;
        }

        public static Vector2[] GetVectorCorners(this Rectangle rectangle)
        {
            return new Vector2[4]
            {
                new Vector2(rectangle.Left, rectangle.Top),
                new Vector2(rectangle.Right, rectangle.Top),
                new Vector2(rectangle.Left, rectangle.Bottom),
                new Vector2(rectangle.Right, rectangle.Bottom),
            };
        }

        public static Vector2 TopLeft(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Left, rectangle.Top);
        }

        public static Vector2 BottomRight(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Right, rectangle.Bottom);
        }
    }
}
