using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTagger {
    class Token : IToken {
        public string Word { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public string PartOfSpeech { get; set; }
    }
}
