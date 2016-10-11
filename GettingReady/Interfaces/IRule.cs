using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReady.Interfaces
{
    public interface IRule
    {
        StateCondition PreConditions { get; }
        StateChange ProposedChange { get; }
    }
}
