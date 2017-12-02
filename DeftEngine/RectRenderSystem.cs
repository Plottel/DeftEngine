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
    public class RectDisplaySystem : ISystem, IDisplaySystem, IQuerySystem
    {
        public bool Query(Entity e)
            => e.Has<RectDisplayComponent>() && e.Has<SpatialComponent>();

        public void Display(ECSData ecsData, SpriteBatch spriteBatch)
        {
            var entities = ecsData.pool.Query(this);

            foreach (var e in entities)
                spriteBatch.FillRectangle(e.Get<SpatialComponent>().Bounds, e.Get<RectDisplayComponent>().color);            
        }
    }
}
