using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser {
    class ParserState {
        public IEarleyGrammarRule Rule { get; set; }
        public IEarleyGrammarRuleCursor Cursor { get; set; }
        public int Origin { get; set; }
        public string Comment { get; set; }

        public IEarleyData GetNextSymbol() {
            return this.Cursor.GetNextSymbol();
        }

        public override string ToString() {
            var rule = this.Cursor.ToString();
            
            return string.Format(
                "{0, -10} ({1}) # {2}",
                rule,
                this.Origin,
                this.Comment);
        }
    }
}
