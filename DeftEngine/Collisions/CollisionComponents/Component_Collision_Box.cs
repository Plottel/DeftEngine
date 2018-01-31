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
        public Vector2 offsetEntityMid;
        public Vector2 offsetSize;

        public Box box;

        public void SetDefault(Entity e)
        {
            offsetEntityMid = Vector2.Zero;
            offsetSize = Vector2.Zero;
            box = new Box(e.MidVector, e.Size, e.Rotation);
        }

        public void Sync(Entity e)
        {
            box = new Box(e.MidVector + offsetEntityMid,
                e.Size + offsetSize,
                e.Rotation);
        }
    }
}
