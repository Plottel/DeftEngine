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

        // TODO: Change so they're all OUTSIDE the border FULLY
        public static Rectangle[] Get8ExternalBorderBoxes(this Rectangle r, int size)
        {
            var mid = r.Center;

            // Start top left and work around clockwise.
            return new Rectangle[8]
            {
                new Rectangle(r.Left - size, r.Top - size, size, size),
                new Rectangle(mid.X, r.Top - size, size, size),
                new Rectangle(r.Right, r.Top - size, size, size),
                new Rectangle(r.Right, mid.Y, size, size),
                new Rectangle(r.Right, r.Bottom, size, size),
                new Rectangle(mid.X, r.Bottom, size, size),
                new Rectangle(r.Left - size, r.Bottom, size, size),
                new Rectangle(r.Left - size, mid.Y, size, size)
            };
        }

        public static AnchorPoint GetBoxAnchorPointAtPos(this Rectangle r, Vector2 pos, int boxSize)
        {
            var boxes = r.Get8ExternalBorderBoxes(boxSize);

            for (int i = 0; i < 8; ++i)
            {
                if (boxes[i].Contains(pos))
                    return (AnchorPoint)i;
            }

            return AnchorPoint.None;
        }

        public static Vector2[] Get8BorderVectors(this Rectangle r)
        {
            var mid = r.Center;
            // Start top left and work around clockwise.
            return new Vector2[8]
            {
                new Vector2(r.Left, r.Top),
                new Vector2(mid.X, r.Top),
                new Vector2(r.Right, r.Top),
                new Vector2(r.Right, mid.Y),
                new Vector2(r.Right, r.Bottom),
                new Vector2(mid.X, r.Bottom),
                new Vector2(r.Left, r.Bottom),
                new Vector2(r.Left, mid.Y)
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
