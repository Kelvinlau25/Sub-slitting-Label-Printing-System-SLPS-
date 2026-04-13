using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class LabelPlanModel : BasePageModel
    {
        public List<string> SelectedSlitLotNos { get; set; } = new List<string>();

        public LabelPlanModel()
        {
            SetupKey = "VIEW_LOT_SLITTING_SERIES";
            DefaultSort = "CREATED_DATE";
            SortDirection = "1";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = false;
            AddControl = false;
            RecordTypeColumn = 11;
        }

        public override void BindData()
        {
            string ulevel = SessionGet("ULEVEL") ?? "";
            string companyCode = SessionGet("COMPANYCODE") ?? "";
            string lotID = Item1 ?? "";
            DeleteControl = ulevel != "2";

            if (string.IsNullOrEmpty(SortField))
                SortField = DefaultSort;

            Library.Database.ListCollection list;

            if (ulevel == "3")
            {
                if (!string.IsNullOrEmpty(lotID))
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "LabelPlan_ReDir_func('" + companyCode + "', '" + lotID + "')",
                        "CREATED_DATE",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
                else
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "LabelPlan_func('" + companyCode + "')",
                        "CREATED_DATE",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lotID))
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "LabelPlan_ReDir_All_func('" + lotID + "')",
                        "CREATED_DATE",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
                else
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "LabelPlan_All_func('" + companyCode + "')",
                        "CREATED_DATE",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
            }

            GridData = list.Data;
            TotalRecords = list.TotalRow;

            // Load selected checkboxes from session
            string saved = SessionGet("LabelPlan_Selected");
            if (!string.IsNullOrEmpty(saved))
            {
                SelectedSlitLotNos = new List<string>(saved.Split(',', StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public override IActionResult OnGet()
        {
            ParseQueryString();

            if (FunctionControl && !string.IsNullOrEmpty(DefaultSort) && string.IsNullOrEmpty(SortField))
                return Redirect(GenerateList);

            BindData();
            PopulateSearchViewData();
            return Page();
        }

        public IActionResult OnPostToggleCheckbox(string slitLotNo, bool isChecked)
        {
            ParseQueryString();

            string saved = SessionGet("LabelPlan_Selected") ?? "";
            var selected = new List<string>(saved.Split(',', StringSplitOptions.RemoveEmptyEntries));

            if (isChecked && !selected.Contains(slitLotNo))
            {
                selected.Add(slitLotNo);
            }
            else if (!isChecked && selected.Contains(slitLotNo))
            {
                selected.Remove(slitLotNo);
            }

            SessionSet("LabelPlan_Selected", string.Join(",", selected));
            SelectedSlitLotNos = selected;

            BindData();
            PopulateSearchViewData();
            return Page();
        }

        public IActionResult OnPostExport()
        {
            ParseQueryString();

            string saved = SessionGet("LabelPlan_Selected") ?? "";
            var selected = new List<string>(saved.Split(',', StringSplitOptions.RemoveEmptyEntries));

            string userId = SessionGet("USERID") ?? "";
            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            // Mark selected records as PRINT_SEL = 1 (same as original Export.aspx)
            foreach (string slitLotNo in selected)
            {
                Library.Database.BLL.LotSlitting.UpdPrintSel(slitLotNo, true, "Update", userId, clientIp);
            }

            // Query all columns from VIEW_LOT_SLITTING_SERIES where PRINT_SEL = 1
            DataTable dt = Library.Database.BLL.LotSlitting.GetExportData();

            if (dt == null || dt.Rows.Count == 0)
            {
                BindData();
                PopulateSearchViewData();
                return Page();
            }

            var sb = new StringBuilder();

            // CSV header - all column names from the view
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append(column.ColumnName + ",");
            }
            sb.AppendLine();

            // CSV data rows
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    string val = row[column.ColumnName].ToString().Replace(",", ";");
                    // Append "A" suffix to LOTNO and SLIT_LOT_NO (same as original)
                    if (column.ColumnName == "LOTNO" || column.ColumnName == "SLIT_LOT_NO")
                    {
                        sb.Append(val + "A,");
                    }
                    else
                    {
                        sb.Append(val + ",");
                    }
                }
                sb.AppendLine();
            }

            // Reset PRINT_SEL (Init) for selected records
            foreach (string slitLotNo in selected)
            {
                Library.Database.BLL.LotSlitting.UpdPrintSel(slitLotNo, true, "Init", userId, clientIp);
            }

            // Clear selection after export
            SessionSet("LabelPlan_Selected", "");

            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "application/text", "printlabel.csv");
        }
    }
}
