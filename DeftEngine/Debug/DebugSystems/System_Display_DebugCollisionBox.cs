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
            var entities = ECSCore.pool.GetEntities<Component_Collision_AABox>();
            Component_Collision_AABox box;

            foreach (var e in entities)
            {
                box = e.Get<Component_Collision_AABox>();
                spriteBatch.DrawRectangle(box.bounds, Color.LawnGreen, 1);
            }
        }
    }
}
