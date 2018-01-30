using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public struct Box
    {
        public Vector2 center;
        public Vector2 size;
        public float rotation;

        public Vector2[] Corners
        {
            get
            {
                var result = new Vector2[4];
                float radianRot = MathHelper.ToRadians(rotation);

                //Fetch aligned corner points.
                result[0] = center + (size / 2);                            // Bottom Right
                result[1] = center + new Vector2(-size.X / 2, size.Y / 2);  // Bottom Left
                result[2] = center - (size / 2);                            // Top Left 
                result[3] = center + new Vector2(size.X / 2, -size.Y / 2);  // Top Right

                for (int i = 0; i < 4; ++i)
                    result[i] = RotateVector(result[i], rotation, center);

                return result;
            }
        }

        public Vector2 RotateVector(Vector2 v, float degreesRotation)
        {
            float radianRot = MathHelper.ToRadians(degreesRotation);
            Matrix2D rotationMatrix = Matrix2D.CreateRotationZ(radianRot);
            return Vector2.Transform(v, rotationMatrix);
        }

        public Vector2 RotateVector(Vector2 v, float degreesRotation, Vector2 origin)
        {
            return Vector2.Transform(v - origin, Matrix.CreateRotationZ(MathHelper.ToRadians(degreesRotation))) + origin;
        }
    }


}
