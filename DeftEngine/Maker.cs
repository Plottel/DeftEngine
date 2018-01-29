using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public static class Maker
    {
        private static Dictionary<string, Entity> _prototypes =
            new Dictionary<string, Entity>();

        public static void AddPrototype(string name, Entity prototype)
            => _prototypes[name] = prototype;

        public static Entity Make(string name)
        {
            if (_prototypes.ContainsKey(name))
            {
                Entity result = Serializer.CopyEntity(_prototypes[name]);
                ECSCore.pool.AddEntity(result);
                return result;
            }
            return null;
        }
    }
}
