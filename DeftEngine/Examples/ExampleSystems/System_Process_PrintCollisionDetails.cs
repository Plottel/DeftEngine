using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class System_Process_PrintCollisionDetails : ISystem, IProcessSystem
    {
        public void Process()
        {
            var pool = ECSCore.collisionPool;
            int numStart = pool.NumStartCollisions;
            int numCol = pool.NumCollidingCollisions;
            int numStop = pool.NumStopCollisions;

            if (numStart + numCol + numStop == 0)
                return;

            Console.WriteLine("Start : " + numStart + " - " + "Colliding : " + numCol + " - " + "Stop : " + numStop);
        }
    }
}
