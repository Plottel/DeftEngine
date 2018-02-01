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
        public Vector2 _offsetEntityPos;
        public Vector2 _offsetSize;

        public Vector2 OffsetEntityPos
        {
            get => _offsetEntityPos;

            set
            {
                Vector2 delta = value - _offsetEntityPos;
                _bounds.Location += delta.ToPoint();
                _offsetEntityPos = value;
            }
        }

        public Vector2 OffsetSize
        {
            get => _offsetSize;

            set
            {
                Vector2 delta = value - _offsetSize;
                _bounds.Size += delta.ToPoint();
                _offsetSize = value;
            }
        }

        public Rectangle Bounds
        {
            get => _bounds;
        }


        public Rectangle _bounds;

        public void SetDefault(Entity e)
        {
            _offsetEntityPos = Vector2.Zero;
            _offsetSize = Vector2.Zero;
            _bounds = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint());
        }

        public void Sync(Entity e)
        {
            _bounds = new Rectangle(e.Pos.ToPoint() + _offsetEntityPos.ToPoint(), 
                e.Size.ToPoint() + _offsetSize.ToPoint());
        }
    }
}
