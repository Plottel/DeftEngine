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
    public class DropDownList : TextBox
    {
        private bool _isExpanded = false;
        private int _originalLayer;
        private Rectangle _optionsInputArea = new Rectangle();
        private string _value;

        private List<string> _options = new List<string>();

        public List<string> Options
        {
            get => _options;
            set
            {
                _options = value;
                Value = _options[0];

                // TODO: Add resizing based on max string length.
            }
        }


        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                Text = value;
            }
        }

        public override void OnClick()
        {
            if (!_isExpanded)
            {
                if (_inputArea.Contains(Input.MousePos))
                {
                    _isExpanded = true;
                    _originalLayer = Layer;
                    Layer = DeftUI.TopLayer + 1;
                    SyncInputArea();
                }
            }
            else if (_optionsInputArea.Contains(Input.MousePos))
            {
                float heightIntoOptionsArea = Input.MousePos.Y - _optionsInputArea.Y;
                int optionChosen = (int)Math.Floor(heightIntoOptionsArea / _inputArea.Height);

                Value = Options[optionChosen];


                _isExpanded = false;
                Layer = _originalLayer;
                SyncInputArea();
            }
        }

        public override void Display(SpriteBatch spriteBatch)
        {
            if (_isExpanded)
            {
                spriteBatch.DrawString(Font, _label, new Vector2(Pos.X + X_PADDING, Bounds.Center.Y - (_labelSize.Y / 2)), ColorScheme.GadgetText);
                DisplayOptions(spriteBatch);
            }
            else
                base.Display(spriteBatch);
        }

        private void DisplayOptions(SpriteBatch spriteBatch)
        {
            Rectangle optionArea;

            for (int i = 0; i < Options.Count; ++i)
            {
                optionArea = _inputArea;
                optionArea.Y = _inputArea.Y + (_inputArea.Height * i);

                spriteBatch.FillRectangle(optionArea, Color.LightSlateGray);
                spriteBatch.DrawString(Assets.GetFont("GadgetFont10"), 
                    Options[i], 
                    new Vector2(optionArea.X + X_PADDING, optionArea.Center.Y - (_inputLineHeight / 2)), 
                    ColorScheme.InputBoxText);
            }
        }

        public override void SyncInputArea()
        {
            base.SyncInputArea();
            _optionsInputArea = _inputArea;

            if (_isExpanded)
                _optionsInputArea.Height = _inputArea.Height * Options.Count;
        }
















        public override void ApplyTextOpCode(string textOpCode)
        {
            // Override with blank method to avoid normal TextBox entry.
        }

        public override void OnTextEntry(string text)
        {
            // Override with blank method to avoid normal TextBox entry.
        }


    }
}
