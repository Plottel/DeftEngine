using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    /// <summary>
    /// A simple wrapper around an XNA Vector2 which ensures the vector stays normalized.
    /// Useful when a vector is used purely to simulate direction.
    /// Use case: Vector2 velocity = direction.V * speed;
    /// </summary>
    public struct NormalV2
    {
        private Vector2 _vector;

        public NormalV2(Vector2 vector)
            => _vector = Vector2.Normalize(vector);

        public Vector2 V
        {
            get => _vector;
            set => _vector = Vector2.Normalize(value);
        }
    }
}
