Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub gvDetail_BeforePerformDataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid As ASPxGridView = DirectCast(sender, ASPxGridView)
        Session("CategoryID") = grid.GetMasterRowKeyValue()
        grid.ClientInstanceName = "detailGrid" & grid.GetMasterRowKeyValue()
    End Sub

    Protected Sub gvDetail_HtmlRowPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableRowEventArgs)
        If e.RowType = GridViewRowType.Data Then
            Dim grid As ASPxGridView = DirectCast(sender, ASPxGridView)
            e.Row.Attributes.Add("categoryID", grid.GetMasterRowKeyValue().ToString())
            e.Row.Attributes.Add("productID", e.KeyValue.ToString())
        End If
    End Sub

    Protected Sub gvMaster_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Dim grid As ASPxGridView = DirectCast(sender, ASPxGridView)
        Dim productID As String = e.Parameters.Split("|"c)(0)
        Dim newCategoryID As String = e.Parameters.Split("|"c)(1)
        gvDetailDS.UpdateCommand = "UPDATE Products SET CategoryID = " & newCategoryID & " WHERE ProductID = " & productID & ""
        gvDetailDS.Update()
        grid.DataBind()
    End Sub

    Protected Sub gvDetail_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid As ASPxGridView = DirectCast(sender, ASPxGridView)
        grid.ClientSideEvents.Init = "" & ControlChars.CrLf & _
"        function (s, e) {" & ControlChars.CrLf & _
"            var table = s.GetMainTable();  " & ControlChars.CrLf & _
"            var att = document.createAttribute('categoryID');      " & ControlChars.CrLf & _
"            att.value = " & grid.GetMasterRowKeyValue().ToString() & ";                           " & ControlChars.CrLf & _
"            table.setAttributeNode(att);" & ControlChars.CrLf & _
"        }"
    End Sub
End Class