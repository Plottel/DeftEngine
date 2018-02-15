using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class ColorBox : Gadget
    {
        public ColorBox()
        {
            fontSize = 14;

            Add<ByteBox>("R");
            Add<ByteBox>("G");
            Add<ByteBox>("B");
            Add<ByteBox>("A");
        }

        public Color Value
        {
            get
            {
                Color c = new Color();

                c.R = Get<ByteBox>("R").Value;
                c.G = Get<ByteBox>("G").Value;
                c.B = Get<ByteBox>("B").Value;
                c.A = Get<ByteBox>("A").Value;

                return c;
            }

            set
            {
                Get<ByteBox>("R").Value = value.R;
                Get<ByteBox>("G").Value = value.G;
                Get<ByteBox>("B").Value = value.B;
                Get<ByteBox>("A").Value = value.A;
            }
        }
    }
}
