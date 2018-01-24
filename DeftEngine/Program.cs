using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            ECSCore.systemPool.Add<System_Process_PrintCollisionDetails>();



            var e = Maker.MakeBlank(new Vector2(200, 200), new Vector2(50, 50));
            e.Add<Component_Display_Box>();
            e.Get<Component_Display_Box>().color = Color.Blue;
            e.Add<Component_WASDMovement>();
            e.Add<Component_Collision_Box>();
            e.Get<Component_Collision_Box>().size = e.size;
            e.Get<Component_Collision_Box>().offset = Vector2.Zero;

            var e2 = Maker.MakeBlank(new Vector2(500, 200), new Vector2(50, 50));
            e2.Add<Component_Display_Circle>();
            e2.Get<Component_Display_Circle>().color = Color.Blue;
            e2.Add<Component_Collision_Circle>();
            e2.Get<Component_Collision_Circle>().radius = (int)e2.size.X / 2;
            e2.Get<Component_Collision_Circle>().offset = Vector2.Zero;
        }
    }
#endif
}
