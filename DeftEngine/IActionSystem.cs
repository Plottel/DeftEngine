using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public interface IActionSystem
    {
        Type GetActionType();
        void EnqueueAction(DeftAction action);
        void OnAction(DeftAction action);
        void Process(ECSData ecsData);
    }
}
