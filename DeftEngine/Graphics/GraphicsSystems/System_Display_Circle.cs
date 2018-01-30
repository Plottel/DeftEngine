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
    public class System_Display_Circle : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Display_Circle>();
            float radius;

            foreach (var e in entities)
            {
                radius = e.Size.X / 2;
                spriteBatch.DrawCircle(e.MidVector, radius, 360, e.Get<Component_Display_Circle>().color, radius);
            }        
        }
    }
}
