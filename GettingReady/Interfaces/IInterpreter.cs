using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GettingReady.Interfaces
{
    public interface IInterpreter
    {
        string Interpret(StateChange change, Context context);
        string InterpretFailure(Context context);
    }
}
