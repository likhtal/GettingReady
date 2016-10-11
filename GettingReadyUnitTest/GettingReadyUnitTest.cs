using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GettingReady;
using GettingReady.Interfaces;
using GettingReadyConsole;

namespace GettingReadyUnitTestProject
{
    [TestClass]
    public class GettingReadyUnitTest
    {
        List<ICmd> availableCmds;
        RuleEngine rulesEngine;

        Outside WHOCARES = new Outside(new[] { "HOT", "COLD" });
        Outside HOT = new Outside(new[] { "HOT" });
        Outside COLD = new Outside(new[] { "COLD" });

        Iam Whatever = new Iam();
        Iam InPajamas = new Iam(new[] { "InPajamas" });
        Iam InPants = new Iam(new[] { "InPants" });
        Iam InShoes = new Iam(new[] { "InShoes" }); 
        Iam InShirt = new Iam(new[] { "InShirt" });
        Iam InJacket = new Iam(new[] { "InJacket" });
        Iam DressedForHot = new Iam(new[] { "InPants", "InShirt", "InShoes" });
        Iam DressedForCold = new Iam(new[] { "InPants", "InShirt", "InShoes", "InJacket" });
        Iam ReadyToGo = new Iam(new[] { "ReadyToGo" });

        Interpreter interpreter;

        Processor<Iam, Outside> processor;

        [TestInitialize]
        public void Initialize()
        {
            availableCmds = new List<ICmd>();

            availableCmds.Add(new Cmd(1, new StateChange(Change.Add, "InShoes")));
            availableCmds.Add(new Cmd(2, new StateChange(Change.Add, "InShirt")));
            availableCmds.Add(new Cmd(3, new StateChange(Change.Add, "InJacket")));
            availableCmds.Add(new Cmd(4, new StateChange(Change.Add, "InPants")));
            availableCmds.Add(new Cmd(5, new StateChange(Change.Add, "ReadyToGo")));
            availableCmds.Add(new Cmd(6, new StateChange(Change.Remove, "InPajamas")));

            rulesEngine = new RuleEngine();

            rulesEngine
    .AddRule(new Rule(new StateChange(Change.Add, "InShoes"),
        new StateCondition(InPants, new Iam(new[] { "InPajamas", "InShoes" }), WHOCARES)))
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

            interpreter = new Interpreter();

            processor = new Processor<Iam, Outside>(availableCmds, rulesEngine, interpreter);
        }

        [TestMethod]
        public void TestMethod1()
        {

            var currentState1 = new Iam(new[] { "InPajamas" });
            var actual1 = processor.Run(currentState1, "HOT 6, 4, 2, 1, 5");
            Assert.AreEqual("Removing PJs, shorts, t-shirt, sandals, leaving house", actual1);

            var currentState2 = new Iam(new[] { "InPajamas" });
            var actual2 = processor.Run(currentState2, "COLD 6, 4, 2, 3, 1, 5");
            Assert.AreEqual("Removing PJs, pants, shirt, jacket, boots, leaving house", actual2);

            var currentState3 = new Iam(new[] { "InPajamas" });
            var actual3 = processor.Run(currentState3, "COLD 4");
            Assert.AreEqual("fail", actual3);

            var currentState4 = new Iam(new[] { "InPajamas" });
            var actual4 = processor.Run(currentState4, "HOT 6, 4, 3");
            Assert.AreEqual("Removing PJs, shorts, fail", actual4);

            var currentState5 = new Iam(new[] { "InPajamas" });
            var actual5 = processor.Run(currentState5, "HOT 6, 4, 4");
            Assert.AreEqual("Removing PJs, shorts, fail", actual5);

            var currentState6 = new Iam(new[] { "InPajamas" });
            var actual6 = processor.Run(currentState6, "COLD 6, 4, 2, 3, 5");
            Assert.AreEqual("Removing PJs, pants, shirt, jacket, fail", actual6);
        }
    }
}
