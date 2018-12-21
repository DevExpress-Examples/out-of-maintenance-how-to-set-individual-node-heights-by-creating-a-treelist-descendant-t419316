Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace CustomTreeList_NodeHeight
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()

            Dim list As New BindingList(Of MyRecord)()
            list.Add(New MyRecord(0, -1, "Corporate Headquarters", 1000000, "Monterey"))
            list.Add(New MyRecord(1, 0, "Sales and Marketing", 22000, "San Francisco"))
            list.Add(New MyRecord(2, 0, "Finance", 40000, "Monterey"))
            list.Add(New MyRecord(3, 0, "Engineering", 1100000, "Monterey"))
            list.Add(New MyRecord(4, -1, "Customer Services", 850000, "Burlington, VT"))

            treeList1.DataSource = list
            treeList1.Columns("Budget").Format.FormatType = DevExpress.Utils.FormatType.Numeric
            treeList1.Columns("Budget").Format.FormatString = "c0"
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            treeList1.ForceInitialize()
            treeList1.ExpandAll()
            CType(treeList1.Nodes(0), MyTreeListNode).Height = 35
            CType(treeList1.Nodes(0).Nodes(1), MyTreeListNode).Height = 35
        End Sub
    End Class

    Public Class MyRecord
        Public Property ID() As Integer
        Public Property ParentID() As Integer

        Public Property Department() As String
        Public Property Location() As String
        Public Property Budget() As Decimal

        Public Sub New(ByVal id As Integer, ByVal parentID As Integer, ByVal department As String, ByVal budget As Decimal, ByVal location As String)
            Me.ID = id
            Me.ParentID = parentID
            Me.Department = department
            Me.Budget = budget
            Me.Location = location
        End Sub
    End Class
End Namespace
