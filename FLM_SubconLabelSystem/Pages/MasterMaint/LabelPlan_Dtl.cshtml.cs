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
                DisplayCompanyCode = r["COMPANYCODE"].ToString();
                DisplayPlanYearMonth = r["PLAN_YEAR_MONTH"].ToString();
                DisplayProdLine = r["PRODLINE_NO"].ToString();
                DisplayPC1Mother = r["PC1_MOTHER"].ToString();
                DisplayPC2Mother = r["PC2_MOTHER"].ToString();
                DisplayUnitWeightMother = r["M_UNITWEIGHT"].ToString();
                DisplayPC1Customer = r["PC1_CUST"].ToString();
                DisplayPC2Customer = r["PC2_CUST"].ToString();
                DisplayUnitWeightCustomer = r["C_UNITWEIGHT"].ToString();
                DisplayLotNo = r["LOTNO"].ToString();
                DisplayLotSlitNo = r["SLIT_LOT_NO"].ToString();
                DisplayStatus = r["STATUS"].ToString();
                CreatedBy = r["CREATED_BY"].ToString();
                CreatedDate = r["CREATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["CREATED_DATE"]) : (DateTime?)null;
                UpdatedBy = r["UPDATED_BY"].ToString();
                UpdatedDate = r["UPDATED_DATE"] != DBNull.Value ? Convert.ToDateTime(r["UPDATED_DATE"]) : (DateTime?)null;
            }
        }

        public IActionResult OnPostDelete()
        {
            ParseQueryString();
            LoadDisplayData();

            string result = Library.Database.BLL.LotSlitting.Maint(Key, DisplayLotNo, "", "", "", "", "", "", "", "", "", "", "", ((int)EnumAction.Delete).ToString());
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
