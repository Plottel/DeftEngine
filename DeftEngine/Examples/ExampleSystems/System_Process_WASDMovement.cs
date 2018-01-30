using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeftEngine
{
    public class System_Process_WASDMovement : ISystem, IProcessSystem
    {
        public void Process()
        {
            var entities = ECSCore.pool.GetEntities<Component_WASDMovement>();
            float dx = 0;
            float dy = 0;

            if (Input.KeyDown(Keys.W)) dy -= 1;
            if (Input.KeyDown(Keys.S)) dy += 1;
            if (Input.KeyDown(Keys.A)) dx -= 1;
            if (Input.KeyDown(Keys.D)) dx += 1;

            Vector2 delta = new Vector2(dx, dy);

            foreach (var e in entities)
                e.MoveBy(delta);
        }
    }
}
