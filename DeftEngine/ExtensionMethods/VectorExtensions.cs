using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public static class VectorExtensions
    {
        public static Vector2 Rotate(this Vector2 v, float radians)
        {
            Matrix2D rotationMatrix = Matrix2D.CreateRotationZ(radians);
            return Vector2.Transform(v, rotationMatrix);
        }

        public static Vector2 Rotate(this Vector2 v, float radians, Vector2 origin)
        {
            return Vector2.Transform(v - origin, Matrix.CreateRotationZ(radians)) + origin;
        }

        public static Vector2 Add(this Vector2 vec, int other)
        {
            vec.X += other;
            vec.Y += other;
            return vec;
        }

        public static Vector2 AddX(this Vector2 vec, int other)
        {
            vec.X += other;
            return vec;
        }

        public static Vector2 AddY(this Vector2 vec, int other)
        {
            vec.Y += other;
            return vec;
        }
    }
}
