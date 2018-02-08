using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public class InputBox : Gadget
    {
        public const int INPUT_AREA_Y_PADDING = 3;

        private Vector2 _labelSize;
        private Rectangle _inputArea;
        private string _text;
        private int _inputLineHeight;

        public InputBox()
        {
            Size = new Vector2(175, 30);
            _inputLineHeight = (int)Assets.GetFont("Arial10").MeasureString("00000").Y;
            Text = "";
        }

        public override string Label
        {
            get => _label;

            set
            {
                _label = value;
                _labelSize = Assets.GetFont("Arial12").MeasureString(_label);
                SyncInputArea();
            }
        }

        public string Text
        {
            get => _text;

            set
            {
                _text = value;
                Vector2 textSize = Assets.GetFont("Arial10").MeasureString(_text);

                if (textSize.X > _inputArea.X)
                    _inputArea.X = (int)textSize.X + X_PADDING;
            }
        }

        public override void OnDrag()
        {
            base.OnDrag();
            SyncInputArea();
        }

        public override void OnResize()
        {
            base.OnResize();
            SyncInputArea();
        }

        public override void OnTextEntry(string text)
        {
            base.OnTextEntry(text);

            if (text == "BACKSPACE" && text.Length > 0)
                Text = Text.Remove(Text.Length - 1);
            else if (text == "DELETE")
                Text = "";
            else
                Text += text;
        }

        public override void Display(SpriteBatch spriteBatch)
        {
            base.Display(spriteBatch);
            spriteBatch.FillRectangle(_inputArea, Color.LightSlateGray);

            spriteBatch.DrawString(_label, new Vector2(Pos.X + X_PADDING, Bounds.Center.Y - (_labelSize.Y / 2)), Color.WhiteSmoke);
            spriteBatch.DrawString(Assets.GetFont("Arial10"), _text, new Vector2(_inputArea.X + X_PADDING, _inputArea.Center.Y - (_inputLineHeight / 2)), Color.Black);
        }

        private void SyncInputArea()
        {
            _inputArea = new Rectangle(
                (int)(Pos.X + X_PADDING + _labelSize.X + X_PADDING),
                (int)Pos.Y + Y_PADDING,
                (int)(Size.X - X_PADDING - _labelSize.X - X_PADDING),
                (int)Size.Y - Y_PADDING * 2);
        }
    }
}
