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
    public class System_Display_Box : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Display_Box>();

            foreach (var e in entities)
                spriteBatch.FillRectangle(e.pos, new Size2(e.size.X, e.size.Y), e.Get<Component_Display_Box>().color);
        }
    }
}
