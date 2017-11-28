using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public static class ECSCore
    {
        public static EntityPool pool = new EntityPool();
        public static EntitySystemPool systemPool = new EntitySystemPool();
        public static SpriteBatch spriteBatch;

        public static void Start()
        {
            pool = new EntityPool();

            systemPool = new EntitySystemPool();

            systemPool.Add<SpawnEntityOnClickSystem>();
            systemPool.Add<RectDisplaySystem>();


            systemPool.SyncEntityQueriesWithPool();
        }

        public static void RunUpdateSystems(ECSData ecsData)
        {
            foreach (var system in systemPool.updatePool)
                system.Process(ecsData);
        }

        public static void RunEventSystems(ECSData ecsData)
        {
            foreach (var system in systemPool.eventPool)
                system.Process(ecsData);
        }

        public static void RunDisplaySystems(ECSData ecsData)
        {
            foreach (var system in systemPool.displayPool)
                system.Process(ecsData);
        }
    }
}
