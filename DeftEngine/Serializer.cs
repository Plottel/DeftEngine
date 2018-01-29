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
        // Use the obnoxious type checking function.
        // Do a LOT of stuff on program start up.
        // Loop through each component type, figure out:
        //          - Does it contain any complex types? If no, super simple serialization
        //          - If yes, store which ones
        //          - When copying, check if contains complex type or not. Different serialization function depending.

        // Add extra dictionary for in-built support of serialization of some types (Vector, Rectangle etc)


        // Copy Field()
        // Copy SimpleData()
        // Copy ComplexData()
        // If (IsSimpleType) SetField else CopyField() recursively call until down to simple dat types

        public static IComponent Copy(IComponent c)
        {
            var componentType = c.GetType();
            IComponent result = (IComponent)Activator.CreateInstance(c.GetType());

            var fields = componentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).ToList();

            foreach (var field in fields)
            {
                var fieldType = field.GetType();

                if (fieldType.IsSimpleType())
                    field.SetValue(result, field.GetValue(c));
                else
                {

                }


            }


            return result;
        }

        private static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] {
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}
