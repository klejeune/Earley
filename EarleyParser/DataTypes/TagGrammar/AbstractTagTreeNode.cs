using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.TagGrammar {
    public abstract class AbstractTagTreeNode {
        public string Name { get; private set; }

        public virtual bool IsTerminal { get { return false; } }

        public virtual bool AcceptsAdjunction {
            get { return false; }
        }

        public virtual bool AcceptsSubstitution { get { return false; } }

        public AbstractTagTreeNode(string name) {
            this.Name = name;
        }

        public IEnumerable<AbstractTagTreeNode> SubNodes { get { return this.subNodes; } }

        private List<AbstractTagTreeNode> subNodes = new List<AbstractTagTreeNode>();

        public AbstractTagTreeNode ParentNode { get; set; }

        public virtual bool IsAdjunction { get { return false; } }

        public int GetIndexInParent() {
            return this.ParentNode.subNodes.IndexOf(this);
        }

        public override bool Equals(object obj) {
            return this.Name == ((AbstractTagTreeNode)obj).Name;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        public void AddChild(AbstractTagTreeNode child) {
            this.subNodes.Add(child);
        }

        public virtual bool IsCompatibleWith(IEarleyInput input) {
            return false;
        }

        public bool Any(Func<AbstractTagTreeNode, bool> predicate) {
            return predicate(this) || this.subNodes.Any(predicate);
        }

        public virtual bool CanBeEmpty {
            get { return false; }
        }

        public IEnumerable<OrientedTreeNode> Split() {
            if (this.SubNodes.Any()) {
                return new[] {
                    new OrientedTreeNode(this, Direction.Left),
                    new OrientedTreeNode(this, Direction.Right),
                };
            }
            else {
                return new[] {
                    new OrientedTreeNode(this, Direction.None),
                };
            }
        }
    }
}
