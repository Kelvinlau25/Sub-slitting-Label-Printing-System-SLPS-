
Partial Class master_main
    Inherits System.Web.UI.MasterPage

    Private _pointer As Boolean = False 

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Session("gstrUserID") = "Admin"

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Request.QueryString(ACL.Control.URL.URLEMPLOYEEID) IsNot Nothing Then
            Session("gstrUserID") = ACL.Security.Encryption.Decrypt(Request.QueryString(ACL.Control.URL.URLEMPLOYEEID))
            _pointer = True
        End If

        If Request.QueryString(ACL.Control.URL.URLCOMPANYID) IsNot Nothing Then
            Session("gstrUserCom") = Request.QueryString(ACL.Control.URL.URLCOMPANYID)
            _pointer = True
        End If

        If Session("gstrUserID") Is Nothing Then
            Response.Redirect("~/SessionExpired.aspx")
        End If
    End Sub
End Class

