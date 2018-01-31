using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public static class PointExtensions
    {
        public static int Col(this Point pt)
        {
            return pt.X;
        }

        public static int Row(this Point pt)
        {
            return pt.Y;
        }
    }
}
