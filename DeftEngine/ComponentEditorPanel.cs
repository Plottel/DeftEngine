using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace DeftEngine
{
    public class ComponentEditorPanel : Gadget, IUpdateGadget
    {
        private IComponent _component;

        public void SetComponent(IComponent component)
        {
            _component = component;

            // Remove previous gadgets.
            foreach (var child in _children)
                DeftUI.Unsubscribe(child);
            _children.Clear();

            // Generate new gadgets.
            foreach (var field in GetComponentFields(component))
            {
                Type gadgetType = DeftEditor.GetGadgetType(field.FieldType);
                dynamic fieldGadget = Add(gadgetType, field.Name);

                var fieldValue = field.GetValue(_component);
                gadgetType.GetProperty("Value").SetValue(fieldGadget, fieldValue);
            }
        }

        public void Update()
        {
            if (_component == null) return;

            foreach (var field in GetComponentFields(_component))
            {
                dynamic fieldGadget = Get(field.Name);
                field.SetValue(_component, fieldGadget.Value);
            }
        }

        private FieldInfo[] GetComponentFields(IComponent c)
            => c.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
    }
}
