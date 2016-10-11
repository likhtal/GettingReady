using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GettingReady
{
    public class StateCondition
    {
        public StateSet Outside { get; private set; }
        public StateSet Required { get; private set; }
        public StateSet Disallowed { get; private set; }

        public StateCondition(StateSet required, StateSet noWay, StateSet environment)
        {
            Required = required;
            Disallowed = noWay;
            Outside = environment;
        }

        public bool Check(Context context)
        {
            var requiredOK = context.Iam.ContainsAll(Required);
            var disallowedOK = !context.Iam.ContainsAny(Disallowed);

            return requiredOK && disallowedOK && Outside.ContainsAll(context.Outside);
        }
    }
}
