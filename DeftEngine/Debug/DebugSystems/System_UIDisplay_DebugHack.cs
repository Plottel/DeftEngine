using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace DeftEngine
{
    public class System_UIDisplay_DebugHack : ISystem, IUIDisplaySystem
    {
        private string _string = "";

        public void DisplayUI(SpriteBatch spriteBatch)
        {
            if (Input.InputString != "")
                _string += Input.PumpInputString();

            if (Input.KeyTyped(Microsoft.Xna.Framework.Input.Keys.Back))
                _string = _string.Remove(_string.Length - 1);

            spriteBatch.DrawString(Assets.GetFont("Arial12"), _string, new Vector2(100, 100), Color.Black);
        }
    }
}
