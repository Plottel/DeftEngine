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
    public class Query_RectDisplayAndSpatialComponents : IEntityQuery
    {
        public bool Query(Entity e)
        {
            return e.Has<RectDisplayComponent>() && e.Has<SpatialComponent>();
        }
    }

    public class RectDisplaySystem : IEntitySystem, IDisplaySystem
    {
        public IEntityQuery GetQuery()
                => new Query_RectDisplayAndSpatialComponents();

        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.Query<Query_RectDisplayAndSpatialComponents>();

            var batch = ecsData.spriteBatch;
            foreach (var e in entities)
                batch.FillRectangle(e.Get<SpatialComponent>().Bounds, e.Get<RectDisplayComponent>().color);            
        }
    }
}
