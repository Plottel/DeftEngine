using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    // Event listeners are automatically set up for any class
    // inheriting from Deft Event.
    public class Event_OnRightMouseClick : DeftEvent { }
    public class Event_OnRightMousePress : DeftEvent { }
    public class Event_OnLeftMouseClick : DeftEvent { }
    public class Event_OnLeftMousePress : DeftEvent { }

    public class Event_OnMouseMove : DeftEvent { }
    public class Event_OnLeftMouseDrag : DeftEvent { }
    public class Event_OnRightMouseDrag : DeftEvent { }
}
