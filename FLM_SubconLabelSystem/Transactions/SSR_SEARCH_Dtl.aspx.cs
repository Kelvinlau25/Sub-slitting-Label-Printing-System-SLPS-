using System;
using System.Data;
using System.Drawing;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class Transactions_SSR_SEARCH_Dtl : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ddlReqStat.Items.Clear();
        ddlReqStat.Items.Add(new ListItem("Submitted", "Submitted"));
        ddlReqStat.Items.Add(new ListItem("Cancel", "Cancel"));
        ddlReqStat.DataBind();

        ddlVenStat.Items.Clear();
        ddlVenStat.Items.Add(new ListItem("N/A", "N/A"));
        ddlVenStat.Items.Add(new ListItem("Received", "Received"));
        ddlVenStat.Items.Add(new ListItem("In Production", "In Production"));
        ddlVenStat.Items.Add(new ListItem("Complete", "Complete"));
        ddlVenStat.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["itm1"] == null && Request.QueryString["itm2"] == null)
        {
            string rRefno = string.Empty;
            int rIdSsr = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(rRefno, rIdSsr);

            lblCompCode.Text = dtSsr.Rows[0]["COMPANYTO"].ToString();
            lblRefNo.Text = dtSsr.Rows[0]["REFNO"].ToString();
            rRefno = dtSsr.Rows[0]["REFNO"].ToString();
            lblDate.Text = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"].ToString()).ToString("dd/MM/yyyy");
            lblRev.Text = dtSsr.Rows[0]["REVISIONCOUNT"].ToString();

            lblrequest.Text = dtSsr.Rows[0]["REQUEST_STATUS"].ToString();
            ddlReqStat.Visible = false;

            lblvendor.Text = dtSsr.Rows[0]["VENDOR_STATUS"].ToString();
            ddlVenStat.Visible = false;

            lblDept.Text = dtSsr.Rows[0]["REQUEST_BY"].ToString();
            lblBy.Text = dtSsr.Rows[0]["DEPARTMENT"].ToString();

            Display_SSRListing(rRefno, rIdSsr.ToString(), 0);
            lbltittle.Text = " Delete";
        }
        else
        {
            pninfo.Visible = false;
            pnconfirmation.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;

            string rRefno = Request.QueryString["itm1"].Trim();
            int rIdSsr = Convert.ToInt32(Request.QueryString["itm2"].Trim());
            DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(rRefno, rIdSsr);

            lblCompCode.Text = dtSsr.Rows[0]["COMPANYTO"].ToString();
            lblRefNo.Text = dtSsr.Rows[0]["REFNO"].ToString();
            lblDate.Text = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"].ToString()).ToString("dd/MM/yyyy");
            lblRev.Text = dtSsr.Rows[0]["REVISIONCOUNT"].ToString();

            if (!Page.IsPostBack)
            {
                ddlReqStat.SelectedValue = dtSsr.Rows[0]["REQUEST_STATUS"].ToString();
                ddlVenStat.SelectedValue = dtSsr.Rows[0]["VENDOR_STATUS"].ToString();
                if (ddlReqStat.SelectedValue.Equals("Cancel"))
                {
                    ddlVenStat.Enabled = false;
                    ddlReqStat.Enabled = false;
                }
            }

            lblDept.Text = dtSsr.Rows[0]["REQUEST_BY"].ToString();
            lblBy.Text = dtSsr.Rows[0]["DEPARTMENT"].ToString();

            Display_SSRListing(Request.QueryString["itm1"].Trim(), Request.QueryString["itm2"].Trim(), 0);

            if (ddlReqStat.SelectedValue.Equals("Cancel"))
            {
                UpdStat_Button.Visible = false;
            }

            lbltittle.Text = " Received";
        }

        if (Request.QueryString["itm2"] != null && Request.QueryString["itm2"] != "" && ddlReqStat.SelectedValue != "Cancel")
        {
            if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "3")
            {
                ddlReqStat.Enabled = false;
            }
            else
            {
                ddlVenStat.Enabled = false;
            }
        }
    }

    private string Compile2OneDecimal(string pstrValue)
    {
        string strValue = Decimal.Round(Convert.ToDecimal(pstrValue), 1).ToString("#,###,###,##0.0");
        return strValue;
    }

    protected void Display_SSRListing(string rRefno, string rIdSsr, byte pbyteOption)
    {
        DataTable dtSSRList = new DataTable();
        DataTable dtWBlank = new DataTable();

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(rRefno, Convert.ToInt32(rIdSsr));

        if (dtSSRList.Rows.Count > 0)
        {
            pnlList.Visible = true;

            dtWBlank.Columns.Add(new DataColumn("REFNO", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("PRODLINE_NO", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("PC1_MOTHER", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("PC2_MOTHER", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("QTY", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("M_WEIGHT", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("M_TOTAL_WEIGHT", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("PC1_CUST", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("PC2_CUST", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("C_QTY", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("C_WEIGHT", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("C_TOTAL_WEIGHT", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("SUBSLIT_WASTE", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("ETD", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("ETA", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("REMARK", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("SEQ", typeof(string)));
            dtWBlank.Columns.Add(new DataColumn("CHK", typeof(string)));

            string prevPc2Mother = string.Empty;
            string prevPc1Mother = string.Empty;
            string prevProdLineNo = string.Empty;
            string prevSeqMother = string.Empty;
            DataRow dr = null;

            int cQty = 0;
            decimal cTotalWeight = 0;
            int mQty = 0;
            decimal mTotalWeight = 0;
            decimal subSlitWaste = 0;

            int k = -1;

            for (int i = 0; i <= dtSSRList.Rows.Count - 1; i++)
            {
                if (dtSSRList.Rows[i]["PC2_MOTHER"].ToString().Equals(prevPc2Mother) &&
                    dtSSRList.Rows[i]["PC1_MOTHER"].ToString().Equals(prevPc1Mother) &&
                    dtSSRList.Rows[i]["PRODLINE_NO"].ToString().Equals(prevProdLineNo) &&
                    dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString().Equals(prevSeqMother))
                {
                    dr = dtWBlank.NewRow();
                    dtWBlank.Rows.Add(dr);
                    k++;

                    dtWBlank.Rows[k]["REFNO"] = dtSSRList.Rows[i]["REFNO"];
                    dtWBlank.Rows[k]["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                    dtWBlank.Rows[k]["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                    dtWBlank.Rows[k]["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];

                    decimal cWeight = Convert.ToDecimal(dtSSRList.Rows[i]["C_WEIGHT"]);
                    dtWBlank.Rows[k]["C_WEIGHT"] = Compile2OneDecimal(cWeight.ToString());

                    decimal cTotalWeightRow = Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                    dtWBlank.Rows[k]["C_TOTAL_WEIGHT"] = Compile2OneDecimal(cTotalWeightRow.ToString());

                    dtWBlank.Rows[k]["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                    dtWBlank.Rows[k]["PRODLINE_NO"] = string.Empty;
                    dtWBlank.Rows[k]["PC1_MOTHER"] = string.Empty;
                    dtWBlank.Rows[k]["PC2_MOTHER"] = string.Empty;
                    dtWBlank.Rows[k]["QTY"] = DBNull.Value;
                    dtWBlank.Rows[k]["M_WEIGHT"] = DBNull.Value;
                    dtWBlank.Rows[k]["M_TOTAL_WEIGHT"] = DBNull.Value;
                    dtWBlank.Rows[k]["SUBSLIT_WASTE"] = DBNull.Value;
                    dtWBlank.Rows[k]["ETD"] = DBNull.Value;
                    dtWBlank.Rows[k]["ETA"] = DBNull.Value;
                    dtWBlank.Rows[k]["SEQ"] = DBNull.Value;
                    dtWBlank.Rows[k]["CHK"] = "0";

                    cQty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                    cTotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                }
                else
                {
                    if (!prevPc2Mother.Equals(string.Empty) && !prevPc1Mother.Equals(string.Empty) &&
                        !prevProdLineNo.Equals(string.Empty) && !prevSeqMother.Equals(string.Empty))
                    {
                        k++;
                        dr = dtWBlank.NewRow();
                        dtWBlank.Rows.Add(dr);
                        dtWBlank.Rows[k]["PC2_MOTHER"] = string.Empty;
                        dtWBlank.Rows[k]["CHK"] = "0";
                    }

                    dr = dtWBlank.NewRow();
                    dtWBlank.Rows.Add(dr);
                    k++;

                    dtWBlank.Rows[k]["REFNO"] = dtSSRList.Rows[i]["REFNO"];
                    dtWBlank.Rows[k]["PRODLINE_NO"] = dtSSRList.Rows[i]["PRODLINE_NO"];
                    dtWBlank.Rows[k]["PC1_MOTHER"] = dtSSRList.Rows[i]["PC1_MOTHER"];
                    dtWBlank.Rows[k]["PC2_MOTHER"] = dtSSRList.Rows[i]["PC2_MOTHER"];
                    dtWBlank.Rows[k]["QTY"] = dtSSRList.Rows[i]["QTY"];

                    decimal mWeight = Convert.ToDecimal(dtSSRList.Rows[i]["M_WEIGHT"]);
                    dtWBlank.Rows[k]["M_WEIGHT"] = Compile2OneDecimal(mWeight.ToString());

                    decimal mTotalWeightRow = Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                    dtWBlank.Rows[k]["M_TOTAL_WEIGHT"] = Compile2OneDecimal(mTotalWeightRow.ToString());

                    dtWBlank.Rows[k]["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                    dtWBlank.Rows[k]["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                    dtWBlank.Rows[k]["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];

                    decimal cWeight = Convert.ToDecimal(dtSSRList.Rows[i]["C_WEIGHT"]);
                    dtWBlank.Rows[k]["C_WEIGHT"] = Compile2OneDecimal(cWeight.ToString());

                    decimal cTotalWeightRow = Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                    dtWBlank.Rows[k]["C_TOTAL_WEIGHT"] = Compile2OneDecimal(cTotalWeightRow.ToString());

                    decimal subslitWasteRow = Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                    dtWBlank.Rows[k]["SUBSLIT_WASTE"] = Compile2OneDecimal(subslitWasteRow.ToString());

                    dtWBlank.Rows[k]["ETD"] = Convert.ToDateTime(dtSSRList.Rows[i]["ETD"].ToString()).ToString("dd/MM/yyyy");
                    dtWBlank.Rows[k]["ETA"] = Convert.ToDateTime(dtSSRList.Rows[i]["ETA"].ToString()).ToString("dd/MM/yyyy");
                    dtWBlank.Rows[k]["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                    dtWBlank.Rows[k]["SEQ"] = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();
                    dtWBlank.Rows[k]["CHK"] = "1";

                    mQty += Convert.ToInt32(dtSSRList.Rows[i]["QTY"]);
                    mTotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                    subSlitWaste += Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                    cQty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                    cTotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                }

                prevPc2Mother = dtSSRList.Rows[i]["PC2_MOTHER"].ToString();
                prevPc1Mother = dtSSRList.Rows[i]["PC1_MOTHER"].ToString();
                prevProdLineNo = dtSSRList.Rows[i]["PRODLINE_NO"].ToString();
                prevSeqMother = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();

                if (pbyteOption == 0 && (Request.QueryString["itm2"] == null || Request.QueryString["itm2"] == string.Empty))
                {
                    lblcreatedby.Text = dtSSRList.Rows[i]["CREATED_BY"].ToString();
                    lblcreateddate.Text = dtSSRList.Rows[i]["CREATED_DATE"].ToString();
                    lblupdatedby.Text = dtSSRList.Rows[i]["UPDATED_BY"].ToString();
                    lblupdateddate.Text = dtSSRList.Rows[i]["CREATED_DATE"].ToString();
                }
            }

            ViewState["dtWBlank"] = dtWBlank;

            if (pbyteOption == 0)
            {
                lblMQty.Text = mQty.ToString("#,###,###,##0.0");
                lblMTotalWeight.Text = Compile2OneDecimal(mTotalWeight.ToString());
                lblCQty.Text = cQty.ToString();
                lblCTotalWeight.Text = Compile2OneDecimal(cTotalWeight.ToString());
                lblSubSlitWaste.Text = Compile2OneDecimal(subSlitWaste.ToString());

                grdList.DataSource = dtWBlank;
                grdList.DataBind();

                if (Request.QueryString["itm2"] != null && Request.QueryString["itm2"] != string.Empty)
                {
                    if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "3")
                    {
                        ddlReqStat.Enabled = false;
                        UpdStat_Button.Visible = true;
                        NewRev_Button.Visible = false;
                        Export_Button.Visible = true;
                        Cancel_Button.Visible = true;
                        SSRTotal.Visible = false;
                    }
                    else
                    {
                        UpdStat_Button.Visible = true;
                        NewRev_Button.Visible = true;
                        Export_Button.Visible = true;
                        Cancel_Button.Visible = true;
                        SSRTotal.Visible = false;
                    }
                }
                else
                {
                    UpdStat_Button.Visible = false;
                    NewRev_Button.Visible = false;
                    Export_Button.Visible = false;
                    Cancel_Button.Visible = false;
                }
            }

            SSRTotal.Visible = false;
        }
    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;

        if (ViewState["dtWBlank"] != null)
        {
            grdList.DataSource = (DataTable)ViewState["dtWBlank"];
            grdList.DataBind();
        }
    }

    protected void Export_Button_Click(object sender, EventArgs e)
    {
        string rRefno = Request.QueryString["itm1"].Trim();
        int rIdSsr = Convert.ToInt32(Request.QueryString["itm2"].Trim());
        string userid = Session["USERID"] != null ? Session["USERID"].ToString() : string.Empty;

        Display_SSRListing(rRefno, rIdSsr.ToString(), 1);

        string ssrStr = Library.Database.BLL.SubSlitRequest.GET_SSR_TO_EXCEL(
            rRefno, rIdSsr, userid,
            Convert.ToInt32(lblMQty.Text.Replace(",", "")),
            Convert.ToDecimal(lblMTotalWeight.Text.Replace(",", "")),
            Convert.ToInt32(lblCQty.Text.Replace(",", "")),
            Convert.ToDecimal(lblCTotalWeight.Text.Replace(",", "")),
            Convert.ToDecimal(lblSubSlitWaste.Text.Replace(",", "")),
            (DataTable)ViewState["dtWBlank"]);

        string fileName = "SubSlittingRequest" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        fileName = "attachment;filename=" + fileName;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", fileName);
        Response.Charset = string.Empty;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Output.Write(ssrStr);
        Response.Flush();
        Response.End();
    }

    protected void UpdStat_Button_Click(object sender, EventArgs e)
    {
        int rIdSsr = Convert.ToInt32(Request.QueryString["itm2"].Trim());

        string temp = Library.Database.BLL.SubSlitRequest.SSRUpdateStat(
            lblRefNo.Text, rIdSsr,
            ddlReqStat.SelectedValue, ddlVenStat.SelectedValue);

        if (temp == "1")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(
                this.Page, "The Status of Sub Slittng Request under " + lblRefNo.Text + " is updated successfully.");
        }
        else
        {
            if (temp == "0")
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, "1"));
            else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp);
        }

        if (ddlReqStat.SelectedValue.Equals("Cancel"))
        {
            ddlVenStat.Enabled = false;
            ddlReqStat.Enabled = false;
        }
    }

    protected void ddlReqStat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReqStat.SelectedValue.Equals("Cancel"))
        {
            ddlVenStat.SelectedValue = "N/A";
            ddlVenStat.Enabled = false;
        }
    }

    protected void Cancel_Button_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Transactions/SSR_SEARCH.aspx");
    }

    protected void NewRev_Button_Click(object sender, EventArgs e)
    {
        string temp = "0";
        string temp2 = "0";
        string temp3 = "0";
        int vIdSsr = 0;
        int vMothSeq = 0;
        string vEtd = string.Empty;
        string vEta = string.Empty;
        int chkint = 0;

        string rRefno = Request.QueryString["itm1"].Trim();
        int rIdSsr = Convert.ToInt32(Request.QueryString["itm2"].Trim());
        DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(rRefno, rIdSsr);

        int revCount = Convert.ToInt32(dtSsr.Rows[0]["REVISIONCOUNT"]) + 1;
        string transDate = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"].ToString()).ToString("MM/dd/yyyy");

        temp = Library.Database.BLL.SubSlitRequest.SubSlitDup(
            "0",
            dtSsr.Rows[0]["COMPANYFROM"].ToString(),
            dtSsr.Rows[0]["COMPANYTO"].ToString(),
            dtSsr.Rows[0]["REFNO"].ToString(),
            revCount, transDate, "New", "N/A", 1);

        DataTable dtIdSsr = Library.Database.BLL.SubSlitRequest.GetIDSSR(dtSsr.Rows[0]["REFNO"].ToString(), revCount);

        if (dtIdSsr.Rows.Count > 0)
        {
            vIdSsr = Convert.ToInt32(dtIdSsr.Rows[0]["ID_SUBSLIT_REQUEST"]);
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "Error - Couldn't find Sub Slitting Request ID for Refno " +
                dtSsr.Rows[0]["REFNO"].ToString() + " and Revision " + revCount);
            return;
        }

        DataTable dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(rRefno, rIdSsr);

        if (dtSSRList.Rows.Count > 0)
        {
            string prevSeqMother = string.Empty;

            for (int i = 0; i <= dtSSRList.Rows.Count - 1; i++)
            {
                if (dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString().Equals(prevSeqMother))
                {
                    temp3 = Library.Database.BLL.SubSlitRequest.SubSlitChildDup(
                        vMothSeq, rRefno,
                        dtSSRList.Rows[i]["PC1_CUST"].ToString(),
                        dtSSRList.Rows[i]["PC2_CUST"].ToString(),
                        dtSSRList.Rows[i]["C_QTY"].ToString(),
                        dtSSRList.Rows[i]["C_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["REMARK"].ToString(),
                        "0", 1);

                    if (temp3 != "1")
                    {
                        if (temp3 == "0")
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, "1"));
                        else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp3);
                        return;
                    }
                }
                else
                {
                    vEtd = Convert.ToDateTime(dtSSRList.Rows[i]["ETD"].ToString()).ToString("MM/dd/yyyy");
                    vEta = Convert.ToDateTime(dtSSRList.Rows[i]["ETA"].ToString()).ToString("MM/dd/yyyy");

                    temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDup(
                        vIdSsr.ToString(),
                        dtSSRList.Rows[i]["REFNO"].ToString(),
                        dtSSRList.Rows[i]["PC1_MOTHER"].ToString(),
                        dtSSRList.Rows[i]["PC2_MOTHER"].ToString(),
                        dtSSRList.Rows[i]["PRODLINE_NO"].ToString(),
                        dtSSRList.Rows[i]["QTY"].ToString(),
                        dtSSRList.Rows[i]["M_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["M_TOTAL_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["SUBSLIT_WASTE"].ToString(),
                        vEtd, vEta, 1);

                    if (int.TryParse(temp2, out chkint) && temp2 != "0")
                    {
                        DataTable dtMothSeq = Library.Database.BLL.SubSlitRequest.GetMotherSeq(vIdSsr, temp2);
                        if (dtMothSeq.Rows.Count > 0)
                        {
                            vMothSeq = Convert.ToInt32(dtMothSeq.Rows[0]["SUBSLIT_REQ_MOTHER_SEQNO"]);
                        }
                        else
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                                "Error - Couldn't find SubSlitting Request Mother SeqNo for PC2 Mother " +
                                dtSSRList.Rows[i]["PC2_MOTHER"].ToString() +
                                " and SubSlitting Request ID " + vIdSsr);
                            return;
                        }
                    }
                    else
                    {
                        if (temp2 == "0")
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, "1"));
                        else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp2);
                    }

                    temp3 = Library.Database.BLL.SubSlitRequest.SubSlitChildDup(
                        vMothSeq, rRefno,
                        dtSSRList.Rows[i]["PC1_CUST"].ToString(),
                        dtSSRList.Rows[i]["PC2_CUST"].ToString(),
                        dtSSRList.Rows[i]["C_QTY"].ToString(),
                        dtSSRList.Rows[i]["C_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["REMARK"].ToString(),
                        "0", 1);

                    if (temp3 != "1")
                    {
                        if (temp3 == "0")
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, "1"));
                        else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp3);
                        return;
                    }
                }

                prevSeqMother = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();
            }

            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "The Sub Slittng Request for RefNo " + rRefno +
                " and New Revision " + revCount.ToString() + " is created successfully.");

            Response.Redirect("~/Transactions/SUBSLIT_REQ_.aspx?itm1=" + rRefno + "&itm2= " + vIdSsr);
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "No Sub Slitting Request records for Refno " +
                dtSsr.Rows[0]["REFNO"].ToString() + " and Revision " +
                dtSsr.Rows[0]["REVISIONCOUNT"].ToString());
        }
    }

    protected void grdList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            GridViewRow gr = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
            TableCell tc = new TableCell();

            AddMergedCells(gr, ref tc, 2, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "Total", Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblMQty.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblMTotalWeight.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 2, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblCQty.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblCTotalWeight.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblSubSlitWaste.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 3, string.Empty, Color.AliceBlue.Name);

            gr.Cells.Add(tc);

            Table gvTable = (Table)e.Row.Parent;
            gvTable.Controls.Add(gr);
        }
    }

    protected void AddMergedCells(GridViewRow objGridViewRow, ref TableCell objTableCell, int colspan, string cellText, string backColor)
    {
        objTableCell = new TableCell();
        objTableCell.Text = cellText;
        objTableCell.ColumnSpan = colspan;
        objTableCell.Style.Add("background-color", backColor);
        objTableCell.HorizontalAlign = HorizontalAlign.Center;
        objGridViewRow.Cells.Add(objTableCell);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string temp = "0";
        if (rbyes.Checked)
        {
            temp = Library.Database.BLL.SubSlitRequest.SubSlitMaint(
                Request.QueryString["id"], string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, 5);

            if (temp == "1")
            {
                Response.Redirect("~/Transactions/SSR_SEARCH.aspx");
            }
        }
        else
        {
            string message = "Please Choose Yes to confirm delete";
            string script = "<script type='text/javascript'> alert('" + message + "');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox", script);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Transactions/SSR_SEARCH.aspx");
    }
}