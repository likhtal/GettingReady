using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady.Interfaces;

namespace GettingReady
{
    public class Rule : IRule
    {
        public StateCondition PreConditions { get; private set; }
        public StateChange ProposedChange { get; private set; }

        public Rule(StateChange proposedChange, StateCondition preConditions)
        {
            ProposedChange = proposedChange;
            PreConditions = preConditions;
        }
    }
}
