using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class SpawnEntityOnClickSystem : IEntitySystem, IEventSystem, IUpdateSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_NO_ENTITIES;

        public void SubscribeToEvents()
        {
            ECSCore.eventPool.SubscribeTo<Event_OnLeftMouseClick>(this);
        }

        public void OnEvent(ECSData ecsData, params object[] args)
        {
            var newEntity = EntityFactory.Fighter(Input.MousePos);
            //ecsData.pool.AddEntity(newEntity);

            var setMyVel = new Action_SetVelocity();
            setMyVel.newVelocity = Vector2.Zero;
            setMyVel.actor = newEntity;
            ECSCore.actionPool.AddAction(setMyVel);
        }

        public void Process(ECSData ecsData)
        {
        }

        private Entity MakeDefaultEntity()
        {
            var newEntity = new Entity();
            var spatial = new SpatialComponent();
            var rectDisplay = new RectDisplayComponent();
            var velocity = new VelocityComponent();

            spatial.pos = Input.MousePos;
            spatial.size = new Vector2(50, 50);
            spatial.rotation = 0;

            rectDisplay.color = Color.Blue;

            //velocity.velocity = new Vector2(5, 5);

            // TODO: BIG BIG BIG COUPLING PROBLEM HERE
            // Every time I add a component... it triggers on the ECS core and the Entity Pool and reruns queries and such.
            // DeCouple Entities from ECSCore.
            // Really need something like entityPool.AddComponent(Entity e, IComponent c);
            newEntity.AddComponent(spatial);
            newEntity.AddComponent(velocity);
            newEntity.AddComponent(rectDisplay);

            return newEntity;
        }
    }
}
