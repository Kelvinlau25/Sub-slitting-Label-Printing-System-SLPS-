Imports System.Collections.Generic
Imports ACL.MenuBar.Object
Imports System.Data

Partial Class Menu

    Inherits System.Web.UI.Page

    Protected _list As LeftMenuItemList
    Protected _words As String
    Private _pointer As Boolean = False
    Private _SignOutURL As String = String.Empty
    Protected ReadOnly Property SignOutURL() As String
        Get
            Return _SignOutURL
        End Get
    End Property

    Private _HomeURL As String = String.Empty
    Protected ReadOnly Property HomeURL() As String
        Get
            Return _HomeURL
        End Get
    End Property

    Private _ResetURL As String = String.Empty
    Protected ReadOnly Property ResetURL() As String
        Get
            Return _ResetURL
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session("gstrUserID") = Session("USERID")
        Session("gstrUserComp") = Session("COMPANYCODE")
        Session("U_Level") = Session("ULEVEL")
        Session("gettemp") = Session("USERNAME")
        Session("LoginHis") = DateTime.Now.ToString("dd MMMM yyyy")

        Me._SignOutURL = ResolveUrl("~/Default.aspx")
        Me._HomeURL = ResolveUrl("~/Menu.aspx")
        Me._ResetURL = ResolveUrl("~/MasterMaint/ChangePassword.aspx")

        If Session("gstrUserID") Is Nothing Then
            Response.Redirect("~/SessionExpired.aspx?ReturnURL=" & Server.UrlEncode(Request.RawUrl))
        End If

        If (Session("pswlenerr")) Then
            Session.Remove("pswlenerr")
            resetpwd.Visible = True
            resetpwd.Attributes.Add("Style", "font-weight:900;")
        Else
            resetpwd.Visible = False
        End If

        If _list Is Nothing Then
            _list = New LeftMenuItemList
        End If

        Dim _menulist As DataTable = Library.Database.BLL.MenuListing.Load_Menu_Listing("")

        Dim mylistHtml As New StringBuilder
        Dim mycounter As Integer = 0
        Dim _str_Category As String = ""
        Dim _str_MenuName As String = ""
        Dim _int_left_menu_id As Integer = 0
        Dim _obj_List As New ArrayList
        Dim _str_menu_id As String = ""


        For Each dr As DataRow In _menulist.Rows
            mycounter += 1
            _str_MenuName = dr("MENU_NAME").ToString.Trim

            If (_str_Category.Equals(dr("CATEGORY").ToString.Trim) = False) And (Session("ULEVEL").Equals("3") = False) Then

                _str_Category = dr("CATEGORY").ToString.Trim

                If _int_left_menu_id > 0 Then

                    liItems.Text &= String.Format("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>", _str_menu_id, mylistHtml)

                    mylistHtml.Length = 0
                    mylistHtml.Capacity = 0

                End If

                _str_menu_id = "left_menu_" & _int_left_menu_id
                _int_left_menu_id += 1

                If Session("ULEVEL") = 3 And (_str_Category = "HouseKeeping") Then

                Else
                    _list.AddItem(New LeftMenuItem(_str_menu_id, _str_Category, False))
                End If
            End If

            If (_str_Category.Equals(dr("CATEGORY").ToString.Trim) = False) And (Session("ULEVEL").Equals("3") = True) And (_str_MenuName.Equals("Sub-Slittting Request - Add") = False) Then

                _str_Category = dr("CATEGORY").ToString.Trim

                If _int_left_menu_id > 0 Then

                    liItems.Text &= String.Format("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>", _str_menu_id, mylistHtml)

                    mylistHtml.Length = 0
                    mylistHtml.Capacity = 0

                End If

                _str_menu_id = "left_menu_" & _int_left_menu_id
                _int_left_menu_id += 1

                If Session("ULEVEL") = 3 And (_str_Category = "HouseKeeping") Then

                Else
                    _list.AddItem(New LeftMenuItem(_str_menu_id, _str_Category, False))
                End If
            End If

            If (Session("ULEVEL").Equals("3") = False) Or (Session("ULEVEL").Equals("3") = True And _str_MenuName.Equals("Sub-Slittting Request - Add") = False) Then
                mylistHtml.AppendFormat("<li class='{2}'><a {3} href='{0}'>{1}</a></li>", GenerateKeywords(dr("MENU_LINK"), Session("gstrUserID"), Session("gstrUserComp"), Session("gettemp"), dr("MENU_NAME")), dr("MENU_NAME"), If(mycounter Mod 2 = 0, "alt", "nor"), "target='page'")
            End If

        Next

        liItems.Text &= String.Format("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>", _str_menu_id, mylistHtml)


        If Now.Hour < 12 Then
            _words = "Good Morning"
        ElseIf Now.Hour >= 12 And Now.Hour <= 17 Then
            _words = "Good Afternoon"
        Else
            _words = "Good Evening"
        End If

    End Sub

    Public Function GenerateKeywords(ByVal URL As String, ByVal ID As String, ByVal Company As String, ByVal Name As String, ByVal System As String) As String
        GenerateKeywords = Server.HtmlEncode(ResolveUrl(URL))
    End Function

    Private Sub systemCheck(ByVal Systemname As String)
        'Validate the system
        Session("system") = 0

        If Session("system") = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Alert", "alert('Invalid System');", True)
            Exit Sub
        End If
    End Sub

End Class
