Imports System.Data
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient
Imports Library.Database

Partial Class Transaction_SlitSeries_Dtl
    Inherits Control.Base
    Public lbl As String()
    Public lbl2 As String()
    Public lblUnit As String()

    Public Sub New()
        MyBase.SetupKey = "PC2_LOTNO"
    End Sub

    Public Overrides Sub BindData()

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfRefNo.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Ref.No")
        rfddlProdLine.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Production Line")
        rfddlPC1Customer.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "PC1 Customer")
        rfddlPC1Mother.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "PC1 Mother")
        rfNoOfSlit.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "No of Slit")
        rfLotNo.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Lot Number")
        rftxtyeardate.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Year/Month")
        rfrdPos.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "Matrix Position")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.IsPostBack) Then

            If Not Request.QueryString("expired") Is Nothing Then

                If Request.QueryString("expired").ToString.Trim.Equals("1") Then

                    Dim _str_msg = "You are not allow to Edit as the Sub-slitting Request for this Reference Number was created " + System.Configuration.ConfigurationManager.AppSettings("Slitting_Series_Expired_Days") + " days before."

                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", "<script type='text/javascript'> alert('" + _str_msg + "');</script>")

                End If

            End If

            Dim dt As DataTable = Library.Database.BLL.SlitSeries.GetDDLData("2")


            ''------------------------refno ------------------------------------

            Dim Company_Code As String = Session("COMPANYCODE")
            Dim dfn As DataTable = Library.Database.BLL.SlitSeries.GetRefByComp(Company_Code)

            ddlRefNo.Items.Clear()
            ddlRefNo.Items.Insert(0, New ListItem("--Select--", ""))

            If dfn.Rows.Count > 0 Then
                For i As Integer = 0 To dfn.Rows.Count - 1
                    ddlRefNo.Items.Add(New ListItem(dfn.Rows(i).Item("REFNO").ToString, dfn.Rows(i).Item("REFNO").ToString))
                Next
            End If

        End If

        Dim hpLink As HyperLink = DirectCast(UCAction.FindControl("hpLink"), HyperLink)
        hpLink.Visible = False

    End Sub



    Protected Sub UCAction_DisplayMode() Handles UCAction.DisplayMode

        Cdisplay.Visible = True
        Cmodify.Visible = False

        Dim _datatable As Data.DataTable = Library.Database.BLL.SlitSeries.GetData(MyBase.Key)

        lblCompCode.Text = _datatable.Rows(0)("COMPANYCODE").ToString
        lblPlanYrMth.Text = _datatable.Rows(0)("PLAN_YEAR_MONTH").ToString
        lblProdLine.Text = _datatable.Rows(0)("PRODLINE_NO").ToString
        lblRefNo.Text = _datatable.Rows(0)("REFNO").ToString

        lblPC1Mother.Text = _datatable.Rows(0)("PC1_MOTHER").ToString
        lblPC2Mother.Text = _datatable.Rows(0)("PC2_MOTHER").ToString
        lblUnitWeightMthr.Text = _datatable.Rows(0)("UNIT_WEIGHT_MOTHER").ToString

        lblPC1Customer.Text = _datatable.Rows(0)("PC1_CUST").ToString
        lblPC2Customer.Text = _datatable.Rows(0)("PC2_CUST").ToString
        lblUnitWeightCust.Text = _datatable.Rows(0)("UNIT_WEIGHT_CUSTOMER").ToString

        lblLotNo.Text = _datatable.Rows(0)("LOTNO").ToString
        lblNumOfSlit.Text = _datatable.Rows(0)("NO_OF_SLIT").ToString

        Dim typeofslit = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(0)
        Dim matrixpos = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(1)
        Dim matrixinc = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(2)

        If (typeofslit = "1") Then
            lblTypeOfSlit.Text = "Sequence"
        ElseIf (typeofslit = "2") Then
            lblTypeOfSlit.Text = "Even"
        ElseIf (typeofslit = "3") Then
            lblTypeOfSlit.Text = "Odd"
        ElseIf (typeofslit = "4") Then
            lblTypeOfSlit.Text = "Matrix (Position: " & matrixpos & " Increment: " & matrixinc & ")"
        End If

        lblLotSlitStatus.Text = _datatable.Rows(0)("LOT_STATUS").ToString


        UCAction.CreatedBy = _datatable.Rows(0)("CREATED_BY").ToString
        UCAction.CreatedDate = IIf(IsDBNull(_datatable.Rows(0)("CREATED_DATE")), "", _datatable.Rows(0)("CREATED_DATE").ToString)
        UCAction.CreatedLoc = _datatable.Rows(0)("CREATED_LOC").ToString
        UCAction.UpdatedBy = _datatable.Rows(0)("UPDATED_BY").ToString
        UCAction.UpdatedDate = _datatable.Rows(0)("UPDATED_DATE").ToString

        UCAction.EditMode = _datatable.Rows(0)("REC_TYPE") <> "5"

    End Sub

    Protected Sub UCAction_ModifyMode() Handles UCAction.ModifyMode
        Cdisplay.Visible = False
        Cmodify.Visible = True

        If MyBase.Action = EnumAction.Edit Then

            If Not (Page.IsPostBack) Then
                Dim _datatable As Data.DataTable = Library.Database.BLL.SlitSeries.GetData(MyBase.Key)

                '====================
                ' Check expired Sliting Series

                If (Date.Now.Subtract(Date.Parse(_datatable.Rows(0)("UPDATED_DATE"))).Days() >= _
                    CInt(System.Configuration.ConfigurationManager.AppSettings("Slitting_Series_Expired_Days"))) Then

                    _datatable.Dispose()

                    Response.Redirect(Request.RawUrl.Replace("action=3", "action=7&expired=1"))
                    Exit Sub
                End If

                '====================

                txtLotNo.Visible = False
                lbLotNo.Visible = True
                txtCompanyCode.Text = _datatable.Rows(0)("COMPANYCODE").ToString

                ''refno
                ddlRefNo.SelectedIndex = ddlRefNo.Items.IndexOf(ddlRefNo.Items.FindByValue(_datatable.Rows(0)("REFNO").ToString))

                If ddlRefNo.SelectedIndex = 0 Then

                    Dim _str_msg = "You are not allow to Edit this Sub-slitting Request."

                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", "<script type='text/javascript'> alert('" + _str_msg + "');history.go(-1);</script>")



                End If

                txtDate.Text = _datatable.Rows(0)("PLAN_YEAR_MONTH").ToString

                '------------------------Production Line ------------------------------------
                ddlProdLine.Visible = True

                ProductNumber_listing(_datatable.Rows(0)("REFNO").ToString)

                ddlProdLine.SelectedIndex = ddlProdLine.Items.IndexOf(ddlProdLine.Items.FindByValue(_datatable.Rows(0)("PRODLINE_NO").ToString))

                '------------------------PC1 Mother ------------------------------------
                ddlPC1Mother.Visible = True

                PC1_Mother_listing()

                ddlPC1Mother.SelectedIndex = ddlPC1Mother.Items.IndexOf(ddlPC1Mother.Items.FindByText(_datatable.Rows(0)("PC1_MOTHER").ToString))

                txtPC2Mother.Text = _datatable.Rows(0)("PC2_MOTHER").ToString
                txtUnitWeightMother.Text = _datatable.Rows(0)("UNIT_WEIGHT_MOTHER").ToString

                '------------------------PC1 Customer ------------------------------------
                ddlPC1Customer.Visible = True

                PC1_Customer_Listing(_datatable.Rows(0)("PC2_MOTHER").ToString)

                ddlPC1Customer.SelectedIndex = ddlPC1Customer.Items.IndexOf(ddlPC1Customer.Items.FindByText(_datatable.Rows(0)("PC1_CUST").ToString))

                txtPC2Customer.Text = _datatable.Rows(0)("PC2_CUST").ToString
                txtUnitWeightCustomer.Text = _datatable.Rows(0)("UNIT_WEIGHT_CUSTOMER").ToString

                lbLotNo.Text = _datatable.Rows(0)("LOTNO").ToString
                txtLotNo.Text = _datatable.Rows(0)("LOTNO").ToString
                txtNoOfSlit.Text = _datatable.Rows(0)("NO_OF_SLIT").ToString
                lbLotSlitStatus.Text = _datatable.Rows(0)("LOT_STATUS").ToString
                'If Not (Page.IsPostBack) Then
                Dim typeofslit = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(0)
                Dim matrixpos = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(1)
                Dim matrixinc = _datatable.Rows(0)("TYPE_OF_SLIT").ToString.Split(",")(2)

                If (typeofslit = "1") Then
                    rdSeq.Checked = True
                ElseIf (typeofslit = "2") Then
                    rdEven.Checked = True
                ElseIf (typeofslit = "3") Then
                    rdOdd.Checked = True
                ElseIf (typeofslit = "4") Then
                    rdMatrix.Checked = True
                    rdPos.Text = matrixpos
                    rdInc.Text = matrixinc
                End If
                UCAction.CreatedBy = _datatable.Rows(0)("CREATED_BY").ToString
                UCAction.CreatedDate = IIf(IsDBNull(_datatable.Rows(0)("CREATED_DATE")), "", _datatable.Rows(0)("CREATED_DATE").ToString)
                UCAction.CreatedLoc = _datatable.Rows(0)("CREATED_LOC").ToString
                UCAction.UpdatedBy = _datatable.Rows(0)("UPDATED_BY").ToString
                UCAction.UpdatedDate = _datatable.Rows(0)("UPDATED_DATE").ToString
                UCAction.UpdatedLoc = _datatable.Rows(0)("UPDATED_LOC").ToString
            End If

        ElseIf MyBase.Action = EnumAction.Add Then
            txtLotNo.Visible = True
            lbLotNo.Visible = False
            lbtxtLotSlitStatus.Visible = False
            Label32.Visible = False
            lbLotSlitStatus.Visible = False

            txtCompanyCode.Text = Session("COMPANYCODE").ToString

            'Get PC2 Mother
            '-----------------------------------------------------------------------------------------------
            Dim dt_sel As DataTable = Library.Database.BLL.SlitSeries.GetPCMOTHER2(ddlRefNo.SelectedValue)

            Dim lblPC = ""

            If dt_sel.Rows.Count > 0 Then

                For i = 0 To dt_sel.Rows.Count - 1
                    lblPC = lblPC & """" & dt_sel.Rows(i)("PC2").ToString() & " - uw: " & dt_sel.Rows(i)("UNIT_WEIGHT").ToString() & """" & ","
                Next
                lblPC = Mid(lblPC, 1, Len(lblPC) - 1)

            End If

            lbl = lblPC.Split(",")

            '------------------------------------------------------------------------------------------------
            'Get PC2 Customer
            '-----------------------------------------------------------------------------------------------
            Dim dt_sel2 As DataTable = Library.Database.BLL.SlitSeries.GetPC2CUST(ddlRefNo.SelectedValue)

            Dim lblPC2 = ""

            If dt_sel2.Rows.Count > 0 Then
                For i = 0 To dt_sel2.Rows.Count - 1
                    lblPC2 = lblPC2 & """" & dt_sel2.Rows(i)("PC2").ToString() & " - uw: " & dt_sel2.Rows(i)("UNIT_WEIGHT").ToString() & """" & ","
                Next
                lblPC2 = Mid(lblPC2, 1, Len(lblPC2) - 1)
            End If

            lbl2 = lblPC2.Split(",")
            '------------------------------------------------------------------------------------------------

        End If

    End Sub

    ''' <summary>
    ''' Handler the add Submit Function
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Protected Sub UCAction_AddAction() Handles UCAction.AddAction, UCAction.EditAction

        Dim cbtypeOfSlit As String = ""
        Dim matrixvariable As String = "0,0"

        If (rdSeq.Checked) Then
            cbtypeOfSlit = "1," & matrixvariable
        ElseIf (rdOdd.Checked) Then
            cbtypeOfSlit = "3," & matrixvariable
        ElseIf (rdEven.Checked) Then
            cbtypeOfSlit = "2," & matrixvariable
        ElseIf (rdMatrix.Checked) Then
            If (rdInc.Text Is Nothing Or rdInc.Text = "") Then
                rdInc.Text = 0
            End If
            matrixvariable = rdPos.Text & "," & rdInc.Text
            cbtypeOfSlit = "4," & matrixvariable
        End If

        Dim _temp As String = ""
        Dim Update_Status As String = ""

        If MyBase.Action = EnumAction.Add Then
            Update_Status = Library.Database.BLL.CHECK_LOTNO_DUP.check_lotno_dup(txtCompanyCode.Text.Trim, txtLotNo.Text.Trim)
        End If

        If Update_Status = "1" Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "func", "showDialogue()", True)

        Else

            'Non duplicate

            If MyBase.Action = EnumAction.Edit Then

                _temp = Library.Database.BLL.SlitSeries.Maint(MyBase.Key, txtCompanyCode.Text.Trim, ddlRefNo.SelectedValue, lbLotNo.Text.Trim, ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text, _
                     ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue, _
                     txtNoOfSlit.Text.Trim, txtDate.Text, cbtypeOfSlit, _
                     CInt(MyBase.Action))

                LabelEdit.Text = _temp

                If _temp = "1" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Successfully, MyBase.Action.ToString))
                    Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
                Else
                    If _temp = "0" Then
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
                    Else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                    End If
                End If

            ElseIf MyBase.Action = EnumAction.Add Then

                _temp = Library.Database.BLL.SlitSeries.Maint("0", txtCompanyCode.Text.Trim, ddlRefNo.SelectedValue, txtLotNo.Text.Trim, ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text, _
                    ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue, _
                    txtNoOfSlit.Text.Trim, txtDate.Text, cbtypeOfSlit, _
                    CInt(MyBase.Action))
            End If

            If MyBase.Action = EnumAction.Edit Then

            Else

                If _temp = "1" Then
                    'Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Successfully, MyBase.Action.ToString))
                    ddlPC1Customer.SelectedIndex = 0
                    txtPC2Customer.Text = ""
                    txtUnitWeightCustomer.Text = ""
                    txtNoOfSlit.Text = ""
                    rdPos.Text = ""
                    rdInc.Text = ""
                Else
                    If _temp = "0" Then
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
                    Else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                    End If
                End If

            End If



        End If



    End Sub

    Protected Sub UCAction_AddResetAction() Handles UCAction.AddResetAction, UCAction.EditResetAction
        Response.Redirect(Request.RawUrl)
    End Sub

    ''' <summary>
    ''' Delete Action
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Protected Sub UCAction_DeleteAction() Handles UCAction.DeleteAction
        Dim _temp As String = Library.Database.BLL.SlitSeries.Maint(MyBase.Key, lblCompCode.Text, ddlRefNo.SelectedValue, lblLotNo.Text, lblProdLine.Text, lblPC1Mother.Text, _
                                                                           lblPC2Mother.Text, lblPC1Customer.Text, lblPC2Customer.Text, _
                                                                           lblLotNo.Text, lblNumOfSlit.Text, lblTypeOfSlit.Text, CInt(Library.Root.Control.Base.EnumAction.Delete))

        Dim script As String = "<script type='text/javascript'> alert('" + _temp + "');</script>"

        If _temp = "1" Then
            Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
        Else
            If _temp = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
            End If
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim cbtypeOfSlit As String = ""
        Dim matrixvariable As String = "0,0"

        If (rdSeq.Checked) Then
            cbtypeOfSlit = "1," & matrixvariable
        ElseIf (rdOdd.Checked) Then
            cbtypeOfSlit = "3," & matrixvariable
        ElseIf (rdEven.Checked) Then
            cbtypeOfSlit = "2," & matrixvariable
        ElseIf (rdMatrix.Checked) Then
            If (rdInc.Text Is Nothing Or rdInc.Text = "") Then
                rdInc.Text = 0
            End If
            matrixvariable = rdPos.Text & "," & rdInc.Text
            cbtypeOfSlit = "4," & matrixvariable
        End If

        Dim _temp As String = ""
        If MyBase.Action = EnumAction.Edit Then

            _temp = Library.Database.BLL.SlitSeries.Maint(MyBase.Key, txtCompanyCode.Text.Trim, ddlRefNo.SelectedValue, lbLotNo.Text.Trim, ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text, _
                    ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue, _
                    txtNoOfSlit.Text.Trim, txtDate.Text, cbtypeOfSlit.ToString, _
                    CInt(MyBase.Action))

            LabelEdit.Text = _temp
            If _temp = "1" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Successfully, MyBase.Action.ToString))
                Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
            Else
                If _temp = "0" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
                Else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                End If
            End If

        ElseIf MyBase.Action = EnumAction.Add Then
            ' User click to proceed with duplicate
            If (inpHide.Value = "1") Then
                _temp = Library.Database.BLL.SlitSeries.Maint("0", txtCompanyCode.Text.Trim, ddlRefNo.SelectedValue, txtLotNo.Text.Trim, ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text, _
                   ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue, _
                   txtNoOfSlit.Text.Trim, txtDate.Text, cbtypeOfSlit.ToString, _
                   CInt(MyBase.Action))
            Else
                Exit Sub
            End If
        End If


        If MyBase.Action = EnumAction.Edit Then

        Else
            ' User click to proceed with duplicate, pop up error msg
            If (inpHide.Value = "1") Then
                If _temp = "1" Then
                    'Response.Redirect(MyBase.GetUrl(EnumAction.None), False)
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Successfully, MyBase.Action.ToString))
                    ddlPC1Customer.SelectedIndex = 0
                    txtPC2Customer.Text = ""
                    txtUnitWeightCustomer.Text = ""
                    txtNoOfSlit.Text = ""
                    rdPos.Text = ""
                    rdInc.Text = ""
                Else
                    If _temp = "0" Then
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, MyBase.Action.ToString))
                    Else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                    End If
                End If
            Else
                Exit Sub
            End If
        End If

    End Sub

    Private Function filter_duplicate_value_4_list(ByVal pobj_data As DataTable, ByVal pstr_fieldName As String) As String()

        Dim _arr_str(pobj_data.Rows.Count) As String
        Dim _bol_existed As Boolean = False
        Dim _int_iArr As Integer = 0
        Dim _str_Found As String = ""
        Dim _int_iCaptured As Integer = 0

        For _int_iMain As Integer = 0 To (pobj_data.Rows.Count - 1)

            _bol_existed = False

            _str_Found = pobj_data.Rows(_int_iMain)(pstr_fieldName).ToString.Trim

            For _int_iArr = 0 To (_arr_str.GetLength(0) - 1)

                If _str_Found.Equals(_arr_str(_int_iArr)) = True Then

                    _bol_existed = True

                    Exit For

                End If

            Next _int_iArr

            If _bol_existed = False Then

                _arr_str(_int_iCaptured) = _str_Found
                _int_iCaptured += 1

            End If

        Next _int_iMain

        Return _arr_str

    End Function

    Private Sub ProductNumber_listing(ByVal pstr_refNo As String)

        ddlProdLine.Visible = True
        Dim _dtprodline As Data.DataTable = Library.Database.BLL.SlitSeries.GetPRODLINE2(pstr_refNo)

        ddlProdLine.Items.Clear()
        ddlProdLine.Items.Insert(0, New ListItem("--Select--", ""))

        If _dtprodline.Rows.Count > 0 Then

            Dim _arr_str_PRODLINE_NO() As String = filter_duplicate_value_4_list(_dtprodline, "PRODLINE_NO")


            For Each _str_captured As String In _arr_str_PRODLINE_NO

                If _str_captured Is Nothing Then

                    Exit For

                End If

                If _str_captured.Equals("") = False Then

                    ddlProdLine.Items.Add(New ListItem(_str_captured, _str_captured))

                Else
                    Exit For
                End If
            Next
        End If

        ddlPC1Mother.Visible = True
        ddlPC1Customer.Visible = True

        ddlPC1Mother.Items.Clear()
        ddlPC1Mother.Items.Add(New ListItem("--Select--", ""))

        ddlPC1Customer.Items.Clear()
        ddlPC1Customer.Items.Add(New ListItem("--Select--", ""))


    End Sub

    Protected Sub ddlRefNo_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRefNo.SelectedIndexChanged

        ProductNumber_listing(ddlRefNo.SelectedValue)


        txtDate.Text = ""
        txtPC2Mother.Text = ""
        txtPC2Customer.Text = ""
        txtUnitWeightCustomer.Text = ""
        txtUnitWeightMother.Text = ""
        txtNoOfSlit.Text = ""
        txtLotNo.Text = ""

    End Sub

    Private Sub PC1_Mother_listing()

        Dim _pc1 As Data.DataTable = Library.Database.BLL.SlitSeries.GetDDLData2_Rev01(ddlRefNo.SelectedValue, ddlProdLine.SelectedValue)

        ddlPC1Mother.Items.Clear()
        ddlPC1Mother.Items.Add(New ListItem("--Select--", ""))

        ddlPC1Mother.Visible = True

        If _pc1.Rows.Count > 0 Then

            Dim _arr_str_PC1_MOTHER() As String = filter_duplicate_value_4_list(_pc1, "PC1_MOTHER")

            For Each _str_captured As String In _arr_str_PC1_MOTHER

                If _str_captured Is Nothing Then

                    Exit For

                End If

                If _str_captured.Equals("") = False Then

                    ddlPC1Mother.Items.Add(New ListItem(_str_captured, _str_captured))

                Else
                    Exit For
                End If
            Next
        End If

        _pc1.Dispose()

    End Sub

    Protected Sub ddlProdLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProdLine.SelectedIndexChanged

        PC1_Mother_listing()



        txtPC2Mother.Text = ""
        txtPC2Customer.Text = ""
        txtUnitWeightCustomer.Text = ""
        txtUnitWeightMother.Text = ""
        ddlPC1Customer.SelectedIndex = 0
        txtNoOfSlit.Text = ""
        txtLotNo.Text = ""
        rdPos.Text = ""
        rdInc.Text = ""

    End Sub


    Private Sub PC1_Customer_Listing(ByVal pstr_PC2Mother As String)

        Dim dtmaxRev As DataTable = Library.Database.BLL.SubSlitRequest.chkRefNo(ddlRefNo.SelectedValue)
        Dim IdSubSlit As String = String.Empty

        If dtmaxRev.Rows.Count > 0 Then

            IdSubSlit = dtmaxRev.Rows(0)("ID_SUBSLIT_REQUEST").ToString

        End If

        dtmaxRev.Dispose()

        Dim _pc1cust As Data.DataTable = Library.Database.BLL.SlitSeries.GetDDLPC1Cust_Rev01(ddlRefNo.SelectedValue, IdSubSlit, ddlProdLine.SelectedValue, ddlPC1Mother.SelectedValue, pstr_PC2Mother)

        ddlPC1Customer.Items.Clear()
        ddlPC1Customer.Items.Add(New ListItem("--Select--", ""))

        If _pc1cust.Rows.Count > 0 Then

            Dim _arr_str_PC1() As String = filter_duplicate_value_4_list(_pc1cust, "PC1")

            For Each _str_captured As String In _arr_str_PC1

                If _str_captured Is Nothing Then

                    Exit For

                End If

                If _str_captured.Equals("") = False Then

                    ddlPC1Customer.Items.Add(New ListItem(_str_captured, _str_captured))

                Else
                    Exit For
                End If
            Next
        End If

        _pc1cust.Dispose()

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        If String.IsNullOrEmpty(hdn_PC2_Mother.Value) = False Then
            txtPC2Mother.Text = hdn_PC2_Mother.Value
            txtUnitWeightMother.Text = hdn_Unit_Weight_Mother.Value
        Else
            hdn_PC1_Customer.Value = ddlPC1Customer.SelectedIndex
        End If




        If String.IsNullOrEmpty(hdn_PC1_Customer.Value) = False Then
            txtPC2Customer.Text = hdn_PC2_Customer.Value
            ddlPC1Customer.SelectedIndex = Convert.ToInt32(hdn_PC1_Customer.Value)
            txtUnitWeightCustomer.Text = hdn_UnitWeightCustomer.Value
        Else
            PC1_Customer_Listing(hdn_PC2_Mother.Value)

        End If

    End Sub

    Protected Sub ddlPC1Customer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPC1Customer.SelectedIndexChanged
        hdn_PC1_Customer.Value = ddlPC1Customer.SelectedIndex

        txtPC2Customer.Text = ""
        txtUnitWeightCustomer.Text = ""
        txtNoOfSlit.Text = ""
        rdPos.Text = ""
        rdInc.Text = ""
    End Sub

    Protected Sub ddlPC1Mother_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPC1Mother.SelectedIndexChanged
        txtPC2Mother.Text = ""
        txtUnitWeightMother.Text = ""
        ddlPC1Customer.SelectedIndex = 0
        hdn_PC1_Customer.Value = ""
        txtPC2Customer.Text = ""
        hdn_PC2_Customer.Value = ""
        txtUnitWeightCustomer.Text = ""
        hdn_UnitWeightCustomer.Value = ""
        txtNoOfSlit.Text = ""
        rdPos.Text = ""
        rdInc.Text = ""
    End Sub


End Class

