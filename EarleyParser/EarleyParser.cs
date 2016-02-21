using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.DataTypes.Regular;
using EarleyParser.EarleyBase;

namespace EarleyParser {
    class EarleyParser {
        public bool Parse(Grammar grammar, IEarleyInput[] input) {
            var states = new List<ParserState>[input.Length + 1];

            for (int i = 0; i < states.Length; i++) {
                states[i] = new List<ParserState>();
            }

            foreach (var startRule in grammar.StartRules) {
                states[0].Add(new ParserState {
                    Rule = startRule,
                    Cursor = startRule.CreateCursor(),
                    Origin = 0,
                    Comment = "Start rule",
                });
            }

            for (int stateCursor = 0; stateCursor < input.Length + 1; stateCursor++) {
                int innerCursor = 0;
                var currentStateList = states[stateCursor];
                List<ParserState> nextStateList = null;

                if (stateCursor < input.Length) {
                    nextStateList = states[stateCursor + 1];
                }

                while (innerCursor < currentStateList.Count) {
                    var currentState = currentStateList[innerCursor];
                    var nextSymbol = currentState.GetNextSymbol();

                    if (nextSymbol != null) {
                        

                        if (nextSymbol.IsTerminal
                            && stateCursor < input.Length
                            && nextSymbol.IsCompatibleWith(input[stateCursor])
                            && nextStateList != null) {
                            // Scanning
                            var newState = new ParserState {
                                Rule = currentState.Rule,
                                Cursor = currentState.Cursor.MoveForward(),
                                Origin = currentState.Origin,
                                Comment = "Scan from S(" + stateCursor + ")(" + (innerCursor + 1) + ")",
                            };

                            nextStateList.Add(newState);
                        }
                        else if (!nextSymbol.IsTerminal) {
                            var rulesWithLeft = grammar.GetRulesWithLeft(nextSymbol);

                            // Prediction
                            foreach (var rule in rulesWithLeft) {
                                if (!currentStateList.Any(state => state.Rule == rule)) {
                                    var newState = new ParserState {
                                        Rule = rule,
                                        Cursor = rule.CreateCursor(),
                                        Origin = stateCursor,
                                        Comment = "Predict from (" + (innerCursor + 1) + ")",
                                    };

                                    currentStateList.Add(newState);
                                }
                            }

                            if (nextSymbol.CanBeEmpty) {
                                var newState = new ParserState {
                                    Rule = currentState.Rule,
                                    Cursor = currentState.Cursor.MoveForward(),
                                    Origin = currentState.Origin,
                                    Comment = "Optional from (" + (innerCursor + 1) + ")",
                                };

                                currentStateList.Add(newState);
                            }
                        }
                    }
                    else {
                        // Completion
                        var stateListToExplore = states[currentState.Origin];

                        for (int stateToExploreIndex = 0; stateToExploreIndex < stateListToExplore.Count; stateToExploreIndex++) {
                            var state = stateListToExplore[stateToExploreIndex];
                            var stateNextSymbol = state.GetNextSymbol();

                            if (stateNextSymbol != null && currentState.Rule.IsDerivableFrom(stateNextSymbol)) {
                                var newState = new ParserState {
                                    Rule = state.Rule,
                                    Cursor = state.Cursor.MoveForward(),
                                    Origin = state.Origin,
                                    Comment = "Complete from (" + (innerCursor + 1) + ") and S("
                                    + currentState.Origin + ")(" + (stateToExploreIndex + 1) + ")",
                                };

                                currentStateList.Add(newState);
                            }
                        }
                    }
                    innerCursor++;
                }

                //PrintStateList(stateCursor, input, currentStateList);

            }

            //foreach (var stateList in states.Select((s, i) => new { List = s, Index = i })) {
            //    PrintStateList(stateList.Index, input, stateList.List);
            //}

            return states.Last().Any(s => IsCompleteParse(grammar, s));
        }

        private bool IsCompleteParse(Grammar grammar, ParserState state) {
            return grammar.StartRules.Contains(state.Rule)
                   && state.Cursor.IsComplete;
        }

        private void PrintStateList(int index, IEarleyInput[] input, IEnumerable<ParserState> states) {
            Console.WriteLine("S({0}) : {1}·{2}",
                index,
                string.Concat(input.Take(index)),
                string.Concat(input.Skip(index)));

            for (int i = 0; i < states.Count(); i++) {
                this.PrintState((i + 1).ToString(), states.ElementAt(i));
            }

            Console.WriteLine();
        }

        private void PrintState(string index, ParserState state) {
            Console.WriteLine("({0}) {1}", index, state);
        }
    }
}
