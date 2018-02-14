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
            if (StringIsAllDigits(text))
            {
                string oldText = Text;
                Text += text;

                if (StringEndIsDot(oldText) || (StringHasExactlyOneDot(Text) && StringEndIsZero(text)))
                {
                    // Don't parse - it's ".0" and changing Value will remove it back to pre-decimal point.
                }
                else
                {
                    if (float.TryParse(Text, out float newValue))
                        Value = newValue;
                }
            }
            else if (StringHasExactlyOneDot(Text + text))   // Check NEW string will only have one dot.
            {
                if (StringHasCharsOtherThanDotOrZero(text))
                    return;

                Text += text;

                if (StringEndIsNotDotOrZero(Text))
                {
                    if (float.TryParse(Text, out float newValue))
                        Value = newValue;
                }
            }
        }

        private bool StringHasExactlyOneDot(string text)
            => text.Count(c => c == '.') == 1;

        private bool StringIsAllDigits(string text)
            => text.All(c => c >= '0' && c <= '9');

        private bool StringEndIsNotDotOrZero(string text)
        {
            if (text.Length == 0) return true;
            char c = text[text.Length - 1];
            return c != '.' && c != '0';
        }

        private bool StringEndIsDot(string text)
            => text.Length != 0 && text[text.Length - 1] == '.';

        private bool StringEndIsZero(string text)
            => text.Length != 0 && text[text.Length - 1] == '0';

        private bool StringHasCharsOtherThanDotOrZero(string text)
        {
            foreach (char c in text)
            {
                if (c != '.' && c != '0')
                    return true;
            }
            return false;
        }
    }
}
