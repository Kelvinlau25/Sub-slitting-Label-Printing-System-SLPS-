using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class PRINT_ALIGN_INIT_DtlModel : BasePageModel
    {
        private const string NumericPattern = @"^(0|[1-9][0-9]*)$";
        private const string NumericError = "Must be a valid non-negative integer!";

        public PRINT_ALIGN_INIT_DtlModel()
        {
            SetupKey = "PRINT_ALIGN_INIT";
            FunctionControl = false;
        }

        // Display fields
        public string DisplayPrinterName { get; set; } = string.Empty;
        public string DisplayCompanyCode { get; set; } = string.Empty;
        public string DisplayTextFont { get; set; } = string.Empty;
        public string DisplayTextFontSize { get; set; } = string.Empty;
        public string DisplayBarcodeFont { get; set; } = string.Empty;
        public string DisplayBarcodeFontSize { get; set; } = string.Empty;
        public string DisplayWidthX { get; set; } = string.Empty;
        public string DisplayWidthY { get; set; } = string.Empty;
        public string DisplayLengthHeaderX { get; set; } = string.Empty;
        public string DisplayLengthHeaderY { get; set; } = string.Empty;
        public string DisplayPackCodeX { get; set; } = string.Empty;
        public string DisplayPackCodeY { get; set; } = string.Empty;
        public string DisplayUnitWeightX { get; set; } = string.Empty;
        public string DisplayUnitWeightY { get; set; } = string.Empty;
        public string DisplayNumPerPackX { get; set; } = string.Empty;
        public string DisplayNumPerPackY { get; set; } = string.Empty;
        public string DisplaySlitLotNoX { get; set; } = string.Empty;
        public string DisplaySlitLotNoY { get; set; } = string.Empty;
        public string DisplayPC1X { get; set; } = string.Empty;
        public string DisplayPC1Y { get; set; } = string.Empty;
        public string DisplayGradeX { get; set; } = string.Empty;
        public string DisplayGradeY { get; set; } = string.Empty;
        public string DisplayLengthX { get; set; } = string.Empty;
        public string DisplayLengthY { get; set; } = string.Empty;
        public string DisplayCoreCodeX { get; set; } = string.Empty;
        public string DisplayCoreCodeY { get; set; } = string.Empty;
        public string DisplayThicknessX { get; set; } = string.Empty;
        public string DisplayThicknessY { get; set; } = string.Empty;
        public string DisplayBarcodeX { get; set; } = string.Empty;
        public string DisplayBarcodeY { get; set; } = string.Empty;
        public string DisplayTypeX { get; set; } = string.Empty;
        public string DisplayTypeY { get; set; } = string.Empty;
        public string DisplayDefaultYes { get; set; } = string.Empty;
        public string DisplayDefaultNo { get; set; } = string.Empty;

        // Form fields
        [BindProperty] [Required(ErrorMessage = "Printer Name cannot be empty!")]
        public string PrinterName { get; set; }

        [BindProperty] [Required(ErrorMessage = "Company Code cannot be empty!")]
        public string CompanyCode { get; set; }

        [BindProperty] [Required(ErrorMessage = "Text Font cannot be empty!")]
        public string TextFont { get; set; }

        [BindProperty] [Required(ErrorMessage = "Text Font Size cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string TextFontSize { get; set; }

        [BindProperty] [Required(ErrorMessage = "Barcode Font cannot be empty!")]
        public string BarcodeFont { get; set; }

        [BindProperty] [Required(ErrorMessage = "Barcode Font Size cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string BarcodeFontSize { get; set; }

        [BindProperty] [Required(ErrorMessage = "Width X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string WidthX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Width Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string WidthY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Length Header X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string LengthHeaderX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Length Header Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string LengthHeaderY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Pack Code X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string PackCodeX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Pack Code Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string PackCodeY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Unit Weight X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string UnitWeightX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Unit Weight Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string UnitWeightY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Num Per Pack X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string NumPerPackX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Num Per Pack Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string NumPerPackY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Slit Lot No X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string SlitLotNoX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Slit Lot No Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string SlitLotNoY { get; set; }

        [BindProperty] [Required(ErrorMessage = "PC1 X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string PC1X { get; set; }
        [BindProperty] [Required(ErrorMessage = "PC1 Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string PC1Y { get; set; }

        [BindProperty] [Required(ErrorMessage = "Grade X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string GradeX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Grade Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string GradeY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Length X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string LengthX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Length Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string LengthY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Core Code X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string CoreCodeX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Core Code Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string CoreCodeY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Thickness X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string ThicknessX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Thickness Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string ThicknessY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Barcode X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string BarcodeX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Barcode Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string BarcodeY { get; set; }

        [BindProperty] [Required(ErrorMessage = "Type X cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string TypeX { get; set; }
        [BindProperty] [Required(ErrorMessage = "Type Y cannot be empty!")] [RegularExpression(NumericPattern, ErrorMessage = NumericError)]
        public string TypeY { get; set; }

        [BindProperty]
        public string DefaultPrinter { get; set; } = "No";

        // Audit fields
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }

        public override void BindData() { }

        public override IActionResult OnGet()
        {
            ParseQueryString();

            if (Action == EnumAction.View || Action == EnumAction.Delete)
            {
                LoadDisplayData();
            }
            else if (Action == EnumAction.Edit)
            {
                LoadEditData();
            }

            return Page();
        }

        private void LoadDisplayData()
        {
            DataTable dt = Library.Database.BLL.PrintAlignInit.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                DisplayPrinterName = r["Printer_Name"].ToString();
                DisplayCompanyCode = r["CompanyCode"].ToString();
                DisplayTextFont = r["Text_Font"].ToString();
                DisplayTextFontSize = r["Text_Font_Size"].ToString();
                DisplayBarcodeFont = r["Barcode_Font"].ToString();
                DisplayBarcodeFontSize = r["Barcode_Font_Size"].ToString();
                DisplayWidthX = r["Width_X"].ToString();
                DisplayWidthY = r["Width_Y"].ToString();
                DisplayLengthHeaderX = r["Length_Header_X"].ToString();
                DisplayLengthHeaderY = r["Length_Header_Y"].ToString();
                DisplayPackCodeX = r["Pack_Code_X"].ToString();
                DisplayPackCodeY = r["Pack_Code_Y"].ToString();
                DisplayUnitWeightX = r["Unit_Weight_X"].ToString();
                DisplayUnitWeightY = r["Unit_Weight_Y"].ToString();
                DisplayNumPerPackX = r["Num_Per_Pack_X"].ToString();
                DisplayNumPerPackY = r["Num_Per_Pack_Y"].ToString();
                DisplaySlitLotNoX = r["Slit_Lot_No_X"].ToString();
                DisplaySlitLotNoY = r["Slit_Lot_No_Y"].ToString();
                DisplayPC1X = r["PC1_X"].ToString();
                DisplayPC1Y = r["PC1_Y"].ToString();
                DisplayGradeX = r["Grade_X"].ToString();
                DisplayGradeY = r["Grade_Y"].ToString();
                DisplayLengthX = r["Length_X"].ToString();
                DisplayLengthY = r["Length_Y"].ToString();
                DisplayCoreCodeX = r["Core_Code_X"].ToString();
                DisplayCoreCodeY = r["Core_Code_Y"].ToString();
                DisplayThicknessX = r["Thickness_X"].ToString();
                DisplayThicknessY = r["Thickness_Y"].ToString();
                DisplayBarcodeX = r["Barcode_X"].ToString();
                DisplayBarcodeY = r["Barcode_Y"].ToString();
                DisplayTypeX = r["Type_X"].ToString();
                DisplayTypeY = r["Type_Y"].ToString();
                DisplayDefaultYes = Convert.ToBoolean(r["Default_Printer"]) ? "1" : "0";
                DisplayDefaultNo = Convert.ToBoolean(r["Default_Printer"]) ? "0" : "1";
                CreatedBy = r["CREATED_BY"].ToString();
                CreatedDate = r["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = r["UPDATED_BY"].ToString();
                UpdatedDate = r["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        private void LoadEditData()
        {
            DataTable dt = Library.Database.BLL.PrintAlignInit.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                PrinterName = r["Printer_Name"].ToString();
                CompanyCode = r["CompanyCode"].ToString();
                TextFont = r["Text_Font"].ToString();
                TextFontSize = r["Text_Font_Size"].ToString();
                BarcodeFont = r["Barcode_Font"].ToString();
                BarcodeFontSize = r["Barcode_Font_Size"].ToString();
                WidthX = r["Width_X"].ToString();
                WidthY = r["Width_Y"].ToString();
                LengthHeaderX = r["Length_Header_X"].ToString();
                LengthHeaderY = r["Length_Header_Y"].ToString();
                PackCodeX = r["Pack_Code_X"].ToString();
                PackCodeY = r["Pack_Code_Y"].ToString();
                UnitWeightX = r["Unit_Weight_X"].ToString();
                UnitWeightY = r["Unit_Weight_Y"].ToString();
                NumPerPackX = r["Num_Per_Pack_X"].ToString();
                NumPerPackY = r["Num_Per_Pack_Y"].ToString();
                SlitLotNoX = r["Slit_Lot_No_X"].ToString();
                SlitLotNoY = r["Slit_Lot_No_Y"].ToString();
                PC1X = r["PC1_X"].ToString();
                PC1Y = r["PC1_Y"].ToString();
                GradeX = r["Grade_X"].ToString();
                GradeY = r["Grade_Y"].ToString();
                LengthX = r["Length_X"].ToString();
                LengthY = r["Length_Y"].ToString();
                CoreCodeX = r["Core_Code_X"].ToString();
                CoreCodeY = r["Core_Code_Y"].ToString();
                ThicknessX = r["Thickness_X"].ToString();
                ThicknessY = r["Thickness_Y"].ToString();
                BarcodeX = r["Barcode_X"].ToString();
                BarcodeY = r["Barcode_Y"].ToString();
                TypeX = r["Type_X"].ToString();
                TypeY = r["Type_Y"].ToString();
                string defYes = Convert.ToBoolean(r["Default_Printer"]) ? "1" : "0";
                DefaultPrinter = defYes == "1" ? "Yes" : "No";
                CreatedBy = r["CREATED_BY"].ToString();
                CreatedDate = r["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = r["UPDATED_BY"].ToString();
                UpdatedDate = r["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        public IActionResult OnPostSubmit()
        {
            ParseQueryString();
            if (!ModelState.IsValid)
            {
                if (Action == EnumAction.View || Action == EnumAction.Delete) LoadDisplayData();
                return Page();
            }

            string defaultYes = DefaultPrinter == "Yes" ? "1" : "0";
            string defaultNo = DefaultPrinter == "No" ? "1" : "0";

            string result = "0";
            if (Action == EnumAction.Edit)
                result = Library.Database.BLL.PrintAlignInit.Maint(Key, PrinterName, TextFont, WidthX, TextFontSize, WidthY, BarcodeFont, LengthHeaderX, BarcodeFontSize, LengthHeaderY, PackCodeX, UnitWeightX, PackCodeY, UnitWeightY, NumPerPackX, SlitLotNoX, NumPerPackY, SlitLotNoY, PC1X, GradeX, PC1Y, GradeY, LengthX, CoreCodeX, LengthY, CoreCodeY, ThicknessX, BarcodeX, ThicknessY, BarcodeY, TypeX, TypeY, defaultYes == "1", defaultNo == "1", CompanyCode, ((int)EnumAction.Edit).ToString());
            else if (Action == EnumAction.Add)
                result = Library.Database.BLL.PrintAlignInit.Maint(Key, PrinterName, TextFont, WidthX, TextFontSize, WidthY, BarcodeFont, LengthHeaderX, BarcodeFontSize, LengthHeaderY, PackCodeX, UnitWeightX, PackCodeY, UnitWeightY, NumPerPackX, SlitLotNoX, NumPerPackY, SlitLotNoY, PC1X, GradeX, PC1Y, GradeY, LengthX, CoreCodeX, LengthY, CoreCodeY, ThicknessX, BarcodeX, ThicknessY, BarcodeY, TypeX, TypeY, defaultYes == "1", defaultNo == "1", CompanyCode, ((int)EnumAction.Add).ToString());

            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Operation failed" : result.Replace("'", "\\'")) + "');");
            if (Action == EnumAction.Edit) LoadEditData();
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            ParseQueryString();
            LoadDisplayData();

            string result = Library.Database.BLL.PrintAlignInit.Maint(Key, DisplayPrinterName, DisplayTextFont, DisplayWidthX, DisplayTextFontSize, DisplayWidthY, DisplayBarcodeFont, DisplayLengthHeaderX, DisplayBarcodeFontSize, DisplayLengthHeaderY, DisplayPackCodeX, DisplayUnitWeightX, DisplayPackCodeY, DisplayUnitWeightY, DisplayNumPerPackX, DisplaySlitLotNoX, DisplayNumPerPackY, DisplaySlitLotNoY, DisplayPC1X, DisplayGradeX, DisplayPC1Y, DisplayGradeY, DisplayLengthX, DisplayCoreCodeX, DisplayLengthY, DisplayCoreCodeY, DisplayThicknessX, DisplayBarcodeX, DisplayThicknessY, DisplayBarcodeY, DisplayTypeX, DisplayTypeY, DisplayDefaultYes == "1", DisplayDefaultNo == "1", DisplayCompanyCode, ((int)EnumAction.Delete).ToString());
            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Delete failed" : result.Replace("'", "\\'")) + "');");
            return Page();
        }

        public IActionResult OnPostReset()
        {
            return Redirect(Request.Path + Request.QueryString);
        }
    }
}
