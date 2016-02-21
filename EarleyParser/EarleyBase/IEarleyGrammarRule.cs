using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarleyParser.EarleyBase {
    public interface IEarleyGrammarRule {
        bool IsDerivableFrom(IEarleyData data);
        string ToString(IEarleyGrammarRuleCursor cursor);
        IEarleyGrammarRuleCursor CreateCursor();
        bool IsStartRule { get; }
    }
}
