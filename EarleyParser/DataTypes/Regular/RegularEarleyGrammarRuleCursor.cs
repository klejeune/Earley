using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.Regular {
    class RegularEarleyGrammarRuleCursor: IEarleyGrammarRuleCursor {
        public RegularGrammarRule Rule { get; private set; }
        public int Cursor { get; private set; }

        public RegularEarleyGrammarRuleCursor(RegularGrammarRule rule) {
            this.Rule = rule;
            this.Cursor = 0;
        }

        public IEarleyData GetNextSymbol() {
            if (Cursor < this.Rule.Right.Count()) {
                return this.Rule.Right.ElementAt(Cursor);
            }
            else {
                return null;
            }
        }
        
        public IEarleyGrammarRuleCursor MoveForward() {
            return new RegularEarleyGrammarRuleCursor(this.Rule) {
                Cursor = this.Cursor + 1,
            };
        }

        public override string ToString() {
            var rule = this.Rule.Left
                     + " -> "
                     + string.Concat(this.Rule.Right.Take(this.Cursor))
                     + "·"
                     + string.Concat(this.Rule.Right.Skip(this.Cursor));

            return rule;
        }

        public bool IsComplete {
            get { return this.Cursor == this.Rule.Right.Count(); }
        }
    }
}
