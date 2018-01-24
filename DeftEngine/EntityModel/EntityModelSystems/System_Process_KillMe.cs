﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Process_KillMe : ISystem, IProcessSystem
    {
        public void Process ()
        {
            var entities = ECSCore.pool.GetEntities<Component_KillMe>();

            for (int i = entities.Count - 1; i >= 0; --i)
                ECSCore.pool.RemoveEntity(entities[i]);
        }
    }
}
