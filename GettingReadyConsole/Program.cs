using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady;
using GettingReady.Interfaces;

namespace GettingReadyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var availableCmds = new List<ICmd>();

            // Full list of available commands: e.g. add shoes
            availableCmds.Add(new Cmd(1, new StateChange(Change.Add, "InShoes")));
            availableCmds.Add(new Cmd(2, new StateChange(Change.Add, "InShirt")));
            availableCmds.Add(new Cmd(3, new StateChange(Change.Add, "InJacket")));
            availableCmds.Add(new Cmd(4, new StateChange(Change.Add, "InPants")));
            availableCmds.Add(new Cmd(5, new StateChange(Change.Add, "ReadyToGo")));
            availableCmds.Add(new Cmd(6, new StateChange(Change.Remove, "InPajamas")));

            var rulesEngine = new RuleEngine();

            // shortcuts:
            var WHOCARES = new Outside(new[] { "HOT", "COLD" });
            var HOT = new Outside(new[] { "HOT"});
            var COLD = new Outside(new[] { "COLD" });

            var Whatever = new Iam();
            var InPajamas = new Iam(new[] { "InPajamas" });
            var InPants = new Iam(new[] { "InPants" });
            var InShoes = new Iam(new[] { "InShoes" });
            var InShirt = new Iam(new[] { "InShirt" });
            var InJacket = new Iam(new[] { "InJacket" });
            var DressedForHot = new Iam(new[] { "InPants", "InShirt", "InShoes" });
            var DressedForCold = new Iam(new[] { "InPants", "InShirt", "InShoes", "InJacket" });
            var ReadyToGo = new Iam(new[] { "ReadyToGo" });

            rulesEngine
                // In Order to add Shoes, required are: pants, forbidden: pajamas, shoes; outside weather: who cares?
                .AddRule(new Rule(new StateChange(Change.Add, "InShoes"),
                    new StateCondition(required: InPants, noWay: new Iam(new[] { "InPajamas", "InShoes" }), environment: WHOCARES)))
                // etc.
                .AddRule(new Rule(new StateChange(Change.Add, "InPants"),
                    new StateCondition(Whatever, new Iam(new[] { "InPajamas", "InShoes", "InPants" }), WHOCARES)))
                .AddRule(new Rule(new StateChange(Change.Add, "InJacket"),
                    new StateCondition(InShirt, new Iam(new[] { "InPajamas", "InJacket" }), COLD)))
                .AddRule(new Rule(new StateChange(Change.Add, "InShirt"),
                    new StateCondition(Whatever, new Iam(new[] { "InPajamas", "InJacket", "InShirt" }), WHOCARES)))
                .AddRule(new Rule(new StateChange(Change.Remove, "InPajamas"),
                    new StateCondition(InPajamas, Whatever, WHOCARES)))
                .AddRule(new Rule(new StateChange(Change.Add, "ReadyToGo"),
                    new StateCondition(DressedForCold, Whatever, COLD)))
                .AddRule(new Rule(new StateChange(Change.Add, "ReadyToGo"),
                    new StateCondition(DressedForHot, Whatever, HOT)));

            // an interpreter to interpret the situation depending on the weather and produce output
            var interpreter = new Interpreter();

            // create a processor and load it with available commands, rules and interpretor
            // the processor itself is fully independent of any specific commands, rules etc.
            // that is, it can be loaded to solve any problem that involves states, commands and rules
            // (well, within certain limits, of course - e.g. commands should just changes states, 
            // and rules can only allow transition depending on some preconditions.)
            var processor = new Processor<Iam, Outside>(availableCmds, rulesEngine, interpreter);

            Console.WriteLine("Enter valid sequence, or type QUIT to quit.");

            var input = string.Empty;
            Iam currentState;

            while (true)
            {
                input = Console.ReadLine();
                if (input == string.Empty) continue;
                if (input.ToLower() == "quit") break;

                currentState = new Iam(new[] { "InPajamas" });

                try
                {
                    // run processor on initialState (which will change in time) and input
                    Console.WriteLine(processor.Run(currentState, input));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("An error has occurred: " + ex.Message);
                }
            }
        }
    }
}
