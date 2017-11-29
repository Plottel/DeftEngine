using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class EntitySystemPool
    {
        public List<IEntitySystem> entitySystems;
        public List<IDisplaySystem> displaySystems;
        public List<IUpdateSystem> updateSystems;
        public List<IEventSystem> eventSystems;
        public List<IActionSystem> actionSystems;

        public EntitySystemPool()
        {
            entitySystems = new List<IEntitySystem>();
            displaySystems = new List<IDisplaySystem>();
            updateSystems = new List<IUpdateSystem>();
            eventSystems = new List<IEventSystem>();
            actionSystems = new List<IActionSystem>();
        }

        public void SyncEntityQueriesWithPool()
        {
            foreach (var system in entitySystems)
                ECSCore.pool.AddQuery(system.GetQuery());
        } 

        public void Add(IEntitySystem system)
        {
            entitySystems.Add(system);

            var isDisplay = system as IDisplaySystem;
            var isUpdate = system as IUpdateSystem;
            var isEvent = system as IEventSystem;
            var isAction = system as IActionSystem;

            if (isDisplay != null)  displaySystems.    Add(isDisplay);
            if (isUpdate != null)   updateSystems.     Add(isUpdate);

            if (isEvent != null)
            {
                eventSystems.Add(isEvent);
                isEvent.SubscribeToEvents();
            }

            if (isAction != null)
            {
                actionSystems.Add(isAction);
                ECSCore.actionPool.SubscribeTo(isAction.GetActionType(), isAction);
            }
        }

        public void Add<T>() where T : IEntitySystem
            => Add ((T)Activator.CreateInstance(typeof(T)));
    }
}
