using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DeftEngine
{
    public class ComponentEditorPanel : Gadget
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
                Add(gadgetType, field.Name);
            }
        }

        // Consider IUpdateGadget interface -> represents a gadget that needs to be updated every frame. Kinda IM but not really.
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
