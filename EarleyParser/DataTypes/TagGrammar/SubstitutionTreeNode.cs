using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarleyParser.DataTypes.TagGrammar {
    class SubstitutionTreeNode : AbstractTagTreeNode {
        public SubstitutionTreeNode(string name) : base(name) {}

        public override string ToString() {
            return this.Name + "↓";
        }

        public override bool AcceptsSubstitution {
            get { return true; }
        }
    }
}
