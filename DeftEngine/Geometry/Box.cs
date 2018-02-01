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
        private Vector2 _center;
        private Vector2 _size;
        private float _rotation;
        private Vector2[] _corners;

        private Vector2 _localXAxis;
        private Vector2 _localYAxis;

        public Vector2 LocalXAxis
        {
            get => _localXAxis;
        }

        public Vector2 LocalYAxis
        {
            get => _localYAxis;
        }

        public Box(Vector2 center) : this(center, Vector2.Zero, 0) {}
        public Box(Vector2 center, Vector2 size) : this(center, size, 0) {}

        public Box(Vector2 center, Vector2 size, float rotation)
        {
            _center = center;
            _size = size;
            _rotation = rotation;
            _corners = new Vector2[4];
            _localXAxis = Vector2.Zero;
            _localYAxis = Vector2.Zero;
            CalculateOBB();
        }

        public Vector2 Center
        {
            get => _center;
            set { _center = value; CalculateOBB(); }
        }

        public Vector2 Size
        {
            get => _size;
            set { _size = value; CalculateOBB(); }
        }

        /// <summary>
        /// The rotation of the box in degrees.
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            set { _rotation = value; CalculateOBB(); }
        }

        /// <summary>
        /// Given in order: BotRight, BotLeft, TopLeft, TopRight
        /// </summary>
        public Vector2[] Corners
        {
            get => _corners;
        }  

        public Vector2 ClosestPointTo(Vector2 v)
        {
            Vector2 displacement = v - _center;
            float xExtent = _size.X / 2;
            float yExtent = _size.Y / 2;

            Vector2 closestPt = _center;

            float xDist = Vector2.Dot(displacement, _localXAxis);

            // Clamp
            if (xDist > xExtent) xDist = xExtent;
            if (xDist < -xExtent) xDist = -xExtent;

            closestPt += xDist * _localXAxis;

            float yDist = Vector2.Dot(displacement, _localYAxis);

            // Clamp
            if (yDist > yExtent) yDist = yExtent;
            if (yDist < -yExtent) yDist = -yExtent;

            closestPt += yDist * _localYAxis;

            return closestPt;
        }
        
        private void CalculateOBB()
        {
            // Calculate corners
            float radianRot = MathHelper.ToRadians(_rotation);

            _corners[0] = _center + (_size / 2);                                // Bottom Right
            _corners[1] = _center + new Vector2(-_size.X / 2, _size.Y / 2);     // Bottom Left
            _corners[2] = _center - (_size / 2);                                // Top Left 
            _corners[3] = _center + new Vector2(_size.X / 2, -_size.Y / 2);     // Top Right

            for (int i = 0; i < 4; ++i)
                _corners[i] = _corners[i].Rotate(radianRot, _center);

            // Calculate local axes
            _localXAxis = new Vector2((float)Math.Cos(radianRot), (float)Math.Sin(radianRot));
            _localYAxis = _localXAxis.PerpendicularClockwise();
            
        }
    }
}
