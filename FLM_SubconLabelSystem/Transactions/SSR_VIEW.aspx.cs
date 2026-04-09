using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_SSR_VIEW : System.Web.UI.Page
{
    public string[] lbl;

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

        DataTable dt_sel = Library.Database.BLL.SubSlitRequest.GetRefNoList();

        string lblstr = "";

        if (dt_sel.Rows.Count > 0)
        {
            for (int i = 0; i <= dt_sel.Rows.Count - 1; i++)
            {
                lblstr += "\"" + dt_sel.Rows[i]["REFNO"].ToString() + "\"" + ",";
            }
            lblstr = lblstr.Substring(0, lblstr.Length - 1);
        }

        lbl = lblstr.Split(',');
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtSubReqId.Visible = false;

        DataTable _dtGetDept = Library.Database.BLL.SubSlitRequest.GetUserData(Session["USERID"].ToString());
        lblDept.Text = _dtGetDept.Rows[0]["DEPARTMENT"].ToString();
        lblBy.Text = _dtGetDept.Rows[0]["NAME"].ToString();

        if (!Page.IsPostBack)
        {
            UpdStat_Button.Visible = false;
            NewRev_Button.Visible = false;
            Export_Button.Visible = false;
            Cancel_Button.Visible = false;
            SSRTotal.Visible = false;
        }

        if (Page.IsPostBack)
        {
            Display_SSRListing();
        }

        if (Session["ULEVEL"].ToString() == "2" || Session["ULEVEL"].ToString() == "3")
        {
            ddlReqStat.Enabled = false;
        }
        else
        {
            ddlReqStat.Enabled = true;
        }
    }

    protected void Display_SSRListing()
    {
        DataTable dtSSRList = new DataTable();
        DataTable dtWBlank = new DataTable();

        dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, "0");

        if (dtSSRList.Rows.Count > 0 && !txtRefNo.Text.Equals(""))
        {
            if (dtSSRList.Rows[0]["REQUEST_STATUS"].Equals("Submitted") ||
                dtSSRList.Rows[0]["REQUEST_STATUS"].Equals("Cancel"))
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
                dtWBlank.Columns.Add(new DataColumn("CHK", typeof(string)));

                string Prev_PC2Mother = "";
                DataRow dr = null;

                int C_Qty = 0;
                decimal C_TotalWeight = 0;
                int M_Qty = 0;
                decimal M_TotalWeight = 0;
                decimal SubSlitWaste = 0;

                int k = -1;

                for (int i = 0; i <= dtSSRList.Rows.Count - 1; i++)
                {
                    if (dtSSRList.Rows[i]["PC2_MOTHER"].ToString().Equals(Prev_PC2Mother))
                    {
                        dr = dtWBlank.NewRow();
                        dtWBlank.Rows.Add(dr);
                        k++;

                        dtWBlank.Rows[k]["REFNO"] = dtSSRList.Rows[i]["REFNO"];
                        dtWBlank.Rows[k]["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                        dtWBlank.Rows[k]["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                        dtWBlank.Rows[k]["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];
                        dtWBlank.Rows[k]["C_WEIGHT"] = dtSSRList.Rows[i]["C_WEIGHT"];
                        dtWBlank.Rows[k]["C_TOTAL_WEIGHT"] = dtSSRList.Rows[i]["C_TOTAL_WEIGHT"];
                        dtWBlank.Rows[k]["REMARK"] = dtSSRList.Rows[i]["REMARK"];

                        dtWBlank.Rows[k]["PRODLINE_NO"] = "";
                        dtWBlank.Rows[k]["PC1_MOTHER"] = "";
                        dtWBlank.Rows[k]["PC2_MOTHER"] = "";
                        dtWBlank.Rows[k]["QTY"] = DBNull.Value;
                        dtWBlank.Rows[k]["M_WEIGHT"] = DBNull.Value;
                        dtWBlank.Rows[k]["M_TOTAL_WEIGHT"] = DBNull.Value;
                        dtWBlank.Rows[k]["SUBSLIT_WASTE"] = DBNull.Value;
                        dtWBlank.Rows[k]["ETD"] = DBNull.Value;
                        dtWBlank.Rows[k]["ETA"] = DBNull.Value;
                        dtWBlank.Rows[k]["CHK"] = "0";

                        C_Qty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                        C_TotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                    }
                    else
                    {
                        if (!Prev_PC2Mother.Equals(""))
                        {
                            k++;
                            dr = dtWBlank.NewRow();
                            dtWBlank.Rows.Add(dr);
                            dtWBlank.Rows[k]["PC2_MOTHER"] = "";
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
                        dtWBlank.Rows[k]["M_WEIGHT"] = dtSSRList.Rows[i]["M_WEIGHT"];
                        dtWBlank.Rows[k]["M_TOTAL_WEIGHT"] = dtSSRList.Rows[i]["M_TOTAL_WEIGHT"];

                        dtWBlank.Rows[k]["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                        dtWBlank.Rows[k]["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                        dtWBlank.Rows[k]["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];
                        dtWBlank.Rows[k]["C_WEIGHT"] = dtSSRList.Rows[i]["C_WEIGHT"];
                        dtWBlank.Rows[k]["C_TOTAL_WEIGHT"] = dtSSRList.Rows[i]["C_TOTAL_WEIGHT"];

                        dtWBlank.Rows[k]["SUBSLIT_WASTE"] = dtSSRList.Rows[i]["SUBSLIT_WASTE"];
                        dtWBlank.Rows[k]["ETD"] = dtSSRList.Rows[i]["ETD"];
                        dtWBlank.Rows[k]["ETA"] = dtSSRList.Rows[i]["ETA"];
                        dtWBlank.Rows[k]["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                        dtWBlank.Rows[k]["CHK"] = "1";

                        M_Qty += Convert.ToInt32(dtSSRList.Rows[i]["QTY"]);
                        M_TotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                        SubSlitWaste += Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                        C_Qty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                        C_TotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                    }

                    Prev_PC2Mother = dtSSRList.Rows[i]["PC2_MOTHER"].ToString();
                }

                lblMQty.Text = M_Qty.ToString();
                lblMTotalWeight.Text = M_TotalWeight.ToString();
                lblCQty.Text = C_Qty.ToString();
                lblCTotalWeight.Text = C_TotalWeight.ToString();
                lblSubSlitWaste.Text = SubSlitWaste.ToString();

                ViewState["dtWBlank"] = dtWBlank;
                grdList.DataSource = dtWBlank;
                grdList.DataBind();

                if (Session["ULEVEL"].ToString() == "3")
                {
                    ddlReqStat.Enabled = false;
                    UpdStat_Button.Visible = true;
                    NewRev_Button.Visible = false;
                    Export_Button.Visible = false;
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
        }
        else
        {
            UpdStat_Button.Visible = false;
            NewRev_Button.Visible = false;
            Export_Button.Visible = false;
            Cancel_Button.Visible = false;
            SSRTotal.Visible = false;
        }
    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;

        if (ViewState["dtWBlank"] != null)
        {
            grdList.DataSource = ViewState["dtWBlank"];
            grdList.DataBind();
        }
    }

    protected void Export_Button_Click(object sender, EventArgs e)
    {
        string r_Refno = txtRefNo.Text;
        int r_ID_SSR = Convert.ToInt32(txtSubReqId.Text);

        string userid = Session["USERID"].ToString();

        string ssr_str = Library.Database.BLL.SubSlitRequest.GET_SSR_TO_EXCEL(
            txtRefNo.Text, Convert.ToInt32(txtSubReqId.Text), userid,
            Convert.ToInt32(lblMQty.Text), Convert.ToDecimal(lblMTotalWeight.Text),
            Convert.ToInt32(lblCQty.Text), Convert.ToDecimal(lblCTotalWeight.Text),
            Convert.ToDecimal(lblSubSlitWaste.Text), (DataTable)ViewState["dtWBlank"]);

        string _str_fileName = "SubSlittingRequest" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        _str_fileName = "attachment;filename=" + _str_fileName;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", _str_fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.Output.Write(ssr_str);
        Response.Flush();
        Response.End();
    }

    protected void UpdStat_Button_Click(object sender, EventArgs e)
    {
        string _temp = "0";
        int r_ID_SSR = Convert.ToInt32(txtSubReqId.Text);

        _temp = Library.Database.BLL.SubSlitRequest.SSRUpdateStat(
            txtRefNo.Text, r_ID_SSR,
            ddlReqStat.SelectedValue, ddlVenStat.SelectedValue);

        if (_temp == "1")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "The Status of Sub Slittng Request under " + txtRefNo.Text + " is updated successfully.");
        }
        else
        {
            if (_temp == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    string.Format(Resources.Message.Failed, "1"));
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp);
            }
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
        Response.End();
    }

    protected void NewRev_Button_Click(object sender, EventArgs e)
    {
        string _temp = "0";
        string _temp2 = "0";
        string _temp3 = "0";
        int v_IDSSR = 0;
        int v_MothSeq = 0;
        string v_ETD = "";
        string v_ETA = "";

        string r_Refno = txtRefNo.Text;
        int r_ID_SSR = Convert.ToInt32(txtSubReqId.Text);

        DataTable dt_SSR = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno, r_ID_SSR);

        int RevCount = Convert.ToInt32(dt_SSR.Rows[0]["REVISIONCOUNT"]) + 1;
        string Trans_Date = Convert.ToDateTime(dt_SSR.Rows[0]["DATEREQ"].ToString()).ToString("MM-dd-yyyy");

        _temp = Library.Database.BLL.SubSlitRequest.SubSlitDup(
            "0",
            dt_SSR.Rows[0]["COMPANYFROM"].ToString(),
            dt_SSR.Rows[0]["COMPANYTO"].ToString(),
            dt_SSR.Rows[0]["REFNO"].ToString(),
            RevCount,
            Trans_Date,
            "New", "N/A", 1);

        DataTable dt_IDSSR = Library.Database.BLL.SubSlitRequest.GetIDSSR(
            dt_SSR.Rows[0]["REFNO"].ToString(), RevCount);

        if (dt_IDSSR.Rows.Count > 0)
        {
            v_IDSSR = Convert.ToInt32(dt_IDSSR.Rows[0]["ID_SUBSLIT_REQUEST"]);
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "Error - Couldn't find Sub Slitting Request ID for Refno " +
                dt_SSR.Rows[0]["REFNO"].ToString() + " and Revision " + RevCount);
            return;
        }

        DataTable dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(r_Refno, r_ID_SSR);

        if (dtSSRList.Rows.Count > 0)
        {
            string Prev_PC2Mother = "";

            for (int i = 0; i <= dtSSRList.Rows.Count - 1; i++)
            {
                if (dtSSRList.Rows[i]["PC2_MOTHER"].ToString().Equals(Prev_PC2Mother))
                {
                    _temp3 = Library.Database.BLL.SubSlitRequest.SubSlitChildDup(
                        v_MothSeq, r_Refno,
                        dtSSRList.Rows[i]["PC1_CUST"].ToString(),
                        dtSSRList.Rows[i]["PC2_CUST"].ToString(),
                        dtSSRList.Rows[i]["C_QTY"].ToString(),
                        dtSSRList.Rows[i]["C_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["REMARK"].ToString(),
                        "0", 1);

                    if (_temp3 != "1")
                    {
                        if (_temp3 == "0")
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                                string.Format(Resources.Message.Failed, "1"));
                        else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp3);
                        return;
                    }
                }
                else
                {
                    v_ETD = Convert.ToDateTime(dtSSRList.Rows[i]["ETD"].ToString()).ToString("MM-dd-yyyy");
                    v_ETA = Convert.ToDateTime(dtSSRList.Rows[i]["ETA"].ToString()).ToString("MM-dd-yyyy");

                    _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDup(
                        v_IDSSR.ToString(),
                        dtSSRList.Rows[i]["REFNO"].ToString(),
                        dtSSRList.Rows[i]["PC1_MOTHER"].ToString(),
                        dtSSRList.Rows[i]["PC2_MOTHER"].ToString(),
                        dtSSRList.Rows[i]["PRODLINE_NO"].ToString(),
                        dtSSRList.Rows[i]["QTY"].ToString(),
                        dtSSRList.Rows[i]["M_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["M_TOTAL_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["SUBSLIT_WASTE"].ToString(),
                        v_ETD, v_ETA, 1);

                    if (_temp2 == "1")
                    {
                        DataTable dt_MothSeq = Library.Database.BLL.SubSlitRequest.GetMotherSeq(
                            v_IDSSR, dtSSRList.Rows[i]["PC2_MOTHER"].ToString());

                        if (dt_MothSeq.Rows.Count > 0)
                        {
                            v_MothSeq = Convert.ToInt32(dt_MothSeq.Rows[0]["SUBSLIT_REQ_MOTHER_SEQNO"]);
                        }
                        else
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                                "Error - Couldn't find SubSlitting Request Mother SeqNo for PC2 Mother " +
                                dtSSRList.Rows[i]["PC2_MOTHER"].ToString() +
                                " and SubSlitting Request ID " + v_IDSSR);
                            return;
                        }
                    }
                    else
                    {
                        if (_temp2 == "0")
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                                string.Format(Resources.Message.Failed, "1"));
                        else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp2);
                    }

                    _temp3 = Library.Database.BLL.SubSlitRequest.SubSlitChildDup(
                        v_MothSeq, r_Refno,
                        dtSSRList.Rows[i]["PC1_CUST"].ToString(),
                        dtSSRList.Rows[i]["PC2_CUST"].ToString(),
                        dtSSRList.Rows[i]["C_QTY"].ToString(),
                        dtSSRList.Rows[i]["C_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString(),
                        dtSSRList.Rows[i]["REMARK"].ToString(),
                        "0", 1);

                    if (_temp3 != "1")
                    {
                        if (_temp3 == "0")
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                                string.Format(Resources.Message.Failed, "1"));
                        else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp3);
                        return;
                    }
                }

                Prev_PC2Mother = dtSSRList.Rows[i]["PC2_MOTHER"].ToString();
            }

            string mssg = "The Sub Slittng Request for RefNo " + r_Refno +
                " and New Revision " + RevCount.ToString() + " is created successfully.";
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, mssg);

            string url = "~/Transactions/SUBSLIT_REQ_.aspx?itm1=" + r_Refno + "&itm2= " + v_IDSSR;
            Response.Redirect(url);
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "No Sub Slitting Request records for Refno " +
                dt_SSR.Rows[0]["REFNO"].ToString() + " and Revision " +
                dt_SSR.Rows[0]["REVISIONCOUNT"].ToString());
            return;
        }
    }

    protected void txtRefNo_TextChanged(object sender, EventArgs e)
    {
        DataTable dtmaxRev = Library.Database.BLL.SubSlitRequest.chkRefNo(txtRefNo.Text);

        if (dtmaxRev.Rows.Count > 0)
        {
            if (dtmaxRev.Rows[0]["REQUEST_STATUS"].Equals("New"))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    "This Refno Status is New. Please go to Sub Slitting Request - Add screen");
                txtRefNo.Text = "";
            }
            else
            {
                txtSubReqId.Text = dtmaxRev.Rows[0]["ID_SUBSLIT_REQUEST"].ToString();
                lblCompCode.Text = dtmaxRev.Rows[0]["COMPANYTO"].ToString();
                lblDate.Text = Convert.ToDateTime(dtmaxRev.Rows[0]["DATEREQ"].ToString()).ToString("dd-MM-yyyy");
                lblRev.Text = dtmaxRev.Rows[0]["REVISIONCOUNT"].ToString();

                ddlReqStat.SelectedValue = dtmaxRev.Rows[0]["REQUEST_STATUS"].ToString();
                ddlVenStat.SelectedValue = dtmaxRev.Rows[0]["VENDOR_STATUS"].ToString();

                if (ddlReqStat.SelectedValue.Equals("Cancel"))
                {
                    ddlVenStat.Enabled = false;
                    ddlReqStat.Enabled = false;
                }

                Display_SSRListing();
            }
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "This Refno does not exist.");
            txtRefNo.Text = "";
        }
    }

    protected void grdList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            GridView gv1 = (GridView)sender;
            TableCell tc = new TableCell();
            GridViewRow gr = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

            AddMergedCells(gr, ref tc, 2, "", Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "Total", Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblMQty.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "", Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblMTotalWeight.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 2, "", Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblCQty.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "", Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblCTotalWeight.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblSubSlitWaste.Text, Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 3, "", Color.AliceBlue.Name);

            gr.Cells.Add(tc);

            Table gvTable = (Table)e.Row.Parent;
            gvTable.Controls.Add(gr);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, ref TableCell objtablecell,
        int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
}