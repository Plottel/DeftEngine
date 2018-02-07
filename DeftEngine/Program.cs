using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace DeftEngine
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new DeftEngine())
            {
                ECSCore.Setup();
                SetupProgram();
                ECSCore.Start();

                game.Run();
            }
        }

        /// <summary>
        /// Define your setup logic here.
        /// </summary>
        public static void SetupProgram()
        {
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

            Gadget g = new Gadget();
            g.isDraggable = true;
            g.isResizable = true;
            g.layer = 10;
            g.Pos = new Vector2(800, 200);
            g.Size = new Vector2(100, 50);

            DeftUI.AddGadget(g);
            
        }
    }
#endif
}
