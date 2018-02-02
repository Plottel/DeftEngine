using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public class System_Display_Texture : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Display_Texture>()
                .OrderBy(e => e.Pos.Y).ToList();

            foreach (var e in entities)
                spriteBatch.Draw(
                    Assets.GetTexture(e.Get<Component_Display_Texture>().TextureName),
                    e.Pos,
                    e.Rotation,
                    e.Size);

        }
    }
}
