using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class RectangleBox : Gadget
    {
        public RectangleBox()
        {
            fontSize = 14;
            Add<IntBox>("X");
            Add<IntBox>("Y");
            Add<IntBox>("Width");
            Add<IntBox>("Height");
        }

        public Rectangle Value
        {
            get
            {
                Rectangle r = new Rectangle();
                r.X = Get<IntBox>("X").Value;
                r.Y = Get<IntBox>("Y").Value;
                r.Width = Get<IntBox>("Width").Value;
                r.Height = Get<IntBox>("Height").Value;
                return r;
            }

            set
            {
                Get<IntBox>("X").Value = value.X;
                Get<IntBox>("Y").Value = value.Y;
                Get<IntBox>("Width").Value = value.Width;
                Get<IntBox>("Height").Value = value.Height;
            }
        }
    }
}
