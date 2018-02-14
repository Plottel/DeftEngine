using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace DeftEngine
{
    public class System_Process_DebugHack : ISystem, IProcessSystem
    {
        public void Process()
        {
            if (Input.KeyDown(Keys.R))
            {
                var entities = ECSCore.pool.GetEntities<Component_Collision_Box>();

                foreach (var e in entities)
                    e.Rotation += 1;
            }

            if (Input.KeyTyped(Keys.Space))
            {
                if (DeftUI.focus != null)
                    DeftUI.focus.Add<RectangleBox>("Dicks");
            }
        }
    }
}
