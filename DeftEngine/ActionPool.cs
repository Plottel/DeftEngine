using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeftEngine
{
    public class ActionPool
    {
        private static List<Type> _allActionTypes;

        public Dictionary<Type, List<DeftAction>> actionBuffer = 
            new Dictionary<Type, List<DeftAction>>();

        public static void Init()
        {
            _allActionTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                              from assemblyType in domainAssembly.GetTypes()
                              where typeof(DeftAction).IsAssignableFrom(assemblyType)
                              select assemblyType).ToList();
        }

        public ActionPool()
        {
            foreach (var actionType in _allActionTypes)
            {
                var listType = typeof(List<>).MakeGenericType(actionType);
                actionBuffer[actionType] = new List<DeftAction>();
            }
        }

        public List<T> GetActions<T>() where T : DeftAction
        {
            var actionType = typeof(T);

            if (actionBuffer.ContainsKey(actionType))
                return actionBuffer[typeof(T)].Cast<T>().ToList();

            return new List<T>();
        }

        public void AddAction(DeftAction action)
        {
            var actionType = action.GetType();

            if (!actionBuffer.ContainsKey(actionType))
                actionBuffer[actionType] = new List<DeftAction>();

            actionBuffer[actionType].Add(action);
        }

        public void ClearActionBuffer()
        {
            foreach (var actionMap in actionBuffer)
                actionMap.Value.Clear();
        }
    }
}
