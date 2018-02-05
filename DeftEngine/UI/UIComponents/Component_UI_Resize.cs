using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    // Start at top left and work around clockwise.
    public enum AnchorPoint
    {
        TopLeft = 0,
        Top = 1,
        TopRight = 2,
        Right = 3,
        BotRight = 4,
        Bot = 5,
        BotLeft = 6,
        Left = 7,
        None = 8
    }

    public class Component_UI_Resize : IComponent
    {
        public AnchorPoint anchorPoint;
    }
}
