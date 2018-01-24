using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Process_VelocityMovement : ISystem, IProcessSystem
    {
        public void Process()
        {
            var entities = ECSCore.pool.GetEntities<Component_Velocity>();
            Component_Velocity velComp;

            foreach (var e in entities)
            {
                velComp = e.Get<Component_Velocity>();
                e.MoveBy(velComp.direction * velComp.speed);
            }                
        }
    }
}
