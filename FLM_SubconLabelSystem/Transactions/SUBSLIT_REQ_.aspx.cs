using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_SubSlitRequest : System.Web.UI.Page
{
    private string Trans_Date = "";
    private string ETD_Date = "";
    private string ETA_Date = "";
    private string FirstTime_Flag = "Y";
    private string Edit_Flag = "N";
    public string[] lbl;
    public string[] lbl2;
    public string[] lbl3;
    public string[] lblchild;
    public string[] lblchild2;

    private DataTable dtPC1;
    private DataTable dtPC2;
    private DataTable dtProdLine;
    private DataTable dtPC1Child;
    private DataTable dtPC2Child;

    protected void Page_Init(object sender, EventArgs e)
    {
        btnNext.Click += btnNext_Click;
        btnReset.Click += btnReset_Click;
        btnAddRow.Click += (s, ev) => btnAddRow_Click();
        btnSave.Click += btnSave_Click;
        btnEdit.Click += btnEdit_Click;
        btnDelete.Click += btnDelete_Click;
        btnpc2.Click += btnpc2_Click;
        btnpc2child.Click += btnpc2child_Click;
        Button1.Click += Button1_Click;
        Button2.Click += Button2_Click;
        Button3.Click += Button3_Click;
        Submit_Button.Click += Submit_Button_Click;
        Cancel_Button.Click += Cancel_Button_Click;
        ddlCompCode.SelectedIndexChanged += ddlCompCode_SelectedIndexChanged;
        txtQty.TextChanged += txtQty_TextChanged;
        grdChild.RowCommand += grdChildRowCommand;

        DataTable _dtCompName = Library.Database.BLL.SubSlitRequest.GetDLLData("CompanyCode", "");

        ddlCompCode.Items.Clear();
        ddlCompCode.Items.Add(new ListItem("--Select--", "0"));
        for (int i = 0; i < _dtCompName.Rows.Count; i++)
        {
            ddlCompCode.Items.Add(new ListItem(
                _dtCompName.Rows[i]["CompanyCode"].ToString(),
                _dtCompName.Rows[i]["ID_MM_COMPANY"].ToString()));
        }
        ddlCompCode.DataBind();

        if (ViewState["CompCode"] != null)
        {
            ddlCompCode.SelectedIndex = Convert.ToInt32(ViewState["CompCode"]);
        }

        if (ViewState["PRODLINE"] == null)
        {
            dtProdLine = Library.Database.BLL.SubSlitRequest.GetDLLData("PRODLINE", "");
            lbl = Array.ConvertAll(dtProdLine.Select(), row => "\"" + row["PRODLINE_NO"].ToString() + "\"");
            ViewState["lbl"] = lbl;
            ViewState["PRODLINE"] = dtProdLine;
        }
        else
        {
            dtProdLine = (DataTable)ViewState["PRODLINE"];
            lbl = (string[])ViewState["lbl"];
        }

        if (ViewState["PC1"] == null)
        {
            dtPC1 = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "");
            lbl2 = Array.ConvertAll(dtPC1.Select(), row => "\"" + row["PC1"].ToString() + "\"");
            ViewState["PC1"] = dtPC1;
            ViewState["lbl2"] = lbl2;
        }
        else
        {
            dtPC1 = (DataTable)ViewState["PC1"];
            lbl2 = (string[])ViewState["lbl2"];
        }

        if (ViewState["PC2"] == null)
        {
            dtPC2 = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "");
            lbl3 = Array.ConvertAll(dtPC2.Select(), row => "\"" + row["PC2"].ToString() + "\"");
            ViewState["PC2"] = dtPC2;
            ViewState["lbl3"] = lbl3;
        }
        else
        {
            dtPC2 = (DataTable)ViewState["PC2"];
            lbl3 = (string[])ViewState["lbl3"];
        }

        if (ViewState["PC1_1_Child"] == null)
        {
            dtPC1Child = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "");
            lblchild = Array.ConvertAll(dtPC1Child.Select(), row => "\"" + row["PC1"].ToString() + "\"");
            ViewState["PC1_1_Child"] = dtPC1Child;
            ViewState["lblchild"] = lblchild;
        }
        else
        {
            dtPC1Child = (DataTable)ViewState["PC1_1_Child"];
            lblchild = (string[])ViewState["lblchild"];
        }

        if (ViewState["PC1_2_Child"] == null)
        {
            dtPC2Child = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "");
            lblchild2 = Array.ConvertAll(dtPC2Child.Select(), row => "\"" + row["PC2"].ToString() + "\"");
            ViewState["PC1_2_Child"] = dtPC2Child;
            ViewState["lblchild2"] = lblchild2;
        }
        else
        {
            dtPC2Child = (DataTable)ViewState["PC1_2_Child"];
            lblchild2 = (string[])ViewState["lblchild2"];
        }

        txtRefNo.ReadOnly = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtETD.Attributes.Add("readonly", "readonly");
        txtETA.Attributes.Add("readonly", "readonly");

        if (Request.QueryString["itm1"] == null && Request.QueryString["itm2"] == null)
        {
            if (!Page.IsPostBack)
            {
                pnlChild.Visible = false;
                pnlList.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                Submit_Button.Visible = false;
                Cancel_Button.Visible = false;
            }
        }
        else
        {
            string decodedString = HttpUtility.HtmlDecode(Request.QueryString["itm1"].Trim());
            string r_Refno = decodedString;
            int r_ID_SSR = Convert.ToInt32(Request.QueryString["itm2"].Trim());
            DataTable dt_SSR = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(r_Refno, r_ID_SSR);

            if (!Page.IsPostBack)
            {
                ddlCompCode.SelectedValue = dt_SSR.Rows[0]["COMPTOID"].ToString();
                txtRefNo.Text = dt_SSR.Rows[0]["REFNO"].ToString();
                txtDate.Text = Convert.ToDateTime(dt_SSR.Rows[0]["DATEREQ"].ToString()).ToString("dd/MM/yyyy");
            }

            lblRev.Text = dt_SSR.Rows[0]["REVISIONCOUNT"].ToString();
            lblVenStat.Text = dt_SSR.Rows[0]["VENDOR_STATUS"].ToString();

            if (!Page.IsPostBack)
            {
                pnlChild.Visible = false;
            }

            pnlList.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnNext.Visible = true;
            Submit_Button.Visible = true;
            Cancel_Button.Visible = true;

            Display_SSRListing();
        }

        DataTable _dtGetDept = Library.Database.BLL.SubSlitRequest.GetUserData(Session["USERID"].ToString());
        lblDept.Text = _dtGetDept.Rows[0]["DEPARTMENT"].ToString();
        lblBy.Text = _dtGetDept.Rows[0]["NAME"].ToString();

        if (ViewState["dtWBlank"] != null)
        {
            grdList.DataSource = (DataTable)ViewState["dtWBlank"];
            grdList.DataBind();
        }

        if (Session["CheckBoxArray"] != null)
        {
            ArrayList CheckBoxArray = (ArrayList)Session["CheckBoxArray"];
            ArrayList PC2MotherArray = (ArrayList)Session["PC2MotherArray"];

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (grdList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    int CheckBoxIndex = grdList.PageSize * grdList.PageIndex + (i + 1);

                    HiddenField h_PC2Mother = (HiddenField)grdList.Rows[i].FindControl("HiddenField1");
                    string h_PC2Mother_Value = h_PC2Mother.Value;
                    HiddenField h_PC1Mother = (HiddenField)grdList.Rows[i].FindControl("HiddenField2");
                    string h_PC1Mother_Value = h_PC1Mother.Value;
                    HiddenField h_ProdLineNo = (HiddenField)grdList.Rows[i].FindControl("HiddenField3");
                    string h_ProdLineNo_Value = h_ProdLineNo.Value;
                    HiddenField h_SeqMother = (HiddenField)grdList.Rows[i].FindControl("HiddenField4");
                    string h_SeqMother_Value = h_SeqMother.Value;

                    CheckBox chk = (CheckBox)grdList.Rows[i].FindControl("RadioButton1");

                    if (chk.Visible)
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1
                            && h_PC2Mother_Value == Session["PC2Mother"].ToString()
                            && h_PC1Mother_Value == Session["PC1Mother"].ToString()
                            && h_ProdLineNo_Value == Session["ProdLineNo"].ToString()
                            && h_SeqMother_Value == Session["SeqMother"].ToString())
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                    }
                }
            }
        }
    }

    protected void calTotalWeight()
    {
        decimal qtyVal;
        decimal unitWeightVal;

        if (!string.IsNullOrEmpty(lblUnitWeight.Text) && !string.IsNullOrEmpty(txtQty.Text)
            && decimal.TryParse(txtQty.Text, out qtyVal)
            && decimal.TryParse(lblUnitWeight.Text, out unitWeightVal))
        {
            decimal totWeight = Math.Round(qtyVal * unitWeightVal, 1);
            string totWeightStr = totWeight.ToString();
            if (!totWeightStr.Contains("."))
                totWeightStr += ".0";
            decimal tot = decimal.Parse(totWeightStr);
            lblTotWeight.Text = tot.ToString("#,###,###,##0.0");
        }
        else
        {
            lblTotWeight.Text = "0.0";
        }
    }

    public void Calculate()
    {
        DataTable _dtPC2id = Library.Database.BLL.SubSlitRequest.GetPC2ID(ddlPC2.Text);
        decimal Unit_Weight = 0.0m;

        if (_dtPC2id.Rows.Count > 0)
        {
            string PC2ID = _dtPC2id.Rows[0]["ID_MM_PC2"].ToString();
            DataTable _dtGetUWeight = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID);
            Unit_Weight = Convert.ToDecimal(_dtGetUWeight.Rows[0]["UNIT_WEIGHT"]);
            lblUnitWeight.Text = Unit_Weight.ToString("#,###,###,##0.0");
        }
        else
        {
            lblUnitWeight.Text = "0.0";
        }

        if (txtQty.Text == "0")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter Qty more than 0.");
            return;
        }

        calTotalWeight();

        decimal parsedTot;
        decimal momSubslit = 0.0m;
        if (decimal.TryParse(lblTotWeight.Text.Replace(",", ""), out parsedTot))
            momSubslit = parsedTot;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                    TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                    TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                    Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                    Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                    TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                    dt.Rows[i]["PC1_CUST"] = ddlPC1Child.Text;
                    dt.Rows[i]["PC2_CUST"] = ddlPC2Child.Text;
                    dt.Rows[i]["C_QTY"] = string.IsNullOrEmpty(txtQtyC.Text) ? (object)DBNull.Value : txtQtyC.Text;
                    dt.Rows[i]["C_WEIGHT"] = string.IsNullOrEmpty(lblUnitWeightC.Text) ? (object)DBNull.Value : lblUnitWeightC.Text;
                    dt.Rows[i]["C_TOTAL_WEIGHT"] = string.IsNullOrEmpty(lblTotWeightC.Text) ? (object)DBNull.Value : lblTotWeightC.Text;
                    dt.Rows[i]["REMARK"] = string.IsNullOrEmpty(txtRemarkC.Text) ? (object)DBNull.Value : txtRemarkC.Text;

                    if (!string.IsNullOrEmpty(ddlPC2Child.Text))
                    {
                        DataTable _dtPC2Childid = Library.Database.BLL.SubSlitRequest.GetPC2ID(ddlPC2Child.Text);
                        string PC2ChildID = _dtPC2Childid.Rows[0]["ID_MM_PC2"].ToString();
                        DataTable _dtGetUWeightC = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ChildID);
                        lblUnitWeightC.Text = _dtGetUWeightC.Rows[0]["UNIT_WEIGHT"].ToString();
                        dt.Rows[i]["C_WEIGHT"] = lblUnitWeightC.Text;
                    }
                    else
                    {
                        lblUnitWeightC.Text = "";
                        dt.Rows[i]["C_WEIGHT"] = DBNull.Value;
                    }

                    decimal cQty;
                    decimal cUnitW;
                    if (!string.IsNullOrEmpty(lblUnitWeightC.Text) && !string.IsNullOrEmpty(txtQtyC.Text)
                        && decimal.TryParse(txtQtyC.Text, out cQty)
                        && decimal.TryParse(lblUnitWeightC.Text, out cUnitW))
                    {
                        decimal cTot = Math.Round(cQty * cUnitW, 1);
                        string cTotStr = cTot.ToString();
                        if (!cTotStr.Contains(".")) cTotStr += ".0";
                        lblTotWeightC.Text = cTotStr;
                        dt.Rows[i]["C_TOTAL_WEIGHT"] = lblTotWeightC.Text;
                    }
                    else
                    {
                        lblTotWeightC.Text = "";
                        dt.Rows[i]["C_TOTAL_WEIGHT"] = DBNull.Value;
                    }

                    decimal mTot;
                    decimal cTotW;
                    if (!string.IsNullOrEmpty(lblTotWeight.Text) && !string.IsNullOrEmpty(lblTotWeightC.Text)
                        && decimal.TryParse(lblTotWeight.Text.Replace(",", ""), out mTot)
                        && decimal.TryParse(lblTotWeightC.Text.Replace(",", ""), out cTotW))
                    {
                        momSubslit = decimal.Parse((Convert.ToDouble(momSubslit) - Convert.ToDouble(cTotW)).ToString("0.0"));
                    }
                }
                ViewState["CurrentTable"] = dt;
            }
        }

        lblSubSlit.Text = momSubslit.ToString("#,###,###,##0.0");
        SetPreviousData();

        dtProdLine = (DataTable)ViewState["PRODLINE"];
        lbl = (string[])ViewState["lbl"];
        dtPC1 = (DataTable)ViewState["PC1"];
        lbl2 = (string[])ViewState["lbl2"];
        dtPC2 = (DataTable)ViewState["PC2"];
        lbl3 = (string[])ViewState["lbl3"];
        dtPC1Child = (DataTable)ViewState["PC1_1_Child"];
        lblchild = (string[])ViewState["lblchild"];
        dtPC2Child = (DataTable)ViewState["PC1_2_Child"];
        lblchild2 = (string[])ViewState["lblchild2"];
    }

    public void ChildCalculate()
    {
        decimal parsedTot;
        decimal momSubslit = 0.0m;
        if (decimal.TryParse(lblTotWeight.Text.Replace(",", ""), out parsedTot))
            momSubslit = parsedTot;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                    TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                    TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                    Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                    Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                    TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                    dt.Rows[i]["PC1_CUST"] = ddlPC1Child.Text;
                    dt.Rows[i]["PC2_CUST"] = ddlPC2Child.Text;
                    dt.Rows[i]["C_QTY"] = string.IsNullOrEmpty(txtQtyC.Text) ? (object)DBNull.Value : txtQtyC.Text;
                    dt.Rows[i]["C_WEIGHT"] = string.IsNullOrEmpty(lblUnitWeightC.Text) ? (object)DBNull.Value : lblUnitWeightC.Text;
                    dt.Rows[i]["C_TOTAL_WEIGHT"] = string.IsNullOrEmpty(lblTotWeightC.Text) ? (object)DBNull.Value : lblTotWeightC.Text;
                    dt.Rows[i]["REMARK"] = string.IsNullOrEmpty(txtRemarkC.Text) ? (object)DBNull.Value : txtRemarkC.Text;

                    if (!string.IsNullOrEmpty(ddlPC2Child.Text))
                    {
                        DataTable _dtPC2id = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text);
                        string PC2ID = _dtPC2id.Rows[0]["ID_MM_PC2"].ToString();
                        DataTable _dtGetUWeightC = Library.Database.BLL.SubSlitRequest.GetPC2Data(PC2ID);
                        decimal unitW = Convert.ToDecimal(_dtGetUWeightC.Rows[0]["UNIT_WEIGHT"]);
                        lblUnitWeightC.Text = unitW.ToString("#,###,###,##0.0");
                        dt.Rows[i]["C_WEIGHT"] = lblUnitWeightC.Text;
                    }
                    else
                    {
                        lblUnitWeightC.Text = "";
                        dt.Rows[i]["C_WEIGHT"] = DBNull.Value;
                    }

                    decimal cQty;
                    decimal cUnitW;
                    if (!string.IsNullOrEmpty(lblUnitWeightC.Text) && !string.IsNullOrEmpty(txtQtyC.Text)
                        && decimal.TryParse(txtQtyC.Text, out cQty)
                        && decimal.TryParse(lblUnitWeightC.Text.Replace(",", ""), out cUnitW))
                    {
                        decimal cTot = Math.Round(cQty * cUnitW, 1);
                        string cTotStr = cTot.ToString();
                        if (!cTotStr.Contains(".")) cTotStr += ".0";
                        decimal totVal = decimal.Parse(cTotStr);
                        lblTotWeightC.Text = totVal.ToString("#,###,###,##0.0");
                        dt.Rows[i]["C_TOTAL_WEIGHT"] = lblTotWeightC.Text;
                    }
                    else
                    {
                        lblTotWeightC.Text = "";
                        dt.Rows[i]["C_TOTAL_WEIGHT"] = DBNull.Value;
                    }

                    decimal mTot;
                    decimal cTotW;
                    if (!string.IsNullOrEmpty(lblTotWeight.Text) && !string.IsNullOrEmpty(lblTotWeightC.Text)
                        && decimal.TryParse(lblTotWeight.Text.Replace(",", ""), out mTot)
                        && decimal.TryParse(lblTotWeightC.Text.Replace(",", ""), out cTotW))
                    {
                        momSubslit = decimal.Parse((Convert.ToDouble(momSubslit) - Convert.ToDouble(cTotW)).ToString("0.0"));
                    }
                }
                ViewState["CurrentTable"] = dt;
            }
        }

        lblSubSlit.Text = momSubslit.ToString("#,###,###,##0.0");
        SetPreviousData();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        Calculate();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
        btnNext.Visible = true;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!pnlChild.Visible)
        {
            if (ddlCompCode.SelectedValue == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select To which Company.");
                return;
            }
            if (string.IsNullOrEmpty(txtRefNo.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Insert Ref No.");
                return;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select Date.");
                return;
            }
            if (string.IsNullOrEmpty(ddlProdLine.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select Production Line.");
                return;
            }
            if (string.IsNullOrEmpty(ddlPC1.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC1.");
                return;
            }
            if (string.IsNullOrEmpty(ddlPC2.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC2.");
                return;
            }

            string _chk = Library.Database.BLL.SubSlitRequest.Chk_next(ddlProdLine.Text, ddlPC1.Text);

            if (_chk == "0")
            {
                decimal dummyQty;
                if (string.IsNullOrEmpty(txtQty.Text) || Convert.IsDBNull(txtQty.Text)
                    || txtQty.Text == "0" || !decimal.TryParse(txtQty.Text, out dummyQty))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter Qty more than 0.");
                    return;
                }
                if (string.IsNullOrEmpty(txtETD.Text))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select ETD PFR Date.");
                    return;
                }
                if (string.IsNullOrEmpty(txtETA.Text))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select ETA SUB-SLIT CONTRACTOR.");
                    return;
                }

                pnlChild.Visible = true;
                btnAddRow_Click();
            }
            else if (_chk == "1")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox",
                    "<script type='text/javascript'> alert('Invalid PC1 Mother');</script>");
                ddlPC1.Text = "";
            }
            else if (_chk == "2")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox",
                    "<script type='text/javascript'> alert('Invalid Production Line No');</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox",
                    "<script type='text/javascript'> alert('Invalid Production Line No and PC1 Mother');</script>");
            }
        }
    }

    protected void btnAddRow_Click()
    {
        DataTable dt;
        DataRow dr;

        if (ViewState["CurrentTable"] == null)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("IMAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("ID_SUBSLIT_REQUEST_CHILD", typeof(string)));
            dt.Columns.Add(new DataColumn("SUBSLIT_REQ_MOTHER_SEQNO", typeof(string)));
            dt.Columns.Add(new DataColumn("PC1_CUST", typeof(string)));
            dt.Columns.Add(new DataColumn("PC2_CUST", typeof(string)));
            dt.Columns.Add(new DataColumn("C_QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("C_WEIGHT", typeof(string)));
            dt.Columns.Add(new DataColumn("C_TOTAL_WEIGHT", typeof(string)));
            dt.Columns.Add(new DataColumn("REMARK", typeof(string)));
        }
        else
        {
            if (PC2ChildCheck() == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "PC2 Customer cannot duplicate");
                return;
            }

            dt = (DataTable)ViewState["CurrentTable"];

            TextBox ddlPC1Child = (TextBox)grdChild.Rows[dt.Rows.Count - 1].FindControl("ddlPC1Child");
            TextBox ddlPC2Child = (TextBox)grdChild.Rows[dt.Rows.Count - 1].FindControl("ddlPC2Child");
            TextBox txtQtyC = (TextBox)grdChild.Rows[dt.Rows.Count - 1].FindControl("txtQtyC");
            Label lblUnitWeightC = (Label)grdChild.Rows[dt.Rows.Count - 1].FindControl("lblUnitWeightC");
            Label lblTotWeightC = (Label)grdChild.Rows[dt.Rows.Count - 1].FindControl("lblTotWeightC");
            TextBox txtRemarkC = (TextBox)grdChild.Rows[dt.Rows.Count - 1].FindControl("txtRemarkC");

            decimal dummyQtyC;
            if (string.IsNullOrEmpty(txtQtyC.Text) || txtQtyC.Text == "0"
                || !decimal.TryParse(txtQtyC.Text, out dummyQtyC))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter Qty more than 0.");
                return;
            }

            dt.Rows[dt.Rows.Count - 1]["C_QTY"] = txtQtyC.Text;
            dt.Rows[dt.Rows.Count - 1]["C_WEIGHT"] = lblUnitWeightC.Text;
            dt.Rows[dt.Rows.Count - 1]["C_TOTAL_WEIGHT"] = lblTotWeightC.Text;
            dt.Rows[dt.Rows.Count - 1]["REMARK"] = txtRemarkC.Text;

            if (string.IsNullOrEmpty(ddlPC1Child.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC1 Customer Before Add New");
                return;
            }
            else
            {
                DataTable dtPC1child = Library.Database.BLL.SubSlitRequest.Chk_label("2", "", ddlPC1Child.Text, "");
                if (dtPC1child.Rows.Count == 0)
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Invalid PC1 Customer");
                    ddlPC1Child.Text = "";
                    return;
                }
            }

            if (string.IsNullOrEmpty(ddlPC2Child.Text))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC2 Customer Before Add New");
                return;
            }

            dt.Rows[dt.Rows.Count - 1]["PC1_CUST"] = ddlPC1Child.Text;
            dt.Rows[dt.Rows.Count - 1]["PC2_CUST"] = ddlPC2Child.Text;
        }

        dr = dt.NewRow();
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        grdChild.DataSource = dt;
        grdChild.DataBind();

        SetPreviousData();
    }

    private string PC2ChildCheck()
    {
        DataTable dtchk = (DataTable)ViewState["CurrentTable"];

        if (dtchk != null)
        {
            for (int i = 0; i < dtchk.Rows.Count; i++)
            {
                if (dtchk.Rows[i]["PC2_CUST"] == DBNull.Value) break;

                TextBox _ddlPC2Val1 = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");

                for (int j = 0; j < dtchk.Rows.Count; j++)
                {
                    if (dtchk.Rows[j]["PC2_CUST"] == DBNull.Value) break;

                    TextBox _ddlPC2Val2 = (TextBox)grdChild.Rows[j].FindControl("ddlPC2Child");
                    if (i != j && _ddlPC2Val1.Text == _ddlPC2Val2.Text)
                        return "0";
                }
            }
        }
        return "1";
    }

    private string PC2ChildValQty()
    {
        DataTable dtchk = (DataTable)ViewState["CurrentTable"];
        decimal dummy;

        if (dtchk != null)
        {
            if (dtchk.Rows.Count > 0)
            {
                for (int i = 0; i < dtchk.Rows.Count; i++)
                {
                    if (dtchk.Rows[i]["C_QTY"].ToString() == "0"
                        || Convert.IsDBNull(dtchk.Rows[i]["C_QTY"])
                        || !decimal.TryParse(dtchk.Rows[i]["C_QTY"].ToString(), out dummy))
                        return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        return "1";
    }

    private string PC1ChildVal()
    {
        DataTable dtPC1 = (DataTable)ViewState["CurrentTable"];

        if (dtPC1 != null)
        {
            if (dtPC1.Rows.Count > 0)
            {
                for (int i = 0; i < dtPC1.Rows.Count; i++)
                {
                    if (dtPC1.Rows[i]["PC1_CUST"].ToString() == "")
                        return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        return "1";
    }

    private string PC2ChildVal()
    {
        DataTable dtPC2 = (DataTable)ViewState["CurrentTable"];

        if (dtPC2 != null)
        {
            if (dtPC2.Rows.Count > 0)
            {
                for (int i = 0; i < dtPC2.Rows.Count; i++)
                {
                    if (dtPC2.Rows[i]["PC2_CUST"].ToString() == "")
                        return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        return "1";
    }

    protected void setChildDdl(int j)
    {
        // Intentionally empty
    }

    private void SetPreviousData()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                setChildDdl(i);

                TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                ddlPC1Child.Text = dt.Rows[i]["PC1_CUST"].ToString();
                ddlPC2Child.Text = dt.Rows[i]["PC2_CUST"].ToString();
                txtQtyC.Text = dt.Rows[i]["C_QTY"].ToString();
                lblUnitWeightC.Text = dt.Rows[i]["C_WEIGHT"].ToString();
                lblTotWeightC.Text = dt.Rows[i]["C_TOTAL_WEIGHT"].ToString();
                txtRemarkC.Text = dt.Rows[i]["REMARK"].ToString();
            }
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Intentionally empty
    }

    protected void grdChildRowCommand(object sender, GridViewCommandEventArgs e)
    {
        update_ChildRow();

        if (e.CommandName.ToUpper() == "DELETE")
        {
            Session["RowCommand"] = "DELETE";
        }

        if (Session["RowCommand"] != null && Session["RowCommand"].ToString() == "DELETE")
        {
            int Idx = int.Parse(e.CommandArgument.ToString());
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            dt.Rows[Idx].Delete();
            dt.AcceptChanges();

            ViewState["CurrentTable"] = dt;
            grdChild.DataSource = dt;
            grdChild.DataBind();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    setChildDdl(i);
                    TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                    TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                    TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                    Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                    Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                    TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                    ddlPC1Child.Text = dt.Rows[i]["PC1_CUST"].ToString();
                    ddlPC2Child.Text = dt.Rows[i]["PC2_CUST"].ToString();
                    txtQtyC.Text = dt.Rows[i]["C_QTY"].ToString();
                    lblUnitWeightC.Text = dt.Rows[i]["C_WEIGHT"].ToString();
                    lblTotWeightC.Text = dt.Rows[i]["C_TOTAL_WEIGHT"].ToString();
                    txtRemarkC.Text = dt.Rows[i]["REMARK"].ToString();
                }
            }
            else
            {
                ViewState["CurrentTable"] = null;
            }
            ChildCalculate();
        }
    }

    protected void update_ChildRow()
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                dt.Rows[i]["C_QTY"] = txtQtyC.Text;
                dt.Rows[i]["C_WEIGHT"] = lblUnitWeightC.Text;
                dt.Rows[i]["C_TOTAL_WEIGHT"] = lblTotWeightC.Text;
                dt.Rows[i]["REMARK"] = txtRemarkC.Text;
                dt.Rows[i]["PC1_CUST"] = ddlPC1Child.Text;
                dt.Rows[i]["PC2_CUST"] = ddlPC2Child.Text;
            }
        }

        ViewState["CurrentTable"] = dt;
        grdChild.DataSource = dt;
        grdChild.DataBind();

        SetPreviousData();
    }

    protected void update_save_ChildRow()
    {
        if (PC2ChildCheck() == "0")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "PC2 Customer cannot duplicate");
            return;
        }

        DataTable dt = (DataTable)ViewState["CurrentTable"];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                decimal dummyQtyC;
                if (string.IsNullOrEmpty(txtQtyC.Text) || txtQtyC.Text == "0"
                    || !decimal.TryParse(txtQtyC.Text, out dummyQtyC))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter Qty more than 0.");
                    return;
                }

                if (string.IsNullOrEmpty(ddlPC1Child.Text))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC1 Customer before add new");
                    return;
                }

                if (string.IsNullOrEmpty(ddlPC2Child.Text))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC2 Customer before add new");
                    return;
                }

                dt.Rows[i]["C_QTY"] = txtQtyC.Text;
                dt.Rows[i]["C_WEIGHT"] = lblUnitWeightC.Text;
                dt.Rows[i]["C_TOTAL_WEIGHT"] = lblTotWeightC.Text;
                dt.Rows[i]["REMARK"] = txtRemarkC.Text;
                dt.Rows[i]["PC1_CUST"] = ddlPC1Child.Text;
                dt.Rows[i]["PC2_CUST"] = ddlPC2Child.Text;
            }
        }

        ViewState["CurrentTable"] = dt;
        grdChild.DataSource = dt;
        grdChild.DataBind();

        SetPreviousData();
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Handled in grdChildRowCommand
    }

    public void Save()
    {
        string _temp = "0";
        string _temp2 = "0";
        string _temp4 = "0";
        int chkint = 0;

        string pCompTo = ddlCompCode.SelectedValue;

        DataTable dt2 = Library.Database.BLL.SubSlitRequest.Chk_label("1", ddlProdLine.Text, "", "");
        if (dt2.Rows.Count == 0)
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Invalid Production Line No");
            return;
        }

        DataTable dt3 = Library.Database.BLL.SubSlitRequest.Chk_label("2", "", ddlPC1.Text, "");
        if (dt3.Rows.Count == 0)
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Invalid PC1 Mother");
            return;
        }

        DataTable _dtPRODLINEid = Library.Database.BLL.SubSlitRequest.GetProdLineID(ddlProdLine.Text);
        DataTable _dtPC1id = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1.Text);
        DataTable _dtPC2id = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2.Text);

        string PRODLINEID = _dtPRODLINEid.Rows[0]["ID_MM_PRODLINE"].ToString();
        string PC1ID = _dtPC1id.Rows[0]["ID_MM_PC1"].ToString();
        string PC2ID = _dtPC2id.Rows[0]["ID_MM_PC2"].ToString();

        if (string.IsNullOrEmpty(ddlPC1.Text))
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC1 Mother.");
            btnSave.Enabled = true;
            pnlChild.Visible = true;
            return;
        }

        if (string.IsNullOrEmpty(ddlProdLine.Text))
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select Production Line.");
            btnSave.Enabled = true;
            pnlChild.Visible = true;
            return;
        }

        decimal dummyQty;
        if (string.IsNullOrEmpty(txtQty.Text) || Convert.IsDBNull(txtQty.Text)
            || txtQty.Text == "0" || !decimal.TryParse(txtQty.Text, out dummyQty))
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter Qty more than 0.");
            btnSave.Enabled = true;
            pnlChild.Visible = true;
            return;
        }

        if (PC2ChildCheck() == "0")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "PC2 Customer cannot duplicate");
            pnlChild.Visible = true;
            return;
        }

        if (ViewState["CurrentTable"] == null)
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Error, Please try again.");
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            return;
        }

        if (txtRefNo.Text.Contains("&"))
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "The character & cannot be used in Ref No. Please replace it with a different value.");
            btnSave.Enabled = true;
            pnlChild.Visible = true;
            return;
        }

        DataTable dt = (DataTable)ViewState["CurrentTable"];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                decimal dummyQtyC;
                if (string.IsNullOrEmpty(txtQtyC.Text) || txtQtyC.Text == "0"
                    || !decimal.TryParse(txtQtyC.Text, out dummyQtyC))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please enter Qty more than 0.");
                    return;
                }

                if (string.IsNullOrEmpty(ddlPC1Child.Text))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC1 Customer before add new");
                    return;
                }

                if (string.IsNullOrEmpty(ddlPC2Child.Text))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please Select PC2 Customer before add new");
                    return;
                }

                dt.Rows[i]["C_QTY"] = txtQtyC.Text;
                dt.Rows[i]["C_WEIGHT"] = lblUnitWeightC.Text;
                dt.Rows[i]["C_TOTAL_WEIGHT"] = lblTotWeightC.Text;
                dt.Rows[i]["REMARK"] = txtRemarkC.Text;
                dt.Rows[i]["PC1_CUST"] = ddlPC1Child.Text;
                dt.Rows[i]["PC2_CUST"] = ddlPC2Child.Text;
            }
        }

        ViewState["CurrentTable"] = dt;
        grdChild.DataSource = dt;
        grdChild.DataBind();
        SetPreviousData();

        Trans_Date = txtDate.Text.Substring(6, 4) + "-" + txtDate.Text.Substring(3, 2) + "-" + txtDate.Text.Substring(0, 2);

        _temp = Library.Database.BLL.SubSlitRequest.SubSlitMaint("0", pCompTo, pCompTo, txtRefNo.Text, lblRev.Text, Trans_Date, lblReqStat.Text, lblVenStat.Text, 1);

        if (_temp == "1")
        {
            ETD_Date = txtETD.Text.Substring(6, 4) + "-" + txtETD.Text.Substring(3, 2) + "-" + txtETD.Text.Substring(0, 2);
            ETA_Date = txtETA.Text.Substring(6, 4) + "-" + txtETA.Text.Substring(3, 2) + "-" + txtETA.Text.Substring(0, 2);

            if (ViewState["Edit_Flag"] != null && ViewState["Edit_Flag"].ToString() == "Y")
            {
                _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherMaint(txtSeqMother.Text, txtRefNo.Text, PC1ID, PC2ID, PRODLINEID, txtQty.Text,
                    lblUnitWeight.Text.Replace(",", ""), lblTotWeight.Text.Replace(",", ""), lblSubSlit.Text.Replace(",", ""), ETD_Date, ETA_Date, 3);
            }
            else
            {
                _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherMaint("0", txtRefNo.Text, PC1ID, PC2ID, PRODLINEID, txtQty.Text,
                    lblUnitWeight.Text.Replace(",", ""), lblTotWeight.Text.Replace(",", ""), lblSubSlit.Text.Replace(",", ""), ETD_Date, ETA_Date, 1);
            }

            if (int.TryParse(_temp2, out chkint) && _temp2 != "0")
            {
                if (ViewState["Edit_Flag"] != null && ViewState["Edit_Flag"].ToString() == "Y")
                {
                    _temp4 = Library.Database.BLL.SubSlitRequest.SubSlitChildDel(txtRefNo.Text, txtSeqMother.Text, PC2ID, 2);
                    if (_temp4 != "1")
                    {
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                            _temp4 == "0" ? string.Format(Resources.Message.Failed, "1") : _temp4);
                        return;
                    }
                }

                Insert_PC2_Child(_temp2);

                pnlList.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;

                int M_Qty;
                decimal M_TotalWeight;
                int C_Qty;
                decimal C_TotalWeight;
                decimal SubSlitWaste;

                DataTable dtWBlank = BuildSSRListingTable(txtRefNo.Text,
                    out M_Qty, out M_TotalWeight, out C_Qty, out C_TotalWeight, out SubSlitWaste);

                lblMQty.Text = M_Qty.ToString("#,###,###,##0.0");
                lblMTotalWeight.Text = M_TotalWeight.ToString("#,###,###,##0.0");
                lblCQty.Text = C_Qty.ToString();
                lblCTotalWeight.Text = C_TotalWeight.ToString("#,###,###,##0.0");
                lblSubSlitWaste.Text = SubSlitWaste.ToString("#,###,###,##0.0");

                ViewState["dtWBlank"] = dtWBlank;
                grdList.DataSource = dtWBlank;
                grdList.DataBind();

                ViewState["CurrentTable"] = null;
                pnlChild.Visible = false;
                btnNext.Visible = true;
                ddlPC2.Visible = true;
                Submit_Button.Visible = true;
                Cancel_Button.Visible = true;
                ViewState["Edit_Flag"] = "N";
                ddlPC2.Enabled = true;
                ddlProdLine.Enabled = true;
                ddlPC1.Enabled = true;
                btnPC2Mother.Visible = true;

                ddlProdLine.Text = "";
                ddlPC1.Text = "";
                ddlPC2.Text = "";
                txtQty.Text = "";
                lblUnitWeight.Text = "";
                lblTotWeight.Text = "";
                lblSubSlit.Text = "";
                txtETD.Text = "";
                txtETA.Text = "";
                SSRTotal.Visible = false;

                Session["CheckBoxArray"] = null;
                Session["PC2Mother"] = null;
                Session["SeqMother"] = null;
                Session["PC2MotherArray"] = null;
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    _temp2 == "0" ? string.Format(Resources.Message.Failed, "1") : _temp2);
            }
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                _temp == "0" ? string.Format(Resources.Message.Failed, "1") : _temp);
        }

        if (_temp == "1") FirstTime_Flag = "N";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (check_refNo())
        {
            double subSlitVal;
            if (double.TryParse(lblSubSlit.Text.Replace(",", ""), out subSlitVal) && subSlitVal < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script",
                    "showDialogue(\"" + lblSubSlit.Text + "\");", true);
                pnlChild.Visible = true;
            }
            else
            {
                txtRefNo.ReadOnly = true;
                Save();
            }
        }
    }

    private bool check_refNo()
    {
        if (txtRefNo.Text.Trim() == "")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "This Refno can't be nothing.");
            return false;
        }

        DataTable dtmaxRev = Library.Database.BLL.SubSlitRequest.chkRefNo(txtRefNo.Text);

        if (dtmaxRev.Rows.Count > 0)
        {
            if (dtmaxRev.Rows[0]["REQUEST_STATUS"].ToString() != "New")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    "This Refno is already submitted/cancelled. Please re-enter new RefNo");
                txtRefNo.Text = "";
                dtmaxRev.Dispose();
                return false;
            }
            else
            {
                txtSubReqId.Text = dtmaxRev.Rows[0]["ID_SUBSLIT_REQUEST"].ToString();
                DataTable dt_SSR = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(txtRefNo.Text, Convert.ToInt32(txtSubReqId.Text));

                ddlCompCode.SelectedValue = dt_SSR.Rows[0]["COMPTOID"].ToString();
                txtDate.Text = Convert.ToDateTime(dt_SSR.Rows[0]["DATEREQ"].ToString()).ToString("dd/MM/yyyy");
                lblRev.Text = dt_SSR.Rows[0]["REVISIONCOUNT"].ToString();
                lblVenStat.Text = dt_SSR.Rows[0]["VENDOR_STATUS"].ToString();

                Display_SSRListing();
            }
        }
        else
        {
            txtSubReqId.Text = "0";
            pnlChild.Visible = false;
            pnlList.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            Submit_Button.Visible = false;
            Cancel_Button.Visible = false;
            Session["CheckBoxArray"] = null;
            Session["PC2Mother"] = null;
            Session["SeqMother"] = null;
            Session["PC2MotherArray"] = null;
        }

        dtmaxRev.Dispose();
        return true;
    }

    private string Insert_PC2_Child(string SeqMother)
    {
        string v_temp = "0";

        DataTable _dtPC2id = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2.Text);
        string PC2ID = _dtPC2id.Rows[0]["ID_MM_PC2"].ToString();

        DataTable dt = (DataTable)ViewState["CurrentTable"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
            TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
            TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
            Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
            Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
            TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

            if (string.IsNullOrEmpty(ddlPC1Child.Text)) break;

            ddlPC1Child.Text = dt.Rows[i]["PC1_CUST"].ToString();
            ddlPC2Child.Text = dt.Rows[i]["PC2_CUST"].ToString();
            txtQtyC.Text = dt.Rows[i]["C_QTY"].ToString();
            lblUnitWeightC.Text = dt.Rows[i]["C_WEIGHT"].ToString();
            lblTotWeightC.Text = dt.Rows[i]["C_TOTAL_WEIGHT"].ToString();
            txtRemarkC.Text = dt.Rows[i]["REMARK"].ToString();

            DataTable _dtPC1Childid = Library.Database.BLL.SubSlitRequest.GetPC1IDData(ddlPC1Child.Text);
            DataTable _dtPC2Childid = Library.Database.BLL.SubSlitRequest.GetPC2IDData(ddlPC2Child.Text);

            string PC1CHILDID = _dtPC1Childid.Rows[0]["ID_MM_PC1"].ToString();
            string PC2CHILDID = _dtPC2Childid.Rows[0]["ID_MM_PC2"].ToString();

            if (ViewState["Edit_Flag"] != null && ViewState["Edit_Flag"].ToString() == "Y")
            {
                v_temp = Library.Database.BLL.SubSlitRequest.SubSlitChildMaint(SeqMother, txtRefNo.Text, PC1CHILDID, PC2CHILDID, txtQtyC.Text,
                    lblUnitWeightC.Text.Replace(",", ""), lblTotWeightC.Text.Replace(",", ""), txtRemarkC.Text, PC2ID, ddlProdLine.Text, ddlPC1.Text, 3);
            }
            else
            {
                v_temp = Library.Database.BLL.SubSlitRequest.SubSlitChildMaint(SeqMother, txtRefNo.Text, PC1CHILDID, PC2CHILDID, txtQtyC.Text,
                    lblUnitWeightC.Text.Replace(",", ""), lblTotWeightC.Text.Replace(",", ""), txtRemarkC.Text, "0", ddlProdLine.Text, ddlPC1.Text, 1);
            }

            if (v_temp != "1")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    v_temp == "0" ? string.Format(Resources.Message.Failed, "1") : v_temp);
            }
        }

        if (v_temp == "1")
        {
            string msg = ViewState["Edit_Flag"] != null && ViewState["Edit_Flag"].ToString() == "Y"
                ? "PC2 Mother and/or its Child are updated successfully."
                : "SubSlit Request is added successfully.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + msg + "')", true);
        }

        return v_temp;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Session["SeqMother"] == null) return;

        Edit_Flag = "Y";
        ViewState["Edit_Flag"] = Edit_Flag;
        pnlChild.Visible = true;
        btnNext.Visible = false;
        ddlPC2.Enabled = false;
        ddlProdLine.Enabled = false;
        ddlPC1.Enabled = false;
        btnPC2Mother.Visible = false;

        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("IMAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("ID_SUBSLIT_REQUEST_CHILD", typeof(string)));
        dt.Columns.Add(new DataColumn("SUBSLIT_REQ_MOTHER_SEQNO", typeof(string)));
        dt.Columns.Add(new DataColumn("PC1_CUST", typeof(string)));
        dt.Columns.Add(new DataColumn("PC2_CUST", typeof(string)));
        dt.Columns.Add(new DataColumn("C_QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("C_WEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("C_TOTAL_WEIGHT", typeof(string)));
        dt.Columns.Add(new DataColumn("REMARK", typeof(string)));

        if (Session["SeqMother"].ToString() == "")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "Please click the required PC2 Mother/Child for edit.");
            return;
        }

        DataTable dtSSR = Library.Database.BLL.SubSlitRequest.SSRList(txtRefNo.Text, Session["SeqMother"].ToString());

        if (dtSSR.Rows.Count > 0)
        {
            for (int i = 0; i < dtSSR.Rows.Count; i++)
            {
                if (i == 0)
                {
                    txtSeqMother.Text = dtSSR.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();

                    DataTable dt_ProdLine = Library.Database.BLL.SubSlitRequest.GetProdLineID(dtSSR.Rows[i]["PRODLINE_NO"].ToString());
                    ddlProdLine.Text = dt_ProdLine.Rows.Count > 0 ? dt_ProdLine.Rows[0]["PRODLINE_NO"].ToString() : "";

                    DataTable dt_PC1 = Library.Database.BLL.SubSlitRequest.GetPC1ID(dtSSR.Rows[i]["PC1_MOTHER"].ToString());
                    ddlPC1.Text = dt_PC1.Rows.Count > 0 ? dt_PC1.Rows[0]["PC1"].ToString() : "";

                    DataTable dt_PC2 = Library.Database.BLL.SubSlitRequest.GetPC2ID(dtSSR.Rows[i]["PC2_MOTHER"].ToString());
                    ddlPC2.Text = dt_PC2.Rows.Count > 0 ? dt_PC2.Rows[0]["PC2"].ToString() : "";

                    txtQty.Text = dtSSR.Rows[i]["QTY"].ToString();

                    decimal MUnitWeight = Convert.ToDecimal(dtSSR.Rows[i]["M_WEIGHT"].ToString());
                    lblUnitWeight.Text = MUnitWeight.ToString("#,###,###,##0.0");

                    decimal MTotWeight = Convert.ToDecimal(dtSSR.Rows[i]["M_TOTAL_WEIGHT"].ToString());
                    lblTotWeight.Text = MTotWeight.ToString("#,###,###,##0.0");

                    decimal subSlitWaste = Convert.ToDecimal(dtSSR.Rows[i]["SUBSLIT_WASTE"].ToString());
                    lblSubSlit.Text = subSlitWaste.ToString("#,###,###,##0.0");

                    txtETD.Text = Convert.ToDateTime(dtSSR.Rows[i]["ETD"].ToString()).ToString("dd/MM/yyyy");
                    txtETA.Text = Convert.ToDateTime(dtSSR.Rows[i]["ETA"].ToString()).ToString("dd/MM/yyyy");
                }

                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                DataTable dt_PC1Child = Library.Database.BLL.SubSlitRequest.GetPC1ID(dtSSR.Rows[i]["PC1_CUST"].ToString());
                dt.Rows[i]["PC1_CUST"] = dt_PC1Child.Rows.Count > 0 ? dt_PC1Child.Rows[0]["PC1"].ToString() : "0";

                DataTable dt_PC2Child = Library.Database.BLL.SubSlitRequest.GetPC2ID(dtSSR.Rows[i]["PC2_CUST"].ToString());
                dt.Rows[i]["PC2_CUST"] = dt_PC2Child.Rows.Count > 0 ? dt_PC2Child.Rows[0]["PC2"].ToString() : "0";

                dt.Rows[i]["C_QTY"] = dtSSR.Rows[i]["C_QTY"].ToString();
                dt.Rows[i]["C_WEIGHT"] = dtSSR.Rows[i]["C_WEIGHT"].ToString();
                dt.Rows[i]["C_TOTAL_WEIGHT"] = dtSSR.Rows[i]["C_TOTAL_WEIGHT"].ToString();
                dt.Rows[i]["REMARK"] = dtSSR.Rows[i]["REMARK"].ToString();
            }

            ViewState["CurrentTable"] = dt;
            grdChild.DataSource = dt;
            grdChild.DataBind();

            for (int i = 0; i < dtSSR.Rows.Count; i++)
            {
                setChildDdl(i);

                TextBox ddlPC1Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC1Child");
                TextBox ddlPC2Child = (TextBox)grdChild.Rows[i].FindControl("ddlPC2Child");
                TextBox txtQtyC = (TextBox)grdChild.Rows[i].FindControl("txtQtyC");
                Label lblUnitWeightC = (Label)grdChild.Rows[i].FindControl("lblUnitWeightC");
                Label lblTotWeightC = (Label)grdChild.Rows[i].FindControl("lblTotWeightC");
                TextBox txtRemarkC = (TextBox)grdChild.Rows[i].FindControl("txtRemarkC");

                ddlPC1Child.Text = dt.Rows[i]["PC1_CUST"].ToString();
                ddlPC2Child.Text = dt.Rows[i]["PC2_CUST"].ToString();
                txtQtyC.Text = dt.Rows[i]["C_QTY"].ToString();

                decimal CUnitWeight = Convert.ToDecimal(dt.Rows[i]["C_WEIGHT"].ToString());
                lblUnitWeightC.Text = CUnitWeight.ToString("#,###,###,##0.0");

                decimal CTotalWeight = Convert.ToDecimal(dt.Rows[i]["C_TOTAL_WEIGHT"].ToString());
                lblTotWeightC.Text = CTotalWeight.ToString("#,###,###,##0.0");

                txtRemarkC.Text = dt.Rows[i]["REMARK"].ToString();
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["SeqMother"] == null)
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "Please click radio button for the required PC2 Mother/Child.");
            return;
        }

        string d_PC2Mother = Session["PC2Mother"].ToString();
        string d_PC1Mother = Session["PC1Mother"].ToString();
        string d_ProdLineNo = Session["ProdLineNo"].ToString();
        string d_SeqMother = Session["SeqMother"].ToString();

        string _temp1 = Library.Database.BLL.SubSlitRequest.SubSlitChildDelFrList(
            txtRefNo.Text, d_PC2Mother, d_PC1Mother, d_ProdLineNo, d_SeqMother, 2);

        if (_temp1 == "1")
        {
            string _temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDel(
                txtRefNo.Text, d_PC2Mother, d_PC1Mother, d_ProdLineNo, d_SeqMother, 2);

            if (_temp2 == "1")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    "This selected PC2 Mother/Child " + d_PC2Mother + " is deleted successfully.");
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                    _temp2 == "0" ? string.Format(Resources.Message.Failed, "1") : _temp1);
                return;
            }
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                _temp1 == "0" ? string.Format(Resources.Message.Failed, "1") : _temp1);
            return;
        }

        int M_Qty;
        decimal M_TotalWeight;
        int C_Qty;
        decimal C_TotalWeight;
        decimal SubSlitWaste;

        DataTable dtWBlank = BuildSSRListingTable(txtRefNo.Text,
            out M_Qty, out M_TotalWeight, out C_Qty, out C_TotalWeight, out SubSlitWaste);

        if (dtWBlank.Rows.Count > 0)
        {
            pnlList.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;

            lblMQty.Text = M_Qty.ToString("#,###,###,##0.0");
            lblMTotalWeight.Text = M_TotalWeight.ToString("#,###,###,##0.0");
            lblCQty.Text = C_Qty.ToString();
            lblCTotalWeight.Text = C_TotalWeight.ToString("#,###,###,##0.0");
            lblSubSlitWaste.Text = SubSlitWaste.ToString("#,###,###,##0.0");
        }
        else
        {
            pnlList.Visible = false;
            Submit_Button.Visible = false;
            Cancel_Button.Visible = false;
            SSRTotal.Visible = false;
        }

        ViewState["dtWBlank"] = dtWBlank;
        grdList.DataSource = dtWBlank;
        grdList.DataBind();
    }

    private DataTable BuildSSRListingTable(string refNo,
        out int M_Qty, out decimal M_TotalWeight, out int C_Qty, out decimal C_TotalWeight, out decimal SubSlitWaste)
    {
        M_Qty = 0; M_TotalWeight = 0; C_Qty = 0; C_TotalWeight = 0; SubSlitWaste = 0;

        DataTable dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(refNo, "0");
        DataTable dtWBlank = new DataTable();

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

        if (dtSSRList.Rows.Count == 0) return dtWBlank;

        string Prev_PC2Mother = "", Prev_PC1Mother = "", Prev_ProdLineNo = "", Prev_SeqMother = "";
        int k = -1;

        for (int i = 0; i < dtSSRList.Rows.Count; i++)
        {
            DataRow dr2;
            bool isSameGroup = dtSSRList.Rows[i]["PC2_MOTHER"].ToString() == Prev_PC2Mother
                && dtSSRList.Rows[i]["PC1_MOTHER"].ToString() == Prev_PC1Mother
                && dtSSRList.Rows[i]["PRODLINE_NO"].ToString() == Prev_ProdLineNo
                && dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString() == Prev_SeqMother;

            if (isSameGroup)
            {
                dr2 = dtWBlank.NewRow();
                dtWBlank.Rows.Add(dr2);
                k++;

                dtWBlank.Rows[k]["REFNO"] = dtSSRList.Rows[i]["REFNO"];
                dtWBlank.Rows[k]["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                dtWBlank.Rows[k]["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                dtWBlank.Rows[k]["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];

                decimal C_Weight = Convert.ToDecimal(dtSSRList.Rows[i]["C_WEIGHT"]);
                dtWBlank.Rows[k]["C_WEIGHT"] = C_Weight.ToString("#,###,###,##0.0");
                decimal C_Total_Weight = Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                dtWBlank.Rows[k]["C_TOTAL_WEIGHT"] = C_Total_Weight.ToString("#,###,###,##0.0");

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
                dtWBlank.Rows[k]["SEQ"] = DBNull.Value;
                dtWBlank.Rows[k]["CHK"] = "0";

                C_Qty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                C_TotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
            }
            else
            {
                if (Prev_PC2Mother != "" && Prev_PC1Mother != "" && Prev_ProdLineNo != "" && Prev_SeqMother != "")
                {
                    k++;
                    dr2 = dtWBlank.NewRow();
                    dtWBlank.Rows.Add(dr2);
                    dtWBlank.Rows[k]["PC2_MOTHER"] = "";
                    dtWBlank.Rows[k]["CHK"] = "0";
                }

                dr2 = dtWBlank.NewRow();
                dtWBlank.Rows.Add(dr2);
                k++;

                dtWBlank.Rows[k]["REFNO"] = dtSSRList.Rows[i]["REFNO"];
                dtWBlank.Rows[k]["PRODLINE_NO"] = dtSSRList.Rows[i]["PRODLINE_NO"];
                dtWBlank.Rows[k]["PC1_MOTHER"] = dtSSRList.Rows[i]["PC1_MOTHER"];
                dtWBlank.Rows[k]["PC2_MOTHER"] = dtSSRList.Rows[i]["PC2_MOTHER"];
                dtWBlank.Rows[k]["QTY"] = dtSSRList.Rows[i]["QTY"];

                decimal M_Weight = Convert.ToDecimal(dtSSRList.Rows[i]["M_WEIGHT"]);
                dtWBlank.Rows[k]["M_WEIGHT"] = M_Weight.ToString("#,###,###,##0.0");
                decimal M_Total_Weight = Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                dtWBlank.Rows[k]["M_TOTAL_WEIGHT"] = M_Total_Weight.ToString("#,###,###,##0.0");

                dtWBlank.Rows[k]["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                dtWBlank.Rows[k]["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                dtWBlank.Rows[k]["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];

                decimal C_Weight = Convert.ToDecimal(dtSSRList.Rows[i]["C_WEIGHT"]);
                dtWBlank.Rows[k]["C_WEIGHT"] = C_Weight.ToString("#,###,###,##0.0");
                decimal C_Total_Weight = Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                dtWBlank.Rows[k]["C_TOTAL_WEIGHT"] = C_Total_Weight.ToString("#,###,###,##0.0");

                decimal Subslit_Waste = Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                dtWBlank.Rows[k]["SUBSLIT_WASTE"] = Subslit_Waste.ToString("#,###,###,##0.0");

                dtWBlank.Rows[k]["ETD"] = Convert.ToDateTime(dtSSRList.Rows[i]["ETD"].ToString()).ToString("dd/MM/yyyy");
                dtWBlank.Rows[k]["ETA"] = Convert.ToDateTime(dtSSRList.Rows[i]["ETA"].ToString()).ToString("dd/MM/yyyy");
                dtWBlank.Rows[k]["SEQ"] = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();
                dtWBlank.Rows[k]["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                dtWBlank.Rows[k]["CHK"] = "1";

                M_Qty += Convert.ToInt32(dtSSRList.Rows[i]["QTY"]);
                M_TotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                SubSlitWaste += Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                C_Qty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                C_TotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
            }

            Prev_PC2Mother = dtSSRList.Rows[i]["PC2_MOTHER"].ToString();
            Prev_PC1Mother = dtSSRList.Rows[i]["PC1_MOTHER"].ToString();
            Prev_ProdLineNo = dtSSRList.Rows[i]["PRODLINE_NO"].ToString();
            Prev_SeqMother = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();
        }

        return dtWBlank;
    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;

        if (ViewState["dtWBlank"] != null)
        {
            grdList.DataSource = (DataTable)ViewState["dtWBlank"];
            grdList.DataBind();
        }

        if (Session["CheckBoxArray"] != null)
        {
            ArrayList CheckBoxArray = (ArrayList)Session["CheckBoxArray"];

            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (grdList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    int CheckBoxIndex = grdList.PageSize * grdList.PageIndex + (i + 1);

                    HiddenField h_PC2Mother = (HiddenField)grdList.Rows[i].FindControl("HiddenField1");
                    HiddenField h_PC1Mother = (HiddenField)grdList.Rows[i].FindControl("HiddenField2");
                    HiddenField h_ProdLineNo = (HiddenField)grdList.Rows[i].FindControl("HiddenField3");
                    HiddenField h_SeqMother = (HiddenField)grdList.Rows[i].FindControl("HiddenField4");

                    CheckBox chk = (CheckBox)grdList.Rows[i].FindControl("RadioButton1");

                    if (chk.Visible)
                    {
                        chk.Checked = CheckBoxArray.IndexOf(CheckBoxIndex) != -1
                            && h_PC2Mother.Value == Session["PC2Mother"].ToString()
                            && h_PC1Mother.Value == Session["PC1Mother"].ToString()
                            && h_ProdLineNo.Value == Session["ProdLineNo"].ToString()
                            && h_SeqMother.Value == Session["SeqMother"].ToString();
                    }
                }
            }
        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        ArrayList CheckBoxArray = Session["CheckBoxArray"] != null
            ? (ArrayList)Session["CheckBoxArray"] : new ArrayList();
        ArrayList PC2MotherArray = Session["PC2MotherArray"] != null
            ? (ArrayList)Session["PC2MotherArray"] : new ArrayList();
        ArrayList SeqMotherArray = Session["SeqMotherArray"] != null
            ? (ArrayList)Session["SeqMotherArray"] : new ArrayList();

        string curr_PC2Mother_value = "";
        string curr_PC1Mother_value = "";
        string curr_ProdLineNo_value = "";
        string curr_SeqMother_value = "";

        for (int i = 0; i < grdList.Rows.Count; i++)
        {
            if (grdList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                RadioButton chk = (RadioButton)grdList.Rows[i].Cells[0].FindControl("RadioButton1");

                if (chk != null && chk.Visible)
                {
                    HiddenField r_PC2Mother = (HiddenField)grdList.Rows[i].FindControl("HiddenField1");
                    HiddenField r_PC1Mother = (HiddenField)grdList.Rows[i].FindControl("HiddenField2");
                    HiddenField r_ProdLineNo = (HiddenField)grdList.Rows[i].FindControl("HiddenField3");
                    HiddenField r_SeqMother = (HiddenField)grdList.Rows[i].FindControl("HiddenField4");

                    int CheckBoxIndex = grdList.PageSize * grdList.PageIndex + (i + 1);

                    string seqVal = r_SeqMother.Value;
                    string prevSeq = Session["SeqMother"] != null ? Session["SeqMother"].ToString() : "";

                    if (chk.Checked && seqVal != prevSeq)
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1)
                        {
                            CheckBoxArray.Add(CheckBoxIndex);
                            PC2MotherArray.Add(r_PC2Mother.Value);
                            SeqMotherArray.Add(seqVal);
                        }
                        curr_PC2Mother_value = r_PC2Mother.Value;
                        curr_PC1Mother_value = r_PC1Mother.Value;
                        curr_ProdLineNo_value = r_ProdLineNo.Value;
                        curr_SeqMother_value = seqVal;
                    }
                    else
                    {
                        chk.Checked = false;
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                        {
                            CheckBoxArray.Remove(CheckBoxIndex);
                            PC2MotherArray.Remove(r_PC2Mother.Value);
                            SeqMotherArray.Remove(seqVal);
                        }
                    }
                }
            }
        }

        Session["CheckBoxArray"] = CheckBoxArray;
        Session["PC2Mother"] = curr_PC2Mother_value;
        Session["PC1Mother"] = curr_PC1Mother_value;
        Session["ProdLineNo"] = curr_ProdLineNo_value;
        Session["SeqMother"] = curr_SeqMother_value;
        Session["PC2MotherArray"] = PC2MotherArray;
        Session["SeqMotherArray"] = SeqMotherArray;
    }

    protected void Cancel_Button_Click(object sender, EventArgs e)
    {
        Session["CheckBoxArray"] = null;
        Session["SeqMother"] = null;
        Session["PC2Mother"] = null;
        Session["PC2MotherArray"] = null;
        Session["SeqMotherArray"] = null;

        if (Request.QueryString["itm1"] == null && Request.QueryString["itm2"] == null)
            Response.End();
        else
            Response.Redirect("~/Transactions/SSR_SEARCH.aspx");
    }

    protected void Submit_Button_Click(object sender, EventArgs e)
    {
        Session["CheckBoxArray"] = null;
        Session["SeqMother"] = null;
        Session["PC2Mother"] = null;
        Session["PC2MotherArray"] = null;
        Session["SeqMotherArray"] = null;

        DataTable dt_r = Library.Database.BLL.SubSlitRequest.CHECK_SUBMITTED_REQ(txtRefNo.Text, Convert.ToInt32(lblRev.Text));

        if (dt_r.Rows.Count > 0)
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "This RefNo " + txtRefNo.Text + "and Revison " + lblRev.Text + " exists in the system. Please look up in Search Sub-Slitting Request.");
            return;
        }

        string u_temp = Library.Database.BLL.SubSlitRequest.UpdateReq(txtRefNo.Text, Convert.ToInt32(lblRev.Text));
        if (u_temp == "1")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                "This RefNo " + txtRefNo.Text + "and Revison " + lblRev.Text + " is submitted successfully.");
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page,
                u_temp == "0" ? string.Format(Resources.Message.Failed, "1") : u_temp);
        }

        btnNext.Visible = true;
        Submit_Button.Visible = false;
        Cancel_Button.Visible = false;
        pnlChild.Visible = false;
        pnlList.Visible = false;
        btnEdit.Visible = false;
        btnDelete.Visible = false;

        ddlProdLine.Text = "";
        ddlPC1.Text = "";
        ddlPC2.Text = "";
        txtQty.Text = "";
        lblUnitWeight.Text = "";
        lblTotWeight.Text = "";
        lblSubSlit.Text = "";
        txtETD.Text = "";
        txtETA.Text = "";
        txtSubReqId.Text = "";
        txtRefNo.Text = "";
        ddlCompCode.SelectedValue = "0";
        txtDate.Text = "";
        lblRev.Text = "";
    }

    protected void txtQTYC_TextChanged(object sender, EventArgs e)
    {
        ChildCalculate();
    }

    protected void Display_SSRListing()
    {
        int M_Qty;
        decimal M_TotalWeight;
        int C_Qty;
        decimal C_TotalWeight;
        decimal SubSlitWaste;

        DataTable dtWBlank = BuildSSRListingTable(txtRefNo.Text,
            out M_Qty, out M_TotalWeight, out C_Qty, out C_TotalWeight, out SubSlitWaste);

        if (dtWBlank.Rows.Count > 0)
        {
            pnlList.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            Submit_Button.Visible = true;
            Cancel_Button.Visible = true;
            SSRTotal.Visible = false;

            lblMQty.Text = M_Qty.ToString("#,###,###,##0.0");
            lblMTotalWeight.Text = M_TotalWeight.ToString("#,###,###,##0.0");
            lblCQty.Text = C_Qty.ToString();
            lblCTotalWeight.Text = C_TotalWeight.ToString("#,###,###,##0.0");
            lblSubSlitWaste.Text = SubSlitWaste.ToString("#,###,###,##0.0");

            ViewState["dtWBlank"] = dtWBlank;
            grdList.DataSource = dtWBlank;
            grdList.DataBind();
        }
        else
        {
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            Submit_Button.Visible = false;
            Cancel_Button.Visible = false;
            SSRTotal.Visible = false;
        }
    }

    protected void grdList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            GridViewRow gr = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
            TableCell tc = new TableCell();

            AddMergedCells(gr, ref tc, 3, "", System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "Total", System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblMQty.Text, System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "", System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblMTotalWeight.Text, System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 2, "", System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblCQty.Text, System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, "", System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblCTotalWeight.Text, System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 1, lblSubSlitWaste.Text, System.Drawing.Color.AliceBlue.Name);
            AddMergedCells(gr, ref tc, 3, "", System.Drawing.Color.AliceBlue.Name);

            gr.Cells.Add(tc);

            Table gvTable = (Table)e.Row.Parent;
            gvTable.Controls.Add(gr);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, ref TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (inpHide.Value == "1")
            Save();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Calculate();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        ChildCalculate();
    }

    protected void btnpc2_Click(object sender, EventArgs e)
    {
        Calculate();
    }

    protected void btnpc2child_Click(object sender, EventArgs e)
    {
        ChildCalculate();
    }

    protected void ddlCompCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["CompCode"] = ddlCompCode.SelectedIndex;
    }
}