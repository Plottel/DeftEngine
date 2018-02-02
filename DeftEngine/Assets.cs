using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace DeftEngine
{
    public static class Assets
    {
        private static Dictionary<string, Texture2D> _textures =
            new Dictionary<string, Texture2D>();

        public static ContentManager content;

        public static void LoadAssets()
        {
            // Recursively trace structure

            // foreach folder
            // Load Textures in that folder
            // fetch all folders
            // repeat until no more sub folders
            var allTextureNames = Directory.GetFiles("Content/Textures/", "*.xnb").Select(Path.GetFileNameWithoutExtension).ToList();
            foreach (var textureName in allTextureNames)
                _textures[textureName.ToLower()] = content.Load<Texture2D>("Textures/" + textureName);
        }

        public static bool HasTexture(string textureName) => _textures.ContainsKey(textureName.ToLower());

        public static Texture2D GetTexture(string textureName)
        {
            var lower = textureName.ToLower();

            if (_textures.ContainsKey(lower))
                return _textures[lower];

            throw new Exception("Texture with name: " + textureName + " not found.");
        }
    }
}
