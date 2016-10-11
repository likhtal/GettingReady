using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady.Interfaces;

namespace GettingReady
{
    public class RuleEngine : IRuleEngine
    {
        // for each state change we have OR-list of (complex) Conditions
        private Dictionary<StateChange, List<StateCondition>> parsed = new Dictionary<StateChange, List<StateCondition>>();

        public RuleEngine()
        {
        }

        public RuleEngine(IEnumerable<IRule> rules)
        {
            foreach (var rule in rules)
            {
                AddRule(rule);
            }
        }

        public IRuleEngine AddRule(IRule rule)
        {
            if (!parsed.ContainsKey(rule.ProposedChange))
            {
                parsed.Add(rule.ProposedChange, new List<StateCondition>());
            }

            parsed[rule.ProposedChange].Add(rule.PreConditions);

            return this;
        }

        public IRuleEngine ClearRules()
        {
            parsed.Clear();

            return this;
        }

        public bool IsAllowed(StateChange proposedChange, Context context)
        {
            foreach (var condition in parsed[proposedChange])
            {
                if (condition.Check(context)) return true;
            }

            return false;
        }
    }
}
