using System;
using System.Collections.Generic;
using System.Linq;
using GettingReady.Interfaces;

namespace GettingReady
{
    public class Processor<TIn, TOut> : IStringProcessor<TIn, TOut> 
        where TOut: StateSet, new()
        where TIn : StateSet
    {
        private List<ICmd> _availableCmds;
        private IRuleEngine _rulesEngine;
        private IInterpreter _interpreter;

        public Processor(List<ICmd> availableCmds, IRuleEngine rulesEngine, IInterpreter interpretor)
        {
            _availableCmds = availableCmds;
            _rulesEngine = rulesEngine;
            _interpreter = interpretor;
        }

        public string Run(TIn currentState, string commands)
        {
            var inputInfo = new InputInfo(commands, _availableCmds);

            var context = new Context(currentState, inputInfo.StateOutside, inputInfo.Cmds);

            var output = new List<string>();

            while(context.Cmds.Count > 0)
            {
                var cmd = context.Cmds.Peek();

                if (_rulesEngine.IsAllowed(cmd.ProposedChange, context))
                {
                    cmd.Do(context);
                    output.Add(_interpreter.Interpret(cmd.ProposedChange, context));
                }
                else
                {
                    output.Add(_interpreter.InterpretFailure(context));
                    break;
                }

                context.Cmds.Dequeue();
            }

            return string.Join(", ", output.ToArray());
        }

        private class InputInfo
        {
            public readonly TOut StateOutside;
            public readonly Queue<ICmd> Cmds = new Queue<ICmd>();

            public InputInfo(TOut stateOutside, IEnumerable<ICmd> cmds)
            {
                StateOutside = stateOutside;
                foreach (var cmd in cmds) Cmds.Enqueue(cmd);
            }

            public InputInfo(string s, List<ICmd> availableCmds)
            {
                s = s.Trim();

                if (s == string.Empty) throw new ArgumentException("Cmd Parser: Empty input!");

                if (s.IndexOf(' ') < 0) throw new ArgumentException("Cmd Parser: Invalid input!");

                var outs = s.Substring(0, s.IndexOf(' '));

                StateOutside = new TOut();

                StateOutside.Add(outs);

                var parsed = availableCmds.ToDictionary(x => x.N, x => x);

                // Converting to list of integers
                var ls = s.Substring(s.IndexOf(' ')).Trim().Split(',').ToList().ConvertAll(x => x.Trim().ToI());

                foreach (var scmd in ls)
                {
                    if (!parsed.ContainsKey(scmd))
                    {
                        throw new ArgumentException(string.Format("Cmd Parser: Wrong value in the list of commands: {0}!", s.IndexOf(' ')));
                    }

                    Cmds.Enqueue(parsed[scmd]);
                }
            }
        }
    }
}
