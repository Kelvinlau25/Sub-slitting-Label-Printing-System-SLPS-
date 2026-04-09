using System;
using System.Data;

public partial class MasterMaint_MM_COMPANY_Dtl : Control.Base
{
    public MasterMaint_MM_COMPANY_Dtl()
    {
        SetupKey = "MM_COMPANY";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfCompCode.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Company Code");
        rfCompName.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Company Name");
        rfAddress.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Address");
        rfTelephone.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Telephone");
        rfEmail.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Email Address");
        rfSlit.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Slit Code");
        rfEmail2.ErrorMessage = "Email Address format:   eg.you@(domain.com)";

        UCAction.DisplayMode += UCAction_DisplayMode;
        UCAction.ModifyMode += UCAction_ModifyMode;
        UCAction.AddAction += UCAction_AddAction;
        UCAction.EditAction += UCAction_AddAction;
        UCAction.AddResetAction += UCAction_AddResetAction;
        UCAction.EditResetAction += UCAction_AddResetAction;
        UCAction.DeleteAction += UCAction_DeleteAction;
    }

    protected void UCAction_DisplayMode()
    {
        Cdisplay.Visible = true;
        Cmodify.Visible = false;

        DataTable _datatable = Library.Database.BLL.Company.GetData(Key);
        lblCompCode.Text = _datatable.Rows[0]["COMPANYCODE"].ToString();
        lblCompName.Text = _datatable.Rows[0]["COMPANYNAME"].ToString().ToUpper();
        lblAddress.Text = _datatable.Rows[0]["ADDRESS"].ToString().ToUpper();
        lblTelephone.Text = _datatable.Rows[0]["TELEPHONE"].ToString();
        lblEmail.Text = _datatable.Rows[0]["Email"].ToString();
        lblslit.Text = _datatable.Rows[0]["SLIT_CODE"].ToString();

        UCAction.CreatedBy = "";
        UCAction.CreatedDate = new DateTime();
        UCAction.CreatedLoc = "";
        DateTime tmpCreateDate;
        if (DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
        {
            UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();
            UCAction.CreatedDate = tmpCreateDate;
            UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
        }

        UCAction.UpdatedBy = "";
        UCAction.UpdatedDate = new DateTime();
        UCAction.UpdatedLoc = "";
        DateTime tmpUpdateDate;
        if (DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
        {
            UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();
            UCAction.UpdatedDate = tmpUpdateDate;
            UCAction.UpdatedLoc = _datatable.Rows[0]["UPDATED_LOC"].ToString();
        }

        UCAction.EditMode = _datatable.Rows[0]["REC_TYPE"].ToString() != "5";
    }

    protected void UCAction_ModifyMode()
    {
        Cdisplay.Visible = false;
        Cmodify.Visible = true;

        if (!IsPostBack)
        {
            if (Action == EnumAction.Edit)
            {
                txtCompCode.Visible = false;
                lbCompCode.Visible = true;
                DataTable _datatable = Library.Database.BLL.Company.GetData(Key);
                txtCompCode.Text = _datatable.Rows[0]["COMPANYCODE"].ToString();
                lbCompCode.Text = _datatable.Rows[0]["COMPANYCODE"].ToString();
                txtCompName.Text = _datatable.Rows[0]["COMPANYNAME"].ToString();
                txtAddress.Text = _datatable.Rows[0]["ADDRESS"].ToString();
                txtTelephone.Text = _datatable.Rows[0]["TELEPHONE"].ToString();
                txtEmail.Text = _datatable.Rows[0]["EMAIL"].ToString();
                txtSlit.Text = _datatable.Rows[0]["SLIT_CODE"].ToString();

                UCAction.CreatedBy = "";
                UCAction.CreatedDate = new DateTime();
                UCAction.CreatedLoc = "";
                DateTime tmpCreateDate;
                if (DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
                {
                    UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();
                    UCAction.CreatedDate = tmpCreateDate;
                    UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
                }

                UCAction.UpdatedBy = "";
                UCAction.UpdatedDate = new DateTime();
                UCAction.UpdatedLoc = "";
                DateTime tmpUpdateDate;
                if (DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
                {
                    UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();
                    UCAction.UpdatedDate = tmpUpdateDate;
                    UCAction.UpdatedLoc = _datatable.Rows[0]["UPDATED_LOC"].ToString();
                }
            }
            else if (Action == EnumAction.Add)
            {
                lbCompCode.Visible = false;
                txtCompCode.Visible = true;
            }
        }
    }

    /// <summary>
    /// Handler the add Submit Function
    /// </summary>
    protected void UCAction_AddAction()
    {
        string _temp = "";
        string slit = txtSlit.Text.Trim();

        if (Action == EnumAction.Edit)
        {
            _temp = Library.Database.BLL.Company.Maint(Key, txtCompCode.Text.Trim(), txtCompName.Text.Trim().ToUpper(),
                slit, txtAddress.Text.Trim().ToUpper(), txtTelephone.Text.Trim(),
                txtEmail.Text.Trim(), ((int)Action).ToString());
        }
        else if (Action == EnumAction.Add)
        {
            _temp = Library.Database.BLL.Company.Maint("0", txtCompCode.Text.Trim(), txtCompName.Text.Trim().ToUpper(),
                slit, txtAddress.Text.Trim().ToUpper(), txtTelephone.Text.Trim(),
                txtEmail.Text.Trim(), ((int)Action).ToString());
        }

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            if (_temp == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
            }
        }
    }

    protected void UCAction_AddResetAction()
    {
        Response.Redirect(Request.RawUrl);
    }

    /// <summary>
    /// Delete Action
    /// </summary>
    protected void UCAction_DeleteAction()
    {
        string _temp = Library.Database.BLL.Company.Maint(Key, lblCompCode.Text, lblCompName.Text, "",
            lblAddress.Text, lblTelephone.Text, lblEmail.Text,
            ((int)Library.Root.Control.Base.EnumAction.Delete).ToString());

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            if (_temp == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}