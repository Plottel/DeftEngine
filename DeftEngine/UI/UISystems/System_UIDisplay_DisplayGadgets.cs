using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public class System_UIDisplay_DisplayGadgets : ISystem, IUIDisplaySystem
    {
        public void DisplayUI(SpriteBatch spriteBatch)
        {
            foreach (var g in DeftUI.BackToFrontGadgets)
                g.Display(spriteBatch);
        }
    }
}
