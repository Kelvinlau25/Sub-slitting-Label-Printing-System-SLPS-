Imports System.Data

Partial Class Transactions_SSR_VIEW
    Inherits System.Web.UI.Page
    Public lbl As String()

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

        Dim dt_sel As DataTable = Library.Database.BLL.SubSlitRequest.GetRefNoList()

        Dim lblstr = ""

        If dt_sel.Rows.Count > 0 Then

            For i = 0 To dt_sel.Rows.Count - 1
                lblstr = lblstr & """" & dt_sel.Rows(i)("REFNO").ToString() & """" & ","

            Next
            lblstr = Mid(lblstr, 1, Len(lblstr) - 1)
        End If

        lbl = lblstr.Split(",")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtSubReqId.Visible = False

        Dim _dtGetDept As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetUserData(Session("USERID"))
        lblDept.Text = _dtGetDept.Rows(0)("DEPARTMENT").ToString
        lblBy.Text = _dtGetDept.Rows(0)("NAME").ToString

        If Not (Page.IsPostBack) Then
            UpdStat_Button.Visible = False
            NewRev_Button.Visible = False
            Export_Button.Visible = False
            Cancel_Button.Visible = False
            SSRTotal.Visible = False
        End If

        If Page.IsPostBack Then
            Display_SSRListing()
        End If

        If Session("ULEVEL") = 2 Or Session("ULEVEL") = 3 Then
            ddlReqStat.Enabled = False
        Else
            ddlReqStat.Enabled = True
        End If
        
    End Sub

    Protected Sub Display_SSRListing()

        Dim dtSSRList As New DataTable()
        Dim dtWBlank As New DataTable()

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, "0")

        If (dtSSRList.Rows.Count > 0) And txtRefNo.Text.Equals("") = False Then
            If (dtSSRList.Rows(0)("REQUEST_STATUS").Equals("Submitted") Or dtSSRList.Rows(0)("REQUEST_STATUS").Equals("Cancel")) Then
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
                dtWBlank.Columns.Add(New DataColumn("CHK", GetType(String)))

                Dim Prev_PC2Mother As String = ""
                Dim dr As DataRow = Nothing

                Dim C_Qty As Integer = 0
                Dim C_TotalWeight As Decimal = 0
                Dim M_Qty As Integer = 0
                Dim M_TotalWeight As Decimal = 0
                Dim SubSlitWaste As Decimal = 0


                Dim k As Integer = -1

                For i As Integer = 0 To dtSSRList.Rows.Count - 1

                    If dtSSRList.Rows(i)("PC2_MOTHER").ToString().Equals(Prev_PC2Mother.ToString()) = True Then

                        dr = dtWBlank.NewRow()
                        dtWBlank.Rows.Add(dr)
                        k = k + 1

                        dtWBlank.Rows(k)("REFNO") = dtSSRList.Rows(i)("REFNO")
                        dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                        dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                        dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")
                        dtWBlank.Rows(k)("C_WEIGHT") = dtSSRList.Rows(i)("C_WEIGHT")
                        dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")
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
                        dtWBlank.Rows(k)("CHK") = "0"

                        C_Qty += dtSSRList.Rows(i)("C_QTY")
                        C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                    Else

                        If Prev_PC2Mother.Equals("") = False Then
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
                        dtWBlank.Rows(k)("M_WEIGHT") = dtSSRList.Rows(i)("M_WEIGHT")
                        dtWBlank.Rows(k)("M_TOTAL_WEIGHT") = dtSSRList.Rows(i)("M_TOTAL_WEIGHT")

                        dtWBlank.Rows(k)("PC1_CUST") = dtSSRList.Rows(i)("PC1_CUST")
                        dtWBlank.Rows(k)("PC2_CUST") = dtSSRList.Rows(i)("PC2_CUST")
                        dtWBlank.Rows(k)("C_QTY") = dtSSRList.Rows(i)("C_QTY")
                        dtWBlank.Rows(k)("C_WEIGHT") = dtSSRList.Rows(i)("C_WEIGHT")
                        dtWBlank.Rows(k)("C_TOTAL_WEIGHT") = dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                        dtWBlank.Rows(k)("SUBSLIT_WASTE") = dtSSRList.Rows(i)("SUBSLIT_WASTE")
                        dtWBlank.Rows(k)("ETD") = dtSSRList.Rows(i)("ETD")
                        dtWBlank.Rows(k)("ETA") = dtSSRList.Rows(i)("ETA")
                        dtWBlank.Rows(k)("REMARK") = dtSSRList.Rows(i)("REMARK")
                        dtWBlank.Rows(k)("CHK") = "1"

                        M_Qty += dtSSRList.Rows(i)("QTY")
                        M_TotalWeight += dtSSRList.Rows(i)("M_TOTAL_WEIGHT")
                        SubSlitWaste += dtSSRList.Rows(i)("SUBSLIT_WASTE")
                        C_Qty += dtSSRList.Rows(i)("C_QTY")
                        C_TotalWeight += dtSSRList.Rows(i)("C_TOTAL_WEIGHT")

                    End If

                    Prev_PC2Mother = dtSSRList.Rows(i)("PC2_MOTHER").ToString()

                Next

                lblMQty.Text = M_Qty.ToString()
                lblMTotalWeight.Text = M_TotalWeight.ToString()
                lblCQty.Text = C_Qty.ToString()
                lblCTotalWeight.Text = C_TotalWeight.ToString()
                lblSubSlitWaste.Text = SubSlitWaste.ToString()

                ViewState("dtWBlank") = dtWBlank
                grdList.DataSource = dtWBlank
                grdList.DataBind()

                If Session("ULEVEL") = 3 Then
                    ddlReqStat.Enabled = False
                    UpdStat_Button.Visible = True
                    NewRev_Button.Visible = False
                    Export_Button.Visible = False
                    Cancel_Button.Visible = True
                    SSRTotal.Visible = False
                Else
                    UpdStat_Button.Visible = True
                    NewRev_Button.Visible = True
                    Export_Button.Visible = True
                    Cancel_Button.Visible = True
                    SSRTotal.Visible = False
                End If
            End If

        Else

            UpdStat_Button.Visible = False
            NewRev_Button.Visible = False
            Export_Button.Visible = False
            Cancel_Button.Visible = False
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

        Dim r_Refno As String = txtRefNo.Text
        Dim r_ID_SSR As Integer = txtSubReqId.Text

        Dim userid As String = Session("USERID")

        Dim ssr_str As String = String.Empty

        ssr_str = Library.Database.BLL.SubSlitRequest.GET_SSR_TO_EXCEL(txtRefNo.Text, txtSubReqId.Text, userid, lblMQty.Text, lblMTotalWeight.Text, lblCQty.Text, lblCTotalWeight.Text, lblSubSlitWaste.Text, ViewState("dtWBlank"))

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
        Dim r_ID_SSR As Integer = txtSubReqId.Text

        _temp = Library.Database.BLL.SubSlitRequest.SSRUpdateStat(txtRefNo.Text, r_ID_SSR, ddlReqStat.SelectedValue, ddlVenStat.SelectedValue)

        If _temp = "1" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "The Status of Sub Slittng Request under " + txtRefNo.Text + " is updated successfully.")
        Else
            If _temp = "0" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, String.Format(Resources.Message.Failed, "1"))
            Else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
            End If
        End If


    End Sub

    Protected Sub ddlReqStat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReqStat.SelectedIndexChanged

        If ddlReqStat.SelectedValue.Equals("Cancel") = True Then
            ddlVenStat.SelectedValue = "N/A"
            ddlVenStat.Enabled = False
        End If
    End Sub

    Protected Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Response.End()
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

        Dim r_Refno As String = txtRefNo.Text
        Dim r_ID_SSR As Integer = txtSubReqId.Text
        Dim dt_SSR As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno.ToString(), r_ID_SSR.ToString())

        Dim RevCount As Integer = dt_SSR.Rows(0)("REVISIONCOUNT") + 1
        Dim Trans_Date As String = (CDate(dt_SSR.Rows(0)("DATEREQ").ToString)).ToString("MM-dd-yyyy")

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

            Dim Prev_PC2Mother As String = ""

            For i As Integer = 0 To dtSSRList.Rows.Count - 1

                If dtSSRList.Rows(i)("PC2_MOTHER").ToString().Equals(Prev_PC2Mother.ToString()) = True Then

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

                    v_ETD = (CDate(dtSSRList.Rows(i)("ETD").ToString)).ToString("MM-dd-yyyy")
                    v_ETA = (CDate(dtSSRList.Rows(i)("ETA").ToString)).ToString("MM-dd-yyyy")
                    _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDup(v_IDSSR, dtSSRList.Rows(i)("REFNO").ToString(), dtSSRList.Rows(i)("PC1_MOTHER").ToString(), dtSSRList.Rows(i)("PC2_MOTHER").ToString(), dtSSRList.Rows(i)("PRODLINE_NO").ToString(), dtSSRList.Rows(i)("QTY").ToString(), dtSSRList.Rows(i)("M_WEIGHT").ToString(), dtSSRList.Rows(i)("M_TOTAL_WEIGHT").ToString(), dtSSRList.Rows(i)("SUBSLIT_WASTE").ToString(), v_ETD.ToString(), v_ETA.ToString(), "1")

                    If _temp2 = "1" Then
                        Dim dt_MothSeq As DataTable = Library.Database.BLL.SubSlitRequest.GetMotherSeq(v_IDSSR, dtSSRList.Rows(i)("PC2_MOTHER").ToString())
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

                Prev_PC2Mother = dtSSRList.Rows(i)("PC2_MOTHER").ToString()

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

    Protected Sub txtRefNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRefNo.TextChanged
        Dim dtmaxRev As DataTable = Library.Database.BLL.SubSlitRequest.chkRefNo(txtRefNo.Text)
        If dtmaxRev.Rows.Count > 0 Then

            If dtmaxRev.Rows(0)("REQUEST_STATUS").Equals("New") = True Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This Refno Status is New. Please go to Sub Slitting Request - Add screen")
                txtRefNo.Text = ""
            Else
                txtSubReqId.Text = dtmaxRev.Rows(0)("ID_SUBSLIT_REQUEST").ToString
                lblCompCode.Text = dtmaxRev.Rows(0)("COMPANYTO").ToString()
                lblDate.Text = (CDate(dtmaxRev.Rows(0)("DATEREQ").ToString)).ToString("dd-MM-yyyy")
                lblRev.Text = dtmaxRev.Rows(0)("REVISIONCOUNT").ToString()

                ddlReqStat.SelectedValue = dtmaxRev.Rows(0)("REQUEST_STATUS").ToString()
                ddlVenStat.SelectedValue = dtmaxRev.Rows(0)("VENDOR_STATUS").ToString()

                If ddlReqStat.SelectedValue.Equals("Cancel") Then
                    ddlVenStat.Enabled = False
                    ddlReqStat.Enabled = False
                End If

                Display_SSRListing()

            End If
        Else
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "This Refno does not exist.")
            txtRefNo.Text = ""
        End If

    End Sub

    Protected Sub grdList_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Footer Then

            Dim gv1 As GridView = DirectCast(sender, GridView)

            Dim tc As New TableCell()

            Dim gr As New GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal)

            AddMergedCells(gr, tc, 2, "", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, "Total", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblMQty.text, System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, "", System.Drawing.Color.AliceBlue.Name)
            AddMergedCells(gr, tc, 1, lblMTotalWeight.text, System.Drawing.Color.AliceBlue.Name)
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

End Class
