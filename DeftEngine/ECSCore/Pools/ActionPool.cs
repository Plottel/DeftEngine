using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    /// <summary>
    /// The Action Pool stores all Actions. Methods are provided for adding, removing and accessing actions.
    /// Actions are processed and cleared at the end of each update. Therefore, an Action will only ever persist for one update.
    /// </summary>
    public class ActionPool
    {
        /// <summary>
        /// Used to initialize actionBuffer when an Action Pool object is created.
        /// </summary>
        private static List<Type> _allActionTypes;

        /// <summary>
        /// Action storage. Cleared once per update.
        /// </summary>
        private Dictionary<Type, List<DeftAction>> actionBuffer = 
            new Dictionary<Type, List<DeftAction>>();

        /// <summary>
        /// Populates the static list of all Action types. 
        /// </summary>
        public static void Init()
        {
            _allActionTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                              from assemblyType in domainAssembly.GetTypes()
                              where typeof(DeftAction).IsAssignableFrom(assemblyType)
                              select assemblyType).ToList();
        }

        /// <summary>
        /// Initializes actionBuffer according to static _allActionTypes list.
        /// </summary>
        public ActionPool()
        {
            foreach (var actionType in _allActionTypes)
                actionBuffer[actionType] = new List<DeftAction>();
        }

        /// <summary>
        /// Fetches all actions in the pool of the specified type. 
        /// Use this method to fetch Actions inside an IActionSystem.
        /// </summary>
        public List<T> GetActions<T>() where T : DeftAction
        {
            var actionType = typeof(T);

            if (actionBuffer.ContainsKey(actionType))
                return actionBuffer[actionType].Cast<T>().ToList();

            return new List<T>();
        }

        /// <summary>
        /// Fetches all actions in the pool of the specified type.
        /// Use this method to fetch Actions inside an IActionSystem.
        /// </summary>
        public List<DeftAction> GetActions(Type actionType)
        {
            if (actionBuffer.ContainsKey(actionType))
                return actionBuffer[actionType];

            return new List<DeftAction>();
        }

        /// <summary>
        /// Adds the passed in Action to the actionBuffer list matching its type.
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(DeftAction action)
        {
            Debug.Assert(actionBuffer.ContainsKey(action.GetType()));
            actionBuffer[action.GetType()].Add(action);
        }

        /// <summary>
        /// Clears all Actions. Called at the end of each update.
        /// </summary>
        public void ClearActionBuffer()
        {
            foreach (var actionMap in actionBuffer)
                actionMap.Value.Clear();
        }
    }
}
