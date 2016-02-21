using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EarleyParser.DataTypes.Regular;
using EarleyParser.EarleyBase;

namespace EarleyParser.DataTypes.TagGrammar {
    class TagTreeCursor : IEarleyGrammarRuleCursor {
        public OrientedTreeNode CurrentNode { get { return this.Nodes[this.currentIndex]; } }
        public List<OrientedTreeNode> Nodes { get; private set; }
        private int currentIndex;

        public TagTreeCursor(List<OrientedTreeNode> nodes, int index = 0) {
            this.Nodes = nodes;
            this.currentIndex = index;
        }

        public IEarleyData GetNextSymbol() {
            if (this.currentIndex + 1 < this.Nodes.Count) {
                return this.CurrentNode;
            }
            else {
                return null;
            }

        }

        public override string ToString() {
            return string.Join(" ", this.Nodes.Take(currentIndex))
                + "·"
                + string.Join(" ", this.Nodes.Skip(currentIndex));
        }

        //private AbstractTagTreeNode GetNextSymbolInner() {
        //    OrientedTreeNode nodeToDescend = null;
        //    OrientedTreeNode currentNode = this.CurrentNode;

        //    while (currentNode.ParentNode != null && nodeToDescend == null) {
        //        var indexInParent = currentNode.GetIndexInParent();

        //        if (indexInParent + 1 < currentNode.ParentNode.SubNodes.Count()) {
        //            nodeToDescend = currentNode.ParentNode.SubNodes.ElementAt(indexInParent + 1);
        //        }
        //        else {
        //            currentNode = currentNode.ParentNode;
        //        }
        //    }

        //    if (nodeToDescend == null) {
        //        // We were already at the last node
        //        return null;
        //    }
        //    else {
        //        while (nodeToDescend.SubNodes.Any()) {
        //            nodeToDescend = nodeToDescend.SubNodes.First();
        //        }

        //        return nodeToDescend;
        //    }
        //}



        public IEarleyGrammarRuleCursor MoveForward() {
            return new TagTreeCursor(this.Nodes, this.currentIndex + 1);



            ////var nextSymbol = this.GetNextSymbolInner();

            //AbstractTagTreeNode nextNode;
            //Direction direction;

            //if (this.CurrentNode.ParentNode == null && this.Direction == Direction.Right) {
            //    nextNode = null;
            //    direction = Direction.Right;
            //}
            //else if (this.CurrentNode.SubNodes.Any() && this.Direction == Direction.Left) {
            //    //if (this.CurrentNode.SubNodes.Any()) {
            //        nextNode = this.CurrentNode.SubNodes.First();
            //        direction = Direction.Left;
            //    //}
            //    //else {
            //    //    nextNode = this.CurrentNode;
            //    //    direction = CursorDirection.Right;
            //    //}
            //}
            //else {
            //    var indexInParent = this.CurrentNode.GetIndexInParent();
            //    if (indexInParent < this.CurrentNode.ParentNode.SubNodes.Count() - 1) {
            //        nextNode = this.CurrentNode.ParentNode.SubNodes.ElementAt(indexInParent + 1);
            //        direction = Direction.Left;
            //    }
            //    else {
            //        nextNode = CurrentNode.ParentNode;
            //        direction = Direction.Right;
            //    }
            //}

            //return new TagTreeCursor(nextNode, direction);
        }

        public bool IsComplete {
            get { return this.GetNextSymbol() == null; }
        }
    }
}
