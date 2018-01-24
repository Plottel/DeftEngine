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
            Component_Collision_Box box;

            foreach (var e in entities)
            {
                box = e.Get<Component_Collision_Box>();
                spriteBatch.DrawRectangle(e.pos + box.offset, box.size, Color.LawnGreen, 1);
            }
        }
    }
}
