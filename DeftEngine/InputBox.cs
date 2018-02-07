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
    public class InputBox : Gadget
    {
        public InputBox()
        {
            Size = new Vector2(75, 30);
        }

        public override void Display(SpriteBatch spriteBatch)
        {
            base.Display(spriteBatch);

            spriteBatch.DrawRectangle(Bounds, Color.Black, 3);
        }
    }
}
