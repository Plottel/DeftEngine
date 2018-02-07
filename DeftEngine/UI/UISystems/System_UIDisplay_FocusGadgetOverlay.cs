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
    public class System_UIDisplay_FocusGadgetOverlay : ISystem, IUIDisplaySystem
    {
        public void DisplayUI(SpriteBatch spriteBatch)
        {
            var focus = DeftUI.focus;

            if (focus != null)
            {
                //if (focus.isDraggable)
                    spriteBatch.DrawRectangle(focus.Bounds, ColorScheme.UISelect, 1);

                if (focus.isResizable)
                {
                    foreach (var resizeBox in focus.Bounds.Get8ExternalBorderBoxes(7))
                        spriteBatch.FillRectangle(resizeBox, ColorScheme.UISelect);
                }
            }
        }
    }
}
