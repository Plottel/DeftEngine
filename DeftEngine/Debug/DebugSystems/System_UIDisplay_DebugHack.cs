using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace DeftEngine
{
    public class System_UIDisplay_DebugHack : ISystem, IUIDisplaySystem
    {
        public void DisplayUI(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString("Gadget Count : " + DeftUI.Gadgets.Count, new Vector2(200, 50), Color.Black);
            //for (int i = 0; i < DeftUI.Gadgets.Count; ++i)
            //{
            //    var g = DeftUI.Gadgets[i];
            //    spriteBatch.DrawString(g.GetType() + " on Layer: " + g.Layer,
            //        new Vector2(800, 50 + (i * 20)), 
            //        Color.Black);
            //}
        }
    }
}
