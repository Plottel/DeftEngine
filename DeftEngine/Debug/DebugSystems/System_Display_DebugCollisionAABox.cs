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
    public class System_Display_DebugCollisionAABox : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Collision_AABox>();
            Component_Collision_AABox box;
            Color color;

            foreach (var e in entities)
            {
                box = e.Get<Component_Collision_AABox>();
                color = ECSCore.collisionPool.HasCollision(e) ? Color.Red : Color.LawnGreen;

                spriteBatch.DrawRectangle(box.bounds, color, 1);
            }
        }
    }
}
