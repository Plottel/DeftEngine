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
    public class System_Display_DebugCollisionCircle : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Collision_Circle>();
            Component_Collision_Circle circle;

            foreach (var e in entities)
            {
                circle = e.Get<Component_Collision_Circle>();
                spriteBatch.DrawCircle(e.MidVector + circle.offsetEntityMid, circle.radius, 360, Color.LawnGreen, 1);
            }
        }
    }
}
