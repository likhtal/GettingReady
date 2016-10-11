using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady.Interfaces;

namespace GettingReady
{
    public class Cmd : ICmd
    {
        public int N { get; private set; }
        public StateChange ProposedChange { get; private set; }

        public Cmd(int n, StateChange proposedChange)
        {
            N = n;
            ProposedChange = proposedChange;
        }

        public void Do(Context context)
        {
            if (ProposedChange.Op == Change.Add)
            {
                context.Iam.Add(ProposedChange.State);
            }
            else
            {
                context.Iam.Remove(ProposedChange.State);
            }
        }
    }
}
