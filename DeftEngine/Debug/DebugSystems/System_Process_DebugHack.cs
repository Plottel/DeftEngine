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

            var ent = ECSCore.pool.GetEntities<Component_Display_Box>()[0];
            var cPanel = DeftUI.Get<ComponentEditorPanel>("Component Editor");

            if (Input.KeyDown(Keys.Space))
            {
                ent.Get<Component_Display_Box>().color.R += 1;
                cPanel.SetComponent(ent.Get<Component_Display_Box>());
            }
        }
    }
}
