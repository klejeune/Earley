using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.TagGrammar {
    class LexicalItem : IEarleyInput {
        public string Word { get; private set; }
        public string PartOfSpeech { get; private set; }

        public LexicalItem(string word, string partOfSpeech) {
            this.Word = word;
            this.PartOfSpeech = partOfSpeech;
        }

        public override string ToString() {
            return this.Word + "(" + this.PartOfSpeech + ")";
        }
    }
}
