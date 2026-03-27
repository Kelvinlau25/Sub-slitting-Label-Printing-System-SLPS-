
Partial Class SessionExpired
    Inherits System.Web.UI.Page

    Public ReadOnly Property ReturnURL() As String
        Get
            Return "Default.aspx"
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()
	ClientScript.RegisterStartupScript(Me.GetType(), "Load", "<script type='text/javascript'>window.parent.location.href='Default.aspx'; </script>")
	'response.redirect(resolveurl("Default.aspx"))
       End Sub
End Class
