using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.TagGrammar {
    class TagTree : IEarleyGrammarRule {
        public AbstractTagTreeNode RootNode { get; private set; }
        public string Name { get; private set; }
        public AdjunctionType IsAdjunctionTree { get; private set; }
        public bool IsAuxiliary { get { return this.IsAdjunctionTree != AdjunctionType.None; } }

        public TagTree(string name, AbstractTagTreeNode rootNode) {
            this.Name = name;
            this.RootNode = rootNode;

            if (rootNode.SubNodes.Count() >= 1 && rootNode.SubNodes.First().IsAdjunction) {
                this.IsAdjunctionTree = AdjunctionType.Left;
            }
            else if (rootNode.SubNodes.Count() >= 2 && rootNode.SubNodes.Last().IsAdjunction) {
                this.IsAdjunctionTree = AdjunctionType.Right;
            }
            else if (rootNode.Any(node => node.IsAdjunction)) {
                this.IsAdjunctionTree = AdjunctionType.Complex;
            }
            else {
                this.IsAdjunctionTree = AdjunctionType.None;
            }
        }

        public bool IsDerivableFrom(IEarleyData data) {
            var typedData = (OrientedTreeNode) data;

            if (this.IsAdjunctionTree == AdjunctionType.Complex) {
                throw new InvalidOperationException("Complexe adjunction is not supported (yet).");
            }

            return this.RootNode.Name == typedData.Node.Name &&
                   (typedData.Node.AcceptsAdjunction &&
                    (this.IsAdjunctionTree == AdjunctionType.Left && typedData.Direction == Direction.Right
                     || this.IsAdjunctionTree == AdjunctionType.Right && typedData.Direction == Direction.Left)
                    || this.IsAdjunctionTree == AdjunctionType.None && typedData.Node.AcceptsSubstitution);
        }

        public override string ToString() {
            return this.Name;
        }

        public string ToString(IEarleyGrammarRuleCursor cursor) {
            throw new NotImplementedException();
        }

        public IEarleyGrammarRuleCursor CreateCursor() {
            //var leftNode = this.RootNode;

            //while (leftNode.SubNodes.Any()) {
            //    leftNode = leftNode.SubNodes.First();
            //}

            //return new TagTreeCursor(leftNode);

            var list = new List<OrientedTreeNode>();

            this.FlattenTree(this.RootNode, list);
            
            return new TagTreeCursor(list);
        }

        private void FlattenTree(AbstractTagTreeNode node, List<OrientedTreeNode> list) {
            if (node.SubNodes.Any()) {
                list.Add(new OrientedTreeNode(node, Direction.Left));

                foreach (var child in node.SubNodes) {
                    this.FlattenTree(child, list);
                }

                list.Add(new OrientedTreeNode(node, Direction.Right));
            }
            else {
                list.Add(new OrientedTreeNode(node, Direction.None));
            }
        } 

        public bool IsStartRule {
            get { return !this.IsAuxiliary; }
        }
    }
}
