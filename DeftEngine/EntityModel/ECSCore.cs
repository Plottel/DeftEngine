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
        public static EntityPool pool;
        public static SystemPool systemPool;
        public static EventPool eventPool;
        public static ActionPool actionPool;
        public static CollisionPool collisionPool;
        public static SpriteBatch spriteBatch;

        public static void Setup()
        {
            // Setup pools.
            EntityPool.Init();
            ActionPool.Init();

            pool = new EntityPool();
            systemPool = new SystemPool();
            eventPool = new EventPool();
            actionPool = new ActionPool();
            collisionPool = new CollisionPool();

            // Setup event handlers
            var allEventTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                  from assemblyType in domainAssembly.GetTypes()
                                  where typeof(DeftEvent).IsAssignableFrom(assemblyType)
                                  select assemblyType).ToList();

            foreach (var eventType in allEventTypes)
                eventPool.AddEvent(eventType);

            Collisions.Setup();

            // Setup systems.
            systemPool.Add<System_Process_TriggerInputEvents>();
            systemPool.Add<System_Action_SetVelocity>();

            systemPool.Add<System_Action_MoveTo>();

            systemPool.Add<System_Process_VelocityMovement>();
            systemPool.Add<System_Process_RegisterCollisions>();
            systemPool.Add<System_Process_CleanupStopCollisions>();
            systemPool.Add<System_Process_RegisterNewStopCollisions>();

            systemPool.Add<System_Process_KillMe>();

            systemPool.Add<System_Display_Box>();
            systemPool.Add<System_Display_Circle>();
        }

        public static void Start()
        {
            systemPool.SyncEntityQueriesWithPool();
        }

        public static void RunActionSystems()
        {
            foreach (var system in systemPool.actionSystems)
                system.ProcessActions();

            actionPool.ClearActionBuffer();
        }

        public static void RunProcessSystems()
        {
            foreach (var system in systemPool.processSystems)
                system.Process();
        }

        public static void RunCollisionSystems()
        {
            foreach (var system in systemPool.collisionSystems)
                system.HandleCollisions();
        }

        public static void RunDisplaySystems(SpriteBatch spriteBatch)
        {
            foreach (var system in systemPool.displaySystems)
                system.Display(spriteBatch);
        }
    }
}
