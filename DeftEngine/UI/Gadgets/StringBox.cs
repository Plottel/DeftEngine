using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    /// <summary>
    /// Extends TextBox to provide a "Value" property.
    /// Needed for universal field gadget "Value" interface.
    /// </summary>
    public class StringBox : TextBox
    {
        public string Value
        {
            get => Text;
            set => Text = value;
        }
    }
}
