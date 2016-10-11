using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReady.Interfaces
{
    public interface ICmd
    {
        int N { get; }
        StateChange ProposedChange { get; }
        void Do(Context context);
    }
}
