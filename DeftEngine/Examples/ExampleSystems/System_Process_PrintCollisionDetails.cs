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
            int eNum = 1;

            foreach (var collisionMap in ECSCore.collisionPool.collisions.Values)
            {
                var start = collisionMap[CollisionState.Start];
                var colliding = collisionMap[CollisionState.Colliding];
                var stop = collisionMap[CollisionState.Stop];

                if (start.Count != 0)
                {
                    int x = 5;
                }

                Console.WriteLine("");
                Console.WriteLine("##########################");
                Console.WriteLine("Collision Details for Entity : " + eNum);
                Console.WriteLine(start.Count + " - Start Collisions");
                Console.WriteLine(colliding.Count + " - Colliding Collisions");
                Console.WriteLine(stop.Count + " - Stop Collisions");
                Console.WriteLine("###########################");

                ++eNum;
            }
        }
    }
}
