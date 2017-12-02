using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class SpawnEntityOnClickSystem : ISystem, IEventSystem, IProcessSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_NO_ENTITIES;

        public void SubscribeToEvents(EventPool pool)
        {
            pool.SubscribeTo<Event_OnLeftMouseClick>(this);
        }

        public void OnEvent(ECSData ecsData, DeftEvent theEvent, params object[] args)
        {
            var newEntity = EntityFactory.Default(Input.MousePos, new Vector2(50, 50), Color.Blue);
            //ecsData.pool.AddEntity(newEntity);

            var setMyVel = new Action_SetVelocity();
            setMyVel.newVelocity = new Vector2(1.5f, 1.5f);
            setMyVel.actor = newEntity;
            ECSCore.actionPool.AddAction(setMyVel);
        }

        public void Process(ECSData ecsData)
        {
        }        
    }
}
