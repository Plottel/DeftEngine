using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace DeftEngine
{
    public class OffScreenCleanupSystem : ISystem, IProcessSystem
    {
        public void Process(ECSData ecsData)
        {
            var entities = ecsData.pool.GetEntities<SpatialComponent>();

            var screenBounds = new Rectangle(0, 0, Input.MaxMouseX, Input.MaxMouseY);

            foreach (var e in entities)
            {
                if (!screenBounds.Intersects(e.Get<SpatialComponent>().Bounds))
                    e.AddComponent<KillMeComponent>();
            }
        }
    }
}
