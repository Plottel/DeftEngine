using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public interface IEntitySystem
    {
        IEntityQuery GetQuery();
        void Process(ECSData ecsData);
    }
}
