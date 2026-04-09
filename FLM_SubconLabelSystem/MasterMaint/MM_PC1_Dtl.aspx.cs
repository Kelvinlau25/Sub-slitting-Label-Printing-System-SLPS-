using System;
using System.Data;

public partial class MasterMaint_MM_PC1_Dtl : Control.Base
{
    public MasterMaint_MM_PC1_Dtl()
    {
        SetupKey = "MM_PC1";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfPC1.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PC1");
        rfNameDelivery.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Description");

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

        DataTable _datatable = Library.Database.BLL.PC1.GetData(Key);
        lblPC1.Text = _datatable.Rows[0]["PC1"].ToString();
        lblNameDelivery.Text = _datatable.Rows[0]["DESCRIPTION"].ToString().ToUpper();

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
                txtPC1.Enabled = false;
                lbPC1.Visible = true;
                txtPC1.Visible = false;

                DataTable _datatable = Library.Database.BLL.PC1.GetData(Key);

                txtPC1.Text = _datatable.Rows[0]["PC1"].ToString();
                lbPC1.Text = _datatable.Rows[0]["PC1"].ToString();
                txtNameDelivery.Text = _datatable.Rows[0]["DESCRIPTION"].ToString().ToUpper();

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
                lbPC1.Visible = false;
                txtPC1.Visible = true;
            }
        }
    }

    protected void UCAction_AddAction()
    {
        string _temp = "";

        if (Action == EnumAction.Edit)
        {
            _temp = Library.Database.BLL.PC1.Maint(Key, txtPC1.Text, "0", txtNameDelivery.Text, ((int)Action).ToString());
        }
        else if (Action == EnumAction.Add)
        {
            _temp = Library.Database.BLL.PC1.Maint("0", txtPC1.Text, "0", txtNameDelivery.Text, ((int)Action).ToString());
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

    protected void UCAction_DeleteAction()
    {
        string _temp = Library.Database.BLL.PC1.Maint(Key, lblPC1.Text, "0", lblNameDelivery.Text,
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