using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.TagGrammar {
    public class OrientedTreeNode : IEarleyData {
        public AbstractTagTreeNode Node { get; private set; }
        public Direction Direction { get; private set; }

        public OrientedTreeNode(AbstractTagTreeNode node, Direction direction) {
            this.Node = node;
            this.Direction = direction;
        }

        public bool IsTerminal {
            get { return this.Node.IsTerminal; }
        }

        public bool IsCompatibleWith(IEarleyInput input) {
            return this.Node.IsCompatibleWith(input);
        }

        public bool CanBeEmpty {
            get { return this.Node.CanBeEmpty; }
        }

        public override string ToString() {
            return this.Node.ToString();
        }
    }
}
