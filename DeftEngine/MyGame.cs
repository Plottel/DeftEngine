using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace DeftEngine
{
    public class MyGame : DeftGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            ECSCore.systemPool.Add<System_Process_WASDMovement>();
            ECSCore.systemPool.Add<System_Process_DebugHack>();
            ECSCore.systemPool.Add<System_UIDisplay_DebugHack>();

            DeftDebug.ActivateDebugColliders();

            var e = Maker.Make("Blank", new Vector2(200, 200), new Vector2(50, 50));
            e.Add<Component_Display_Box>();
            e.Get<Component_Display_Box>().color = Color.Blue;
            e.Add<Component_Collision_AABox>();
            //e.Add<Component_WASDMovement>();

            var e2 = Maker.Make("Blank", new Vector2(350, 200), new Vector2(50, 50));
            e2.Add<Component_Display_Circle>();
            e2.Get<Component_Display_Circle>().color = Color.Blue;
            e2.Add<Component_Collision_Circle>();
            //e2.Add<Component_WASDMovement>();

            var e3 = Maker.Make("Blank", new Vector2(500, 500), new Vector2(50, 75));
            e3.Add<Component_Collision_Box>();
            e3.Add<Component_WASDMovement>();

            var e4 = Maker.Make("Blank", new Vector2(280, 180), new Vector2(84, 25));
            e4.Add<Component_Collision_Box>();

            ComponentEditorPanel cPanel = new ComponentEditorPanel();
            cPanel.Label = "Component Editor";
            cPanel.fontSize = 16;
            cPanel.MoveTo(new Vector2(5, 5));
            cPanel.SetComponent(e.Get<Component_Display_Box>());

            DeftUI.Subscribe(cPanel);

            DeftUI.Subscribe(new DropDownList { Label = "Dicks",
                Options = new List<String> { "ASDS", "ASDSAD", "ASDSADASDSAD" } });
        }
    }
}
