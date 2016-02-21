using System.Collections.Generic;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.Regular {
    class RegularGrammarRule : IEarleyGrammarRule {
        public IEarleyData Left { get; set; }
        public IEnumerable<IEarleyData> Right { get; set; }

        public bool IsDerivableFrom(IEarleyData data) {
            return this.Left.Equals(data);
        }

        public string ToString(IEarleyGrammarRuleCursor cursor) {
            var rule = this.Left
                     + " -> "
                     + string.Concat(this.Right);

            return rule;
        }
        
        public IEarleyGrammarRuleCursor CreateCursor() {
           return new RegularEarleyGrammarRuleCursor(this);
        }

        public bool IsStartRule { get; set; }
    }
}
