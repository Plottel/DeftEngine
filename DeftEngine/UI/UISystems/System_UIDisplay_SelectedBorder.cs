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
    public class System_UIDisplay_SelectedBorder : ISystem, IUIDisplaySystem
    {
        public void DisplayUI(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_UI_Selected>();

            foreach (var e in entities)
                spriteBatch.DrawRectangle(e.Pos, e.Size.ToPoint(), Color.LawnGreen, 2);
        }
    }
}
