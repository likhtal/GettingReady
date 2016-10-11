using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReady.Interfaces
{
    public interface IRuleEngine
    {
        IRuleEngine AddRule(IRule rule);
        IRuleEngine ClearRules();
        bool IsAllowed(StateChange proposedChange, Context context);
    }
}
