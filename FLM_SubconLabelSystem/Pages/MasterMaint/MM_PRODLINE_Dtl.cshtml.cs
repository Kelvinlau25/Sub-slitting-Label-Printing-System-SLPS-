using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PRODLINE_DtlModel : BasePageModel
    {
        public MM_PRODLINE_DtlModel()
        {
            SetupKey = "MM_PRODLINE";
            FunctionControl = false;
        }

        // Display fields
        public string DisplayProdLine { get; set; } = string.Empty;
        public string DisplayDescription { get; set; } = string.Empty;

        // Form fields
        [BindProperty]
        [Required(ErrorMessage = "Product Line cannot be empty!")]
        [StringLength(15)]
        public string ProdLine { get; set; }

        [BindProperty]
        [StringLength(30)]
        public string Description { get; set; }

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
            DataTable dt = Library.Database.BLL.MM_PRODLINE.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DisplayProdLine = dt.Rows[0]["PRODLINE_NO"].ToString();
                DisplayDescription = dt.Rows[0]["DESCRIPTION"].ToString();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        private void LoadEditData()
        {
            DataTable dt = Library.Database.BLL.MM_PRODLINE.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                ProdLine = dt.Rows[0]["PRODLINE_NO"].ToString();
                Description = dt.Rows[0]["DESCRIPTION"].ToString();
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

            string result = "0";
            if (Action == EnumAction.Edit)
                result = Library.Database.BLL.MM_PRODLINE.Maint(Key, ProdLine, Description ?? "", (int)EnumAction.Edit);
            else if (Action == EnumAction.Add)
                result = Library.Database.BLL.MM_PRODLINE.Maint(Key, ProdLine, Description ?? "", (int)EnumAction.Add);

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

            string result = Library.Database.BLL.MM_PRODLINE.Maint(Key, DisplayProdLine, DisplayDescription, (int)EnumAction.Delete);
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
