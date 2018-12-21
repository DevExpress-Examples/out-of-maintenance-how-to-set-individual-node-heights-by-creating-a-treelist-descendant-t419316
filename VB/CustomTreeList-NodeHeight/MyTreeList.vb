Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Namespace CustomTreeList_NodeHeight
    Public Class MyTreeList
        Inherits TreeList

        Public Sub New()
            MyBase.New()
            OptionsBehavior.AutoNodeHeight = False
        End Sub

        Protected Overrides Function CreateNode(ByVal nodeID As Integer, ByVal owner As TreeListNodes, ByVal tag As Object) As TreeListNode
            Return New MyTreeListNode(nodeID, owner)
        End Function

        Protected Overrides Sub InternalNodeChanged(ByVal node As TreeListNode, ByVal nodes As TreeListNodes, ByVal changeType As NodeChangeTypeEnum)
            If changeType Is NodeChangeTypeEnum.User1 Then
                LayoutChanged()
            End If
            MyBase.InternalNodeChanged(node, nodes, changeType)
        End Sub
        Protected Overrides Sub RaiseCalcNodeHeight(ByVal node As TreeListNode, ByRef nodeHeight As Integer)
            Dim myNode As MyTreeListNode = TryCast(node, MyTreeListNode)
            If myNode IsNot Nothing Then
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

        Private Const minHeight As Integer = 5

        Private height_Renamed As Integer
        Public Sub New(ByVal id As Integer, ByVal owner As TreeListNodes)
            MyBase.New(id, owner)
            Me.height_Renamed = (TryCast(owner.TreeList, MyTreeList)).DefaultNodesHeight
        End Sub
        Public Property Height() As Integer
            Get
                Return height_Renamed
            End Get
            Set(ByVal value As Integer)
                If Height = value OrElse value < minHeight Then
                    Return
                End If
                height_Renamed = value
                Changed(NodeChangeTypeEnum.User1)
            End Set
        End Property
    End Class
End Namespace


