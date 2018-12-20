using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CustomTreeList_NodeHeight {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            BindingList<MyRecord> list = new BindingList<MyRecord>();
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
