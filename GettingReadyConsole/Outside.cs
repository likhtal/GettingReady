using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GettingReady
{
    /*
      An alternative solution could use:
      public enum Outside { HOT, COLD, WHOCARES }
    */
    public class Outside : StateSet
    {
        public Outside() : base() { }

        public Outside(IEnumerable<string> states) : base(states) { }

        public override HashSet<string> Allowed
        {
            get { return new HashSet<string>(new string[] { "HOT", "COLD" }); }
        }
    }
}
