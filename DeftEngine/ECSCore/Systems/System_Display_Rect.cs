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
    public class System_Display_Rect : ISystem, IDisplaySystem, IQuerySystem
    {
        public bool Query(Entity e)
            => e.Has<Component_RectDisplay>() && e.Has<Component_Spatial>();

        public void Display(ECSData ecsData, SpriteBatch spriteBatch)
        {
            var entities = ecsData.pool.Query(this);

            foreach (var e in entities)
                spriteBatch.FillRectangle(e.Get<Component_Spatial>().Bounds, e.Get<Component_RectDisplay>().color);            
        }
    }
}
