using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CustomTreeList_NodeHeight {
    public class MyTreeList : TreeList {
        public MyTreeList() : base() {
            OptionsBehavior.AutoNodeHeight = false;
        }

        protected override TreeListNode CreateNode(int nodeID, TreeListNodes owner, object tag) {
            return new MyTreeListNode(nodeID, owner);
        }

        protected override void InternalNodeChanged(TreeListNode node, TreeListNodes nodes, NodeChangeTypeEnum changeType) {
            if (changeType == NodeChangeTypeEnum.User1)
                LayoutChanged();
            base.InternalNodeChanged(node, nodes, changeType);
        }
        protected override void RaiseCalcNodeHeight(TreeListNode node, ref int nodeHeight) {
            MyTreeListNode myNode = node as MyTreeListNode;
            if (myNode != null)
                nodeHeight = myNode.Height;
            else
                base.RaiseCalcNodeHeight(node, ref nodeHeight);
        }
        public virtual int DefaultNodesHeight { get { return 18; } }
    }

    public class MyTreeListNode : TreeListNode {
        const int minHeight = 5;
        int height;
        public MyTreeListNode(int id, TreeListNodes owner) : base(id, owner) {
            this.height = (owner.TreeList as MyTreeList).DefaultNodesHeight;
        }
        public int Height {
            get { return height; }
            set {
                if (Height == value || value < minHeight) return;
                height = value;
                Changed(NodeChangeTypeEnum.User1);
            }
        }
    }
}


