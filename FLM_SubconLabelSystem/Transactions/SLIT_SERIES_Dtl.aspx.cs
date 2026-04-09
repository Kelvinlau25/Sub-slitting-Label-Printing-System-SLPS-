using System;
using System.Data;
using System.Web.UI.WebControls;
using Library.Database;

public partial class Transaction_SlitSeries_Dtl : Control.Base
{
    public string[] lbl;
    public string[] lbl2;
    public string[] lblUnit;

    public Transaction_SlitSeries_Dtl()
    {
        base.SetupKey = "PC2_LOTNO";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfRefNo.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Ref.No");
        rfddlProdLine.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Production Line");
        rfddlPC1Customer.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PC1 Customer");
        rfddlPC1Mother.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PC1 Mother");
        rfNoOfSlit.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "No of Slit");
        rfLotNo.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Lot Number");
        rftxtyeardate.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Year/Month");
        rfrdPos.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Matrix Position");

        UCAction.DisplayMode += UCAction_DisplayMode;
        UCAction.ModifyMode += UCAction_ModifyMode;
        UCAction.AddAction += UCAction_AddAction;
        UCAction.EditAction += UCAction_AddAction;
        UCAction.AddResetAction += UCAction_AddResetAction;
        UCAction.EditResetAction += UCAction_AddResetAction;
        UCAction.DeleteAction += UCAction_DeleteAction;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["expired"] != null)
            {
                if (Request.QueryString["expired"].ToString().Trim().Equals("1"))
                {
                    string _str_msg = "You are not allow to Edit as the Sub-slitting Request for this Reference Number was created " +
                        System.Configuration.ConfigurationManager.AppSettings["Slitting_Series_Expired_Days"] + " days before.";

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox", "<script type='text/javascript'> alert('" + _str_msg + "');</script>");
                }
            }

            Library.Database.BLL.SlitSeries.GetDDLData("2");

            string Company_Code = Session["COMPANYCODE"].ToString();
            DataTable dfn = Library.Database.BLL.SlitSeries.GetRefByComp(Company_Code);

            ddlRefNo.Items.Clear();
            ddlRefNo.Items.Insert(0, new ListItem("--Select--", ""));

            if (dfn.Rows.Count > 0)
            {
                for (int i = 0; i < dfn.Rows.Count; i++)
                {
                    ddlRefNo.Items.Add(new ListItem(dfn.Rows[i]["REFNO"].ToString(), dfn.Rows[i]["REFNO"].ToString()));
                }
            }
        }

        HyperLink hpLink = (HyperLink)UCAction.FindControl("hpLink");
        hpLink.Visible = false;
    }

    protected void UCAction_DisplayMode()
    {
        Cdisplay.Visible = true;
        Cmodify.Visible = false;

        DataTable _datatable = Library.Database.BLL.SlitSeries.GetData(base.Key);

        lblCompCode.Text = _datatable.Rows[0]["COMPANYCODE"].ToString();
        lblPlanYrMth.Text = _datatable.Rows[0]["PLAN_YEAR_MONTH"].ToString();
        lblProdLine.Text = _datatable.Rows[0]["PRODLINE_NO"].ToString();
        lblRefNo.Text = _datatable.Rows[0]["REFNO"].ToString();

        lblPC1Mother.Text = _datatable.Rows[0]["PC1_MOTHER"].ToString();
        lblPC2Mother.Text = _datatable.Rows[0]["PC2_MOTHER"].ToString();
        lblUnitWeightMthr.Text = _datatable.Rows[0]["UNIT_WEIGHT_MOTHER"].ToString();

        lblPC1Customer.Text = _datatable.Rows[0]["PC1_CUST"].ToString();
        lblPC2Customer.Text = _datatable.Rows[0]["PC2_CUST"].ToString();
        lblUnitWeightCust.Text = _datatable.Rows[0]["UNIT_WEIGHT_CUSTOMER"].ToString();

        lblLotNo.Text = _datatable.Rows[0]["LOTNO"].ToString();
        lblNumOfSlit.Text = _datatable.Rows[0]["NO_OF_SLIT"].ToString();

        string[] slitParts = _datatable.Rows[0]["TYPE_OF_SLIT"].ToString().Split(',');
        string typeofslit = slitParts[0];
        string matrixpos = slitParts[1];
        string matrixinc = slitParts[2];

        if (typeofslit == "1")
            lblTypeOfSlit.Text = "Sequence";
        else if (typeofslit == "2")
            lblTypeOfSlit.Text = "Even";
        else if (typeofslit == "3")
            lblTypeOfSlit.Text = "Odd";
        else if (typeofslit == "4")
            lblTypeOfSlit.Text = "Matrix (Position: " + matrixpos + " Increment: " + matrixinc + ")";

        lblLotSlitStatus.Text = _datatable.Rows[0]["LOT_STATUS"].ToString();

        UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();

        DateTime tmpCreateDate;
        if (!Convert.IsDBNull(_datatable.Rows[0]["CREATED_DATE"]) &&
            DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
            UCAction.CreatedDate = tmpCreateDate;
        else
            UCAction.CreatedDate = new DateTime();

        UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
        UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();

        DateTime tmpUpdateDate;
        if (!Convert.IsDBNull(_datatable.Rows[0]["UPDATED_DATE"]) &&
            DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
            UCAction.UpdatedDate = tmpUpdateDate;
        else
            UCAction.UpdatedDate = new DateTime();

        UCAction.EditMode = _datatable.Rows[0]["REC_TYPE"].ToString() != "5";
    }

    protected void UCAction_ModifyMode()
    {
        Cdisplay.Visible = false;
        Cmodify.Visible = true;

        if (base.Action == EnumAction.Edit)
        {
            if (!Page.IsPostBack)
            {
                DataTable _datatable = Library.Database.BLL.SlitSeries.GetData(base.Key);

                if (DateTime.Now.Subtract(DateTime.Parse(_datatable.Rows[0]["UPDATED_DATE"].ToString())).Days >=
                    Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Slitting_Series_Expired_Days"]))
                {
                    _datatable.Dispose();
                    Response.Redirect(Request.RawUrl.Replace("action=3", "action=7&expired=1"));
                    return;
                }

                txtLotNo.Visible = false;
                lbLotNo.Visible = true;
                txtCompanyCode.Text = _datatable.Rows[0]["COMPANYCODE"].ToString();

                ddlRefNo.SelectedIndex = ddlRefNo.Items.IndexOf(ddlRefNo.Items.FindByValue(_datatable.Rows[0]["REFNO"].ToString()));

                if (ddlRefNo.SelectedIndex == 0)
                {
                    string _str_msg = "You are not allow to Edit this Sub-slitting Request.";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox", "<script type='text/javascript'> alert('" + _str_msg + "');history.go(-1);</script>");
                }

                txtDate.Text = _datatable.Rows[0]["PLAN_YEAR_MONTH"].ToString();

                ddlProdLine.Visible = true;
                ProductNumber_listing(_datatable.Rows[0]["REFNO"].ToString());
                ddlProdLine.SelectedIndex = ddlProdLine.Items.IndexOf(ddlProdLine.Items.FindByValue(_datatable.Rows[0]["PRODLINE_NO"].ToString()));

                ddlPC1Mother.Visible = true;
                PC1_Mother_listing();
                ddlPC1Mother.SelectedIndex = ddlPC1Mother.Items.IndexOf(ddlPC1Mother.Items.FindByText(_datatable.Rows[0]["PC1_MOTHER"].ToString()));

                txtPC2Mother.Text = _datatable.Rows[0]["PC2_MOTHER"].ToString();
                txtUnitWeightMother.Text = _datatable.Rows[0]["UNIT_WEIGHT_MOTHER"].ToString();

                ddlPC1Customer.Visible = true;
                PC1_Customer_Listing(_datatable.Rows[0]["PC2_MOTHER"].ToString());
                ddlPC1Customer.SelectedIndex = ddlPC1Customer.Items.IndexOf(ddlPC1Customer.Items.FindByText(_datatable.Rows[0]["PC1_CUST"].ToString()));

                txtPC2Customer.Text = _datatable.Rows[0]["PC2_CUST"].ToString();
                txtUnitWeightCustomer.Text = _datatable.Rows[0]["UNIT_WEIGHT_CUSTOMER"].ToString();

                lbLotNo.Text = _datatable.Rows[0]["LOTNO"].ToString();
                txtLotNo.Text = _datatable.Rows[0]["LOTNO"].ToString();
                txtNoOfSlit.Text = _datatable.Rows[0]["NO_OF_SLIT"].ToString();
                lbLotSlitStatus.Text = _datatable.Rows[0]["LOT_STATUS"].ToString();

                string[] slitParts = _datatable.Rows[0]["TYPE_OF_SLIT"].ToString().Split(',');
                string typeofslit = slitParts[0];
                string matrixpos = slitParts[1];
                string matrixinc = slitParts[2];

                if (typeofslit == "1")
                    rdSeq.Checked = true;
                else if (typeofslit == "2")
                    rdEven.Checked = true;
                else if (typeofslit == "3")
                    rdOdd.Checked = true;
                else if (typeofslit == "4")
                {
                    rdMatrix.Checked = true;
                    rdPos.Text = matrixpos;
                    rdInc.Text = matrixinc;
                }

                UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();

                DateTime tmpCreateDate;
                if (!Convert.IsDBNull(_datatable.Rows[0]["CREATED_DATE"]) &&
                    DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
                    UCAction.CreatedDate = tmpCreateDate;
                else
                    UCAction.CreatedDate = new DateTime();

                UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
                UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();

                DateTime tmpUpdateDate;
                if (!Convert.IsDBNull(_datatable.Rows[0]["UPDATED_DATE"]) &&
                    DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
                    UCAction.UpdatedDate = tmpUpdateDate;
                else
                    UCAction.UpdatedDate = new DateTime();

                UCAction.UpdatedLoc = _datatable.Rows[0]["UPDATED_LOC"].ToString();
            }
        }
        else if (base.Action == EnumAction.Add)
        {
            txtLotNo.Visible = true;
            lbLotNo.Visible = false;
            lbtxtLotSlitStatus.Visible = false;
            Label32.Visible = false;
            lbLotSlitStatus.Visible = false;

            txtCompanyCode.Text = Session["COMPANYCODE"].ToString();

            DataTable dt_sel = Library.Database.BLL.SlitSeries.GetPCMOTHER2(ddlRefNo.SelectedValue);
            string lblPC = "";

            if (dt_sel.Rows.Count > 0)
            {
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                {
                    lblPC += "\"" + dt_sel.Rows[i]["PC2"].ToString() + " - uw: " + dt_sel.Rows[i]["UNIT_WEIGHT"].ToString() + "\",";
                }
                lblPC = lblPC.Substring(0, lblPC.Length - 1);
            }

            lbl = lblPC.Split(',');

            DataTable dt_sel2 = Library.Database.BLL.SlitSeries.GetPC2CUST(ddlRefNo.SelectedValue);
            string lblPC2 = "";

            if (dt_sel2.Rows.Count > 0)
            {
                for (int i = 0; i < dt_sel2.Rows.Count; i++)
                {
                    lblPC2 += "\"" + dt_sel2.Rows[i]["PC2"].ToString() + " - uw: " + dt_sel2.Rows[i]["UNIT_WEIGHT"].ToString() + "\",";
                }
                lblPC2 = lblPC2.Substring(0, lblPC2.Length - 1);
            }

            lbl2 = lblPC2.Split(',');
        }
    }

    protected void UCAction_AddAction()
    {
        string cbtypeOfSlit = "";
        string matrixvariable = "0,0";

        if (rdSeq.Checked)
            cbtypeOfSlit = "1," + matrixvariable;
        else if (rdOdd.Checked)
            cbtypeOfSlit = "3," + matrixvariable;
        else if (rdEven.Checked)
            cbtypeOfSlit = "2," + matrixvariable;
        else if (rdMatrix.Checked)
        {
            if (string.IsNullOrEmpty(rdInc.Text))
                rdInc.Text = "0";
            matrixvariable = rdPos.Text + "," + rdInc.Text;
            cbtypeOfSlit = "4," + matrixvariable;
        }

        string _temp = "";
        string Update_Status = "";

        if (base.Action == EnumAction.Add)
        {
            Update_Status = Library.Database.BLL.CHECK_LOTNO_DUP.check_lotno_dup(txtCompanyCode.Text.Trim(), txtLotNo.Text.Trim());
        }

        if (Update_Status == "1")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "showDialogue()", true);
        }
        else
        {
            if (base.Action == EnumAction.Edit)
            {
                _temp = Library.Database.BLL.SlitSeries.Maint(base.Key, txtCompanyCode.Text.Trim(), ddlRefNo.SelectedValue, lbLotNo.Text.Trim(),
                    ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text,
                    ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue,
                    txtNoOfSlit.Text.Trim(), txtDate.Text, cbtypeOfSlit,
                    ((int)base.Action).ToString());

                LabelEdit.Text = _temp;

                if (_temp == "1")
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Successfully, base.Action.ToString()));
                    Response.Redirect(base.GetUrl(EnumAction.None), false);
                }
                else
                {
                    if (_temp == "0")
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, base.Action.ToString()));
                    else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp);
                }
            }
            else if (base.Action == EnumAction.Add)
            {
                _temp = Library.Database.BLL.SlitSeries.Maint("0", txtCompanyCode.Text.Trim(), ddlRefNo.SelectedValue, txtLotNo.Text.Trim(),
                    ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text,
                    ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue,
                    txtNoOfSlit.Text.Trim(), txtDate.Text, cbtypeOfSlit,
                    ((int)base.Action).ToString());

                if (_temp == "1")
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Successfully, base.Action.ToString()));
                    ddlPC1Customer.SelectedIndex = 0;
                    txtPC2Customer.Text = "";
                    txtUnitWeightCustomer.Text = "";
                    txtNoOfSlit.Text = "";
                    rdPos.Text = "";
                    rdInc.Text = "";
                }
                else
                {
                    if (_temp == "0")
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, base.Action.ToString()));
                    else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp);
                }
            }
        }
    }

    protected void UCAction_AddResetAction()
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void UCAction_DeleteAction()
    {
        string _temp = Library.Database.BLL.SlitSeries.Maint(base.Key, lblCompCode.Text, ddlRefNo.SelectedValue, lblLotNo.Text,
            lblProdLine.Text, lblPC1Mother.Text,
            lblPC2Mother.Text, lblPC1Customer.Text, lblPC2Customer.Text,
            lblLotNo.Text, lblNumOfSlit.Text, lblTypeOfSlit.Text,
            ((int)Library.Root.Control.Base.EnumAction.Delete).ToString());

        string script = "<script type='text/javascript'> alert('" + _temp + "');</script>";

        if (_temp == "1")
        {
            Response.Redirect(base.GetUrl(EnumAction.None), false);
        }
        else
        {
            if (_temp == "0")
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, base.Action.ToString()));
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertBox", script);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string cbtypeOfSlit = "";
        string matrixvariable = "0,0";

        if (rdSeq.Checked)
            cbtypeOfSlit = "1," + matrixvariable;
        else if (rdOdd.Checked)
            cbtypeOfSlit = "3," + matrixvariable;
        else if (rdEven.Checked)
            cbtypeOfSlit = "2," + matrixvariable;
        else if (rdMatrix.Checked)
        {
            if (string.IsNullOrEmpty(rdInc.Text))
                rdInc.Text = "0";
            matrixvariable = rdPos.Text + "," + rdInc.Text;
            cbtypeOfSlit = "4," + matrixvariable;
        }

        string _temp = "";

        if (base.Action == EnumAction.Edit)
        {
            _temp = Library.Database.BLL.SlitSeries.Maint(base.Key, txtCompanyCode.Text.Trim(), ddlRefNo.SelectedValue, lbLotNo.Text.Trim(),
                ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text,
                ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue,
                txtNoOfSlit.Text.Trim(), txtDate.Text, cbtypeOfSlit,
                ((int)base.Action).ToString());

            LabelEdit.Text = _temp;

            if (_temp == "1")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Successfully, base.Action.ToString()));
                Response.Redirect(base.GetUrl(EnumAction.None), false);
            }
            else
            {
                if (_temp == "0")
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, base.Action.ToString()));
                else
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp);
            }
        }
        else if (base.Action == EnumAction.Add)
        {
            if (inpHide.Value == "1")
            {
                _temp = Library.Database.BLL.SlitSeries.Maint("0", txtCompanyCode.Text.Trim(), ddlRefNo.SelectedValue, txtLotNo.Text.Trim(),
                    ddlPC1Mother.SelectedItem.Text, txtPC2Mother.Text,
                    ddlPC1Customer.SelectedItem.Text, txtPC2Customer.Text, ddlProdLine.SelectedValue,
                    txtNoOfSlit.Text.Trim(), txtDate.Text, cbtypeOfSlit,
                    ((int)base.Action).ToString());
            }
            else
            {
                return;
            }

            if (inpHide.Value == "1")
            {
                if (_temp == "1")
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Successfully, base.Action.ToString()));
                    ddlPC1Customer.SelectedIndex = 0;
                    txtPC2Customer.Text = "";
                    txtUnitWeightCustomer.Text = "";
                    txtNoOfSlit.Text = "";
                    rdPos.Text = "";
                    rdInc.Text = "";
                }
                else
                {
                    if (_temp == "0")
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.Failed, base.Action.ToString()));
                    else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, _temp);
                }
            }
            else
            {
                return;
            }
        }
    }

    private string[] filter_duplicate_value_4_list(DataTable pobj_data, string pstr_fieldName)
    {
        string[] _arr_str = new string[pobj_data.Rows.Count];
        bool _bol_existed = false;
        int _int_iArr = 0;
        string _str_Found = "";
        int _int_iCaptured = 0;

        for (int _int_iMain = 0; _int_iMain <= (pobj_data.Rows.Count - 1); _int_iMain++)
        {
            _bol_existed = false;
            _str_Found = pobj_data.Rows[_int_iMain][pstr_fieldName].ToString().Trim();

            for (_int_iArr = 0; _int_iArr <= (_arr_str.GetLength(0) - 1); _int_iArr++)
            {
                if (_str_Found.Equals(_arr_str[_int_iArr]))
                {
                    _bol_existed = true;
                    break;
                }
            }

            if (!_bol_existed)
            {
                _arr_str[_int_iCaptured] = _str_Found;
                _int_iCaptured++;
            }
        }

        return _arr_str;
    }

    private void ProductNumber_listing(string pstr_refNo)
    {
        ddlProdLine.Visible = true;
        DataTable _dtprodline = Library.Database.BLL.SlitSeries.GetPRODLINE2(pstr_refNo);

        ddlProdLine.Items.Clear();
        ddlProdLine.Items.Insert(0, new ListItem("--Select--", ""));

        if (_dtprodline.Rows.Count > 0)
        {
            string[] _arr_str_PRODLINE_NO = filter_duplicate_value_4_list(_dtprodline, "PRODLINE_NO");

            foreach (string _str_captured in _arr_str_PRODLINE_NO)
            {
                if (_str_captured == null)
                    break;

                if (!_str_captured.Equals(""))
                    ddlProdLine.Items.Add(new ListItem(_str_captured, _str_captured));
                else
                    break;
            }
        }

        ddlPC1Mother.Visible = true;
        ddlPC1Customer.Visible = true;

        ddlPC1Mother.Items.Clear();
        ddlPC1Mother.Items.Add(new ListItem("--Select--", ""));

        ddlPC1Customer.Items.Clear();
        ddlPC1Customer.Items.Add(new ListItem("--Select--", ""));
    }

    protected void ddlRefNo_Changed(object sender, EventArgs e)
    {
        ProductNumber_listing(ddlRefNo.SelectedValue);

        txtDate.Text = "";
        txtPC2Mother.Text = "";
        txtPC2Customer.Text = "";
        txtUnitWeightCustomer.Text = "";
        txtUnitWeightMother.Text = "";
        txtNoOfSlit.Text = "";
        txtLotNo.Text = "";
    }

    private void PC1_Mother_listing()
    {
        DataTable _pc1 = Library.Database.BLL.SlitSeries.GetDDLData2_Rev01(ddlRefNo.SelectedValue, ddlProdLine.SelectedValue);

        ddlPC1Mother.Items.Clear();
        ddlPC1Mother.Items.Add(new ListItem("--Select--", ""));
        ddlPC1Mother.Visible = true;

        if (_pc1.Rows.Count > 0)
        {
            string[] _arr_str_PC1_MOTHER = filter_duplicate_value_4_list(_pc1, "PC1_MOTHER");

            foreach (string _str_captured in _arr_str_PC1_MOTHER)
            {
                if (_str_captured == null)
                    break;

                if (!_str_captured.Equals(""))
                    ddlPC1Mother.Items.Add(new ListItem(_str_captured, _str_captured));
                else
                    break;
            }
        }

        _pc1.Dispose();
    }

    protected void ddlProdLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        PC1_Mother_listing();

        txtPC2Mother.Text = "";
        txtPC2Customer.Text = "";
        txtUnitWeightCustomer.Text = "";
        txtUnitWeightMother.Text = "";
        ddlPC1Customer.SelectedIndex = 0;
        txtNoOfSlit.Text = "";
        txtLotNo.Text = "";
        rdPos.Text = "";
        rdInc.Text = "";
    }

    private void PC1_Customer_Listing(string pstr_PC2Mother)
    {
        DataTable dtmaxRev = Library.Database.BLL.SubSlitRequest.chkRefNo(ddlRefNo.SelectedValue);
        string IdSubSlit = string.Empty;

        if (dtmaxRev.Rows.Count > 0)
            IdSubSlit = dtmaxRev.Rows[0]["ID_SUBSLIT_REQUEST"].ToString();

        dtmaxRev.Dispose();

        DataTable _pc1cust = Library.Database.BLL.SlitSeries.GetDDLPC1Cust_Rev01(ddlRefNo.SelectedValue, IdSubSlit, ddlProdLine.SelectedValue, ddlPC1Mother.SelectedValue, pstr_PC2Mother);

        ddlPC1Customer.Items.Clear();
        ddlPC1Customer.Items.Add(new ListItem("--Select--", ""));

        if (_pc1cust.Rows.Count > 0)
        {
            string[] _arr_str_PC1 = filter_duplicate_value_4_list(_pc1cust, "PC1");

            foreach (string _str_captured in _arr_str_PC1)
            {
                if (_str_captured == null)
                    break;

                if (!_str_captured.Equals(""))
                    ddlPC1Customer.Items.Add(new ListItem(_str_captured, _str_captured));
                else
                    break;
            }
        }

        _pc1cust.Dispose();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdn_PC2_Mother.Value))
        {
            txtPC2Mother.Text = hdn_PC2_Mother.Value;
            txtUnitWeightMother.Text = hdn_Unit_Weight_Mother.Value;
        }
        else
        {
            hdn_PC1_Customer.Value = ddlPC1Customer.SelectedIndex.ToString();
        }

        if (!string.IsNullOrEmpty(hdn_PC1_Customer.Value))
        {
            txtPC2Customer.Text = hdn_PC2_Customer.Value;
            ddlPC1Customer.SelectedIndex = Convert.ToInt32(hdn_PC1_Customer.Value);
            txtUnitWeightCustomer.Text = hdn_UnitWeightCustomer.Value;
        }
        else
        {
            PC1_Customer_Listing(hdn_PC2_Mother.Value);
        }
    }

    protected void ddlPC1Customer_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdn_PC1_Customer.Value = ddlPC1Customer.SelectedIndex.ToString();

        txtPC2Customer.Text = "";
        txtUnitWeightCustomer.Text = "";
        txtNoOfSlit.Text = "";
        rdPos.Text = "";
        rdInc.Text = "";
    }

    protected void ddlPC1Mother_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPC2Mother.Text = "";
        txtUnitWeightMother.Text = "";
        ddlPC1Customer.SelectedIndex = 0;
        hdn_PC1_Customer.Value = "";
        txtPC2Customer.Text = "";
        hdn_PC2_Customer.Value = "";
        txtUnitWeightCustomer.Text = "";
        hdn_UnitWeightCustomer.Value = "";
        txtNoOfSlit.Text = "";
        rdPos.Text = "";
        rdInc.Text = "";
    }
}