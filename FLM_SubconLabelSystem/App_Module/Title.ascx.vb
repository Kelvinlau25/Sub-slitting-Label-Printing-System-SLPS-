
Partial Class App_Module_Title
    Inherits System.Web.UI.UserControl

    Private _audit As Boolean = False
    Public WriteOnly Property Audit() As Boolean
        Set(ByVal value As Boolean)
            _audit = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _audit = False Then
            Dim setting As Control.Base = CType(Me.Page, Control.Base)
            Me.lblFormTitle.Text = setting.DisplayTitle & If(setting.Action <> Control.Base.EnumAction.None, " - ", String.Empty) & setting.ActionDesc
        Else
            Dim setting As Control.LogBase = CType(Me.Page, Control.LogBase)
            Me.lblFormTitle.Text = setting.DisplayTitle
        End If
    End Sub
End Class
