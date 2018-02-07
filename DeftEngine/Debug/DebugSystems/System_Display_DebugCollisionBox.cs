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
    public class System_Display_DebugCollisionBox : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Collision_Box>();
            Box box;
            Color color;

            foreach (var e in entities)
            {
                box = e.Get <Component_Collision_Box>().box;
                color = ECSCore.collisionPool.HasCollision(e) ? Color.Red : Color.LawnGreen;

                spriteBatch.DrawBox(box, color);
                spriteBatch.DrawLine(box.Center, box.Center + box.LocalXAxis * (box.Size.X * 1.5f), ColorScheme.UISelect, 1);
                spriteBatch.DrawLine(box.Center, box.Center + box.LocalYAxis * (box.Size.Y * 1.5f), ColorScheme.UISelect, 1);
                spriteBatch.DrawPoint(box.ClosestPointTo(Input.MousePos), Color.Red, 5);
            }

        }
    }
}
