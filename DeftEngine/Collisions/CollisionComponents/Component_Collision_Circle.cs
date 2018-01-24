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
    public class Component_Collision_Circle : IComponent, IColliderComponent
    {
        public Vector2 offset;
        public int radius;
    }
}
