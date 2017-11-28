using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class EntitySystemPool
    {
        public List<IEntitySystem> pool;
        public List<IDisplaySystem> displayPool;
        public List<IUpdateSystem> updatePool;
        public List<IEventSystem> eventPool;

        public EntitySystemPool()
        {
            pool = new List<IEntitySystem>();
            displayPool = new List<IDisplaySystem>();
            updatePool = new List<IUpdateSystem>();
            eventPool = new List<IEventSystem>();
        }

        public void SyncEntityQueriesWithPool()
        {
            foreach (var system in pool)
                ECSCore.pool.AddQuery(system.GetQuery());
        } 

        public void Add(IEntitySystem system)
        {
            pool.Add(system);

            var isDisplay = system as IDisplaySystem;
            var isUpdate = system as IUpdateSystem;
            var isEvent = system as IEventSystem;

            if (isDisplay != null)  displayPool.    Add(isDisplay);
            if (isUpdate != null)   updatePool.     Add(isUpdate);
            if (isEvent != null)    eventPool.      Add(isEvent);
        }

        public void Add<T>() where T : IEntitySystem
            => Add ((T)Activator.CreateInstance(typeof(T)));
    }
}
