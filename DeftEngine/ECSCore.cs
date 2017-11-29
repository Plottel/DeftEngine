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
        public static EntitySystemPool systemPool;
        public static EventPool eventPool;
        public static ActionPool actionPool;
        public static SpriteBatch spriteBatch;

        public static void Start()
        {
            // Setup pools.
            pool = new EntityPool();
            systemPool = new EntitySystemPool();
            eventPool = new EventPool();
            actionPool = new ActionPool();

            // Setup event handlers
            eventPool.AddEvent<Event_OnLeftMouseClick>();

            // Setup systems.
            systemPool.Add<InputEventTriggerSystem>();
            systemPool.Add<SetVelocityActionSystem>();
            systemPool.Add<ShootBulletActionSystem>();
            systemPool.Add<SpawnEntityOnClickSystem>();
            systemPool.Add<ShootBulletFighterSystem>();


            // Setup neural net systems
            systemPool.Add<Net_GetInputSystem>();
            systemPool.Add<Net_RunNetSystem>();
            systemPool.Add<Net_ProcessOutputSystem>();

            systemPool.Add<VelocitySystem>();
            systemPool.Add<CollisionSystem>();
            systemPool.Add<OffScreenCleanupSystem>();
            systemPool.Add<KillMeSystem>();
            systemPool.Add<RectDisplaySystem>();
            systemPool.SyncEntityQueriesWithPool();

            // Make two fighters
            var f1 = EntityFactory.Fighter(new Vector2(100, 100));
            var f2 = EntityFactory.Fighter(new Vector2(800, 800));
            var f3 = EntityFactory.Fighter(new Vector2(800, 800));
            var f4 = EntityFactory.Fighter(new Vector2(800, 800));
            var f5 = EntityFactory.Fighter(new Vector2(800, 800));

            f1.Get<NeuralNetComponent>().net.Randomise();
            f2.Get<NeuralNetComponent>().net.Randomise();
        }

        public static void RunActionSystems(ECSData ecsData)
        {
            foreach (var system in systemPool.actionSystems)
                system.Process(ecsData);
        }

        public static void RunUpdateSystems(ECSData ecsData)
        {
            foreach (var system in systemPool.updateSystems)
                system.Process(ecsData);
        }

        public static void RunDisplaySystems(ECSData ecsData)
        {
            foreach (var system in systemPool.displaySystems)
                system.Process(ecsData);

            Console.WriteLine("Ent Count" + pool.GetEntities().Count);
        }
    }
}
