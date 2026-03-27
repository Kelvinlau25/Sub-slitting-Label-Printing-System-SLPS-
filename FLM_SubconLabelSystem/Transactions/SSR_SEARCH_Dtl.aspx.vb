Imports System.Data

Partial Class Transactions_SSR_SEARCH_Dtl
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        ddlReqStat.Items.Clear()
        For i = 0 To 1
            If i = 0 Then
                ddlReqStat.Items.Add(New ListItem("Submitted", "Submitted"))
            End If
            If i = 1 Then
                ddlReqStat.Items.Add(New ListItem("Cancel", "Cancel"))
            End If
        Next
        ddlReqStat.DataBind()

        ddlVenStat.Items.Clear()
        For i = 0 To 3
            If i = 0 Then
                ddlVenStat.Items.Add(New ListItem("N/A", "N/A"))
            End If
            If i = 1 Then
                ddlVenStat.Items.Add(New ListItem("Received", "Received"))
            End If
            If i = 2 Then
                ddlVenStat.Items.Add(New ListItem("In Production", "In Production"))
            End If
            If i = 3 Then
                ddlVenStat.Items.Add(New ListItem("Complete", "Complete"))
            End If
        Next
        ddlVenStat.DataBind()


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("itm1") Is Nothing And Request.QueryString("itm2") Is Nothing Then
            Dim r_Refno As String = ""
            Dim r_ID_SSR As Integer = Request.QueryString("id")
            Dim dt_SSR As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno, r_ID_SSR.ToString())

            lblCompCode.Text = dt_SSR.Rows(0)("COMPANYTO").ToString()
            lblRefNo.Text = dt_SSR.Rows(0)("REFNO").ToString()
            r_Refno = dt_SSR.Rows(0)("REFNO").ToString()
            lblDate.Text = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("dd/MM/yyyy")
            lblRev.Text = dt_SSR.Rows(0)("REVISIONCOUNT").ToString()

            lblrequest.Text = dt_SSR.Rows(0)("REQUEST_STATUS").ToString

            ddlReqStat.Visible = False

            lblvendor.Text = dt_SSR.Rows(0)("VENDOR_STATUS").ToString
            ddlVenStat.Visible = False
            Dim _dtGetDept As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetUserData(Session("USERID"))
            'lblDept.Text = _dtGetDept.Rows(0)("DEPARTMENT").ToString
            'lblBy.Text = _dtGetDept.Rows(0)("NAME").ToString
            lblDept.Text = dt_SSR.Rows(0)("REQUEST_BY").ToString
            lblBy.Text = dt_SSR.Rows(0)("DEPARTMENT").ToString

            Display_SSRListing(r_Refno, r_ID_SSR, 0)
            lbltittle.Text = " Delete"
        Else
            pninfo.Visible = False
            pnconfirmation.Visible = False
            btnSubmit.Visible = False
            btnCancel.Visible = False
            Dim r_Refno As String = Request.QueryString("itm1").Trim
            Dim r_ID_SSR As Integer = Request.QueryString("itm2").Trim
            Dim dt_SSR As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno.ToString(), r_ID_SSR.ToString())

            lblCompCode.Text = dt_SSR.Rows(0)("COMPANYTO").ToString()
            lblRefNo.Text = dt_SSR.Rows(0)("REFNO").ToString()
            lblDate.Text = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("dd/MM/yyyy")
            lblRev.Text = dt_SSR.Rows(0)("REVISIONCOUNT").ToString()

            If Not (Page.IsPostBack) Then
                ddlReqStat.SelectedValue = dt_SSR.Rows(0)("REQUEST_STATUS").ToString()
                ddlVenStat.SelectedValue = dt_SSR.Rows(0)("VENDOR_STATUS").ToString()
                If ddlReqStat.SelectedValue.Equals("Cancel") Then
                    ddlVenStat.Enabled = False
                    ddlReqStat.Enabled = False
                End If
            End If

            Dim _dtGetDept As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetUserData(Session("USERID"))
            'lblDept.Text = _dtGetDept.Rows(0)("DEPARTMENT").ToString
            'lblBy.Text = _dtGetDept.Rows(0)("NAME").ToString
            lblDept.Text = dt_SSR.Rows(0)("REQUEST_BY").ToString
            lblBy.Text = dt_SSR.Rows(0)("DEPARTMENT").ToString

            Display_SSRListing(Request.QueryString("itm1").Trim, Request.QueryString("itm2").Trim, 0)
            If ddlReqStat.SelectedValue.Equals("Cancel") Then
                UpdStat_Button.Visible = False
            End If
            lbltittle.Text = " Received"
        End If
        If Request.QueryString("itm2") <> "" And ddlReqStat.SelectedValue <> "Cancel" Then
            If Session("ULEVEL") = 3 Then
                ddlReqStat.Enabled = False
            Else
                ddlVenStat.Enabled = False
            End If
        End If

    End Sub

    Private Function compile_2_one_decimal(ByVal pstr_value As String) As String


        Dim _str_value As String = Decimal.Round(Convert.ToDecimal(pstr_value), 1).ToString("#,###,###,##0.0")

        Dim _int_dot As Integer = _str_value.IndexOf(".")

        Return _str_value

    End Function

    Protected Sub Display_SSRListing(ByVal r_Refno As String, ByVal r_ID_SSR As String, ByVal pbyte_option As Byte)

        Dim dtSSRList As New DataTable()
        Dim dtWBlank As New DataTable()
        Dim _str_value As String = ""

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(r_Refno, r_ID_SSR)

        If dtSSRList.Rows.Count > 0 Then
            pnlList.Visible = True

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

                    dtWBlank.Rows(k)("C_WEIGHT") = compile_2_one_decimal(C_Weight)

                    Dim C_Total_Weight As Decimal = 0.0
                    C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                    dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = compile_2_one_decimal(C_Total_Weight)
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
                    dtWBlank.Rows(k)("M_WEIGHT") = compile_2_one_decimal(M_Weight)

                    Dim M_Total_Weight As Decimal = 0.0
                    M_Total_Weight = dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = compile_2_one_decimal(M_Total_Weight)

                    dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                    dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                    dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")

                    Dim C_Weight As Decimal = 0.0
                    C_Weight = dtSSRList.Rows(i)("C_WEIGHT")

                    dtWBlank.Rows(k)("C_WEIGHT") = compile_2_one_decimal(C_Weight)

                    Dim C_Total_Weight As Decimal = 0.0
                    C_Total_Weight = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
                    dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = compile_2_one_decimal(C_Total_Weight)

                    Dim Subslit_Waste As Decimal = 0.0
                    Subslit_Waste = dtSSRList.Rows(i)("SUBSLIT_WASTE")
                    dtWBlank.Rows(k)("SUBSLIT_WASTE") = compile_2_one_decimal(Subslit_Waste)

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

                If pbyte_option = 0 Then

                    If Request.QueryString("itm2") = "" Then
                        lblcreatedby.Text = dtSSRList.Rows(i)("CREATED_BY").ToString()
                        lblcreateddate.Text = dtSSRList.Rows(i)("CREATED_DATE").ToString()
                        lblupdatedby.Text = dtSSRList.Rows(i)("UPDATED_BY").ToString()
                        lblupdateddate.Text = dtSSRList.Rows(i)("CREATED_DATE").ToString()
                    End If

                End If


            Next

            ViewState("dtWBlank") = dtWBlank

            If pbyte_option = 0 Then

                Dim MQty As Decimal = 0.0
                MQty = M_Qty.ToString()
                lblMQty.Text = M_Qty.ToString("#,###,###,##0.0")

                Dim MTotalWeight As Decimal = 0.0
                MTotalWeight = M_TotalWeight.ToString()
                lblMTotalWeight.Text = compile_2_one_decimal(MTotalWeight)

                lblCQty.Text = C_Qty.ToString()

                Dim CTotalWeight As Decimal = 0.0
                CTotalWeight = C_TotalWeight.ToString()
                lblCTotalWeight.Text = compile_2_one_decimal(CTotalWeight)

                Dim Sub_Slit_Waste As Decimal = 0.0
                Sub_Slit_Waste = SubSlitWaste.ToString()
                lblSubSlitWaste.Text = compile_2_one_decimal(Sub_Slit_Waste)



                grdList.DataSource = dtWBlank
                grdList.DataBind()


                If Request.QueryString("itm2") <> "" Then
                    If Session("ULEVEL") = 3 Then
                        ddlReqStat.Enabled = False
                        UpdStat_Button.Visible = True
                        NewRev_Button.Visible = False
                        Export_Button.Visible = True
                        Cancel_Button.Visible = True
                        SSRTotal.Visible = False
                    Else
                        UpdStat_Button.Visible = True
                        NewRev_Button.Visible = True
                        Export_Button.Visible = True
                        Cancel_Button.Visible = True
                        SSRTotal.Visible = False
                    End If
                Else
                    UpdStat_Button.Visible = False
                    NewRev_Button.Visible = False
                    Export_Button.Visible = False
                    Cancel_Button.Visible = False
                End If

            End If
            SSRTotal.Visible = False

            End If


    End Sub

    Protected Sub grdList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdList.PageIndexChanging

        grdList.PageIndex = e.NewPageIndex

        If ViewState("dtWBlank") IsNot Nothing Then
            grdList.DataSource = ViewState("dtWBlank")
            grdList.DataBind()
        End If

    End Sub

    Protected Sub Export_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export_Button.Click

        Dim r_Refno As String = Request.QueryString("itm1").Trim
        Dim r_ID_SSR As Integer = Request.QueryString("itm2").Trim

        Dim userid As String = Session("USERID")

        Dim ssr_str As String = String.Empty

        Display_SSRListing(r_Refno, r_ID_SSR, 1)

        ssr_str = Library.Database.BLL.SubSlitRequest.GET_SSR_TO_EXCEL(r_Refno, r_ID_SSR, userid, lblMQty.Text, lblMTotalWeight.Text, lblCQty.Text, lblCTotalWeight.Text, lblSubSlitWaste.Text, ViewState("dtWBlank"))

        Dim _str_fileName = "SubSlittingRequest" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xls"

        _str_fileName = "attachment;filename=" & _str_fileName

        Response.Clear()

        Response.Buffer = True

        Response.AddHeader("content-disposition", _str_fileName)

        Response.Charset = ""

        Response.ContentType = "application/vnd.ms-excel"

        Response.Output.Write(ssr_str)

        Response.Flush()

        Response.End()

    End Sub

    Protected Sub UpdStat_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdStat_Button.Click

        Dim _temp As String = "0"
        Dim r_ID_SSR As Integer = Request.QueryString("itm2").Trim

        _temp = Library.Database.BLL.SubSlitRequest.SSRUpdateStat(lblRefNo.Text, r_ID_SSR, ddlReqStat.SelectedValue, ddlVenStat.SelectedValue)

        If _temp = "1" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "The Status of Sub Slittng Request under " + lblRefNo.Text + " is updated successfully.")
        Else
            If _temp = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
            End If
        End If

        If ddlReqStat.SelectedValue.Equals("Cancel") Then
            ddlVenStat.Enabled = False
            ddlReqStat.Enabled = False
        End If

    End Sub

    Protected Sub ddlReqStat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReqStat.SelectedIndexChanged

        If ddlReqStat.SelectedValue.Equals("Cancel") = True Then
            ddlVenStat.SelectedValue = "N/A"
            ddlVenStat.Enabled = False
        End If

    End Sub

    Protected Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Response.Redirect("~/Transactions/SSR_SEARCH.aspx")
    End Sub

    Protected Sub NewRev_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewRev_Button.Click

        Dim _temp As String = "0"
        Dim _temp2 As String = "0"
        Dim _temp3 As String = "0"
        Dim _temp4 As String = "0"
        Dim v_IDSSR As Integer = 0
        Dim v_MothSeq As Integer = 0
        Dim v_ETD As String = ""
        Dim v_ETA As String = ""
        Dim chkint As Integer = 0

        Dim r_Refno As String = Request.QueryString("itm1").Trim
        Dim r_ID_SSR As Integer = Request.QueryString("itm2").Trim
        Dim dt_SSR As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno.ToString(), r_ID_SSR.ToString())

        Dim RevCount As Integer = dt_SSR.Rows(0)("REVISIONCOUNT") + 1
        'Dim Trans_Date As String = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("dd/MM/yyyy")
        Dim Trans_Date As String = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("MM/dd/yyyy")

        _temp = Library.Database.BLL.SubSlitRequest.SubSlitDup("0", dt_SSR.Rows(0)("COMPANYFROM").ToString(), dt_SSR.Rows(0)("COMPANYTO").ToString(), dt_SSR.Rows(0)("REFNO").ToString(), RevCount, Trans_Date.ToString(), "New", "N/A", "1")

        Dim dt_IDSSR As DataTable = Library.Database.BLL.SubSlitRequest.GetIDSSR(dt_SSR.Rows(0)("REFNO").ToString(), RevCount)

        If dt_IDSSR.Rows.Count > 0 Then
            v_IDSSR = dt_IDSSR.Rows(0)("ID_SUBSLIT_REQUEST")
        Else
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Error - Couldn't find Sub Slitting Request ID for Refno " + dt_SSR.Rows(0)("REFNO").ToString() + " and Revision " + RevCount)
            Exit Sub
        End If

        Dim dtSSRList As New DataTable()

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(r_Refno.ToString(), r_ID_SSR.ToString())

        If dtSSRList.Rows.Count > 0 Then

            Dim Prev_SeqMother As String = ""

            For i As Integer = 0 To dtSSRList.Rows.Count - 1

                If dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString().Equals(Prev_SeqMother.ToString()) = True Then

                    _temp3 = Library.Database.BLL.SubSlitRequest.SubSlitChildDup(v_MothSeq, r_Refno.ToString(), dtSSRList.Rows(i)("PC1_CUST").ToString(), dtSSRList.Rows(i)("PC2_CUST").ToString(), dtSSRList.Rows(i)("C_QTY").ToString(), _
                                                                            dtSSRList.Rows(i)("C_WEIGHT").ToString(), dtSSRList.Rows(i)("C_TOTAL_WEIGHT").ToString(), dtSSRList.Rows(i)("REMARK").ToString(), "0", "1")
                    If _temp3 = "1" Then

                    Else
                        If _temp3 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp3)
                        End If
                        Exit Sub
                    End If

                Else

                    v_ETD = (CDate(dtSSRList.Rows(i)("ETD").ToString)).ToString("MM/dd/yyyy")
                    v_ETA = (CDate(dtSSRList.Rows(i)("ETA").ToString)).ToString("MM/dd/yyyy")

                    _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDup(v_IDSSR, dtSSRList.Rows(i)("REFNO").ToString(), dtSSRList.Rows(i)("PC1_MOTHER").ToString(), dtSSRList.Rows(i)("PC2_MOTHER").ToString(), dtSSRList.Rows(i)("PRODLINE_NO").ToString(), dtSSRList.Rows(i)("QTY").ToString(), dtSSRList.Rows(i)("M_WEIGHT").ToString(), dtSSRList.Rows(i)("M_TOTAL_WEIGHT").ToString(), dtSSRList.Rows(i)("SUBSLIT_WASTE").ToString(), v_ETD.ToString(), v_ETA.ToString(), "1")

                    If Integer.TryParse(_temp2, chkint) And _temp2 <> "0" Then
                        Dim dt_MothSeq As DataTable = Library.Database.BLL.SubSlitRequest.GetMotherSeq(v_IDSSR, _temp2)
                        If dt_MothSeq.Rows.Count > 0 Then
                            v_MothSeq = dt_MothSeq.Rows(0)("SUBSLIT_REQ_MOTHER_SEQNO")
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Error - Couldn't find SubSlitting Request Mother SeqNo for PC2 Mother " + dtSSRList.Rows(i)("PC2_MOTHER").ToString() + " and SubSlitting Request ID " + v_IDSSR)
                            Exit Sub
                        End If
                    Else
                        If _temp2 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp2)
                        End If
                    End If

                    _temp3 = Library.Database.BLL.SubSlitRequest.SubSlitChildDup(v_MothSeq, r_Refno.ToString(), dtSSRList.Rows(i)("PC1_CUST").ToString(), dtSSRList.Rows(i)("PC2_CUST").ToString(), dtSSRList.Rows(i)("C_QTY").ToString(), _
                                                                           dtSSRList.Rows(i)("C_WEIGHT").ToString(), dtSSRList.Rows(i)("C_TOTAL_WEIGHT").ToString(), dtSSRList.Rows(i)("REMARK").ToString(), "0", "1")
                    If _temp3 = "1" Then

                    Else
                        If _temp3 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp3)
                        End If
                        Exit Sub
                    End If

                End If

                Prev_SeqMother = dtSSRList.Rows(i)("SUBSLIT_REQ_MOTHER_SEQNO").ToString()

            Next

            Dim mssg As String = "The Sub Slittng Request for RefNo " + r_Refno + " and New Revision " + RevCount.ToString() + " is created successfully."

            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, mssg)

            Dim url As String = "~/Transactions/SUBSLIT_REQ_.aspx?itm1=" & r_Refno.ToString & "&itm2= " & v_IDSSR
            Response.Redirect(url)

        Else
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "No Sub Slitting Request records for Refno " + dt_SSR.Rows(0)("REFNO").ToString() + " and Revision " + dt_SSR.Rows(0)("REVISIONCOUNT").ToString())
            Exit Sub
        End If

    End Sub

    Protected Sub grdList_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Footer Then

            Dim gv1 As GridView = DirectCast(sender, GridView)

            Dim tc As New TableCell()

            Dim gr As New GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal)

            AddMergedCells(gr, tc, 2, "", System.Drawing.Color.AliceBlue.Name)
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

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim _temp As String = "0"
        If rbyes.Checked = True Then
            _temp = Library.Database.BLL.SubSlitRequest.SubSlitMaint(Request.QueryString("id"), "", "", "", "", "", "", "", 5)
            If _temp = 1 Then
                Response.Redirect("~/Transactions/SSR_SEARCH.aspx")
            End If
        Else
            Dim message As String = "Please Choose Yes to confirm delete"
            Dim Script As String = "<script type='text/javascript'> alert('" + message + "');</script>"
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", Script)
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Transactions/SSR_SEARCH.aspx")
    End Sub
End Class

