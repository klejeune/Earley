using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarleyParser.DataTypes.TagGrammar {
    public class TagGrammarFactory {
        public AbstractTagTreeNode Root(string name, params AbstractTagTreeNode[] children) {
            return DeclareChildren(new BasicTreeNode(name), children);
        }

        public AbstractTagTreeNode Node(string name, params AbstractTagTreeNode[] children) {
            return DeclareChildren(new BasicTreeNode(name), children);
        }

        public AbstractTagTreeNode Substitution(string name, params AbstractTagTreeNode[] children) {
            return DeclareChildren(new SubstitutionTreeNode(name), children);
        }
        public AbstractTagTreeNode Adjunction(string name) {
            return DeclareChildren(new AdjunctionTreeNode(name));
        }

        private AbstractTagTreeNode DeclareChildren(AbstractTagTreeNode parent, params AbstractTagTreeNode[] children) {
            foreach (var child in children) {
                parent.AddChild(child);
                child.ParentNode = parent;
            }

            return parent;
        }

        public AbstractTagTreeNode Anchor() {
            return DeclareChildren(new AnchorTreeNode());
        }
    }
}
