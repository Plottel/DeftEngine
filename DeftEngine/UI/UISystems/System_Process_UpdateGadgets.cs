using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Process_UpdateGadgets : ISystem, IProcessSystem
    {
        public void Process()
        {
            foreach (var gadget in DeftUI.UpdateGadgets)
                gadget.Update();
        }
    }
}
