using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarleyParser.EarleyBase {
    public interface IEarleyGrammarRuleCursor {
        IEarleyData GetNextSymbol();
        IEarleyGrammarRuleCursor MoveForward();
        bool IsComplete { get; }
    }
}
