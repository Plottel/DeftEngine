using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public static class DeftEditor
    {
        private static Dictionary<Type, Type> _gadgetTypeMap =
            new Dictionary<Type, Type>();

        public static void AssociateGadget<GadgetType, DataType>()
            => _gadgetTypeMap[typeof(DataType)] = typeof(GadgetType);

        public static Type GetGadgetType(Type fieldType)
        {
            if (!_gadgetTypeMap.ContainsKey(fieldType))
                throw new Exception("Field Type: " + fieldType.ToString() + " not supported by DeftEditor. Please create your own Gadget for this Type");

            return _gadgetTypeMap[fieldType];
        }

        static DeftEditor()
        {
            AssociateGadget<IntBox, int>();
            AssociateGadget<FloatBox, float>();
            AssociateGadget<StringBox, string>();
            AssociateGadget<ByteBox, byte>();
            AssociateGadget<Vector2Box, Vector2>();
            AssociateGadget<PointBox, Point>();
            AssociateGadget<RectangleBox, Rectangle>();
            AssociateGadget<ColorBox, Color>();
        }
    }
}
