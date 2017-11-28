using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class EntityPool
    {
        private class Query_NoEntities : IEntityQuery
        {
            public bool Query(Entity e) => false;
        }

        private static List<Type> _allComponentTypes;
        public static IEntityQuery QUERY_NO_ENTITIES;

        private List<Entity> _pool;

        private Dictionary<Type, List<Entity>> _poolSortedByComponentType;

        private Dictionary<Type, List<Entity>> _poolQueryResults;

        public static void Init()
        {
            _allComponentTypes = (  from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                    from assemblyType in domainAssembly.GetTypes()
                                    where typeof(IComponent).IsAssignableFrom(assemblyType)
                                    select assemblyType).ToList();

            QUERY_NO_ENTITIES = new Query_NoEntities();            
        }

        public EntityPool()
        {
            _pool = new List<Entity>();
            _poolSortedByComponentType = new Dictionary<Type, List<Entity>>();

            foreach (Type componentType in _allComponentTypes)
                _poolSortedByComponentType[componentType] = new List<Entity>();

            _poolQueryResults = new Dictionary<Type, List<Entity>>();

        }

        /// <summary>
        /// Adds entity to pool and reruns all queries to sort entity into appropriate lists.
        /// </summary>
        /// <param name="e"></param>
        public void AddEntity(Entity e)
        {
            _pool.Add(e);

            foreach (Type componentType in e.ComponentTypes)
                _poolSortedByComponentType[componentType].Add(e);

            foreach (var queryData in _poolQueryResults)
            {
                var query = Activator.CreateInstance(queryData.Key) as IEntityQuery;

                if (query.Query(e))
                    queryData.Value.Add(e);
            }
        }

        /// <summary>
        /// Remove entity from pool and all query lists it was a part of
        /// </summary>
        /// <param name="e"></param>
        public void RemoveEntity(Entity e)
        {
            _pool.Remove(e);

            foreach (Type componentType in e.ComponentTypes)
                _poolSortedByComponentType[componentType].Remove(e);

            foreach (var queryResult in _poolQueryResults.Values)
                queryResult.Remove(e);

        }

        public List<Entity> GetEntities()
        {
            return _pool;
        }

        public List<Entity> GetEntities<T>() where T : IComponent
        {
            return _poolSortedByComponentType[typeof(T)];
        }

        public List<Entity> Query(IEntityQuery query)
        {
            var queryType = query.GetType();

            // If query is already cached, just return it.
            if (_poolQueryResults.ContainsKey(queryType))
                return _poolQueryResults[queryType];

            // If query hasn't been cached, add it.
            return AddQuery(query);
        }

        public List<Entity> Query<T>() where T : IEntityQuery
        {
            var queryType = typeof(T);

            // If query is already cached, just return it.
            if (_poolQueryResults.ContainsKey(queryType))
                return _poolQueryResults[queryType];

            // If query hasn't been cached, add it.
            return AddQuery((T)Activator.CreateInstance(queryType));
        }

        public List<Entity> GetEntities(Type componentType)
        {
            Debug.Assert(typeof(IComponent).IsAssignableFrom(componentType));
            return _poolSortedByComponentType[componentType];
        }

        /// <summary>
        /// Updates the query lists the entity is a part of.
        /// </summary>
        public void OnAddComponent(Entity e, IComponent c)
        {
            _poolSortedByComponentType[c.GetType()].Add(e);
            UpdateComponentBasedQueryResults(e);
            
        }        

        public void OnRemoveComponent(Entity e, IComponent c)
        {
            _poolSortedByComponentType[c.GetType()].Remove(e);
            UpdateComponentBasedQueryResults(e);
        }

        private void UpdateComponentBasedQueryResults(Entity e)
        {
            foreach (var queryData in _poolQueryResults)
            {
                var query = Activator.CreateInstance(queryData.Key) as IEntityQuery;
                var passesQuery = query.Query(e);
                var queryEntities = queryData.Value;

                if (queryEntities.Contains(e))
                {
                    if (!passesQuery) queryEntities.Remove(e);
                }
                else
                {
                    if (passesQuery) queryEntities.Add(e);
                }
            }
        }

        public void ClearQueries()
        {
            _poolQueryResults.Clear();
        }

        public List<Entity> AddQuery(IEntityQuery query)
        {
            var queryResult = new List<Entity>();

            // Add all entities which meet the requirements of the query.
            foreach (Entity e in _pool)
            {
                if (query.Query(e))
                    queryResult.Add(e);
            }

            // Store the query result for quick future access.
            _poolQueryResults[query.GetType()] = queryResult;
            return queryResult;
        }

        public void RemoveQuery<T>() where T : IEntityQuery
        {
            _poolQueryResults.Remove(typeof(T));
        }
    }
}
