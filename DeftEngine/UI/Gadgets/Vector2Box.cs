using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Vector2Box : Gadget
    {
        public Vector2Box()
        {
            fontSize = 14;
            Add<FloatBox>("X");
            Add<FloatBox>("Y");
        }

        public Vector2 Value
        {
            get => new Vector2(Get<FloatBox>("X").Value, Get<FloatBox>("Y").Value);
            set
            {
                Get<FloatBox>("X").Value = value.X;
                Get<FloatBox>("Y").Value = value.Y;
            }            
        }
    }
}
