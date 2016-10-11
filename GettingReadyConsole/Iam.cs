using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GettingReady
{
    /*
    An alternative solution could use:

    [Flags]
    public enum Iam
    {
        Whatever = 0,
        inPajamas = 1,
        InShoes = 2,
        InPants = 4,
        InShirt = 8,
        InJacket = 16,
        ReadyToGo = 32,
        DressedForHot = InShoes | InPants | InShirt,
        DressedForCold = DressedForHot | InJacket
    }
*/
    public class Iam : StateSet
    {
        public Iam() { }
        public Iam(IEnumerable<string> states) : base(states) { }

        public override HashSet<string> Allowed
        {
            get { return new HashSet<string>(new string[] { "InPajamas", "InShoes", "InPants", "InShirt", "InJacket", "ReadyToGo" }); }
        }
    }
}
