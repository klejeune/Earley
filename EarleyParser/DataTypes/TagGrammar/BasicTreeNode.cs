using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarleyParser.DataTypes.TagGrammar {
    class BasicTreeNode : AbstractTagTreeNode {
        public BasicTreeNode(string name) : base(name) {
            
        }

        public override string ToString() {
            return this.Name;
        }

        public override bool AcceptsAdjunction {
            get { return true; }
        }

        public override bool CanBeEmpty {
            get { return true; }
        }
    }
}
