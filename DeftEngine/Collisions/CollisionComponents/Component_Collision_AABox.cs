using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Component_Collision_AABox : IComponent, IColliderComponent
    {
        public Vector2 offsetEntityPos;
        public Vector2 offsetSize;

        public Rectangle bounds;

        public void SetDefault(Entity e)
        {
            offsetEntityPos = Vector2.Zero;
            offsetSize = Vector2.Zero;
            bounds = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint());
        }

        public void Sync(Entity e)
        {
            bounds = new Rectangle(e.Pos.ToPoint() + offsetEntityPos.ToPoint(), 
                e.Size.ToPoint() + offsetSize.ToPoint());
        }
    }
}
