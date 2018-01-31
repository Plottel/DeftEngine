using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public static class DeftDebug
    {
        public static void ActivateDebugColliders()
        {
            ECSCore.systemPool.Add<System_Display_DebugCollisionAABox>();
            ECSCore.systemPool.Add<System_Display_DebugCollisionCircle>();
            ECSCore.systemPool.Add<System_Display_DebugCollisionBox>();
        }
    }
}
