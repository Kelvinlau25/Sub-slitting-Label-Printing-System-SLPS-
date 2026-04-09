using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PC2_DtlModel : BasePageModel
    {
        public MM_PC2_DtlModel()
        {
            SetupKey = "MM_PC2";
            FunctionControl = false;
        }

        // Display fields
        public string DisplayPC2 { get; set; } = string.Empty;
        public string DisplayThickness { get; set; } = string.Empty;
        public string DisplayType { get; set; } = string.Empty;
        public string DisplayWidth { get; set; } = string.Empty;
        public string DisplayLength { get; set; } = string.Empty;
        public string DisplayGrade { get; set; } = string.Empty;
        public string DisplayPackCode { get; set; } = string.Empty;
        public string DisplayCoreCode { get; set; } = string.Empty;
        public string DisplayUnitWeight { get; set; } = string.Empty;
        public string DisplayRemarks { get; set; } = string.Empty;

        // Form fields
        [BindProperty]
        [Required(ErrorMessage = "Thickness cannot be empty!")]
        public string Thickness { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Type cannot be empty!")]
        public new string Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Width cannot be empty!")]
        public string Width { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Length cannot be empty!")]
        public string Length { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Grade cannot be empty!")]
        public string Grade { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Pack Code cannot be empty!")]
        public string PackCode { get; set; }

        [BindProperty]
        public string CoreCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Unit Weight cannot be empty!")]
        [RegularExpression(@"^[0-9]\d{0,9}(\.\d{1,3})?%?$", ErrorMessage = "Invalid Unit Weight format!")]
        public string UnitWeight { get; set; }

        [BindProperty]
        public string Remarks { get; set; }

        // Auto-generated PC2
        public string GeneratedPC2
        {
            get
            {
                string t = Thickness ?? "";
                string tp = Type ?? "";
                string w = Width ?? "";
                string l = Length ?? "";
                string g = Grade ?? "";
                string p = PackCode ?? "";
                string c = CoreCode ?? "";
                return t + "-" + tp + "-" + w + "x" + l + "-" + g + p + c;
            }
        }

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
            DataTable dt = Library.Database.BLL.PC2.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DisplayPC2 = dt.Rows[0]["PC2"].ToString();
                DisplayThickness = dt.Rows[0]["THICKNESS"].ToString();
                DisplayType = dt.Rows[0]["TYPE"].ToString();
                DisplayWidth = dt.Rows[0]["WIDTH"].ToString();
                DisplayLength = dt.Rows[0]["LENGTH"].ToString();
                DisplayGrade = dt.Rows[0]["GRADE"].ToString();
                DisplayPackCode = dt.Rows[0]["PACKCODE"].ToString();
                DisplayCoreCode = dt.Rows[0]["CORECODE"].ToString();
                DisplayUnitWeight = dt.Rows[0]["UNITWEIGHT"].ToString();
                DisplayRemarks = dt.Rows[0]["REMARKS"].ToString();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        private void LoadEditData()
        {
            DataTable dt = Library.Database.BLL.PC2.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                Thickness = dt.Rows[0]["THICKNESS"].ToString();
                Type = dt.Rows[0]["TYPE"].ToString();
                Width = dt.Rows[0]["WIDTH"].ToString();
                Length = dt.Rows[0]["LENGTH"].ToString();
                Grade = dt.Rows[0]["GRADE"].ToString();
                PackCode = dt.Rows[0]["PACKCODE"].ToString();
                CoreCode = dt.Rows[0]["CORECODE"].ToString();
                UnitWeight = dt.Rows[0]["UNITWEIGHT"].ToString();
                Remarks = dt.Rows[0]["REMARKS"].ToString();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
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

            string companyCode = SessionGet("COMPANYCODE") ?? "";
            string pc2 = GeneratedPC2;

            string result = "0";
            if (Action == EnumAction.Edit)
                result = Library.Database.BLL.PC2.Maint(Key, companyCode, pc2, Thickness.ToUpper(), Type.ToUpper(), Width.ToUpper(), Length.ToUpper(), PackCode.ToUpper(), Grade.ToUpper(), (CoreCode ?? "").ToUpper(), UnitWeight, "", Remarks ?? "", (int)EnumAction.Edit);
            else if (Action == EnumAction.Add)
                result = Library.Database.BLL.PC2.Maint(Key, companyCode, pc2, Thickness.ToUpper(), Type.ToUpper(), Width.ToUpper(), Length.ToUpper(), PackCode.ToUpper(), Grade.ToUpper(), (CoreCode ?? "").ToUpper(), UnitWeight, "", Remarks ?? "", (int)EnumAction.Add);

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

            string companyCode = SessionGet("COMPANYCODE") ?? "";
            string result = Library.Database.BLL.PC2.Maint(Key, companyCode, DisplayPC2, DisplayThickness, DisplayType, DisplayWidth, DisplayLength, DisplayPackCode, DisplayGrade, DisplayCoreCode, DisplayUnitWeight, "", DisplayRemarks, (int)EnumAction.Delete);
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
