using System;
using System.Data;

public partial class MasterMaint_MM_PC2_MOTHER_Dtl : Control.Base
{
    public MasterMaint_MM_PC2_MOTHER_Dtl()
    {
        SetupKey = "MM_PC2_MOTHER"; 
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfPC2.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PC2");
        rfThickness.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Thickness");
        rfType.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Type");
        rfWidth.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Width");
        rfLength.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Length");
        rfPackCode.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Packing Code");
        rfGrade.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Grade");
        rfMachine.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Machine");
        rfUnitWeight.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Unit Weight");
        rfNumPack.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "No. Per Pack");

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

        DataTable _datatable = Library.Database.BLL.PC2Mother.GetData(Key);

        lblPC2.Text = _datatable.Rows[0]["PC2M"].ToString();
        lblThickness.Text = _datatable.Rows[0]["THICKNESS"].ToString();
        lblType.Text = _datatable.Rows[0]["TYPE"].ToString();
        lblWidth.Text = _datatable.Rows[0]["WIDTH"].ToString();
        lblLength.Text = _datatable.Rows[0]["LENGTH"].ToString();
        lblPackCode.Text = _datatable.Rows[0]["PACK_CODE"].ToString();
        lblGrade.Text = _datatable.Rows[0]["GRADE"].ToString();
        lblCoreCode.Text = _datatable.Rows[0]["CORE_CODE"].ToString();
        lblMachine.Text = _datatable.Rows[0]["MACHINE"].ToString();
        lblUnitWeight.Text = _datatable.Rows[0]["UNIT_WEIGHT"].ToString();
        lblNumPerPack.Text = _datatable.Rows[0]["NUM_PER_PACK"].ToString();
        lblRemarks.Text = _datatable.Rows[0]["REMARKS"].ToString();

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
                txtPC2.Enabled = false;
                DataTable _datatable = Library.Database.BLL.PC2Mother.GetData(Key);

                txtPC2.Text = _datatable.Rows[0]["PC2M"].ToString();
                txtThickness.Text = _datatable.Rows[0]["THICKNESS"].ToString();
                txtType.Text = _datatable.Rows[0]["TYPE"].ToString();
                txtWidth.Text = _datatable.Rows[0]["WIDTH"].ToString();
                txtLength.Text = _datatable.Rows[0]["LENGTH"].ToString();
                txtPackCode.Text = _datatable.Rows[0]["PACK_CODE"].ToString();
                txtGrade.Text = _datatable.Rows[0]["GRADE"].ToString();
                txtCoreCode.Text = _datatable.Rows[0]["CORE_CODE"].ToString();
                txtMachine.Text = _datatable.Rows[0]["MACHINE"].ToString();
                txtUnitWeight.Text = _datatable.Rows[0]["UNIT_WEIGHT"].ToString();
                txtNumPack.Text = _datatable.Rows[0]["NUM_PER_PACK"].ToString();
                txtRemarks.Text = _datatable.Rows[0]["REMARKS"].ToString();

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
                tblPC2.Visible = false;
            }
        }
    }

    /// <summary>
    /// Handler the add/edit Submit Function
    /// </summary>
    protected void UCAction_AddAction()
    {
        txtPC2.Text = txtThickness.Text + "-" + txtType.Text + "-" + txtWidth.Text + "x" + txtLength.Text +
                      "-" + txtGrade.Text + txtPackCode.Text + txtNumPack.Text + txtCoreCode.Text;

        string _temp = Library.Database.BLL.PC2Mother.Maint(Key, txtPC2.Text, txtThickness.Text, txtType.Text, txtWidth.Text,
                                                             txtLength.Text, txtPackCode.Text, txtGrade.Text, txtCoreCode.Text,
                                                             txtMachine.Text, txtUnitWeight.Text, txtNumPack.Text, txtRemarks.Text,
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
        string _temp = Library.Database.BLL.PC2Mother.Maint(Key, txtPC2.Text, txtThickness.Text, txtType.Text, txtWidth.Text,
                                                             txtLength.Text, txtPackCode.Text, txtGrade.Text, txtCoreCode.Text,
                                                             txtMachine.Text, txtUnitWeight.Text, txtNumPack.Text, txtRemarks.Text,
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