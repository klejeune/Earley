using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.sun.org.apache.xml.@internal.resolver.helpers;

namespace PosTagger {
    public interface IToken {
        string Word { get; }
        int StartPosition { get; }
        int EndPosition { get; }
        string PartOfSpeech { get; }
    }
}
