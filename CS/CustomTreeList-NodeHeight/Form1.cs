using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomTreeList_NodeHeight {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            List<MyRecord> list = new List<MyRecord>();
            list.Add(new MyRecord(0, -1, "Corporate Headquarters", 1000000, "Monterey"));
            list.Add(new MyRecord(1, 0, "Sales and Marketing", 22000, "San Francisco"));
            list.Add(new MyRecord(2, 0, "Finance", 40000, "Monterey"));
            list.Add(new MyRecord(3, 0, "Engineering", 1100000, "Monterey"));
            list.Add(new MyRecord(4, -1, "Customer Services", 850000, "Burlington, VT"));

            treeList1.DataSource = list;
            treeList1.Columns["Budget"].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            treeList1.Columns["Budget"].Format.FormatString = "c0";


            

        }

        private void Form1_Load(object sender, EventArgs e) {
            treeList1.ForceInitialize();
            treeList1.ExpandAll();
            ((MyTreeListNode)treeList1.Nodes[0]).Height = 35;
            ((MyTreeListNode)treeList1.Nodes[0].Nodes[1]).Height = 35;

        }
    }


    public class MyTreeList : TreeList {
        public MyTreeList() : base() {
            OptionsBehavior.AutoNodeHeight = false;
        }

        protected override TreeListNode CreateNode(int nodeID, TreeListNodes owner, object tag) {
            return new MyTreeListNode(nodeID, owner);
        }
        protected override void InternalNodeChanged(TreeListNode node, NodeChangeTypeEnum changeType) {
            if (changeType == NodeChangeTypeEnum.User1)
                LayoutChanged();
            base.InternalNodeChanged(node, changeType);
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

    public class MyRecord {
        public int ID { get; set; }
        public int ParentID { get; set; }

        public string Department { get; set; }
        public string Location { get; set; }
        public decimal Budget { get; set; }

        public MyRecord(int id, int parentID, string department, decimal budget, string location) {
            ID = id;
            ParentID = parentID;
            Department = department;
            Budget = budget;
            Location = location;
        }
    }

}
