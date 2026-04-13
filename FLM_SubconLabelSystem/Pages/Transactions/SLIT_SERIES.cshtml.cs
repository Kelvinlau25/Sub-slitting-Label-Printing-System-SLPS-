using System;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class SLIT_SERIESModel : BasePageModel
    {
        public SLIT_SERIESModel()
        {
            SetupKey = "PC2_LOTNO";
            DefaultSort = "ID_PC2_LOTNO";
            SortDirection = "0";
            GridViewCheckColumn = false;
            PrintControl = false;
            DeleteControl = true;
            GridViewRadioColumn = false;
            ViewHistoryControl = false;
            RecordTypeColumn = 11;
        }

        public override void BindData()
        {
            string companyCode = SessionGet("COMPANYCODE") ?? string.Empty;
            int userLevel = Convert.ToInt32(SessionGet("ULEVEL") ?? "0");
            int showDeleted = ShowDeleted ? 1 : 0;

            Library.Database.ListCollection list;

            if (userLevel == 3)
            {
                list = Library.Database.BLL.SlitSeries.List("|" + companyCode, "ID_PC2_LOTNO",
                    SearchField, SearchValue, "UPDATED_DATE", 1, PageNo, showDeleted);
            }
            else
            {
                list = Library.Database.BLL.SlitSeries.List("PV_PC2_LOTNO", "ID_PC2_LOTNO",
                    SearchField, SearchValue, "UPDATED_DATE", 1, PageNo, showDeleted);
            }

            if (userLevel == 2)
                DeleteControl = false;

            DataTable dt = list.Data;
            if (!dt.Columns.Contains("Create_URL"))
                dt.Columns.Add("Create_URL", typeof(string));

            DataRow[] createRows = dt.Select("STATUS = 'Create'");
            foreach (DataRow row in createRows)
            {
                row["Create_URL"] = "SLIT_SERIES?action=create&ID=" + row["ID_PC2_LOTNO"].ToString().Trim();
            }

            GridData = dt;
            TotalRecords = list.TotalRow;
        }

        public override IActionResult OnGet()
        {
            ParseQueryString();

            // Handle create action
            if (Request.Query.ContainsKey("action") && Request.Query["action"].ToString().ToLower() == "create"
                && Request.Query.ContainsKey("ID"))
            {
                return CreateLotSlitting(Request.Query["ID"].ToString().Trim());
            }

            if (FunctionControl && !string.IsNullOrEmpty(DefaultSort) && string.IsNullOrEmpty(SortField))
                return Redirect(GenerateList);

            BindData();
            PopulateSearchViewData();
            return Page();
        }

        private IActionResult CreateLotSlitting(string lot)
        {
            string companyCode = SessionGet("COMPANYCODE") ?? string.Empty;
            int userLevel = Convert.ToInt32(SessionGet("ULEVEL") ?? "0");
            string userId = SessionGet("USERID") ?? string.Empty;

            DataTable dtData = Library.Database.BLL.SlitSeries.GetData(lot);

            if (dtData.Rows[0]["LOT_STATUS"].ToString() == "Create")
            {
                string[] slitParts = dtData.Rows[0]["TYPE_OF_SLIT"].ToString().Split(',');
                int vTypeOfSlit = Convert.ToInt32(slitParts[0]);
                int vMatrixPos = Convert.ToInt32(slitParts[1]);
                int vMatrixInc = Convert.ToInt32(slitParts[2]);
                string vLotNo = dtData.Rows[0]["LOTNO"].ToString();
                int vNoOfSlit = Convert.ToInt32(dtData.Rows[0]["NO_OF_SLIT"]);
                int vIdLot = Convert.ToInt32(lot);
                string vCompanyCode = dtData.Rows[0]["COMPANYTO"].ToString();
                SessionSet("LOTNO", vLotNo);

                string upd_stat = "";
                if (userLevel == 1)
                    upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(vCompanyCode, vIdLot, vTypeOfSlit, vMatrixPos, vMatrixInc, vLotNo, vNoOfSlit, userId);
                else if (userLevel == 2 && companyCode == vCompanyCode)
                    upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(companyCode, vIdLot, vTypeOfSlit, vMatrixPos, vMatrixInc, vLotNo, vNoOfSlit, userId);
                else if (userLevel == 3)
                    upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(companyCode, vIdLot, vTypeOfSlit, vMatrixPos, vMatrixInc, vLotNo, vNoOfSlit, userId);

                if (!string.IsNullOrEmpty(upd_stat) && upd_stat == "1")
                    return Redirect("~/MasterMaint/LabelPlan?itm1=" + vLotNo);
            }

            // Fallback - show list
            BindData();
            PopulateSearchViewData();
            StartupScript = "alert('Creating Slitting records is not successful or Lot Slitting already completed.');";
            return Page();
        }
    }
}
