using System;
using System.Data;

public partial class MasterMaint_MM_PRODLINE_Dtl : Control.Base
{
    public MasterMaint_MM_PRODLINE_Dtl()
    {
        SetupKey = "MM_PRODLINE";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfProdLine.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PRODLINE_NO");

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

        DataTable _datatable = Library.Database.BLL.MM_PRODLINE.GetData(Key);

        lblProdLine.Text = _datatable.Rows[0]["PRODLINE_NO"].ToString();
        lblDesc.Text = _datatable.Rows[0]["DESCRIPTION"].ToString();

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
                txtProdLine.Visible = false;
                lbProdLine.Visible = true;

                DataTable _datatable = Library.Database.BLL.MM_PRODLINE.GetData(Key);

                txtProdLine.Text = _datatable.Rows[0]["PRODLINE_NO"].ToString();
                lbProdLine.Text = _datatable.Rows[0]["PRODLINE_NO"].ToString();
                txtDesc.Text = _datatable.Rows[0]["DESCRIPTION"].ToString();

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
                txtProdLine.Visible = true;
                lbProdLine.Visible = false;
            }
        }
    }

    /// <summary>
    /// Handler the add/edit Submit Function
    /// </summary>
    protected void UCAction_AddAction()
    {
        string _temp = Library.Database.BLL.MM_PRODLINE.Maint(Key, txtProdLine.Text, txtDesc.Text,
                                                               Convert.ToInt32(Action).ToString());

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            if (_temp == "0")
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
            else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
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
        string _temp = Library.Database.BLL.MM_PRODLINE.Maint(Key, txtProdLine.Text, txtDesc.Text,
                                                               Convert.ToInt32(Library.Root.Control.Base.EnumAction.Delete).ToString());

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            if (_temp == "0")
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
            else
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}