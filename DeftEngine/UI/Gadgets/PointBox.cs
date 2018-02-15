using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class PointBox : Gadget
    {
        public PointBox()
        {
            fontSize = 14;
            Add<IntBox>("X");
            Add<IntBox>("Y");
        }

        public Point Value
        {
            get => new Point(Get<IntBox>("X").Value, Get<IntBox>("Y").Value);
            set
            {
                Get<IntBox>("X").Value = value.X;
                Get<IntBox>("Y").Value = value.Y;
            }
        }
    }
}
