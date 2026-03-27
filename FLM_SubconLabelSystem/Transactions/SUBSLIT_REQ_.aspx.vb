Imports System.Data
Imports System.Windows.Forms

Partial Class Transactions_SubSlitRequest
    Inherits System.Web.UI.Page
    Dim Trans_Date As String = ""
    Dim ETD_Date As String = ""
    Dim ETA_Date As String = ""
    Dim FirstTime_Flag As String = "Y"
    Dim Init_FirstTime_Flag As String = "Y"
    Dim Edit_Flag As String = "N"
    Public lbl As String()
    Public lbl2 As String()
    Public lbl3 As String()
    Public lblchild As String()
    Public lblchild2 As String()

    Private dtPC1 As DataTable
    Private dtPC2 As DataTable
    Private dtProdLine As DataTable
    Private dtPC1Child As DataTable
    Private dtPC2Child As DataTable

    Private dblFooterAmount As Double = 0
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init



        Dim _dtCompName As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetDLLData("CompanyCode", "")

        ddlCompCode.Items.Clear()
        ddlCompCode.Items.Add(New ListItem("--Select--", 0))
        For i = 0 To _dtCompName.Rows.Count - 1
            ddlCompCode.Items.Add(New ListItem(_dtCompName.Rows(i)("CompanyCode").ToString, _dtCompName.Rows(i)("ID_MM_COMPANY").ToString))
        Next
        ddlCompCode.DataBind()

        If Not ViewState("CompCode") Is Nothing Then

            ddlCompCode.SelectedIndex = Convert.ToInt32(ViewState("CompCode"))

        End If

        If ViewState("PRODLINE") Is Nothing Then

            dtProdLine = Library.Database.BLL.SubSlitRequest.GetDLLData("PRODLINE", "")
            lbl = (From row In dtProdLine Select colB = Chr(34) & row("PRODLINE_NO").ToString & Chr(34)).ToArray

            ViewState("lbl") = lbl
            ViewState("PRODLINE") = dtProdLine

        Else

            dtProdLine = ViewState("PRODLINE")
            lbl = ViewState("lbl")

        End If

        If ViewState("PC1") Is Nothing Then

            dtPC1 = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "")
            lbl2 = (From row In dtPC1 Select colB = Chr(34) & row("PC1").ToString & Chr(34)).ToArray
            ViewState("PC1") = dtPC1
            ViewState("lbl2") = lbl2

        Else

            dtPC1 = ViewState("PC1")
            lbl2 = ViewState("lbl2")

        End If

        If ViewState("PC2") Is Nothing Then

            dtPC2 = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "")
            lbl3 = (From row In dtPC2 Select colB = Chr(34) & row("PC2").ToString & Chr(34)).ToArray
            ViewState("PC2") = dtPC2
            ViewState("lbl3") = lbl3

        Else

            dtPC2 = ViewState("PC2")
            lbl3 = ViewState("lbl3")

        End If

        If ViewState("PC1_1_Child") Is Nothing Then

            dtPC1Child = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "")
            lblchild = (From row In dtPC1Child Select colB = Chr(34) & row("PC1").ToString & Chr(34)).ToArray

            ViewState("PC1_1_Child") = dtPC1Child
            ViewState("lblchild") = lblchild
        Else

            dtPC1Child = ViewState("PC1_1_Child")
            lblchild = ViewState("lblchild")

        End If

        If ViewState("PC1_2_Child") Is Nothing Then


            dtPC2Child = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "")
            lblchild2 = (From row In dtPC2Child Select colB = Chr(34) & row("PC2").ToString & Chr(34)).ToArray

            ViewState("PC1_2_Child") = dtPC2Child
            ViewState("lblchild2") = lblchild2
        Else

            dtPC2Child = ViewState("PC1_2_Child")
            lblchild2 = ViewState("lblchild2")

        End If

        txtRefNo.ReadOnly = False

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtDate.Attributes.Add("readonly", "readonly")
        txtETD.Attributes.Add("readonly", "readonly")
        txtETA.Attributes.Add("readonly", "readonly")

        If Request.QueryString("itm1") Is Nothing And Request.QueryString("itm2") Is Nothing Then

            If Not (Page.IsPostBack) Then
                pnlChild.Visible = False
                pnlList.Visible = False
                btnEdit.Visible = False
                btnDelete.Visible = False
                Submit_Button.Visible = False
                Cancel_Button.Visible = False
            End If

        Else
            'Dim r_Refno As String = Request.QueryString("itm1").Trim
            Dim decodedString As String = HttpUtility.HtmlDecode(Request.QueryString("itm1").Trim) 'HANA 11/11/2024 Decode string

            Dim r_Refno As String = decodedString
            Dim r_ID_SSR As Integer = Request.QueryString("itm2").Trim
            Dim dt_SSR As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno, r_ID_SSR)

            If Not (Page.IsPostBack) Then
                ddlCompCode.SelectedValue = dt_SSR.Rows(0)("COMPTOID").ToString()
                txtRefNo.Text = dt_SSR.Rows(0)("REFNO").ToString()
                txtDate.Text = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("dd/MM/yyyy")
            End If

            lblRev.Text = dt_SSR.Rows(0)("REVISIONCOUNT").ToString()
            lblVenStat.Text = dt_SSR.Rows(0)("VENDOR_STATUS").ToString()

            If Not (Page.IsPostBack) Then
                pnlChild.Visible = False
            End If

            pnlList.Visible = True
            btnEdit.Visible = True
            btnDelete.Visible = True
            btnNext.Visible = True
            Submit_Button.Visible = True
            Cancel_Button.Visible = True

            Display_SSRListing()

        End If

        Dim _dtGetDept As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetUserData(Session("USERID"))
        lblDept.Text = _dtGetDept.Rows(0)("DEPARTMENT").ToString
        lblBy.Text = _dtGetDept.Rows(0)("NAME").ToString

        If ViewState("dtWBlank") IsNot Nothing Then
            grdList.DataSource = ViewState("dtWBlank")
            grdList.DataBind()
        End If

        If Session("CheckBoxArray") IsNot Nothing Then

            Dim CheckBoxArray As ArrayList = _
            DirectCast(Session("CheckBoxArray"), ArrayList)

            Dim PC2MotherArray As ArrayList = _
            DirectCast(Session("PC2MotherArray"), ArrayList)

            Dim h_PC2Mother As HiddenField
            Dim h_PC2Mother_Value As String

            Dim h_PC1Mother As HiddenField
            Dim h_PC1Mother_Value As String

            Dim h_ProdLineNo As HiddenField
            Dim h_ProdLineNo_Value As String

            Dim h_SeqMother As HiddenField
            Dim h_SeqMother_Value As String

            For i As Integer = 0 To grdList.Rows.Count - 1

                If grdList.Rows(i).RowType = DataControlRowType.DataRow Then

                    Dim CheckBoxIndex As Integer = grdList.PageSize * grdList.PageIndex + (i + 1)

                    h_PC2Mother = CType(grdList.Rows(i).FindControl("HiddenField1"), HiddenField)
                    h_PC2Mother_Value = h_PC2Mother.Value
                    h_PC1Mother = CType(grdList.Rows(i).FindControl("HiddenField2"), HiddenField)
                    h_PC1Mother_Value = h_PC1Mother.Value
                    h_ProdLineNo = CType(grdList.Rows(i).FindControl("HiddenField3"), HiddenField)
                    h_ProdLineNo_Value = h_ProdLineNo.Value
                    h_SeqMother = CType(grdList.Rows(i).FindControl("HiddenField4"), HiddenField)
                    h_SeqMother_Value = h_SeqMother.Value

                    Dim chk As System.Web.UI.WebControls.CheckBox = _
                        DirectCast(grdList.Rows(i) _
                        .FindControl("RadioButton1"), System.Web.UI.WebControls.CheckBox)

                    If chk.Visible = True Then
                        If CheckBoxArray.IndexOf(CheckBoxIndex) <> -1 And h_PC2Mother_Value = Session("PC2Mother") And h_PC1Mother_Value = Session("PC1Mother") And h_ProdLineNo_Value = Session("ProdLineNo") And h_SeqMother_Value = Session("SeqMother") Then
                            chk.Checked = True
                        Else
                            chk.Checked = False
                        End If
                    End If

                End If
            Next
        End If

    End Sub

    Protected Sub calTotalWeight()
        Dim Tot_Weight As Decimal = 0.0
        If lblUnitWeight.Text <> "" And txtQty.Text <> "" And IsNumeric(txtQty.Text) And IsNumeric(lblUnitWeight.Text) Then
            lblTotWeight.Text = txtQty.Text * lblUnitWeight.Text
            lblTotWeight.Text = Math.Round(CDbl(lblTotWeight.Text), 1)
            If lblTotWeight.Text.IndexOf(".") < 0 Then
                lblTotWeight.Text = lblTotWeight.Text & ".0"
                Tot_Weight = lblTotWeight.Text
                lblTotWeight.Text = Tot_Weight.ToString("#,###,###,##0.0")
            End If
        Else
            lblTotWeight.Text = "0.0"
        End If

    End Sub

    Public Sub Calculate()

        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2ID(ddlPC2.Text)
        Dim Unit_Weight As Decimal = 0.0

        If _dtPC2id.Rows.Count > 0 Then
            Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString
            Dim _dtGetUWeight As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID)
            lblUnitWeight.Text = _dtGetUWeight.Rows(0)("UNIT_WEIGHT")
            Unit_Weight = lblUnitWeight.Text
            lblUnitWeight.Text = Unit_Weight.ToString("#,###,###,##0.0")
        Else
            lblUnitWeight.Text = "0.0"
        End If

        If txtQty.Text.Equals("0") = True Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter Qty more than 0.")
            Exit Sub
        End If
        calTotalWeight()

        Dim rowIndex As Integer = 0
        Dim momSubslit As Decimal = 0.0
        momSubslit = lblTotWeight.Text

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                    dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
                    dt.Rows(i)("C_QTY") = txtQtyC.Text
                    dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    dt.Rows(i)("REMARK") = txtRemarkC.Text

                    If txtQtyC.Text = "" Then
                        dt.Rows(i)("C_QTY") = DBNull.Value
                    Else
                        dt.Rows(i)("C_QTY") = txtQtyC.Text
                    End If

                    If lblUnitWeightC.Text = "" Then
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    End If

                    If lblTotWeightC.Text = "" Then
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    End If

                    If txtRemarkC.Text = "" Then
                        dt.Rows(i)("REMARK") = DBNull.Value
                    Else
                        dt.Rows(i)("REMARK") = txtRemarkC.Text
                    End If

                    'set unit weight

                    If ddlPC2Child.Text <> "" Then
                        Dim _dtPC2Childid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2ID(ddlPC2Child.Text)
                        Dim PC2ChildID = _dtPC2Childid.Rows(0)("ID_MM_PC2").ToString
                        Dim _dtGetUWeightC As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ChildID)
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows(0)("UNIT_WEIGHT").ToString
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    Else
                        lblUnitWeightC.Text = ""
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    End If

                    'set total weight child
                    If lblUnitWeightC.Text <> "" And txtQtyC.Text <> "" And IsNumeric(txtQtyC.Text) And IsNumeric(lblUnitWeightC.Text) Then
                        lblTotWeightC.Text = txtQtyC.Text * lblUnitWeightC.Text
                        lblTotWeightC.Text = Math.Round(CDbl(lblTotWeightC.Text), 1)
                        If lblTotWeightC.Text.IndexOf(".") < 0 Then
                            lblTotWeightC.Text = lblTotWeightC.Text & ".0"
                        End If
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    Else
                        lblTotWeightC.Text = ""
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    End If

                    'set subslit mother
                    If lblTotWeight.Text <> "" And lblTotWeightC.Text <> "" And IsNumeric(lblTotWeight.Text) And IsNumeric(lblTotWeightC.Text) Then
                        momSubslit = (Convert.ToDouble(momSubslit) - Convert.ToDouble(lblTotWeightC.Text)).ToString("0.0")
                    End If
                    rowIndex += 1

                Next
                ViewState("CurrentTable") = dt
            End If
        End If
        'set ddl for all remove this ddl.selected value
        lblSubSlit.Text = momSubslit.ToString("#,###,###,##0.0")
        SetPreviousData()


        dtProdLine = ViewState("PRODLINE")
        lbl = ViewState("lbl")
        dtPC1 = ViewState("PC1")
        lbl2 = ViewState("lbl2")
        dtPC2 = ViewState("PC2")
        lbl3 = ViewState("lbl3")
        dtPC1Child = ViewState("PC1_1_Child")
        lblchild = ViewState("lblchild")
        dtPC2Child = ViewState("PC1_2_Child")
        lblchild2 = ViewState("lblchild2")

    End Sub

    Public Sub ChildCalculate()

        Dim rowIndex As Integer = 0
        Dim momSubslit As Decimal = 0.0
        momSubslit = lblTotWeight.Text


        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                    dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
                    dt.Rows(i)("C_QTY") = txtQtyC.Text
                    dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    dt.Rows(i)("REMARK") = txtRemarkC.Text


                    If txtQtyC.Text = "" Then
                        dt.Rows(i)("C_QTY") = DBNull.Value
                    Else
                        dt.Rows(i)("C_QTY") = txtQtyC.Text
                    End If

                    If lblUnitWeightC.Text = "" Then
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    End If

                    If lblTotWeightC.Text = "" Then
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    End If

                    If txtRemarkC.Text = "" Then
                        dt.Rows(i)("REMARK") = DBNull.Value
                    Else
                        dt.Rows(i)("REMARK") = txtRemarkC.Text
                    End If

                    'set unit weight

                    If ddlPC2Child.Text <> "" Then

                        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text)

                        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

                        Dim _dtGetUWeightC As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID)
                        Dim Unit_Weight As Decimal = 0.0
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows(0)("UNIT_WEIGHT").ToString
                        Unit_Weight = lblUnitWeightC.Text
                        lblUnitWeightC.Text = Unit_Weight.ToString("#,###,###,##0.0")
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    Else
                        lblUnitWeightC.Text = ""
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    End If

                    'set total weight child
                    If lblUnitWeightC.Text <> "" And txtQtyC.Text <> "" And IsNumeric(txtQtyC.Text) And IsNumeric(lblUnitWeightC.Text) Then
                        Dim Tot_Weight As Decimal = 0.0
                        lblTotWeightC.Text = txtQtyC.Text * lblUnitWeightC.Text
                        lblTotWeightC.Text = Math.Round(CDbl(lblTotWeightC.Text), 1)
                        If lblTotWeightC.Text.IndexOf(".") < 0 Then
                            lblTotWeightC.Text = lblTotWeightC.Text & ".0"
                        End If
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                        Tot_Weight = lblTotWeightC.Text
                        lblTotWeightC.Text = Tot_Weight.ToString("#,###,###,##0.0")
                    Else
                        lblTotWeightC.Text = ""
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    End If

                    'set subslit mother
                    If lblTotWeight.Text <> "" And lblTotWeightC.Text <> "" And IsNumeric(lblTotWeight.Text) And IsNumeric(lblTotWeightC.Text) Then
                        momSubslit = (Convert.ToDouble(momSubslit) - Convert.ToDouble(lblTotWeightC.Text)).ToString("0.0")
                    End If
                    rowIndex += 1

                Next
                ViewState("CurrentTable") = dt
            End If
        End If
        'set ddl for all remove this ddl.selected value
        lblSubSlit.Text = momSubslit.ToString("#,###,###,##0.0")

        SetPreviousData()

    End Sub


    Protected Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Calculate()

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.RawUrl)
        btnNext.Visible = True
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click

        If pnlChild.Visible = False Then
            If ddlCompCode.SelectedValue = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select To which Company.")
                Exit Sub
            End If
            If txtRefNo.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Insert Ref No.")
                Exit Sub
            End If
            If txtDate.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select Date.")
                Exit Sub
            End If
            If ddlProdLine.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select Production Line.")
                Exit Sub
            End If
            If ddlPC1.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC1.")
                Exit Sub
            End If
            If ddlPC2.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC2.")
                Exit Sub
            Else
 
            End If

            Dim _chk = Library.Database.BLL.SubSlitRequest.Chk_next(ddlProdLine.Text, ddlPC1.Text)

            If _chk = 0 Then
                Dim _dtPRODLINEid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetProdLineID(ddlProdLine.Text)
                Dim _dtPC1id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1.Text)
                Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2.Text)

                Dim PRODLINEID = _dtPRODLINEid.Rows(0)("ID_MM_PRODLINE").ToString
                Dim PC1ID = _dtPC1id.Rows(0)("ID_MM_PC1").ToString
                Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

                'Dim dtPC2Mother As DataTable = Library.Database.BLL.SubSlitRequest.chkPC2Mother(txtRefNo.Text, PC2ID, ddlProdLine.Text, ddlPC1.Text)

                'If dtPC2Mother.Rows.Count > 0 Then
                '    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This PC2 Mother " + "is already existed. Please re-enter new PC2 Mother")
                '    Exit Sub
                'End If

                If txtQty.Text = "" Or IsDBNull(txtQty.Text) Or txtQty.Text = "0" Or IsNumeric(txtQty.Text) = False Then

                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter Qty more than 0.")
                    Exit Sub
                End If
                If txtETD.Text = "" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select ETD PFR Date.")
                    Exit Sub
                End If
                If txtETA.Text = "" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select ETA SUB-SLIT CONTRACTOR.")
                    Exit Sub
                End If

                pnlChild.Visible = True
                btnAddRow_Click()
            ElseIf _chk = 1 Then
                Dim message As String = "Invalid PC1 Mother"
                Dim Script As String = "<script type='text/javascript'> alert('" + message + "');</script>"
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", Script)
                ddlPC1.Text = ""
            ElseIf _chk = 2 Then
                Dim message As String = "Invalid Production Line No"
                Dim Script As String = "<script type='text/javascript'> alert('" + message + "');</script>"
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", Script)
            Else
                Dim message As String = "Invalid Production Line No and PC1 Mother"
                Dim Script As String = "<script type='text/javascript'> alert('" + message + "');</script>"
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", Script)
            End If
        End If
    End Sub

    Protected Sub btnAddRow_Click() Handles btnAddRow.Click
        Dim name As String = String.Empty
        Dim hyp As String = String.Empty

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        If ViewState("CurrentTable") Is Nothing Then
            dt.Columns.Add(New DataColumn("IMAGE", GetType(String)))
            dt.Columns.Add(New DataColumn("ID_SUBSLIT_REQUEST_CHILD", GetType(String)))
            dt.Columns.Add(New DataColumn("SUBSLIT_REQ_MOTHER_SEQNO", GetType(String)))
            dt.Columns.Add(New DataColumn("PC1_CUST", GetType(String)))
            dt.Columns.Add(New DataColumn("PC2_CUST", GetType(String)))
            dt.Columns.Add(New DataColumn("C_QTY", GetType(String)))
            dt.Columns.Add(New DataColumn("C_WEIGHT", GetType(String)))
            dt.Columns.Add(New DataColumn("C_TOTAL_WEIGHT", GetType(String)))
            dt.Columns.Add(New DataColumn("REMARK", GetType(String)))

        Else

            If PC2ChildCheck() = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "PC2 Customer cannot duplicate")
                Exit Sub
            End If

            dt = DirectCast(ViewState("CurrentTable"), DataTable)

            Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(dt.Rows.Count - 1).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
            Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(dt.Rows.Count - 1).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
            Dim txtQtyC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(dt.Rows.Count - 1).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
            Dim lblUnitWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(dt.Rows.Count - 1).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
            Dim lblTotWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(dt.Rows.Count - 1).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
            Dim txtRemarkC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(dt.Rows.Count - 1).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)


            If txtQtyC.Text = "" Or txtQtyC.Text = "0" Or IsNumeric(txtQtyC.Text) = False Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter Qty more than 0.")
                Exit Sub
            End If

            dt.Rows(dt.Rows.Count - 1)("C_QTY") = txtQtyC.Text
            dt.Rows(dt.Rows.Count - 1)("C_WEIGHT") = lblUnitWeightC.Text
            dt.Rows(dt.Rows.Count - 1)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
            dt.Rows(dt.Rows.Count - 1)("REMARK") = txtRemarkC.Text

            If ddlPC1Child.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC1 Customer Before Add New")
                Exit Sub
            Else
                Dim dtPC1child As DataTable = Library.Database.BLL.SubSlitRequest.Chk_label(2, "", ddlPC1Child.Text, "")
                If dtPC1child.Rows.Count = 0 Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Invalid PC1 Customer")
                    ddlPC1Child.Text = ""
                    Exit Sub
                End If
            End If

            If ddlPC2Child.Text = "" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC2 Customer Before Add New")
                Exit Sub
            End If


            dt.Rows(dt.Rows.Count - 1)("PC1_CUST") = ddlPC1Child.Text
            dt.Rows(dt.Rows.Count - 1)("PC2_CUST") = ddlPC2Child.Text

        End If

        dr = dt.NewRow()

        dt.Rows.Add(dr)

        ViewState("CurrentTable") = dt
        grdChild.DataSource = dt
        grdChild.DataBind()

        SetPreviousData()

    End Sub

    Private Function PC2ChildCheck() As String
        PC2ChildCheck = "1"
        Dim dtchk As New DataTable()

        dtchk = DirectCast(ViewState("CurrentTable"), DataTable)

        If dtchk IsNot Nothing Then
            For i = 0 To dtchk.Rows.Count - 1
                If dtchk.Rows(i)("PC2_CUST").Equals(DBNull.Value) = True Then
                    Exit For
                End If
                Dim _ddlPC2Val1 As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)

                Dim dtchk2 As New DataTable()
                dtchk2 = DirectCast(ViewState("CurrentTable"), DataTable)
                For j = 0 To dtchk.Rows.Count - 1
                    If dtchk2.Rows(j)("PC2_CUST").Equals(DBNull.Value) = True Then
                        Exit For
                    End If
                    Dim _ddlPC2Val2 As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(j).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    If i <> j Then
                        If _ddlPC2Val1.Text = _ddlPC2Val2.Text Then
                            PC2ChildCheck = "0"
                        End If
                    End If

                Next
            Next
        End If

    End Function

    Private Function PC2ChildValQty() As String
        PC2ChildValQty = "1"
        Dim dtchk As New DataTable()

        dtchk = DirectCast(ViewState("CurrentTable"), DataTable)

        If dtchk IsNot Nothing Then
            If dtchk.Rows.Count > 0 Then
                For i = 0 To dtchk.Rows.Count - 1
                    If dtchk.Rows(i)("C_QTY").Equals("0") = True Or IsDBNull(dtchk.Rows(i)("C_QTY")) Or IsNumeric(dtchk.Rows(i)("C_QTY")) = False Then
                        PC2ChildValQty = "0"
                    End If
                Next
            Else
                PC2ChildValQty = "0"
            End If
        End If

    End Function

    Private Function PC1ChildVal() As String
        PC1ChildVal = "1"
        Dim dtPC1 As New DataTable()

        dtPC1 = DirectCast(ViewState("CurrentTable"), DataTable)

        If dtPC1 IsNot Nothing Then
            If dtPC1.Rows.Count > 0 Then
                For i = 0 To dtPC1.Rows.Count - 1
                    If dtPC1.Rows(i)("PC1_CUST").ToString() = "" Then
                        PC1ChildVal = "0"
                    End If
                Next
            Else
                PC1ChildVal = "0"
            End If
        End If

    End Function

    Private Function PC2ChildVal() As String
        PC2ChildVal = "1"
        Dim dtPC2 As New DataTable()

        dtPC2 = DirectCast(ViewState("CurrentTable"), DataTable)

        If dtPC2 IsNot Nothing Then
            If dtPC2.Rows.Count > 0 Then
                For i = 0 To dtPC2.Rows.Count - 1
                    If dtPC2.Rows(i)("PC2_CUST").ToString() = "" Then
                        PC2ChildVal = "0"
                    End If
                Next
            Else
                PC2ChildVal = "0"
            End If
        End If

    End Function

    Protected Sub setChildDdl(ByVal j As Integer)

        Dim _dtPC1Child As DataTable = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "")
        Dim _dtPC2Child As DataTable = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "")

    End Sub

    Protected Sub ddlPC2Child_SelectedIndexChanged() 'use to save row into current table
        Dim rowIndex As Integer = 0
        Dim momSubslit As String = ""
        momSubslit = lblTotWeight.Text

        If PC2ChildCheck() = "0" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "PC2 Customer cannot duplicate")
            Exit Sub
        End If

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                    dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
                    dt.Rows(i)("C_QTY") = txtQtyC.Text
                    dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    dt.Rows(i)("REMARK") = txtRemarkC.Text

                    If txtQtyC.Text = "" Then
                        dt.Rows(i)("C_QTY") = DBNull.Value
                    Else
                        dt.Rows(i)("C_QTY") = txtQtyC.Text
                    End If

                    If lblUnitWeightC.Text = "" Then
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    End If

                    If lblTotWeightC.Text = "" Then
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    End If

                    If txtRemarkC.Text = "" Then
                        dt.Rows(i)("REMARK") = DBNull.Value
                    Else
                        dt.Rows(i)("REMARK") = txtRemarkC.Text
                    End If

                    'set unit weight
                    If ddlPC2Child.Text <> "" Then
                        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text)
                        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

                        Dim _dtGetUWeightC As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID)
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows(0)("UNIT_WEIGHT").ToString
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    Else
                        lblUnitWeightC.Text = ""
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    End If

                    'set total weight child
                    If lblUnitWeightC.Text <> "" And txtQtyC.Text <> "" And IsNumeric(txtQtyC.Text) And IsNumeric(lblUnitWeightC.Text) Then
                        lblTotWeightC.Text = txtQtyC.Text * lblUnitWeightC.Text
                        lblTotWeightC.Text = Math.Round(CDbl(lblTotWeightC.Text), 1)
                        If lblTotWeightC.Text.IndexOf(".") < 0 Then
                            lblTotWeightC.Text = lblTotWeightC.Text & ".0"
                        End If
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    Else
                        lblTotWeightC.Text = ""
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    End If

                    'set subslit mother
                    If lblTotWeight.Text <> "" And lblTotWeightC.Text <> "" And IsNumeric(lblTotWeight.Text) And IsNumeric(lblTotWeightC.Text) Then
                        momSubslit = (Convert.ToDouble(momSubslit) - Convert.ToDouble(lblTotWeightC.Text)).ToString("0.0")
                    End If
                    rowIndex += 1

                Next
                ViewState("CurrentTable") = dt
            End If
        End If
        'set ddl for all remove this ddl.selected value
        lblSubSlit.Text = momSubslit

        SetPreviousData()
    End Sub

    Private Sub SetPreviousData()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    setChildDdl(i)

                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    ddlPC1Child.Text = dt.Rows(i)("PC1_CUST").ToString()
                    ddlPC2Child.Text = dt.Rows(i)("PC2_CUST").ToString()
                    txtQtyC.Text = dt.Rows(i)("C_QTY").ToString()
                    lblUnitWeightC.Text = dt.Rows(i)("C_WEIGHT").ToString()
                    lblTotWeightC.Text = dt.Rows(i)("C_TOTAL_WEIGHT").ToString()
                    txtRemarkC.Text = dt.Rows(i)("REMARK").ToString()

                    rowIndex += 1
                Next
            End If
        End If

    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) 'change table when get spec
        If e.Row.RowType = DataControlRowType.DataRow Then

        End If
    End Sub

    Protected Sub grdChildRowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdChild.RowCommand
        Dim NAVIGATE As Integer = 0
        Dim _temp As String = "0"
        Dim j As Integer

        update_ChildRow()

        If e.CommandName.ToUpper.Equals("DELETE") Then
            NAVIGATE = 1
            Session("RowCommand") = "DELETE"
        End If

        If Session("RowCommand") = "DELETE" Then

            If (NAVIGATE <> 0) Then
                Dim Idx As Integer = Integer.Parse(e.CommandArgument)

                Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)

                j = 0

                dt.Rows(Idx).Delete()
                dt.AcceptChanges()

                ViewState("CurrentTable") = dt
                grdChild.DataSource = dt
                grdChild.DataBind()

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        setChildDdl(i)
                        Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                        Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)

                        Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                        Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                        Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                        Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                        ddlPC1Child.Text = dt.Rows(i)("PC1_CUST").ToString()
                        ddlPC2Child.Text = dt.Rows(i)("PC2_CUST").ToString()
                        txtQtyC.Text = dt.Rows(i)("C_QTY").ToString()
                        lblUnitWeightC.Text = dt.Rows(i)("C_WEIGHT").ToString()
                        lblTotWeightC.Text = dt.Rows(i)("C_TOTAL_WEIGHT").ToString()
                        txtRemarkC.Text = dt.Rows(i)("REMARK").ToString()

                    Next
                Else
                    ViewState("CurrentTable") = Nothing
                End If
                ChildCalculate()
            End If
        End If
    End Sub

    Protected Sub update_ChildRow()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt = DirectCast(ViewState("CurrentTable"), DataTable)

        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                Dim txtQtyC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                Dim lblUnitWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                Dim lblTotWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                Dim txtRemarkC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                dt.Rows(i)("C_QTY") = txtQtyC.Text
                dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                dt.Rows(i)("REMARK") = txtRemarkC.Text

                dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
            Next

        End If

        ViewState("CurrentTable") = dt
        grdChild.DataSource = dt
        grdChild.DataBind()

        SetPreviousData()


    End Sub

    Protected Sub update_save_ChildRow()
        If PC2ChildCheck() = "0" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "PC2 Customer cannot duplicate")
            Exit Sub
        End If

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt = DirectCast(ViewState("CurrentTable"), DataTable)

        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                Dim txtQtyC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                Dim lblUnitWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                Dim lblTotWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                Dim txtRemarkC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)


                If txtQtyC.Text = "" Or txtQtyC.Text = "0" Or IsNumeric(txtQtyC.Text) = False Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter Qty more than 0.")
                    Exit Sub
                End If

                dt.Rows(i)("C_QTY") = txtQtyC.Text
                dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                dt.Rows(i)("REMARK") = txtRemarkC.Text

                If ddlPC1Child.Text = "" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC1 Customer before add new")
                    Exit Sub
                End If

                If ddlPC2Child.Text = "" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC2 Customer before add new")
                    Exit Sub
                End If

                dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
            Next

        End If

        ViewState("CurrentTable") = dt
        grdChild.DataSource = dt
        grdChild.DataBind()

        SetPreviousData()
    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Dim index As Integer = Convert.ToInt32(e.RowIndex)
        Dim _temp As String = Nothing
        Dim _temp1 = 0
        Dim dtp As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
    End Sub

    Public Sub Save()

        Dim _temp As String = "0"
        Dim _temp2 As String = "0"
        Dim _temp3 As String = "0"
        Dim _temp4 As String = "0"
        Dim chkint As Integer = 0

        Dim pCompFrom As String
        pCompFrom = ddlCompCode.SelectedValue

        Dim pCompTo As String
        pCompTo = ddlCompCode.SelectedValue

        Dim dt2 As DataTable = Library.Database.BLL.SubSlitRequest.Chk_label(1, ddlProdLine.Text, "", "")
        If dt2.Rows.Count = 0 Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Invalid Production Line No")
            Exit Sub
        End If

        Dim dt3 As DataTable = Library.Database.BLL.SubSlitRequest.Chk_label(2, "", ddlPC1.Text, "")
        If dt3.Rows.Count = 0 Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Invalid PC1 Mother")
            Exit Sub
        End If

        Dim _dtPRODLINEid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetProdLineID(ddlProdLine.Text)
        Dim _dtPC1id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1.Text)
        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2.Text)

        Dim PRODLINEID = _dtPRODLINEid.Rows(0)("ID_MM_PRODLINE").ToString
        Dim PC1ID = _dtPC1id.Rows(0)("ID_MM_PC1").ToString
        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

        'Dim dtPC2Mother As DataTable = Library.Database.BLL.SubSlitRequest.chkPC2Mother(txtRefNo.Text, PC2ID, ddlProdLine.Text, ddlPC1.Text)

        'If (dtPC2Mother.Rows.Count > 0) And (ViewState("Edit_Flag") = "N") Then
        '    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This PC2 Mother " + "is already existed. Please re-enter new PC2 Mother")
        '    btnSave.Enabled = True
        '    pnlChild.Visible = True
        '    Exit Sub
        'End If

        If ddlPC1.Text = "" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC1 Mother.")
            btnSave.Enabled = True
            pnlChild.Visible = True
            Exit Sub
        End If

        If ddlProdLine.Text = "" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select Production Line.")
            btnSave.Enabled = True
            pnlChild.Visible = True
            Exit Sub
        End If

        If txtQty.Text = "" Or IsDBNull(txtQty.Text) Or txtQty.Text = "0" Or IsNumeric(txtQty.Text) = False Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter Qty more than 0.")
            btnSave.Enabled = True
            pnlChild.Visible = True
            Exit Sub
        End If

        If PC2ChildCheck() = "0" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "PC2 Customer cannot duplicate")
            pnlChild.Visible = True
            Exit Sub
        End If

        If (ViewState("CurrentTable") Is Nothing) Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Error, Please try again.")
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            Exit Sub
        End If

        'HANA 18112024 Add Refno check
        If txtRefNo.Text.Contains("&") Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "The character \u0026 cannot be used in Ref No. Please replace it with a different value.")
            btnSave.Enabled = True
            pnlChild.Visible = True
            Exit Sub
        End If

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt = DirectCast(ViewState("CurrentTable"), DataTable)

        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                Dim txtQtyC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                Dim lblUnitWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                Dim lblTotWeightC As System.Web.UI.WebControls.Label = DirectCast(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                Dim txtRemarkC As System.Web.UI.WebControls.TextBox = DirectCast(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)


                If txtQtyC.Text = "" Or txtQtyC.Text = "0" Or IsNumeric(txtQtyC.Text) = False Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please enter Qty more than 0.")
                    Exit Sub
                End If

                dt.Rows(i)("C_QTY") = txtQtyC.Text
                dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                dt.Rows(i)("REMARK") = txtRemarkC.Text

                If ddlPC1Child.Text = "" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC1 Customer before add new")
                    Exit Sub
                End If

                If ddlPC2Child.Text = "" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please Select PC2 Customer before add new")
                    Exit Sub
                End If

                dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
            Next

        End If

        ViewState("CurrentTable") = dt
        grdChild.DataSource = dt
        grdChild.DataBind()

        SetPreviousData()

        Trans_Date = txtDate.Text.Substring(6, 4) + "-" + txtDate.Text.Substring(3, 2) + "-" + txtDate.Text.Substring(0, 2)

        _temp = Library.Database.BLL.SubSlitRequest.SubSlitMaint("0", pCompFrom, pCompTo, txtRefNo.Text, lblRev.Text, Trans_Date, lblReqStat.Text, lblVenStat.Text, "1")

        If _temp = "1" Then

            ETD_Date = txtETD.Text
            ETA_Date = txtETA.Text
            ETD_Date = txtETD.Text.Substring(6, 4) + "-" + txtETD.Text.Substring(3, 2) + "-" + txtETD.Text.Substring(0, 2)
            ETA_Date = txtETA.Text.Substring(6, 4) + "-" + txtETA.Text.Substring(3, 2) + "-" + txtETA.Text.Substring(0, 2)

            'save mother
            If ViewState("Edit_Flag") = "Y" Then
                _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherMaint(txtSeqMother.Text, txtRefNo.Text, PC1ID, PC2ID, PRODLINEID, txtQty.Text, lblUnitWeight.Text.Replace(",", ""), lblTotWeight.Text.Replace(",", ""), lblSubSlit.Text.Replace(",", ""), ETD_Date, ETA_Date, "3")
            Else
                _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherMaint("0", txtRefNo.Text, PC1ID, PC2ID, PRODLINEID, txtQty.Text, lblUnitWeight.Text.Replace(",", ""), lblTotWeight.Text.Replace(",", ""), lblSubSlit.Text.Replace(",", ""), ETD_Date, ETA_Date, "1")
            End If

            'If _temp2 = "1" Then
            If Integer.TryParse(_temp2, chkint) And _temp2 <> "0" Then
                If ViewState("Edit_Flag") = "Y" Then
                    _temp4 = Library.Database.BLL.SubSlitRequest.SubSlitChildDel(txtRefNo.Text, txtSeqMother.Text, PC2ID, "2")
                    If _temp4 = "1" Then
                    Else
                        If _temp4 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                        End If
                        Exit Sub
                    End If
                End If

                Insert_PC2_Child(_temp2)

                'START LIST PART B   
                pnlList.Visible = True
                btnEdit.Visible = True
                btnDelete.Visible = True
                Dim dtSSRList As New DataTable()
                Dim dtWBlank As New DataTable()

                dtWBlank.Columns.Add(New DataColumn("REFNO", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("PRODLINE_NO", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("PC1_MOTHER", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("PC2_MOTHER", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("QTY", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("M_WEIGHT", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("M_TOTAL_WEIGHT", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("PC1_CUST", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("PC2_CUST", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("C_QTY", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("C_WEIGHT", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("C_TOTAL_WEIGHT", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("SUBSLIT_WASTE", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("ETD", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("ETA", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("REMARK", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("SEQ", GetType(String)))
                dtWBlank.Columns.Add(New DataColumn("CHK", GetType(String)))

                dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, "0")

                If dtSSRList.Rows.Count > 0 Then
                    Dim Prev_PC2Mother As String = ""
                    Dim Prev_PC1Mother As String = ""
                    Dim Prev_ProdLineNo As String = ""
                    Dim Prev_SeqMother As String = ""
                    Dim dr2 As DataRow = Nothing

                    Dim C_Qty As Integer = 0
                    Dim C_TotalWeight As Decimal = 0
                    Dim M_Qty As Integer = 0
                    Dim M_TotalWeight As Decimal = 0
                    Dim SubSlitWaste As Decimal = 0

                    Dim k As Integer = -1

                    For i As Integer = 0 To dtSSRList.Rows.Count - 1

                        If dtSSRList.Rows(i)("PC2_MOTHER").ToString().Equals(Prev_PC2Mother.ToString()) = True And _
                            dtSSRList.Rows(i)("PC1_MOTHER").ToString().Equals(Prev_PC1Mother.ToString()) = True And _
                           dtSSRList.Rows(i)("PRODLINE_NO").ToString().Equals(Prev_ProdLineNo.ToString()) = True And _
                           dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString.Equals(Prev_SeqMother.ToString()) = True Then

                            dr2 = dtWBlank.NewRow()
                            dtWBlank.Rows.Add(dr2)
                            k = k + 1

                            dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                            dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                            dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                            dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                            Dim C_Weight As Decimal = 0.0
                            C_Weight = dtSSRList.Rows(i)("C_WEIGHT")
                            dtWBlank.Rows(k)("C_WEIGHT") = C_Weight.ToString("#,###,###,##0.0")

                            Dim C_Total_Weight As Decimal = 0.0
                            C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                            dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = C_Total_Weight.ToString("#,###,###,##0.0")

                            dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")

                            dtWBlank.Rows(k)("PRODLINE_NO") = ""
                            dtWBlank.Rows(k)("PC1_MOTHER") = ""
                            dtWBlank.Rows(k)("PC2_MOTHER") = ""
                            dtWBlank.Rows(k)("QTY") = DBNull.Value
                            dtWBlank.Rows(k)("M_WEIGHT") = DBNull.Value
                            dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = DBNull.Value
                            dtWBlank.Rows(k)("SUBSLIT_WASTE") = DBNull.Value
                            dtWBlank.Rows(k)("ETD") = DBNull.Value
                            dtWBlank.Rows(k)("ETA") = DBNull.Value
                            dtWBlank.Rows(k)("SEQ") = DBNull.Value
                            dtWBlank.Rows(k)("CHK") = "0"

                            C_Qty += dtSSRList.Rows(i)("C_QTY")
                            C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                        Else

                            If Prev_PC2Mother.Equals("") = False And Prev_PC1Mother.Equals("") = False And Prev_ProdLineNo.Equals("") = False And Prev_SeqMother.Equals("") = False Then
                                k = k + 1
                                dr2 = dtWBlank.NewRow()
                                dtWBlank.Rows.Add(dr2)
                                dtWBlank.Rows(k)("PC2_MOTHER") = ""
                                dtWBlank.Rows(k)("CHK") = "0"

                            End If

                            dr2 = dtWBlank.NewRow()
                            dtWBlank.Rows.Add(dr2)
                            k = k + 1
                            dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                            dtWBlank.Rows(k)("PRODLINE_NO") = dtSSRList.Rows(i)("PRODLINE_NO")
                            dtWBlank.Rows(k)("PC1_MOTHER") = dtSSRList.Rows(i)("PC1_MOTHER")
                            dtWBlank.Rows(k)("PC2_MOTHER") = dtSSRList.Rows(i)("PC2_MOTHER")
                            dtWBlank.Rows(k)("QTY") = dtSSRList.Rows(i)("QTY")

                            Dim M_Weight As Decimal = 0.0
                            M_Weight = dtSSRList.Rows(i)("M_WEIGHT")
                            dtWBlank.Rows(k)("M_WEIGHT") = M_Weight.ToString("#,###,###,##0.0")

                            Dim M_Total_Weight As Decimal = 0.0
                            M_Total_Weight = dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                            dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = M_Total_Weight.ToString("#,###,###,##0.0")

                            dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                            dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                            dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                            Dim C_Weight As Decimal = 0.0
                            C_Weight = dtSSRList.Rows(i)("C_WEIGHT")
                            dtWBlank.Rows(k)("C_WEIGHT") = C_Weight.ToString("#,###,###,##0.0")

                            Dim C_Total_Weight As Decimal = 0.0
                            C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                            dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = C_Total_Weight.ToString("#,###,###,##0.0")

                            Dim Subslit_Waste As Decimal = 0.0
                            Subslit_Waste = dtSSRList.Rows(i)("SUBSLIT_WASTE")
                            dtWBlank.Rows(k)("SUBSLIT_WASTE") = Subslit_Waste.ToString("#,###,###,##0.0")

                            dtWBlank.Rows(k)("ETD") = (CDate(dtSSRList.Rows(i)("ETD").ToString)).ToString("dd/MM/yyyy")
                            dtWBlank.Rows(k)("ETA") = (CDate(dtSSRList.Rows(i)("ETA").ToString)).ToString("dd/MM/yyyy")
                            dtWBlank.Rows(k)("SEQ") = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()
                            dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")
                            dtWBlank.Rows(k)("CHK") = "1"

                            M_Qty += dtSSRList.Rows(i)("QTY")
                            M_TotalWeight += dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                            SubSlitWaste += dtSSRList.Rows(i)("SUBSLIT_WASTE")
                            C_Qty += dtSSRList.Rows(i)("C_QTY")
                            C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                        End If

                        Prev_PC2Mother = dtSSRList.Rows(i)("PC2_MOTHER").ToString()
                        Prev_PC1Mother = dtSSRList.Rows(i)("PC1_MOTHER").ToString()
                        Prev_ProdLineNo = dtSSRList.Rows(i)("PRODLINE_NO").ToString()
                        Prev_SeqMother = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()
                    Next

                    Dim MQty As Decimal = 0.0
                    MQty = M_Qty.ToString()
                    lblMQty.Text = M_Qty.ToString("#,###,###,##0.0")

                    Dim MTotalWeight As Decimal = 0.0
                    MTotalWeight = M_TotalWeight.ToString()
                    lblMTotalWeight.Text = MTotalWeight.ToString("#,###,###,##0.0")

                    lblCQty.Text = C_Qty.ToString()

                    Dim CTotalWeight As Decimal = 0.0
                    CTotalWeight = C_TotalWeight.ToString()
                    lblCTotalWeight.Text = CTotalWeight.ToString("#,###,###,##0.0")

                    Dim Sub_Slit_Waste As Decimal = 0.0
                    Sub_Slit_Waste = SubSlitWaste.ToString()
                    lblSubSlitWaste.Text = Sub_Slit_Waste.ToString("#,###,###,##0.0")

                End If

                ViewState("dtWBlank") = dtWBlank
                grdList.DataSource = dtWBlank
                grdList.DataBind()

                ViewState("CurrentTable") = Nothing
                pnlChild.Visible = False
                btnNext.Visible = True
                ddlPC2.Visible = True
                Submit_Button.Visible = True
                Cancel_Button.Visible = True
                ViewState("Edit_Flag") = "N"
                ddlPC2.Enabled = True
                ddlProdLine.Enabled = True
                ddlPC1.Enabled = True
                btnPC2Mother.Visible = True

                ddlProdLine.Text = ""
                ddlPC1.Text = ""
                ddlPC2.Text = ""
                txtQty.Text = ""
                lblUnitWeight.Text = ""
                lblTotWeight.Text = ""
                lblSubSlit.Text = ""
                txtETD.Text = ""
                txtETA.Text = ""
                SSRTotal.Visible = False

                Session("CheckBoxArray") = Nothing
                Session("PC2Mother") = Nothing
                Session("SeqMother") = Nothing
                Session("PC2MotherArray") = Nothing

            Else
                If _temp2 = "0" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                Else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp2)
                End If
            End If
        Else
            If _temp = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
            End If
        End If

        If _temp = "1" Then
            FirstTime_Flag = "N"
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If check_refNo() = True Then

            If CDbl(lblSubSlit.Text) < 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "script", "showDialogue(""" & lblSubSlit.Text & """);", True)
                pnlChild.Visible = True
            Else
                txtRefNo.ReadOnly = True
                Save()
            End If

        End If

    End Sub

    Private Function check_refNo() As Boolean

        If txtRefNo.Text.Trim.Equals("") = True Then

            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This Refno can't be nothing.")

            Return False

        End If

        Dim dtmaxRev As DataTable = Library.Database.BLL.SubSlitRequest.chkRefNo(txtRefNo.Text)

        If dtmaxRev.Rows.Count > 0 Then

            If dtmaxRev.Rows(0)("REQUEST_STATUS").Equals("New") = False Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This Refno is already submitted/cancelled. Please re-enter new RefNo")
                txtRefNo.Text = ""
                dtmaxRev.Dispose()

                Return False
            Else
                txtSubReqId.Text = dtmaxRev.Rows(0)("ID_SUBSLIT_REQUEST").ToString
                Dim dt_SSR As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(txtRefNo.Text, txtSubReqId.Text)

                ddlCompCode.SelectedValue = dt_SSR.Rows(0)("COMPTOID").ToString()
                txtDate.Text = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("dd/MM/yyyy")
                lblRev.Text = dt_SSR.Rows(0)("REVISIONCOUNT").ToString()
                lblVenStat.Text = dt_SSR.Rows(0)("VENDOR_STATUS").ToString()

                Display_SSRListing()
 
            End If
        Else

            txtSubReqId.Text = "0"
            pnlChild.Visible = False
            pnlList.Visible = False
            btnEdit.Visible = False
            btnDelete.Visible = False
            Submit_Button.Visible = False
            Cancel_Button.Visible = False
            Session("CheckBoxArray") = Nothing
            Session("PC2Mother") = Nothing
            Session("SeqMother") = Nothing
            Session("PC2MotherArray") = Nothing

        End If

        dtmaxRev.Dispose()

        Return True

    End Function

    Private Function Insert_PC2_Child(ByVal SeqMother As Integer) As String

        Insert_PC2_Child = "0"
        Dim v_temp As String = "0"
        Dim rowIndex As Integer = 0

        Dim _dtPRODLINEid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetProdLineID(ddlProdLine.Text)
        Dim _dtPC1id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1.Text)
        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2.Text)

        Dim PRODLINEID = _dtPRODLINEid.Rows(0)("ID_MM_PRODLINE").ToString
        Dim PC1ID = _dtPC1id.Rows(0)("ID_MM_PC1").ToString
        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString


        Dim dt As New DataTable()
        dt = DirectCast(ViewState("CurrentTable"), DataTable)


        For i = 0 To dt.Rows.Count - 1

            Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
            Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
            Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
            Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
            Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
            Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

            If ddlPC1Child.Text = "" Then
                Exit For
            End If

            ddlPC1Child.Text = dt.Rows(i)("PC1_CUST").ToString()
            ddlPC2Child.Text = dt.Rows(i)("PC2_CUST").ToString()
            txtQtyC.Text = dt.Rows(i)("C_QTY").ToString()
            lblUnitWeightC.Text = dt.Rows(i)("C_WEIGHT").ToString()
            lblTotWeightC.Text = dt.Rows(i)("C_TOTAL_WEIGHT").ToString()
            txtRemarkC.Text = dt.Rows(i)("REMARK").ToString()

            Dim _dtPC1Childid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1Child.Text)
            Dim _dtPC2Childid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text)

            Dim PC1CHILDID = _dtPC1Childid.Rows(0)("ID_MM_PC1").ToString
            Dim PC2CHILDID = _dtPC2Childid.Rows(0)("ID_MM_PC2").ToString

            If ViewState("Edit_Flag") = "Y" Then

                v_temp = Library.Database.BLL.SubSlitRequest.SubSlitChildMaint(SeqMother, txtRefNo.Text, PC1CHILDID, PC2CHILDID, txtQtyC.Text, _
                        lblUnitWeightC.Text.Replace(",", ""), lblTotWeightC.Text.Replace(",", ""), txtRemarkC.Text, PC2ID, ddlProdLine.Text, ddlPC1.Text, "3")
            Else

                v_temp = Library.Database.BLL.SubSlitRequest.SubSlitChildMaint(SeqMother, txtRefNo.Text, PC1CHILDID, PC2CHILDID, txtQtyC.Text, _
                       lblUnitWeightC.Text.Replace(",", ""), lblTotWeightC.Text.Replace(",", ""), txtRemarkC.Text, "0", ddlProdLine.Text, ddlPC1.Text, "1")
            End If

            If v_temp = "1" Then

            Else
                If v_temp = "0" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                Else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, v_temp)
                End If
            End If

            rowIndex += 1
        Next

        If v_temp = "1" Then

            If ViewState("Edit_Flag") = "Y" Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('PC2 Mother and/or its Child are updated successfully.')", True)
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('SubSlit Request is added successfully.')", True)
            End If

        End If

    End Function

    Private Function Val_PC2_Child_Duplicate() As String

        Val_PC2_Child_Duplicate = "0"
        Dim v_temp As String = "0"
        Dim rowIndex As Integer = 0

        Dim _dtPRODLINEid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetProdLineID(ddlProdLine.Text)
        Dim _dtPC1id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1.Text)
        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2.Text)

        Dim PRODLINEID = _dtPRODLINEid.Rows(0)("ID_MM_PRODLINE").ToString
        Dim PC1ID = _dtPC1id.Rows(0)("ID_MM_PC1").ToString
        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

        Dim dt As New DataTable()
        dt = DirectCast(ViewState("CurrentTable"), DataTable)

        For i = 0 To dt.Rows.Count - 2

            Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
            ddlPC2Child.Text = dt.Rows(i)("PC2_CUST").ToString()

            Dim dtPC2Child As DataTable = Library.Database.BLL.SubSlitRequest.chkPC2Child(txtRefNo.Text, PC2ID, ddlPC2Child.Text)

            If dtPC2Child.Rows.Count > 0 Then
                Dim v_PC2_Cust As String = dtPC2Child(0)("PC2_CUST").ToString()
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This PC2 Customer " + v_PC2_Cust + " is already existed in SubSlit Customer Roll database. Please re-enter new PC2 Customer")
                Exit Function
            End If

            rowIndex += 1
        Next

        If v_temp = "1" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('SubSlit Request is added successfully.')", True)
        End If

    End Function

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        If Session("SeqMother") Is Nothing Then
            Exit Sub
        End If

        Edit_Flag = "Y"
        ViewState("Edit_Flag") = Edit_Flag
        pnlChild.Visible = True
        btnNext.Visible = False
        ddlPC2.Enabled = False
        ddlProdLine.Enabled = False
        ddlPC1.Enabled = False
        btnPC2Mother.Visible = False
        Dim dtSSR As New DataTable()
        Dim rowIndex As Integer
        Dim v_PC2Mother As String = Session("PC2Mother")
        Dim v_SeqMother As String = Session("SeqMother")

        Dim dt_ProdLine As New DataTable()
        Dim dt_PC1 As New DataTable()
        Dim dt_PC2 As New DataTable()
        Dim dt_PC1Child As New DataTable()
        Dim dt_PC2Child As New DataTable()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt.Columns.Add(New DataColumn("IMAGE", GetType(String)))
        dt.Columns.Add(New DataColumn("ID_SUBSLIT_REQUEST_CHILD", GetType(String)))
        dt.Columns.Add(New DataColumn("SUBSLIT_REQ_MOTHER_SEQNO", GetType(String)))
        dt.Columns.Add(New DataColumn("PC1_CUST", GetType(String)))
        dt.Columns.Add(New DataColumn("PC2_CUST", GetType(String)))
        dt.Columns.Add(New DataColumn("C_QTY", GetType(String)))
        dt.Columns.Add(New DataColumn("C_WEIGHT", GetType(String)))
        dt.Columns.Add(New DataColumn("C_TOTAL_WEIGHT", GetType(String)))
        dt.Columns.Add(New DataColumn("REMARK", GetType(String)))


        If Session("SeqMother").Equals("") = True Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please click the required PC2 Mother/Child for edit.")
        Else
            dtSSR = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, Session("SeqMother"))

            If dtSSR.Rows.Count > 0 Then
                For i As Integer = 0 To dtSSR.Rows.Count - 1
                    If i = 0 Then

                        txtSeqMother.Text = dtSSR.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()

                        dt_ProdLine = Library.Database.BLL.SubSlitRequest.GetProdLineID(dtSSR.Rows(i)("PRODLINE_NO").ToString())
                        If dt_ProdLine.Rows.Count > 0 Then
                            ddlProdLine.Text = dt_ProdLine.Rows(0)("PRODLINE_NO").ToString()
                        Else
                            ddlProdLine.Text = ""
                        End If

                        dt_PC1 = Library.Database.BLL.SubSlitRequest.GetPC1ID(dtSSR.Rows(i)("PC1_MOTHER").ToString())
                        If dt_PC1.Rows.Count > 0 Then
                            ddlPC1.Text = dt_PC1.Rows(0)("PC1").ToString()
                        Else
                            ddlPC1.Text = ""
                        End If

                        dt_PC2 = Library.Database.BLL.SubSlitRequest.GetPC2ID(dtSSR.Rows(i)("PC2_MOTHER").ToString())
                        If dt_PC2.Rows.Count > 0 Then
                            ddlPC2.Text = dt_PC2.Rows(0)("PC2").ToString()
                        Else
                            ddlPC2.Text = ""
                        End If

                        txtQty.Text = dtSSR.Rows(i)("QTY").ToString()

                        Dim MUnitWeight As Decimal = 0.0
                        MUnitWeight = dtSSR.Rows(i)("M_WEIGHT").ToString()
                        lblUnitWeight.Text = MUnitWeight.ToString("#,###,###,##0.0")

                        Dim MTotWeight As Decimal = 0.0
                        MTotWeight = dtSSR.Rows(i)("M_TOTAL_WEIGHT").ToString()
                        lblTotWeight.Text = MTotWeight.ToString("#,###,###,##0.0")

                        Dim Sub_Slit_Waste As Decimal = 0.0
                        Sub_Slit_Waste = dtSSR.Rows(i)("SUBSLIT_WASTE").ToString()
                        lblSubSlit.Text = Sub_Slit_Waste.ToString("#,###,###,##0.0")

                        txtETD.Text = (CDate(dtSSR.Rows(i)("ETD").ToString)).ToString("dd/MM/yyyy")
                        txtETA.Text = (CDate(dtSSR.Rows(i)("ETA").ToString)).ToString("dd/MM/yyyy")
                    End If

                    dr = dt.NewRow()

                    dt.Rows.Add(dr)

                    dt_PC1Child = Library.Database.BLL.SubSlitRequest.GetPC1ID(dtSSR.Rows(i)("PC1_CUST").ToString())
                    If dt_PC1Child.Rows.Count > 0 Then
                        dt.Rows(i)("PC1_CUST") = dt_PC1Child.Rows(0)("PC1").ToString()
                    Else
                        dt.Rows(i)("PC1_CUST") = "0"
                    End If

                    dt_PC2Child = Library.Database.BLL.SubSlitRequest.GetPC2ID(dtSSR.Rows(i)("PC2_CUST").ToString())
                    If dt_PC2Child.Rows.Count > 0 Then
                        dt.Rows(i)("PC2_CUST") = dt_PC2Child.Rows(0)("PC2").ToString()
                    Else
                        dt.Rows(i)("PC2_CUST") = "0"
                    End If

                    dt.Rows(i)("C_QTY") = dtSSR.Rows(i)("C_QTY").ToString()
                    dt.Rows(i)("C_WEIGHT") = dtSSR.Rows(i)("C_WEIGHT").ToString()
                    dt.Rows(i)("C_TOTAL_WEIGHT") = dtSSR.Rows(i)("C_TOTAL_WEIGHT").ToString()
                    dt.Rows(i)("REMARK") = dtSSR.Rows(i)("REMARK").ToString()

                    rowIndex += 1
                Next

                ViewState("CurrentTable") = dt
                grdChild.DataSource = dt
                grdChild.DataBind()

                For i As Integer = 0 To dtSSR.Rows.Count - 1
                    setChildDdl(i)

                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    ddlPC1Child.Text = dt.Rows(i)("PC1_CUST").ToString()
                    ddlPC2Child.Text = dt.Rows(i)("PC2_CUST").ToString()
                    txtQtyC.Text = dt.Rows(i)("C_QTY").ToString()

                    Dim CUnitWeight As Decimal = 0.0
                    CUnitWeight = dt.Rows(i)("C_WEIGHT").ToString()
                    lblUnitWeightC.Text = CUnitWeight.ToString("#,###,###,##0.0")

                    Dim CTotalWeight As Decimal = 0.0
                    CTotalWeight = dt.Rows(i)("C_TOTAL_WEIGHT").ToString()
                    lblTotWeightC.Text = CTotalWeight.ToString("#,###,###,##0.0")

                    txtRemarkC.Text = dt.Rows(i)("REMARK").ToString()

                    rowIndex += 1

                Next
            End If
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If Session("SeqMother") Is Nothing Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please click radio button for the required PC2 Mother/Child.")
            Exit Sub
        End If

        Dim d_PC2Mother As String = Session("PC2Mother")
        Dim d_PC1Mother As String = Session("PC1Mother")
        Dim d_ProdLineNo As String = Session("ProdLineNo")
        Dim d_SeqMother As String = Session("SeqMother")
        Dim _temp1 As String = "0"
        Dim _temp2 As String = "0"

        _temp1 = Library.Database.BLL.SubSlitRequest.SubSlitChildDelFrList(txtRefNo.Text, d_PC2Mother.ToString(), d_PC1Mother.ToString(), d_ProdLineNo.ToString(), d_SeqMother.ToString(), "2")
        If _temp1 = "1" Then
            _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDel(txtRefNo.Text, d_PC2Mother.ToString(), d_PC1Mother.ToString(), d_ProdLineNo.ToString(), d_SeqMother.ToString(), "2")
            If _temp2 = "1" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This selected PC2 Mother/Child " + d_PC2Mother.ToString() + " is deleted successfully.")
            Else
                If _temp2 = "0" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                Else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp1)
                End If
                Exit Sub

            End If

        Else
            If _temp1 = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp1)
            End If
            Exit Sub
        End If

        pnlList.Visible = True
        btnEdit.Visible = True
        btnDelete.Visible = True
        Dim dtSSRList As New DataTable()
        Dim dtWBlank As New DataTable()

        dtWBlank.Columns.Add(New DataColumn("REFNO", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("PRODLINE_NO", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("PC1_MOTHER", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("PC2_MOTHER", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("QTY", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("M_WEIGHT", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("M_TOTAL_WEIGHT", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("PC1_CUST", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("PC2_CUST", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("C_QTY", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("C_WEIGHT", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("C_TOTAL_WEIGHT", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("SUBSLIT_WASTE", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("ETD", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("ETA", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("REMARK", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("SEQ", GetType(String)))
        dtWBlank.Columns.Add(New DataColumn("CHK", GetType(String)))

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, "0")

        If dtSSRList.Rows.Count > 0 Then
            Dim Prev_PC2Mother As String = ""
            Dim Prev_PC1Mother As String = ""
            Dim Prev_ProdLineNo As String = ""
            Dim Prev_SeqMother As String = ""
            Dim dr As DataRow = Nothing

            Dim C_Qty As Integer = 0
            Dim C_TotalWeight As Decimal = 0
            Dim M_Qty As Integer = 0
            Dim M_TotalWeight As Decimal = 0
            Dim SubSlitWaste As Decimal = 0

            Dim k As Integer = -1

            For i As Integer = 0 To dtSSRList.Rows.Count - 1

                If dtSSRList.Rows(i)("PC2_MOTHER").ToString().Equals(Prev_PC2Mother.ToString()) = True And _
                   dtSSRList.Rows(i)("PC1_MOTHER").ToString().Equals(Prev_PC1Mother.ToString()) = True And _
                   dtSSRList.Rows(i)("PRODLINE_NO").ToString().Equals(Prev_ProdLineNo.ToString()) = True And _
                   dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString().Equals(Prev_SeqMother.ToString()) = True Then
                    dr = dtWBlank.NewRow()
                    dtWBlank.Rows.Add(dr)
                    k = k + 1

                    dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                    dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                    dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                    dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                    Dim C_Weight As Decimal = 0.0
                    C_Weight = dtSSRList.Rows(i)("C_WEIGHT")
                    dtWBlank.Rows(k)("C_WEIGHT") = C_Weight.ToString("#,###,###,##0.0")

                    Dim C_Total_Weight As Decimal = 0.0
                    C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = C_Total_Weight.ToString("#,###,###,##0.0")

                    dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")

                    dtWBlank.Rows(k)("PRODLINE_NO") = ""
                    dtWBlank.Rows(k)("PC1_MOTHER") = ""
                    dtWBlank.Rows(k)("PC2_MOTHER") = ""
                    dtWBlank.Rows(k)("QTY") = DBNull.Value
                    dtWBlank.Rows(k)("M_WEIGHT") = DBNull.Value
                    dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = DBNull.Value
                    dtWBlank.Rows(k)("SUBSLIT_WASTE") = DBNull.Value
                    dtWBlank.Rows(k)("ETD") = DBNull.Value
                    dtWBlank.Rows(k)("ETA") = DBNull.Value
                    dtWBlank.Rows(k)("SEQ") = DBNull.Value
                    dtWBlank.Rows(k)("CHK") = "0"

                    C_Qty += dtSSRList.Rows(i)("C_QTY")
                    C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                Else

                    If Prev_PC2Mother.Equals("") = False And Prev_PC1Mother.Equals("") = False And Prev_ProdLineNo.Equals("") = False And Prev_SeqMother.Equals("") = False Then
                        k = k + 1
                        dr = dtWBlank.NewRow()
                        dtWBlank.Rows.Add(dr)
                        dtWBlank.Rows(k)("PC2_MOTHER") = ""
                        dtWBlank.Rows(k)("CHK") = "0"

                    End If

                    dr = dtWBlank.NewRow()
                    dtWBlank.Rows.Add(dr)
                    k = k + 1
                    dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                    dtWBlank.Rows(k)("PRODLINE_NO") = dtSSRList.Rows(i)("PRODLINE_NO")
                    dtWBlank.Rows(k)("PC1_MOTHER") = dtSSRList.Rows(i)("PC1_MOTHER")
                    dtWBlank.Rows(k)("PC2_MOTHER") = dtSSRList.Rows(i)("PC2_MOTHER")
                    dtWBlank.Rows(k)("QTY") = dtSSRList.Rows(i)("QTY")
                    Dim M_Weight As Decimal = 0.0
                    M_Weight = dtSSRList.Rows(i)("M_WEIGHT")
                    dtWBlank.Rows(k)("M_WEIGHT") = M_Weight.ToString("#,###,###,##0.0")

                    Dim M_Total_Weight As Decimal = 0.0
                    M_Total_Weight = dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = M_Total_Weight.ToString("#,###,###,##0.0")

                    dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                    dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                    dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                    Dim C_Weight As Decimal = 0.0
                    C_Weight = dtSSRList.Rows(i)("C_WEIGHT")
                    dtWBlank.Rows(k)("C_WEIGHT") = C_Weight.ToString("#,###,###,##0.0")

                    Dim C_Total_Weight As Decimal = 0.0
                    C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = C_Total_Weight.ToString("#,###,###,##0.0")

                    Dim Subslit_Waste As Decimal = 0.0
                    Subslit_Waste = dtSSRList.Rows(i)("SUBSLIT_WASTE")
                    dtWBlank.Rows(k)("SUBSLIT_WASTE") = Subslit_Waste.ToString("#,###,###,##0.0")

                    dtWBlank.Rows(k)("ETD") = (CDate(dtSSRList.Rows(i)("ETD").ToString)).ToString("dd/MM/yyyy")
                    dtWBlank.Rows(k)("ETA") = (CDate(dtSSRList.Rows(i)("ETA").ToString)).ToString("dd/MM/yyyy")
                    dtWBlank.Rows(k)("SEQ") = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString
                    dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")
                    dtWBlank.Rows(k)("CHK") = "1"

                    M_Qty += dtSSRList.Rows(i)("QTY")
                    M_TotalWeight += dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                    SubSlitWaste += dtSSRList.Rows(i)("SUBSLIT_WASTE")
                    C_Qty += dtSSRList.Rows(i)("C_QTY")
                    C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                End If

                Prev_PC2Mother = dtSSRList.Rows(i)("PC2_MOTHER").ToString()
                Prev_PC1Mother = dtSSRList.Rows(i)("PC1_MOTHER").ToString()
                Prev_ProdLineNo = dtSSRList.Rows(i)("PRODLINE_NO").ToString()
                Prev_SeqMother = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()
            Next

            Dim MQty As Decimal = 0.0
            MQty = M_Qty.ToString()
            lblMQty.Text = M_Qty.ToString("#,###,###,##0.0")

            Dim MTotalWeight As Decimal = 0.0
            MTotalWeight = M_TotalWeight.ToString()
            lblMTotalWeight.Text = MTotalWeight.ToString("#,###,###,##0.0")

            lblCQty.Text = C_Qty.ToString()

            Dim CTotalWeight As Decimal = 0.0
            CTotalWeight = C_TotalWeight.ToString()
            lblCTotalWeight.Text = CTotalWeight.ToString("#,###,###,##0.0")

            Dim Sub_Slit_Waste As Decimal = 0.0
            Sub_Slit_Waste = SubSlitWaste.ToString()
            lblSubSlitWaste.Text = Sub_Slit_Waste.ToString("#,###,###,##0.0")

        Else
            pnlList.Visible = False
            Submit_Button.Visible = False
            Cancel_Button.Visible = False
            SSRTotal.Visible = False
        End If

        ViewState("dtWBlank") = dtWBlank
        grdList.DataSource = dtWBlank
        grdList.DataBind()

    End Sub

    Protected Sub grdList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdList.PageIndexChanging
        grdList.PageIndex = e.NewPageIndex

        If ViewState("dtWBlank") IsNot Nothing Then
            grdList.DataSource = ViewState("dtWBlank")
            grdList.DataBind()
        End If

        If Session("CheckBoxArray") IsNot Nothing Then

            Dim CheckBoxArray As ArrayList = _
            DirectCast(Session("CheckBoxArray"), ArrayList)

            Dim PC2MotherArray As ArrayList = _
            DirectCast(Session("PC2MotherArray"), ArrayList)

            Dim h_PC2Mother As HiddenField
            Dim h_PC2Mother_Value As String
            Dim h_PC1Mother As HiddenField
            Dim h_PC1Mother_Value As String
            Dim h_ProdLineNo As HiddenField
            Dim h_ProdLineNo_Value As String
            Dim h_SeqMother As HiddenField
            Dim h_SeqMother_Value As String

            For i As Integer = 0 To grdList.Rows.Count - 1

                If grdList.Rows(i).RowType = DataControlRowType.DataRow Then

                    Dim CheckBoxIndex As Integer = grdList.PageSize * grdList.PageIndex + (i + 1)

                    h_PC2Mother = CType(grdList.Rows(i).FindControl("HiddenField1"), HiddenField)
                    h_PC2Mother_Value = h_PC2Mother.Value
                    h_PC1Mother = CType(grdList.Rows(i).FindControl("HiddenField2"), HiddenField)
                    h_PC1Mother_Value = h_PC1Mother.Value
                    h_ProdLineNo = CType(grdList.Rows(i).FindControl("HiddenField3"), HiddenField)
                    h_ProdLineNo_Value = h_ProdLineNo.Value
                    h_SeqMother = CType(grdList.Rows(i).FindControl("HiddenField4"), HiddenField)
                    h_SeqMother_Value = h_SeqMother.Value


                    Dim chk As System.Web.UI.WebControls.CheckBox = _
                        DirectCast(grdList.Rows(i) _
                        .FindControl("RadioButton1"), System.Web.UI.WebControls.CheckBox)

                    If chk.Visible = True Then
                        If CheckBoxArray.IndexOf(CheckBoxIndex) <> -1 And h_PC2Mother_Value = Session("PC2Mother") And h_PC1Mother_Value = Session("PC1Mother") And h_ProdLineNo_Value = Session("ProdLineNo") And h_SeqMother_Value = Session("SeqMother") Then
                            chk.Checked = True
                        Else
                            chk.Checked = False
                        End If
                    End If

                End If
            Next
        End If

    End Sub

    Protected Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim SeqMotherArray As ArrayList
        Dim PC2MotherArray As ArrayList
        Dim CheckBoxArray As ArrayList
        Dim CheckBoxIndex As Integer

        Dim gv_PageSize As Integer
        Dim gv_CurrPage As Integer
        Dim r_PC2Mother As HiddenField
        Dim r_PC2Mother_value As String
        Dim curr_PC2Mother_value As String = ""

        Dim r_PC1Mother As HiddenField
        Dim r_PC1Mother_value As String
        Dim curr_PC1Mother_value As String = ""

        Dim r_ProdLineNo As HiddenField
        Dim r_ProdLineNo_value As String
        Dim curr_ProdLineNo_value As String = ""

        Dim r_SeqMother As HiddenField
        Dim r_SeqMother_value As String
        Dim curr_SeqMother_value As String = ""

        Dim rb1 As System.Web.UI.WebControls.RadioButton

        gv_PageSize = grdList.PageSize
        gv_CurrPage = grdList.PageIndex

        If Session("CheckBoxArray") IsNot Nothing Then
            CheckBoxArray = DirectCast(Session("CheckBoxArray"), ArrayList)
        Else
            CheckBoxArray = New ArrayList()
        End If

        If Session("PC2MotherArray") IsNot Nothing Then
            PC2MotherArray = DirectCast(Session("PC2MotherArray"), ArrayList)
        Else
            PC2MotherArray = New ArrayList()
        End If

        If Session("SeqMotherArray") IsNot Nothing Then
            SeqMotherArray = DirectCast(Session("SeqMotherArray"), ArrayList)
        Else
            SeqMotherArray = New ArrayList()
        End If

        For i As Integer = 0 To grdList.Rows.Count - 1
            If grdList.Rows(i).RowType = DataControlRowType.DataRow Then
                Dim chk As System.Web.UI.WebControls.RadioButton = _
                 DirectCast(grdList.Rows(i).Cells(0) _
                 .FindControl("RadioButton1"), System.Web.UI.WebControls.RadioButton)

                If chk.Visible = True Then
                    r_PC2Mother = CType(grdList.Rows(i).FindControl("HiddenField1"), HiddenField)
                    r_PC2Mother_value = r_PC2Mother.Value
                    r_PC1Mother = CType(grdList.Rows(i).FindControl("HiddenField2"), HiddenField)
                    r_PC1Mother_value = r_PC1Mother.Value
                    r_ProdLineNo = CType(grdList.Rows(i).FindControl("HiddenField3"), HiddenField)
                    r_ProdLineNo_value = r_ProdLineNo.Value
                    r_SeqMother = CType(grdList.Rows(i).FindControl("HiddenField4"), HiddenField)
                    r_SeqMother_value = r_SeqMother.Value

                    CheckBoxIndex = grdList.PageSize * grdList.PageIndex + (i + 1)

                    'If chk.Checked And (r_PC2Mother_value <> Session("PC2Mother") Or r_PC1Mother_value <> Session("PC1Mother") Or r_ProdLineNo_value <> Session("ProdLineNo")) Then
                    If chk.Checked And (r_SeqMother_value <> Session("SeqMother")) Then
                        If CheckBoxArray.IndexOf(CheckBoxIndex) = -1 Then
                            CheckBoxArray.Add(CheckBoxIndex)
                            PC2MotherArray.Add(r_PC2Mother_value)
                            SeqMotherArray.Add(r_SeqMother_value)
                        End If
                        curr_PC2Mother_value = r_PC2Mother.Value
                        curr_PC1Mother_value = r_PC1Mother.Value
                        curr_ProdLineNo_value = r_ProdLineNo.Value
                        curr_SeqMother_value = r_SeqMother_value
                    Else
                        rb1 = CType(grdList.Rows(i).FindControl("RadioButton1"), System.Web.UI.WebControls.RadioButton)
                        rb1.Checked = False
                        If CheckBoxArray.IndexOf(CheckBoxIndex) <> -1 Then
                            CheckBoxArray.Remove(CheckBoxIndex)
                            PC2MotherArray.Remove(r_PC2Mother_value)
                            SeqMotherArray.Remove(r_SeqMother_value)
                        End If
                    End If
                End If
            End If
        Next

        Session("CheckBoxArray") = CheckBoxArray
        Session("PC2Mother") = curr_PC2Mother_value.ToString()
        Session("PC1Mother") = curr_PC1Mother_value.ToString()
        Session("ProdLineNo") = curr_ProdLineNo_value.ToString()
        Session("SeqMother") = curr_SeqMother_value.ToString()
        Session("PC2MotherArray") = PC2MotherArray
        Session("SeqMotherArray") = SeqMotherArray

    End Sub

    Protected Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        Session("CheckBoxArray") = Nothing
        Session("SeqMother") = Nothing
        Session("PC2Mother") = Nothing
        Session("PC2MotherArray") = Nothing
        Session("SeqMotherArray") = Nothing

        If Request.QueryString("itm1") Is Nothing And Request.QueryString("itm2") Is Nothing Then
            Response.End()
        Else
            Response.Redirect("~/Transactions/SSR_SEARCH.aspx")
        End If

    End Sub

    Protected Sub Submit_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit_Button.Click

        Session("CheckBoxArray") = Nothing
        Session("SeqMother") = Nothing
        Session("PC2Mother") = Nothing
        Session("PC2MotherArray") = Nothing
        Session("SeqMotherArray") = Nothing

        Dim _temp As String = "0"
        Dim dt_r As DataTable = Library.Database.BLL.SubSlitRequest.CHECK_SUBMITTED_REQ(txtRefNo.Text, lblRev.Text)

        If dt_r.Rows.Count > 0 Then

            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This RefNo " + txtRefNo.Text + "and Revison " + lblRev.Text + " exists in the system. Please look up in Search Sub-Slitting Request.")
            Exit Sub

        Else

            Dim u_temp As String = Library.Database.BLL.SubSlitRequest.UpdateReq(txtRefNo.Text, lblRev.Text)
            If u_temp = "1" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This RefNo " + txtRefNo.Text + "and Revison " + lblRev.Text + " is submitted successfully.")
            Else
                If u_temp = "0" Then
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                Else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, u_temp)
                End If
            End If

        End If

        btnNext.Visible = True
        Submit_Button.Visible = False
        Cancel_Button.Visible = False
        pnlChild.Visible = False
        pnlList.Visible = False
        btnEdit.Visible = False
        btnDelete.Visible = False

        ddlProdLine.Text = ""
        ddlPC1.Text = ""
        ddlPC2.Text = ""
        txtQty.Text = ""
        lblUnitWeight.Text = ""
        lblTotWeight.Text = ""
        lblSubSlit.Text = ""
        txtETD.Text = ""
        txtETA.Text = ""
        txtSubReqId.Text = ""

        txtRefNo.Text = ""
        ddlCompCode.SelectedValue = "0"
        txtDate.Text = ""
        lblRev.Text = ""

    End Sub

    Protected Sub txtQTYC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ChildCalculate()
    End Sub

    Protected Sub Display_SSRListing()
        Dim dtSSRList As New DataTable()
        Dim dtWBlank As New DataTable()

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, "0")

        If dtSSRList.Rows.Count > 0 Then
            pnlList.Visible = True
            btnEdit.Visible = True
            btnDelete.Visible = True

            dtWBlank.Columns.Add(New DataColumn("REFNO", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("PRODLINE_NO", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("PC1_MOTHER", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("PC2_MOTHER", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("QTY", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("M_WEIGHT", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("M_TOTAL_WEIGHT", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("PC1_CUST", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("PC2_CUST", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("C_QTY", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("C_WEIGHT", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("C_TOTAL_WEIGHT", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("SUBSLIT_WASTE", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("ETD", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("ETA", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("REMARK", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("SEQ", GetType(String)))
            dtWBlank.Columns.Add(New DataColumn("CHK", GetType(String)))


            Dim Prev_PC2Mother As String = ""
            Dim Prev_PC1Mother As String = ""
            Dim Prev_ProdLineNo As String = ""
            Dim Prev_SeqMother As String = ""
            Dim dr As DataRow = Nothing

            Dim C_Qty As Integer = 0
            Dim C_TotalWeight As Decimal = 0
            Dim M_Qty As Integer = 0
            Dim M_TotalWeight As Decimal = 0
            Dim SubSlitWaste As Decimal = 0


            Dim k As Integer = -1

            For i As Integer = 0 To dtSSRList.Rows.Count - 1

                If dtSSRList.Rows(i)("PC2_MOTHER").ToString().Equals(Prev_PC2Mother.ToString()) = True And _
                   dtSSRList.Rows(i)("PC1_MOTHER").ToString().Equals(Prev_PC1Mother.ToString()) = True And _
                   dtSSRList.Rows(i)("PRODLINE_NO").ToString().Equals(Prev_ProdLineNo.ToString()) = True And _
                   dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString.Equals(Prev_SeqMother.ToString()) = True Then

                    dr = dtWBlank.NewRow()
                    dtWBlank.Rows.Add(dr)
                    k = k + 1

                    dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                    dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                    dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                    dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                    Dim C_Weight As Decimal = 0.0
                    C_Weight = dtSSRList.Rows(i)("C_WEIGHT")
                    dtWBlank.Rows(k)("C_WEIGHT") = C_Weight.ToString("#,###,###,##0.0")

                    Dim C_Total_Weight As Decimal = 0.0
                    C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = C_Total_Weight.ToString("#,###,###,##0.0")


                    dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")

                    dtWBlank.Rows(k)("PRODLINE_NO") = ""
                    dtWBlank.Rows(k)("PC1_MOTHER") = ""
                    dtWBlank.Rows(k)("PC2_MOTHER") = ""
                    dtWBlank.Rows(k)("QTY") = DBNull.Value
                    dtWBlank.Rows(k)("M_WEIGHT") = DBNull.Value
                    dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = DBNull.Value
                    dtWBlank.Rows(k)("SUBSLIT_WASTE") = DBNull.Value
                    dtWBlank.Rows(k)("ETD") = DBNull.Value
                    dtWBlank.Rows(k)("ETA") = DBNull.Value
                    dtWBlank.Rows(k)("SEQ") = DBNull.Value
                    dtWBlank.Rows(k)("CHK") = "0"

                    C_Qty += dtSSRList.Rows(i)("C_QTY")
                    C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                Else

                    If Prev_PC2Mother.Equals("") = False And Prev_PC1Mother.Equals("") = False And Prev_ProdLineNo.Equals("") = False And Prev_SeqMother.Equals("") = False Then
                        k = k + 1
                        dr = dtWBlank.NewRow()
                        dtWBlank.Rows.Add(dr)
                        dtWBlank.Rows(k)("PC2_MOTHER") = ""
                        dtWBlank.Rows(k)("CHK") = "0"

                    End If

                    dr = dtWBlank.NewRow()
                    dtWBlank.Rows.Add(dr)
                    k = k + 1
                    dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                    dtWBlank.Rows(k)("PRODLINE_NO") = dtSSRList.Rows(i)("PRODLINE_NO")
                    dtWBlank.Rows(k)("PC1_MOTHER") = dtSSRList.Rows(i)("PC1_MOTHER")
                    dtWBlank.Rows(k)("PC2_MOTHER") = dtSSRList.Rows(i)("PC2_MOTHER")
                    dtWBlank.Rows(k)("QTY") = dtSSRList.Rows(i)("QTY")
                    Dim M_Weight As Decimal = 0.0
                    M_Weight = dtSSRList.Rows(i)("M_WEIGHT")
                    dtWBlank.Rows(k)("M_WEIGHT") = M_Weight.ToString("#,###,###,##0.0")

                    Dim M_Total_Weight As Decimal = 0.0
                    M_Total_Weight = dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = M_Total_Weight.ToString("#,###,###,##0.0")

                    dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                    dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                    dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                    Dim C_Weight As Decimal = 0.0
                    C_Weight = dtSSRList.Rows(i)("C_WEIGHT")
                    dtWBlank.Rows(k)("C_WEIGHT") = C_Weight.ToString("#,###,###,##0.0")

                    Dim C_Total_Weight As Decimal = 0.0
                    C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = C_Total_Weight.ToString("#,###,###,##0.0")

                    Dim Subslit_Waste As Decimal = 0.0
                    Subslit_Waste = dtSSRList.Rows(i)("SUBSLIT_WASTE")
                    dtWBlank.Rows(k)("SUBSLIT_WASTE") = Subslit_Waste.ToString("#,###,###,##0.0")

                    dtWBlank.Rows(k)("ETD") = (CDate(dtSSRList.Rows(i)("ETD").ToString)).ToString("dd/MM/yyyy")
                    dtWBlank.Rows(k)("ETA") = (CDate(dtSSRList.Rows(i)("ETA").ToString)).ToString("dd/MM/yyyy")
                    dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")
                    dtWBlank.Rows(k)("SEQ") = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()
                    dtWBlank.Rows(k)("CHK") = "1"

                    M_Qty += dtSSRList.Rows(i)("QTY")
                    M_TotalWeight += dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                    SubSlitWaste += dtSSRList.Rows(i)("SUBSLIT_WASTE")
                    C_Qty += dtSSRList.Rows(i)("C_QTY")
                    C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                End If

                Prev_PC2Mother = dtSSRList.Rows(i)("PC2_MOTHER").ToString()
                Prev_PC1Mother = dtSSRList.Rows(i)("PC1_MOTHER").ToString()
                Prev_ProdLineNo = dtSSRList.Rows(i)("PRODLINE_NO").ToString()
                Prev_SeqMother = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()

            Next

            Dim MQty As Decimal = 0.0
            MQty = M_Qty.ToString()
            lblMQty.Text = M_Qty.ToString("#,###,###,##0.0")

            Dim MTotalWeight As Decimal = 0.0
            MTotalWeight = M_TotalWeight.ToString()
            lblMTotalWeight.Text = MTotalWeight.ToString("#,###,###,##0.0")

            lblCQty.Text = C_Qty.ToString()

            Dim CTotalWeight As Decimal = 0.0
            CTotalWeight = C_TotalWeight.ToString()
            lblCTotalWeight.Text = CTotalWeight.ToString("#,###,###,##0.0")

            Dim Sub_Slit_Waste As Decimal = 0.0
            Sub_Slit_Waste = SubSlitWaste.ToString()
            lblSubSlitWaste.Text = Sub_Slit_Waste.ToString("#,###,###,##0.0")

            ViewState("dtWBlank") = dtWBlank
            grdList.DataSource = dtWBlank
            grdList.DataBind()

            btnEdit.Visible = True
            btnDelete.Visible = True
            Submit_Button.Visible = True
            Cancel_Button.Visible = True
            SSRTotal.Visible = False
        Else
            btnEdit.Visible = False
            btnDelete.Visible = False
            Submit_Button.Visible = False
            Cancel_Button.Visible = False
            SSRTotal.Visible = False
        End If

    End Sub

    Protected Sub grdList_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Footer Then

            Dim gv1 As GridView = DirectCast(sender, GridView)

            Dim tc As New TableCell()

            Dim gr As New GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal)

            AddMergedCells(gr, tc, 3, "", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, "Total", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblMQty.Text, System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, "", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblMTotalWeight.Text, System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 2, "", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblCQty.Text, System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, "", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblCTotalWeight.Text, System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblSubSlitWaste.Text, System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 3, "", System.Drawing.Color.AliceBlue.Name)

            gr.Cells.Add(tc)

            Dim gvTable As Table = DirectCast(e.Row.Parent, Table)
            gvTable.Controls.Add(gr)

        End If
    End Sub

    Protected Sub AddMergedCells(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.Style.Add("background-color", backcolor)
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (inpHide.Value = "1") Then
            Save()
        End If
    End Sub

    Protected Sub ddlPC1Child_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim rowIndex As Integer = 0
        Dim momSubslit As String = ""
        momSubslit = lblTotWeight.Text

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                    dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
                    dt.Rows(i)("C_QTY") = txtQtyC.Text
                    dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    dt.Rows(i)("REMARK") = txtRemarkC.Text

                    If txtQtyC.Text = "" Then
                        dt.Rows(i)("C_QTY") = DBNull.Value
                    Else
                        dt.Rows(i)("C_QTY") = txtQtyC.Text
                    End If

                    If lblUnitWeightC.Text = "" Then
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    End If

                    If lblTotWeightC.Text = "" Then
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    End If

                    If txtRemarkC.Text = "" Then
                        dt.Rows(i)("REMARK") = DBNull.Value
                    Else
                        dt.Rows(i)("REMARK") = txtRemarkC.Text
                    End If

                    'set unit weight
                    If ddlPC2Child.Text <> "" Then
                        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text)
                        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

                        Dim _dtGetUWeightC As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID)
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows(0)("UNIT_WEIGHT").ToString
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    Else
                        lblUnitWeightC.Text = ""
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    End If

                    'set total weight child
                    If lblUnitWeightC.Text <> "" And txtQtyC.Text <> "" And IsNumeric(txtQtyC.Text) And IsNumeric(lblUnitWeightC.Text) Then
                        lblTotWeightC.Text = txtQtyC.Text * lblUnitWeightC.Text
                        lblTotWeightC.Text = Math.Round(CDbl(lblTotWeightC.Text), 1)
                        If lblTotWeightC.Text.IndexOf(".") < 0 Then
                            lblTotWeightC.Text = lblTotWeightC.Text & ".0"
                        End If
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    Else
                        lblTotWeightC.Text = ""
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    End If

                    'set subslit mother
                    If lblTotWeight.Text <> "" And lblTotWeightC.Text <> "" And IsNumeric(lblTotWeight.Text) And IsNumeric(lblTotWeightC.Text) Then
                        momSubslit = (Convert.ToDouble(momSubslit) - Convert.ToDouble(lblTotWeightC.Text)).ToString("0.0")
                    End If
                    rowIndex += 1

                Next
                ViewState("CurrentTable") = dt
            End If
        End If
        'set ddl for all remove this ddl.selected value
        lblSubSlit.Text = momSubslit

        SetPreviousData()
    End Sub

    Protected Sub ddlPC2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPC2.TextChanged


        Dim _dtPRODLINEid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetProdLineID(ddlProdLine.Text)
        Dim _dtPC1id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC1ID(ddlPC1.Text)
        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2ID(ddlPC2.Text)
        Dim Unit_Weight As Decimal = 0.0

        If _dtPC2id.Rows.Count > 0 Then
            Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString
            Dim _dtGetUWeight As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID)
            Unit_Weight = _dtGetUWeight.Rows(0)("UNIT_WEIGHT")
            lblUnitWeight.Text = Unit_Weight.ToString("#,###,###,##0.0")

        Else
            lblUnitWeight.Text = "0.0"
        End If

        calTotalWeight()


        Dim rowIndex As Integer = 0
        Dim momSubslit As Decimal = "0.0"
        momSubslit = lblTotWeight.Text

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                    dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
                    dt.Rows(i)("C_QTY") = txtQtyC.Text
                    dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    dt.Rows(i)("REMARK") = txtRemarkC.Text

                    If txtQtyC.Text = "" Then
                        dt.Rows(i)("C_QTY") = DBNull.Value
                    Else
                        dt.Rows(i)("C_QTY") = txtQtyC.Text
                    End If

                    If lblUnitWeightC.Text = "" Then
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    End If

                    If lblTotWeightC.Text = "" Then
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    End If

                    If txtRemarkC.Text = "" Then
                        dt.Rows(i)("REMARK") = DBNull.Value
                    Else
                        dt.Rows(i)("REMARK") = txtRemarkC.Text
                    End If

                    'set unit weight

                    If ddlPC2Child.Text <> "" Then
                        Dim _dtPC2Childid As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text)
                        Dim PC2ChildID = _dtPC2Childid.Rows(0)("ID_MM_PC2").ToString
                        Dim _dtGetUWeightC As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ChildID)
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows(0)("UNIT_WEIGHT").ToString
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    Else
                        lblUnitWeightC.Text = ""
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value

                    End If

                    'set total weight child
                    If lblUnitWeightC.Text <> "" And txtQtyC.Text <> "" And IsNumeric(txtQtyC.Text) And IsNumeric(lblUnitWeightC.Text) Then
                        lblTotWeightC.Text = txtQtyC.Text * lblUnitWeightC.Text
                        lblTotWeightC.Text = Math.Round(CDbl(lblTotWeightC.Text), 1)
                        If lblTotWeightC.Text.IndexOf(".") < 0 Then
                            lblTotWeightC.Text = lblTotWeightC.Text & ".0"
                        End If
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    Else
                        lblTotWeightC.Text = ""
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    End If

                    'set subslit mother
                    If lblTotWeight.Text <> "" And lblTotWeightC.Text <> "" And IsNumeric(lblTotWeight.Text) And IsNumeric(lblTotWeightC.Text) Then
                        momSubslit = (Convert.ToDouble(momSubslit) - Convert.ToDouble(lblTotWeightC.Text)).ToString("0.0")
                    End If
                    rowIndex += 1

                Next
                ViewState("CurrentTable") = dt
            End If
        End If
        'set ddl for all remove this ddl.selected value
        lblSubSlit.Text = momSubslit.ToString("#,###,###,##0.0")
        SetPreviousData()

    End Sub

    Protected Sub ddlPC2Child_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowIndex As Integer = 0
        Dim momSubslit As String = ""
        momSubslit = lblTotWeight.Text
        Dim Unit_Weight As Decimal = 0.0

        If PC2ChildCheck() = "0" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "PC2 Customer cannot duplicate")
            Exit Sub
        End If

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim ddlPC1Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC1Child"), System.Web.UI.WebControls.TextBox)
                    Dim ddlPC2Child As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("ddlPC2Child"), System.Web.UI.WebControls.TextBox)
                    Dim txtQtyC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtQtyC"), System.Web.UI.WebControls.TextBox)
                    Dim lblUnitWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblUnitWeightC"), System.Web.UI.WebControls.Label)
                    Dim lblTotWeightC As System.Web.UI.WebControls.Label = CType(grdChild.Rows(i).FindControl("lblTotWeightC"), System.Web.UI.WebControls.Label)
                    Dim txtRemarkC As System.Web.UI.WebControls.TextBox = CType(grdChild.Rows(i).FindControl("txtRemarkC"), System.Web.UI.WebControls.TextBox)

                    dt.Rows(i)("PC1_CUST") = ddlPC1Child.Text
                    dt.Rows(i)("PC2_CUST") = ddlPC2Child.Text
                    dt.Rows(i)("C_QTY") = txtQtyC.Text
                    dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    dt.Rows(i)("REMARK") = txtRemarkC.Text

                    If txtQtyC.Text = "" Then
                        dt.Rows(i)("C_QTY") = DBNull.Value
                    Else
                        dt.Rows(i)("C_QTY") = txtQtyC.Text
                    End If

                    If lblUnitWeightC.Text = "" Then
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    End If

                    If lblTotWeightC.Text = "" Then
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    Else
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    End If

                    If txtRemarkC.Text = "" Then
                        dt.Rows(i)("REMARK") = DBNull.Value
                    Else
                        dt.Rows(i)("REMARK") = txtRemarkC.Text
                    End If

                    'set unit weight
                    If ddlPC2Child.Text <> "" Then
                        Dim _dtPC2id As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text)
                        Dim PC2ID = _dtPC2id.Rows(0)("ID_MM_PC2").ToString

                        Dim _dtGetUWeightC As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID)
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows(0)("UNIT_WEIGHT").ToString
                        dt.Rows(i)("C_WEIGHT") = lblUnitWeightC.Text
                    Else
                        lblUnitWeightC.Text = ""
                        dt.Rows(i)("C_WEIGHT") = DBNull.Value
                    End If

                    'set total weight child
                    If lblUnitWeightC.Text <> "" And txtQtyC.Text <> "" And IsNumeric(txtQtyC.Text) And IsNumeric(lblUnitWeightC.Text) Then
                        lblTotWeightC.Text = txtQtyC.Text * lblUnitWeightC.Text
                        lblTotWeightC.Text = Math.Round(CDbl(lblTotWeightC.Text), 1)
                        If lblTotWeightC.Text.IndexOf(".") < 0 Then
                            lblTotWeightC.Text = lblTotWeightC.Text & ".0"
                        End If
                        dt.Rows(i)("C_TOTAL_WEIGHT") = lblTotWeightC.Text
                    Else
                        lblTotWeightC.Text = ""
                        dt.Rows(i)("C_TOTAL_WEIGHT") = DBNull.Value
                    End If

                    'set subslit mother
                    If lblTotWeight.Text <> "" And lblTotWeightC.Text <> "" And IsNumeric(lblTotWeight.Text) And IsNumeric(lblTotWeightC.Text) Then
                        momSubslit = (Convert.ToDouble(momSubslit) - Convert.ToDouble(lblTotWeightC.Text)).ToString("0.0")
                    End If
                    rowIndex += 1

                Next
                ViewState("CurrentTable") = dt
            End If
        End If
        'set ddl for all remove this ddl.selected value
        lblSubSlit.Text = momSubslit

        SetPreviousData()
    End Sub

    Protected Sub btnpc2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpc2.Click
        Calculate()
    End Sub

    Protected Sub btnpc2child_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpc2child.Click

        ChildCalculate()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Calculate()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        ChildCalculate()
    End Sub

    Protected Sub ddlCompCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompCode.SelectedIndexChanged
        ViewState("CompCode") = ddlCompCode.SelectedIndex
    End Sub
End Class
