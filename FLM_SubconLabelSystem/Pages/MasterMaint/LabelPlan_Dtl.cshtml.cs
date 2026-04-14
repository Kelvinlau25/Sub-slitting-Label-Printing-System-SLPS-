using System;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class LabelPlan_DtlModel : BasePageModel
    {
        public LabelPlan_DtlModel()
        {
            SetupKey = "VIEW_LOT_SLITTING_SERIES";
            FunctionControl = false;
            AddControl = false;
        }

        // Display fields
        public string DisplayCompanyCode { get; set; } = string.Empty;
        public string DisplayPlanYearMonth { get; set; } = string.Empty;
        public string DisplayProdLine { get; set; } = string.Empty;
        public string DisplayPC1Mother { get; set; } = string.Empty;
        public string DisplayPC2Mother { get; set; } = string.Empty;
        public string DisplayUnitWeightMother { get; set; } = string.Empty;
        public string DisplayPC1Customer { get; set; } = string.Empty;
        public string DisplayPC2Customer { get; set; } = string.Empty;
        public string DisplayUnitWeightCustomer { get; set; } = string.Empty;
        public string DisplayLotNo { get; set; } = string.Empty;
        public string DisplayLotSlitNo { get; set; } = string.Empty;
        public string DisplayStatus { get; set; } = string.Empty;

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

            return Page();
        }

        private void LoadDisplayData()
        {
            DataTable dt = Library.Database.BLL.LotSlitting.GetData(Key);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                DisplayCompanyCode = r["COMPANYCODE"]?.ToString() ?? string.Empty;
                DisplayPlanYearMonth = r["PLAN_YEAR_MONTH"]?.ToString() ?? string.Empty;
                DisplayProdLine = r["PRODLINE_NO"]?.ToString() ?? string.Empty;
                DisplayPC1Mother = r["PC1_MOTHER"]?.ToString() ?? string.Empty;
                DisplayPC2Mother = r["PC2_MOTHER"]?.ToString() ?? string.Empty;
                DisplayUnitWeightMother = r["M_UNITWEIGHT"]?.ToString() ?? string.Empty;
                DisplayPC1Customer = r["PC1_CUST"]?.ToString() ?? string.Empty;
                DisplayPC2Customer = r["PC2_CUST"]?.ToString() ?? string.Empty;
                DisplayUnitWeightCustomer = r["C_UNITWEIGHT"]?.ToString() ?? string.Empty;
                DisplayLotNo = r["LOTNO"]?.ToString() ?? string.Empty;
                DisplayLotSlitNo = r["SLIT_LOT_NO"]?.ToString() ?? string.Empty;
                DisplayStatus = r["STATUS"]?.ToString() ?? string.Empty;
                CreatedBy = r["CREATED_BY"]?.ToString() ?? string.Empty;
                CreatedDate = r["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = r["UPDATED_BY"]?.ToString() ?? string.Empty;
                UpdatedDate = r["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        public IActionResult OnPostDelete()
        {
            ParseQueryString();
            LoadDisplayData();

            string userId = HttpContext.Session.GetString("gstrUserID") ?? HttpContext.Session.GetString("USERID") ?? "";
            string userIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string result = Library.Database.BLL.LotSlitting.Maint(
                Key,
                DisplayLotNo,
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ((int)EnumAction.Delete).ToString(),
                userId,
                userIp
            );

            if (result == "1")
                return Redirect(GetUrl(EnumAction.None));

            RegisterStartupScript("alert('" + (result == "0" ? "Delete failed" : result.Replace("'", "\\'")) + "');");
            LoadDisplayData();
            return Page();
        }

        public IActionResult OnPostReset()
        {
            return Redirect(Request.Path + Request.QueryString);
        }
    }
}