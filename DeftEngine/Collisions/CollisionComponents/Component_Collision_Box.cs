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
        public Vector2 offsetEntityPos;
        public Vector2 size;

        public Rectangle bounds;

        public void SetDefault(Entity e)
        {
            offsetEntityPos = Vector2.Zero;
            size = e.size;
            bounds = new Rectangle(e.pos.ToPoint(), e.size.ToPoint());
        }
    }
}
