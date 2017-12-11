using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    /// <summary>
    /// Represents an action which changes the velocity
    /// of an entity to a specified vector.
    /// </summary>
    public class Action_SetVelocity : DeftAction
    {
        public Vector2 newVelocity;
    }
}
