using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Component_Collision_Box : IComponent, IColliderComponent
    {
        public Vector2 offset;
        public Vector2 size;
    }
}
