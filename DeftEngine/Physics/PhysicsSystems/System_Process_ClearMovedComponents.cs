using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Process_ClearMovedComponents : ISystem, IProcessSystem
    {
        public void Process()
        {
            var entities = ECSCore.pool.GetEntities<Component_Moved>();

            for (int i = entities.Count - 1; i >= 0; --i)
                entities[i].Remove <Component_Moved>();
        }
    }
}
