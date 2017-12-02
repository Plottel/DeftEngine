using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class SystemPool
    {
        public List<ISystem> entitySystems;
        public List<IDisplaySystem> displaySystems;
        public List<IProcessSystem> updateSystems;
        public List<IEventSystem> eventSystems;
        public List<IActionSystem> actionSystems;
        public List<IQuerySystem> querySystems;
        public List<ISetupSystem> setupSystems;

        public SystemPool()
        {
            entitySystems = new List<ISystem>();
            displaySystems = new List<IDisplaySystem>();
            updateSystems = new List<IProcessSystem>();
            eventSystems = new List<IEventSystem>();
            actionSystems = new List<IActionSystem>();
            querySystems = new List<IQuerySystem>();
            setupSystems = new List<ISetupSystem>();
        }

        public void SyncEntityQueriesWithPool()
        {
            foreach (var system in querySystems)
                ECSCore.pool.AddQuery(system);
        } 

        public void Add(ISystem system)
        {
            entitySystems.Add(system);

            var isDisplay = system as IDisplaySystem;
            var isUpdate = system as IProcessSystem;
            var isEvent = system as IEventSystem;
            var isAction = system as IActionSystem;
            var isQuery = system as IQuerySystem;
            var isSetup = system as ISetupSystem;

            if (isDisplay != null)  displaySystems.    Add(isDisplay);
            if (isUpdate != null)   updateSystems.     Add(isUpdate);

            if (isEvent != null)
            {
                eventSystems.Add(isEvent);
                isEvent.SubscribeToEvents(ECSCore.eventPool);
            }

            if (isAction != null)
            {
                actionSystems.Add(isAction);
            }

            if (isQuery != null) querySystems.Add(isQuery);
            
            if (isSetup != null)
            {
                setupSystems.Add(isSetup);
                isSetup.Setup();
            }
        }

        public void Add<T>() where T : ISystem
            => Add ((T)Activator.CreateInstance(typeof(T)));
    }
}
