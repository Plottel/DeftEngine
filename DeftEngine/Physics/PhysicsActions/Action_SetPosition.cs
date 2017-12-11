using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    /// <summary>
    /// An action to set the position of an Entity.
    /// When processed, this action overrides SpatialComponent.Pos
    /// </summary>
    public class Action_SetPosition : DeftAction
    {
        public Vector2 newPosition;
    }
}
