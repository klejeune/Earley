using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTagger {
    public interface ISentence {
        IEnumerable<IToken> Tokens { get; }
    }
}
