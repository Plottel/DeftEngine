using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class ByteBox : TextBox
    {
        private byte _value;

        public byte Value
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
            if (Text + text == "")
                Value = 0;
            else if (byte.TryParse(Text + text, out byte newValue))
                Value = newValue;
        }
    }
}
