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
    public class TextBox : Gadget
    {
        public const int INPUT_AREA_Y_PADDING = 3;

        protected Vector2 _labelSize;
        protected Rectangle _inputArea;
        private string _text;
        protected int _inputLineHeight;

        public TextBox()
        {
            SetSize(new Vector2(175, 30));
            _inputLineHeight = (int)Assets.GetFont("Arial10").MeasureString("00000").Y;
            Text = "";
        }

        public override string Label
        {
            get => _label;

            set
            {
                _label = value;
                _labelSize = Font.MeasureString(_label);
                SyncInputArea();
            }
        }

        protected string Text
        {
            get => _text;

            set
            {
                _text = value;
                Vector2 textSize = Assets.GetFont("GadgetFont10").MeasureString(_text);
                // TODO: Add text horizontal scrolling when text overflows.
            }
        }

        public override void MoveTo(Vector2 newPos)
        {
            base.MoveTo(newPos);
            SyncInputArea();
        }

        public override void OnParentMoveBy(Vector2 delta)
        {
            base.OnParentMoveBy(delta);
            SyncInputArea();
        }

        public override void SetSize(Vector2 newSize)
        {
            base.SetSize(newSize);
            SyncInputArea();
        }

        public override void Resize(Vector2 deltaSize)
        {
            base.Resize(deltaSize);
            SyncInputArea();
        }

        public override void OnParentResize(Vector2 deltaSize)
        {
            base.OnParentResize(deltaSize);
            SyncInputArea();
        }

        /// <summary>
        /// Works by clearing the text, adding what the opcode needs and then
        /// calling OnTextEntry with an entirely new string. Since the Text has been cleared,
        /// the OnTextEntry will work.
        /// </summary>
        /// <param name="textOpCode"></param>
        public virtual void ApplyTextOpCode(string textOpCode)
        {
            if (Text.Length == 0) return;

            string newText = Text;

            if (textOpCode == "BACKSPACE" && textOpCode.Length > 0)
                newText = Text.Remove(Text.Length - 1);
            else if (textOpCode == "DELETE")
                newText = "";

            Text = "";
            OnTextEntry(newText);
        }

        public override void OnTextEntry(string text)
        {
            base.OnTextEntry(text);
            Text += text;
        }

        public override void Display(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(_inputArea, Color.LightSlateGray);

            spriteBatch.DrawString(Font, _label, new Vector2(Pos.X + X_PADDING, Bounds.Center.Y - (_labelSize.Y / 2)), ColorScheme.GadgetText);
            spriteBatch.DrawString(Assets.GetFont("GadgetFont10"), 
                _text, 
                new Vector2(_inputArea.X + X_PADDING, _inputArea.Center.Y - (_inputLineHeight / 2)), 
                ColorScheme.InputBoxText);
        }

        public virtual void SyncInputArea()
        {
            _inputArea = new Rectangle(
                (int)(Pos.X + X_PADDING + _labelSize.X + X_PADDING),
                (int)Pos.Y + Y_PADDING,
                (int)(Size.X - X_PADDING - _labelSize.X - X_PADDING),
                (int)Size.Y - Y_PADDING * 2);
        }
    }
}
