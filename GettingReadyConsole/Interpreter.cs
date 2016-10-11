using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady;
using GettingReady.Interfaces;

namespace GettingReadyConsole
{
    public class Interpreter : IInterpreter
    {
        public string Interpret(StateChange change, Context context)
        {
            if (change.Op == Change.Add && change.State == "ReadyToGo") return "leaving house";
            if (change.Op == Change.Remove && change.State == "InPajamas") return "Removing PJs";

            if (context.Outside.Contains("COLD"))
            {
                if (change.Op == Change.Add && change.State == "InShoes") return "boots";
                if (change.Op == Change.Add && change.State == "InShirt") return "shirt";
                if (change.Op == Change.Add && change.State == "InPants") return "pants";
                if (change.Op == Change.Add && change.State == "InJacket") return "jacket";
            }
            else if (context.Outside.Contains("HOT"))
            {
                if (change.Op == Change.Add && change.State == "InShoes") return "sandals";
                if (change.Op == Change.Add && change.State == "InShirt") return "t-shirt";
                if (change.Op == Change.Add && change.State == "InPants") return "shorts";
            }

            throw new ArgumentException("We are in a wrong state!");
        }

        public string InterpretFailure(Context context)
        {
            return "fail";
        }
    }
}
