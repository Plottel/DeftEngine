using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class IntBox : TextBox
    {
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                Text = value.ToString();
            }
        }

        public override void OnTextEntry(string text)
        {
            if (int.TryParse(Text + text, out int newValue))
                Value = newValue;
        }
    }
}
