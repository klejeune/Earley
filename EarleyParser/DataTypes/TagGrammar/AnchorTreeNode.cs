using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.TagGrammar {
    class AnchorTreeNode : AbstractTagTreeNode {
        //private static IDictionary<string, IEnumerable<string>> items = new Dictionary<string, IEnumerable<string>> {
        //    {"V", new[] {"mange"}},
        //    {"NP", new[] {"chat", "souris"}},
        //    {"ADV", new[] {"souvent"}},
        //    {"DET", new[] {"le", "des"}},
        //};

        public AnchorTreeNode() : base("♦") { }

        public override bool IsTerminal {
            get { return true; }
        }

        public override bool IsCompatibleWith(IEarleyInput input) {
            var typedInput = (LexicalItem) input;

            return typedInput.PartOfSpeech == this.ParentNode.Name;

            //IEnumerable<string> possibleWordsForCategory = null;

            //return items.TryGetValue(this.ParentNode.Name, out possibleWordsForCategory)
            //       && possibleWordsForCategory.Contains(typedInput.Word);
        }

        public override string ToString() {
            return this.Name;
        }
    }
}
