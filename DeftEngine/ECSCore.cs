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
        public static SpriteBatch spriteBatch;

        public static void Start()
        {
            // Setup pools.
            pool = new EntityPool();
            systemPool = new SystemPool();
            eventPool = new EventPool();
            actionPool = new ActionPool();

            // Setup event handlers
            var allEventTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                  from assemblyType in domainAssembly.GetTypes()
                                  where typeof(DeftEvent).IsAssignableFrom(assemblyType)
                                  select assemblyType).ToList();

            foreach (var eventType in allEventTypes)
                eventPool.AddEvent(eventType);

            systemPool.Add<TestInputEventsSystem>();

            systemPool.Add<System_Teleport>();
            systemPool.Add<System_Regen>();

            // Setup systems.
            systemPool.Add<InputEventTriggerSystem>();
            systemPool.Add<SetVelocityActionSystem>();

            systemPool.Add<System_Action_SetPosition>();

            systemPool.Add<SpawnEntityOnClickSystem>();
            systemPool.Add<RemoveEntityOnRightClickSystem>();

            systemPool.Add<System_Velocity>();
            systemPool.Add<OffScreenCleanupSystem>();
            systemPool.Add<KillMeSystem>();
            systemPool.Add<RectDisplaySystem>();
            systemPool.SyncEntityQueriesWithPool();
        }

        public static void RunActionSystems(ECSData ecsData)
        {
            foreach (var system in systemPool.actionSystems)
                system.ProcessActions();

            actionPool.ClearActionBuffer();
        }

        public static void RunUpdateSystems(ECSData ecsData)
        {
            foreach (var system in systemPool.updateSystems)
                system.Process(ecsData);
        }

        public static void RunDisplaySystems(ECSData ecsData, SpriteBatch spriteBatch)
        {
            foreach (var system in systemPool.displaySystems)
                system.Display(ecsData, spriteBatch);
        }
    }
}
