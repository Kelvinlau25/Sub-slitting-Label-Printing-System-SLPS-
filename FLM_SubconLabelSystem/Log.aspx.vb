
Partial Class Log
    Inherits Control.LogBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub BindData()

        Dim _list As Library.Database.ListCollection
        _list = Library.Database.BLL.Log.GetLogList(MyBase.LogTable, MyBase.Key, MyBase.PageNo)

        grdResult.DataSource = _list.Data
        grdResult.DataBind()

        UCFooter.TotalRecords = _list.TotalRow
    End Sub

End Class
