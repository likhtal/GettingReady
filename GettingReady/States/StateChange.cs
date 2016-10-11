using System;
using System.Collections.Generic;
using System.Linq;

namespace GettingReady
{
    public class StateChange
    {
        public Change Op { get; private set; }
        public string State { get; private set; }

        public StateChange(Change op, string state)
        {
            Op = op;
            State = state;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is StateChange)) return false;
            var that = (StateChange)obj;
            return this.Op == that.Op && this.State == that.State;
        }

        public override int GetHashCode()
        {
            return (int)Op ^ State.GetHashCode();
        }
    }
}
