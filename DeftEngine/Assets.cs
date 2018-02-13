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
            LoadTexturesUnder("Textures");
            LoadFontsUnder("Fonts");
            System.Diagnostics.Debug.Assert(_fonts["gadgetfont12"] != null);
        }

        private static void LoadTexturesUnder(string path)
        {
            var allTextureNames = Directory.GetFiles("Content/" + path, "*.xnb").Select(Path.GetFileNameWithoutExtension).ToList();
            foreach (var textureName in allTextureNames)
                _textures[textureName.ToLower()] = content.Load<Texture2D>(path + "/" + textureName);

            foreach (string folder in Directory.GetDirectories("Content/" + path))
                LoadTexturesUnder(folder.Replace("Content/", ""));
        }

        private static void LoadFontsUnder(string path)
        {
            var allFontNames = Directory.GetFiles("Content/" + path, "*.xnb").Select(Path.GetFileNameWithoutExtension).ToList();
            foreach (var fontName in allFontNames)
                _fonts[fontName.ToLower()] = content.Load<SpriteFont>(path + "/" + fontName);

            foreach (string folder in Directory.GetDirectories("Content/" + path))
                LoadTexturesUnder(folder.Replace("Content/", ""));
        }

        public static bool HasTexture(string textureName) => _textures.ContainsKey(textureName.ToLower());
        public static Texture2D GetTexture(string textureName)
        {
            var lower = textureName.ToLower();
            return _textures.ContainsKey(lower) ? _textures[lower] : _textures["defaulttexture"];
        }

        public static bool HasFont(string fontName) => _fonts.ContainsKey(fontName.ToLower());
        public static SpriteFont GetFont(string fontName)
        {
            string lower = fontName.ToLower();
            return _fonts.ContainsKey(lower) ? _fonts[lower] : _fonts["gadgetfont12"];
        }
    }
}
