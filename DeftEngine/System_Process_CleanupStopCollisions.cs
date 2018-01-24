using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Destroys all "Stop" collisions. Allows "Stop" collisions to persist for precisely one update.
    /// </summary>
    public class System_Process_CleanupStopCollisions : ISystem, IProcessSystem
    {
        public void Process()
        {
            foreach (var collisionMap in ECSCore.collisionPool.collisions.Values)
                collisionMap[CollisionState.Stop].Clear();
        }
    }
}
