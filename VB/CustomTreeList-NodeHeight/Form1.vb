Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace CustomTreeList_NodeHeight
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()

            Dim list As New List(Of MyRecord)()
            list.Add(New MyRecord(0, -1, "Corporate Headquarters", 1000000, "Monterey"))
            list.Add(New MyRecord(1, 0, "Sales and Marketing", 22000, "San Francisco"))
            list.Add(New MyRecord(2, 0, "Finance", 40000, "Monterey"))
            list.Add(New MyRecord(3, 0, "Engineering", 1100000, "Monterey"))
            list.Add(New MyRecord(4, -1, "Customer Services", 850000, "Burlington, VT"))

            treeList1.DataSource = list
            treeList1.Columns("Budget").Format.FormatType = DevExpress.Utils.FormatType.Numeric
            treeList1.Columns("Budget").Format.FormatString = "c0"

        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            treeList1.ForceInitialize()
            treeList1.ExpandAll()
            CType(treeList1.Nodes(0), MyTreeListNode).Height = 35
            CType(treeList1.Nodes(0).Nodes(1), MyTreeListNode).Height = 35
        End Sub
    End Class


    Public Class MyTreeList
        Inherits TreeList
        Public Sub New()
            MyBase.New()
            OptionsBehavior.AutoNodeHeight = False
        End Sub

        Protected Overrides Function CreateNode(ByVal nodeID As Integer, ByVal owner As TreeListNodes, ByVal tag As Object) As TreeListNode
            Return New MyTreeListNode(nodeID, owner)
        End Function
        Protected Overrides Sub InternalNodeChanged(ByVal node As TreeListNode, ByVal changeType As NodeChangeTypeEnum)
            If changeType = NodeChangeTypeEnum.User1 Then
                LayoutChanged()
            End If
            MyBase.InternalNodeChanged(node, changeType)
        End Sub
        Protected Overrides Sub RaiseCalcNodeHeight(ByVal node As TreeListNode, ByRef nodeHeight As Integer)
            Dim myNode As MyTreeListNode = CType(node, MyTreeListNode)
            If Not myNode Is Nothing Then
                nodeHeight = myNode.Height
            Else
                MyBase.RaiseCalcNodeHeight(node, nodeHeight)
            End If
        End Sub
        Public Overridable ReadOnly Property DefaultNodesHeight() As Integer
            Get
                Return 18
            End Get
        End Property
    End Class

    Public Class MyTreeListNode
        Inherits TreeListNode
        Const minHeight As Integer = 5
        Dim fHeight As Integer
        Public Sub New(ByVal id As Integer, ByVal owner As TreeListNodes)
            MyBase.New(id, owner)
            Me.fHeight = CType(owner.TreeList, MyTreeList).DefaultNodesHeight
        End Sub
        Public Property Height() As Integer
            Get
                Return fHeight
            End Get
            Set(ByVal Value As Integer)
                If fHeight = Value Or Value < minHeight Then Return
                fHeight = Value
                Changed(NodeChangeTypeEnum.User1)
            End Set
        End Property
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
