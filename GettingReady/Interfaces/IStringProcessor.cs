using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReady.Interfaces
{
    public interface IStringProcessor<TIn, TOut>
        where TOut : StateSet, new()
        where TIn : StateSet
    {
        string Run(TIn initialState, string commands);
    }
}
