using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public class Component_Display_Texture : IComponent
    {
        private string _textureName;

        public string TextureName
        {
            get => _textureName;

            set
            {
                if (Assets.HasTexture(value))
                    _textureName = value;
            }
        }

        public Component_Display_Texture()
        {
            // Set _textureName to some default texture value.
        }
    }
}
