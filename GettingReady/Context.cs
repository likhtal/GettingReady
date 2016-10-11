using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady.Interfaces;

namespace GettingReady
{
    public class Context
    {
        public StateSet Iam { get; set; }
        public StateSet Outside { get; private set; }
        public Queue<ICmd> Cmds { get; private set; }

        public Context(StateSet iam, StateSet outside, Queue<ICmd> cmds)
        {
            Iam = iam;
            Outside = outside;
            Cmds = cmds;
        }
    }
}
