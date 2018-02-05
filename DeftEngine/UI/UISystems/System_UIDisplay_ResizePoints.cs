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
    public class System_UIDisplay_ResizePoints : ISystem, IQuerySystem, IUIDisplaySystem
    {
        public bool Query(Entity e)
            => e.Has<Component_UI_Resizable>() && e.Has<Component_UI_Selected>();

        public void DisplayUI(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.Query(this);
            Rectangle border;

            foreach (var e in entities)
            {
                border = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint());

                foreach (var resizeRect in border.Get8ExternalBorderBoxes(7))
                    spriteBatch.FillRectangle(resizeRect, Color.LawnGreen);
            }
        }
    }
}
