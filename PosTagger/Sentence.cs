using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTagger {
    class Sentence : ISentence {
        public IEnumerable<IToken> Tokens { get; private set; }

        public Sentence(IEnumerable<IToken> tokens) {
            this.Tokens = tokens;
        }
    }
}
