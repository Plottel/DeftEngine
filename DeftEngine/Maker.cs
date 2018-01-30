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

        public static Entity Make(string name, Vector2 pos, Vector2 size)
        {
            Entity result;

            if (_prototypes.ContainsKey(name))
                result = Serializer.CopyEntity(_prototypes[name]);
            else
                result = new Entity();

            result.MoveTo(pos);
            result.Size = size;

            ECSCore.pool.AddEntity(result);
            return result;
        }
    }
}
