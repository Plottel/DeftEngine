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

        private static Dictionary<string, SpriteFont> _fonts =
            new Dictionary<string, SpriteFont>();

        public static ContentManager content;

        private static void ShowAllFoldersUnder(string path, int indent)
        {
            foreach (string folder in Directory.GetDirectories(path))
            {
                Console.WriteLine("{0}{1}", new string(' ', indent), Path.GetFileName(folder));
                ShowAllFoldersUnder(folder, indent + 2);
            }
        }

        public static void LoadAssets()
        {
            _fonts["arial12"] = content.Load<SpriteFont>("Arial12");
            return;
            // Recursively trace structure

            // foreach folder
            // Load Textures in that folder
            // fetch all folders
            // repeat until no more sub folders
            LoadAssetsUnder("Content/Textures/");


            var allTextureNames = Directory.GetFiles("Content/Textures/", "*.xnb").Select(Path.GetFileNameWithoutExtension).ToList();
            foreach (var textureName in allTextureNames)
                _textures[textureName.ToLower()] = content.Load<Texture2D>("Textures/" + textureName);
        }

        public static void LoadAssetsUnder(string path)
        {
            foreach (string folder in Directory.GetDirectories(path))
            {
                var allTextureNames = Directory.GetFiles(path, "*.xnb").Select(Path.GetFileNameWithoutExtension).ToList();

                foreach (var textureName in allTextureNames)
                    _textures[textureName.ToLower()] = content.Load<Texture2D>(path + textureName);

                LoadAssetsUnder(folder);
            }
        }

        public static bool HasTexture(string textureName) => _textures.ContainsKey(textureName.ToLower());

        public static Texture2D GetTexture(string textureName)
        {
            var lower = textureName.ToLower();

            if (_textures.ContainsKey(lower))
                return _textures[lower];

            throw new Exception("Texture with name: " + textureName + " not found.");
        }

        public static SpriteFont GetFont(string fontName)
        {
            string lower = fontName.ToLower();
            return _fonts.ContainsKey(lower) ? _fonts[lower] : _fonts["arial12"];
        }
    }
}
