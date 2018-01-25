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
        public Vector2 offsetEntityMid;
        public int radius;

        public CircleF bounds;

        public void SetDefault(Entity e)
        {
            offsetEntityMid = Vector2.Zero;
            radius = (int)e.size.X / 2;

            bounds = new CircleF(e.MidPt, radius);
        }
    }
}
