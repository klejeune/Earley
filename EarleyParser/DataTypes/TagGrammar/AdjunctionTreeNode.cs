using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarleyParser.DataTypes.TagGrammar {
    public class AdjunctionTreeNode : AbstractTagTreeNode {
        public AdjunctionTreeNode(string name) : base(name) { }

        public override bool IsAdjunction {
            get { return true; }
        }

        public override bool CanBeEmpty {
            get { return true; }
        }
    }
}
