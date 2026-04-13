using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PC1_DtlModel : BasePageModel
    {
        public MM_PC1_DtlModel()
        {
            SetupKey = "MM_PC1";
            FunctionControl = false;
        }

        // Display fields
        public string DisplayPC1 { get; set; } = string.Empty;
        public string DisplayDescription { get; set; } = string.Empty;

        // Form fields
        [BindProperty]
        [Required(ErrorMessage = "PC1 cannot be empty!")]
        [StringLength(6)]
        public string PC1 { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Description cannot be empty!")]
        [StringLength(50)]
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
            DataTable dt = Library.Database.BLL.PC1.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DisplayPC1 = dt.Rows[0]["PC1"].ToString();
                DisplayDescription = dt.Rows[0]["DESCRIPTION"].ToString().ToUpper();
                CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
                CreatedDate = dt.Rows[0]["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
                UpdatedDate = dt.Rows[0]["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        private void LoadEditData()
        {
            DataTable dt = Library.Database.BLL.PC1.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                PC1 = dt.Rows[0]["PC1"].ToString();
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

            string userId = HttpContext.Session.GetString("gstrUserID") ?? HttpContext.Session.GetString("USERID") ?? "";
            string userIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string result = "0";
            if (Action == EnumAction.Edit)
                result = Library.Database.BLL.PC1.Maint(Key, PC1, "0", Description, ((int)EnumAction.Edit).ToString(), userId, userIp);
            else if (Action == EnumAction.Add)
                result = Library.Database.BLL.PC1.Maint("0", PC1, "0", Description, ((int)EnumAction.Add).ToString(), userId, userIp);

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

            string userId = HttpContext.Session.GetString("gstrUserID") ?? HttpContext.Session.GetString("USERID") ?? "";
            string userIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string result = Library.Database.BLL.PC1.Maint(Key, DisplayPC1, "0", DisplayDescription, ((int)EnumAction.Delete).ToString(), userId, userIp);
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
