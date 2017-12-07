using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Teleport : ISystem, IProcessSystem
    {
        public void Process(ECSData ecsData)
        {
            // Useful for iterating over entities...
            var entities = ecsData.pool.GetEntities<Component_Spatial>();
            Component_Spatial s;

            // Useful when an Entity is off screen...
            var screenBounds = new Rectangle(0, 0, Input.MaxMouseX, Input.MaxMouseY);
            var screenMid = new Vector2(Input.MaxMouseX / 2, Input.MaxMouseY / 2);            

            // Check each entity to see if it's off screen.
            // If it is, submit a new Action_SetPosition to the screen centre.
            foreach (var e in entities)
            {
                s = e.Get<Component_Spatial>();

                if (!s.Bounds.Intersects(screenBounds))
                {
                    var setPosAction = new Action_SetPosition();
                    setPosAction.actor = e;
                    setPosAction.newPosition = screenMid;

                    ECSCore.actionPool.AddAction(setPosAction);
                }
            }
            
        }
    }
}
