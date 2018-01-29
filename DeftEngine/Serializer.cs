using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DeftEngine
{
    public static class Serializer
    {
        private static Type[] _simpleTypes;

        static Serializer()
        {
            _simpleTypes = new Type[] {
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                };
        }

        public static Entity CopyEntity(Entity e)
        {
            Entity result = new Entity();
            result.pos = e.pos;
            result.size = e.size;
            result.rotation = e.rotation;

            foreach (var component in e.ComponentList)
                result.Add(CopyComponent(component));

            return result;
        }

        public static IComponent CopyComponent(IComponent c)
            => CopyObject(c) as IComponent;

        public static T CopyComponent<T>(T component) where T : IComponent
            => (T)CopyObject(component);

        public static object CopyObject(object o)
        {
            var type = o.GetType();
            var result = Activator.CreateInstance(type);

            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var fieldType = field.FieldType;

                if (fieldType.IsSimpleType())
                    field.SetValue(result, field.GetValue(o));
                else
                    field.SetValue(result, CopyObject(field.GetValue(o))); // Recursively call CopyObject until reach SimpleType.
            }

            return result;
        }

        private static bool IsSimpleType(this Type type)
        {
            return
                type.IsPrimitive ||
                _simpleTypes.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}
