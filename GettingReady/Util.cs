using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReady
{
    public static class Util
    {
        public static int ToI(this string s)
        {
            var i = int.MinValue;

            int.TryParse(s, out i);

            return i;
        }
    }
}
