using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Component_Spatial : IComponent
    {
        public Vector2 pos;
        public Vector2 size;
        public float rotation;

        public Rectangle Bounds
        {
            get { return new Rectangle(pos.ToPoint(), size.ToPoint()); }
        }

        public Point MidPt
        {
            get { return new Point((int)pos.X + (int)size.X / 2, (int)pos.Y + (int)size.Y / 2); }
        }

        public Vector2 MidVector
        {
            get { return new Vector2(pos.X + size.X / 2, pos.Y + size.Y / 2); }
        }
    }
}
