using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class FloatBox : TextBox
    {
        private float _value;

        public float Value
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
        }
    }
}
