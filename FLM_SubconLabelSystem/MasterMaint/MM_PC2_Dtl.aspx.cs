using System;
using System.Data;

public partial class MasterMaint_MM_PC2_Dtl : Control.Base
{
    public MasterMaint_MM_PC2_Dtl()
    {
        SetupKey = "MM_PC2";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfThickness.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Thickness");
        rfType.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Type");
        rfWidth.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Width");
        rfLength.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Length");
        rfPackCode.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Packing Code");
        rfGrade.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Grade");
        rfUnitWeight.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Unit Weight");
        reUnitWeight.ErrorMessage = "Invalid Format.Unit Weight Only Accepted numeric with or without decimal value.";
        reUnitWeight.ValidationExpression = @"^[0-9]\d{0,9}(\.\d{1,3})?%?$";

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

        DataTable _datatable = Library.Database.BLL.PC2.GetData(Key);
        lblPC2.Text = _datatable.Rows[0]["PC2"].ToString();
        lblThickness.Text = _datatable.Rows[0]["THICKNESS"].ToString();
        lblType.Text = _datatable.Rows[0]["TYPE"].ToString();
        lblWidth.Text = _datatable.Rows[0]["WIDTH"].ToString();
        lblLength.Text = _datatable.Rows[0]["LENGTH"].ToString();
        lblPackCode.Text = _datatable.Rows[0]["PACK_CODE"].ToString();
        lblGrade.Text = _datatable.Rows[0]["GRADE"].ToString();
        lblCoreCode.Text = _datatable.Rows[0]["CORE_CODE"].ToString();
        lblUnitWeight.Text = _datatable.Rows[0]["UNIT_WEIGHT"].ToString();
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
                lbThickness.Visible = true;
                txtThickness.Visible = false;
                lbType.Visible = true;
                txtType.Visible = false;
                lbWidth.Visible = true;
                txtWidth.Visible = false;
                lbLength.Visible = true;
                txtLength.Visible = false;
                lbPackCode.Visible = true;
                txtPackCode.Visible = false;
                lbCoreCode.Visible = true;
                txtCoreCode.Visible = false;
                lbGrade.Visible = true;
                txtGrade.Visible = false;

                DataTable _datatable = Library.Database.BLL.PC2.GetData(Key);

                txtPC2.Text = _datatable.Rows[0]["PC2"].ToString();
                txtThickness.Text = _datatable.Rows[0]["THICKNESS"].ToString();
                lbThickness.Text = _datatable.Rows[0]["THICKNESS"].ToString();
                txtType.Text = _datatable.Rows[0]["TYPE"].ToString();
                lbType.Text = _datatable.Rows[0]["TYPE"].ToString();
                lbWidth.Text = _datatable.Rows[0]["WIDTH"].ToString();
                txtWidth.Text = _datatable.Rows[0]["WIDTH"].ToString();
                lbLength.Text = _datatable.Rows[0]["LENGTH"].ToString();
                txtLength.Text = _datatable.Rows[0]["LENGTH"].ToString();
                lbPackCode.Text = _datatable.Rows[0]["PACK_CODE"].ToString();
                txtPackCode.Text = _datatable.Rows[0]["PACK_CODE"].ToString();
                lbGrade.Text = _datatable.Rows[0]["GRADE"].ToString();
                txtGrade.Text = _datatable.Rows[0]["GRADE"].ToString();
                txtCoreCode.Text = _datatable.Rows[0]["CORE_CODE"].ToString();
                lbCoreCode.Text = _datatable.Rows[0]["CORE_CODE"].ToString();
                txtUnitWeight.Text = _datatable.Rows[0]["UNIT_WEIGHT"].ToString();
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
                lbThickness.Visible = false;
                txtThickness.Visible = true;
                lbType.Visible = false;
                txtType.Visible = true;
                lbWidth.Visible = false;
                txtWidth.Visible = true;
                lbPackCode.Visible = false;
                txtPackCode.Visible = true;
                lbCoreCode.Visible = false;
                txtCoreCode.Visible = true;
                lbGrade.Visible = false;
                txtGrade.Visible = true;
                tblPC2.Visible = false;
            }
        }
    }

    protected void UCAction_AddAction()
    {
        string Comp_Code = Session["COMPANYCODE"].ToString();
        txtPC2.Text = txtThickness.Text.ToUpper() + "-" + txtType.Text.ToUpper() + "-" + txtWidth.Text.ToUpper() + "x" + txtLength.Text.ToUpper()
                      + "-" + txtGrade.Text.ToUpper() + txtPackCode.Text.ToUpper() + txtCoreCode.Text.ToUpper();

        string _temp = Library.Database.BLL.PC2.Maint(Key, Comp_Code, txtPC2.Text, txtThickness.Text.ToUpper(), txtType.Text.ToUpper(), txtWidth.Text.ToUpper(),
                                                       txtLength.Text.ToUpper(), txtPackCode.Text.ToUpper(), txtGrade.Text.ToUpper(), txtCoreCode.Text.ToUpper(),
                                                       txtUnitWeight.Text, "", txtRemarks.Text, ((int)Action).ToString());

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
        string Company_Code = Session["COMPANYCODE"].ToString();
        string _temp = Library.Database.BLL.PC2.Maint(Key, Company_Code, lblPC2.Text, txtThickness.Text, txtType.Text, txtWidth.Text,
                                                       txtLength.Text, txtPackCode.Text, txtGrade.Text, txtCoreCode.Text,
                                                       txtUnitWeight.Text, "", txtRemarks.Text,
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