using System;
using System.Data;

public partial class MasterMaint_PRINT_ALIGN_INIT_Dtl : Control.Base
{
    public MasterMaint_PRINT_ALIGN_INIT_Dtl()
    {
        SetupKey = "PRINT_ALIGN_INIT";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfPrinterName.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Printer_Name");
        rfTextFont.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Text_Font");
        rfBarcodeFont.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Barcode_Font");

        rfWidthX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Width_X");
        reWidthX.ErrorMessage = "Invalid Format.Width X Only Accepted numeric value.";
        reWidthX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfTextFontSize.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Text_Font_Size");
        reTextFontSize.ErrorMessage = "Invalid Format.Text Font Size Only Accepted numeric value.";
        reTextFontSize.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rftxtWidthY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Width_Y");
        reWidthY.ErrorMessage = "Invalid Format.Width Y Only Accepted numeric value.";
        reWidthY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfLengthHeaderX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Length_Header_X");
        reLengthHeaderX.ErrorMessage = "Invalid Format.Length Header X Only Accepted numeric value.";
        reLengthHeaderX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfBarcodeFontSize.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Barcode_Font_Size");
        reBarcodeFontSize.ErrorMessage = "Invalid Format.Barcode Font Size Only Accepted numeric value.";
        reBarcodeFontSize.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfLengthHeaderY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Length_Header_Y");
        reLengthHeaderY.ErrorMessage = "Invalid Format.Length Header Y Only Accepted numeric value.";
        reLengthHeaderY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfPackCodeX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Pack_Code_X");
        rePackCodeX.ErrorMessage = "Invalid Format.Pack Code X Only Accepted numeric value.";
        rePackCodeX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfUnitWeightX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Unit_Weight_X");
        reUnitWeightX.ErrorMessage = "Invalid Format.Unit Weight X Only Accepted numeric value.";
        reUnitWeightX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfPackCodeY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Pack_Code_Y");
        rePackCodeY.ErrorMessage = "Invalid Format.Pack Code Y Only Accepted numeric value.";
        rePackCodeY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfUnitWeightY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Unit_Weight_Y");
        reUnitWeightY.ErrorMessage = "Invalid Format.Unit Weigth Y Only Accepted numeric value.";
        reUnitWeightY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfNumPerPackX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Num_Per_Pack_X");
        reNumPerPackX.ErrorMessage = "Invalid Format.Num Per Pack X Only Accepted numeric value.";
        reNumPerPackX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfSlitLotNoX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Slit_Lot_No_X");
        reSlitLotNoX.ErrorMessage = "Invalid Format.Slit Lot No X Only Accepted numeric value.";
        reSlitLotNoX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfNumPerPackY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Num_Per_Pack_Y");
        reNumPerPackY.ErrorMessage = "Invalid Format.Num Per Pack Y Only Accepted numeric value.";
        reNumPerPackY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfSlitLotNoY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Slit_Lot_No_Y");
        reSlitLotNoY.ErrorMessage = "Invalid Format.Slit Lot No Y Only Accepted numeric value.";
        reSlitLotNoY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfPC1X.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PC1_X");
        rePC1X.ErrorMessage = "Invalid Format.PC1 X Only Accepted numeric value.";
        rePC1X.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rftGradeX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Grade_X");
        reGradeX.ErrorMessage = "Invalid Format.Grade X Only Accepted numeric value.";
        reGradeX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfPC1Y.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "PC1_Y");
        rePC1Y.ErrorMessage = "Invalid Format.PC1 Y Only Accepted numeric value.";
        rePC1Y.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfGradeY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Grade_Y");
        reGradeY.ErrorMessage = "Invalid Format.Grade Y Only Accepted numeric value.";
        reGradeY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfLengthX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Length_X");
        reLengthX.ErrorMessage = "Invalid Format.Length X Only Accepted numeric value.";
        reLengthX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfCoreCodeX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Core_Code_X");
        reCoreCodeX.ErrorMessage = "Invalid Format.Core Code X Only Accepted numeric value.";
        reCoreCodeX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfLengthY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Length_Y");
        reLengthY.ErrorMessage = "Invalid Format.Length Y Only Accepted numeric value.";
        reLengthY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfCoreCodeY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Core_Code_Y");
        reCoreCodeY.ErrorMessage = "Invalid Format.Core Code Y Only Accepted numeric value.";
        reCoreCodeY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfThicknessX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Thickness_X");
        reThicknessX.ErrorMessage = "Invalid Format.Thickness X Only Accepted numeric value.";
        reThicknessX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfBarcodeX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Barcode_X");
        reBarcodeX.ErrorMessage = "Invalid Format.Barcode X Only Accepted numeric value.";
        reBarcodeX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfThicknessY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Thickness_Y");
        reThicknessY.ErrorMessage = "Invalid Format.Thickness Y Only Accepted numeric value.";
        reThicknessY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfBarcodeY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Barcode_Y");
        reBarcodeY.ErrorMessage = "Invalid Format.Barcode Y Only Accepted numeric value.";
        reBarcodeY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfTypeX.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Type_X");
        reTypeX.ErrorMessage = "Invalid Format.Type X Only Accepted numeric value.";
        reTypeX.ValidationExpression = "^(0|[1-9][0-9]*)$";

        rfTypeY.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Type_Y");
        reTypeY.ErrorMessage = "Invalid Format.Type Y Only Accepted numeric value.";
        reTypeY.ValidationExpression = "^(0|[1-9][0-9]*)$";

        RadioButton2.Checked = true;
    }

    protected void UCAction_DisplayMode(object sender, EventArgs e)
    {
        Cdisplay.Visible = true;
        Cmodify.Visible = false;

        DataTable _datatable = Library.Database.BLL.PrintAlignInit.GetData(Key);

        lblCompanyCode.Text = _datatable.Rows[0]["CompanyCode"].ToString();
        lblPrinterName.Text = _datatable.Rows[0]["Printer_Name"].ToString();
        lblTextFont.Text = _datatable.Rows[0]["Text_Font"].ToString();
        lblWidthX.Text = _datatable.Rows[0]["Width_X"].ToString();
        lblTextFontSize.Text = _datatable.Rows[0]["Text_Font_Size"].ToString();
        lblWidthY.Text = _datatable.Rows[0]["Width_Y"].ToString();
        lblBarcodeFont.Text = _datatable.Rows[0]["Barcode_Font"].ToString();
        lblLengthHeaderX.Text = _datatable.Rows[0]["Length_Header_X"].ToString();
        lblBarcodeFontSize.Text = _datatable.Rows[0]["Barcode_Font_Size"].ToString();
        lblLengthHeaderY.Text = _datatable.Rows[0]["Length_Header_Y"].ToString();

        lblPackCodeX.Text = _datatable.Rows[0]["Pack_Code_X"].ToString();
        lblUnitWeigthX.Text = _datatable.Rows[0]["Unit_Weight_X"].ToString();
        LblPackCodeY.Text = _datatable.Rows[0]["Pack_Code_Y"].ToString();
        lblUnitWeigthY.Text = _datatable.Rows[0]["Unit_Weight_Y"].ToString();

        lblNumPerPackX.Text = _datatable.Rows[0]["Num_Per_Pack_X"].ToString();
        lblSlitLotNoX.Text = _datatable.Rows[0]["Slit_Lot_No_X"].ToString();
        lblNumPerPackY.Text = _datatable.Rows[0]["Num_Per_Pack_Y"].ToString();
        lblSlitLotNoY.Text = _datatable.Rows[0]["Slit_Lot_No_Y"].ToString();

        lblPC1X.Text = _datatable.Rows[0]["PC1_X"].ToString();
        lblGradeX.Text = _datatable.Rows[0]["Grade_X"].ToString();
        lblPC1Y.Text = _datatable.Rows[0]["PC1_Y"].ToString();
        lblGradeY.Text = _datatable.Rows[0]["Grade_Y"].ToString();

        lblLengthX.Text = _datatable.Rows[0]["Length_X"].ToString();
        lblCoreCodeX.Text = _datatable.Rows[0]["Core_Code_X"].ToString();
        lblLengthY.Text = _datatable.Rows[0]["Length_Y"].ToString();
        lblCoreCodeY.Text = _datatable.Rows[0]["Core_Code_Y"].ToString();

        lblThicknessX.Text = _datatable.Rows[0]["Thickness_X"].ToString();
        lblBarcodeX.Text = _datatable.Rows[0]["Barcode_X"].ToString();
        lblThicknessY.Text = _datatable.Rows[0]["Thickness_Y"].ToString();
        lblBarcodeY.Text = _datatable.Rows[0]["Barcode_Y"].ToString();

        lblTypeX.Text = _datatable.Rows[0]["Type_X"].ToString();
        lblTypeY.Text = _datatable.Rows[0]["Type_Y"].ToString();

        lblDefaultPrinter.Text = Convert.ToBoolean(_datatable.Rows[0]["Default_Printer"]) ? "Yes" : "No";

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

    protected void UCAction_ModifyMode(object sender, EventArgs e)
    {
        Cdisplay.Visible = false;
        Cmodify.Visible = true;

        if (!IsPostBack)
        {
            if (Action == EnumAction.Edit)
            {
                txtPrinterName.Visible = false;
                lbPrinterName.Visible = true;

                DataTable _datatable = Library.Database.BLL.PrintAlignInit.GetData(Key);

                lbCompanyCode.Text = _datatable.Rows[0]["CompanyCode"].ToString();
                txtPrinterName.Text = _datatable.Rows[0]["Printer_Name"].ToString();
                lbPrinterName.Text = _datatable.Rows[0]["Printer_Name"].ToString();
                txtTextFont.Text = _datatable.Rows[0]["Text_Font"].ToString();
                txtWidthX.Text = _datatable.Rows[0]["Width_X"].ToString();
                txtTextFontSize.Text = _datatable.Rows[0]["Text_Font_Size"].ToString();
                txtWidthY.Text = _datatable.Rows[0]["Width_Y"].ToString();
                txtBarcodeFont.Text = _datatable.Rows[0]["Barcode_Font"].ToString();
                txtLengthHeaderX.Text = _datatable.Rows[0]["Length_Header_X"].ToString();
                txtBarcodeFontSize.Text = _datatable.Rows[0]["Barcode_Font_Size"].ToString();
                txtLengthHeaderY.Text = _datatable.Rows[0]["Length_Header_Y"].ToString();

                txtPackCodeX.Text = _datatable.Rows[0]["Pack_Code_X"].ToString();
                txtUnitWeightX.Text = _datatable.Rows[0]["Unit_Weight_X"].ToString();
                txtPackCodeY.Text = _datatable.Rows[0]["Pack_Code_Y"].ToString();
                txtUnitWeightY.Text = _datatable.Rows[0]["Unit_Weight_Y"].ToString();

                txtNumPerPackX.Text = _datatable.Rows[0]["Num_Per_Pack_X"].ToString();
                txtSlitLotNoX.Text = _datatable.Rows[0]["Slit_Lot_No_X"].ToString();
                txtNumPerPackY.Text = _datatable.Rows[0]["Num_Per_Pack_Y"].ToString();
                txtSlitLotNoY.Text = _datatable.Rows[0]["Slit_Lot_No_Y"].ToString();

                txtPC1X.Text = _datatable.Rows[0]["PC1_X"].ToString();
                txtGradeX.Text = _datatable.Rows[0]["Grade_X"].ToString();
                txtPC1Y.Text = _datatable.Rows[0]["PC1_Y"].ToString();
                txtGradeY.Text = _datatable.Rows[0]["Grade_Y"].ToString();

                txtLengthX.Text = _datatable.Rows[0]["LENGTH_X"].ToString();
                txtCoreCodeX.Text = _datatable.Rows[0]["Core_Code_X"].ToString();
                txtLengthY.Text = _datatable.Rows[0]["Length_Y"].ToString();
                txtCoreCodeY.Text = _datatable.Rows[0]["Core_Code_Y"].ToString();

                txtThicknessX.Text = _datatable.Rows[0]["Thickness_X"].ToString();
                txtBarcodeX.Text = _datatable.Rows[0]["Barcode_X"].ToString();
                txtThicknessY.Text = _datatable.Rows[0]["Thickness_Y"].ToString();
                txtBarcodeY.Text = _datatable.Rows[0]["Barcode_Y"].ToString();

                txtTypeX.Text = _datatable.Rows[0]["Type_X"].ToString();
                txtTypeY.Text = _datatable.Rows[0]["Type_Y"].ToString();

                bool isDefaultPrinter = Convert.ToBoolean(_datatable.Rows[0]["Default_Printer"]);
                RadioButton1.Checked = isDefaultPrinter;
                RadioButton2.Checked = !isDefaultPrinter;

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
                txtPrinterName.Visible = true;
                lbPrinterName.Visible = false;
                lbCompanyCode.Visible = false;
                Label131.Visible = false;
                Label132.Visible = false;
                RadioButton2.Checked = true;
            }
        }
    }

    /// <summary>
    /// Handler the add Submit Function
    /// </summary>
    protected void UCAction_AddAction(object sender, EventArgs e)
    {
        string Company_Code = Session["COMPANYCODE"].ToString().Trim();

        string _temp = Library.Database.BLL.PrintAlignInit.Maint(Key, txtPrinterName.Text, txtTextFont.Text, txtWidthX.Text, txtTextFontSize.Text,
                                                                  txtWidthY.Text, txtBarcodeFont.Text, txtLengthHeaderX.Text, txtBarcodeFontSize.Text,
                                                                  txtLengthHeaderY.Text, txtPackCodeX.Text, txtUnitWeightX.Text, txtPackCodeY.Text,
                                                                  txtUnitWeightY.Text, txtNumPerPackX.Text, txtSlitLotNoX.Text, txtNumPerPackY.Text, txtSlitLotNoY.Text,
                                                                  txtPC1X.Text, txtGradeX.Text, txtPC1Y.Text, txtGradeY.Text, txtLengthX.Text, txtCoreCodeX.Text, txtLengthY.Text,
                                                                  txtCoreCodeY.Text, txtThicknessX.Text, txtBarcodeX.Text, txtThicknessY.Text, txtBarcodeY.Text, txtTypeX.Text,
                                                                  txtTypeY.Text, RadioButton1.Checked, RadioButton2.Checked, Company_Code, ((int)Action).ToString());

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            string message = _temp == "0"
                ? string.Format(Resources.Message.Failed, Action.ToString())
                : _temp;
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, message);
        }
    }

    protected void UCAction_AddResetAction(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    /// <summary>
    /// Delete Action
    /// </summary>
    protected void UCAction_DeleteAction(object sender, EventArgs e)
    {
        string _temp = Library.Database.BLL.PrintAlignInit.Maint(Key, txtPrinterName.Text, txtTextFont.Text, txtWidthX.Text, txtTextFontSize.Text,
                                                                  txtWidthY.Text, txtBarcodeFont.Text, txtLengthHeaderX.Text, txtBarcodeFontSize.Text,
                                                                  txtLengthHeaderY.Text, txtPackCodeX.Text, txtUnitWeightX.Text, txtPackCodeY.Text,
                                                                  txtUnitWeightY.Text, txtNumPerPackX.Text, txtSlitLotNoX.Text, txtNumPerPackY.Text, txtSlitLotNoY.Text,
                                                                  txtPC1X.Text, txtGradeX.Text, txtPC1Y.Text, txtGradeY.Text, txtLengthX.Text, txtCoreCodeX.Text, txtLengthY.Text,
                                                                  txtCoreCodeY.Text, txtThicknessX.Text, txtBarcodeX.Text, txtThicknessY.Text, txtBarcodeY.Text, txtTypeX.Text,
                                                                  txtTypeY.Text, RadioButton1.Checked, RadioButton2.Checked, lbCompanyCode.Text,
                                                                  ((int)Library.Root.Control.Base.EnumAction.Delete).ToString());

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            string message = _temp == "0"
                ? string.Format(Resources.Message.Failed, Action.ToString())
                : _temp;
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton2.Checked = false;
    }

    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, "Please set Yes on other required printer first before setting to No");
        RadioButton2.Checked = false;
    }
}