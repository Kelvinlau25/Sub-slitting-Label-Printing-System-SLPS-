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
            string companyCode = SessionGet("ESSION") ?? "";
            string lotID = Item1 ?? "";

            Library.Database.ListCollection list;

            if (ulevel == "3")
            {
                if (!string.IsNullOrEmpty(lotID))
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "VIEW_LOT_SLITTING_SERIES_func('" + companyCode + "','" + lotID + "')",
                        "ID_LOT_SLITTING_SERIES",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
                else
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "VIEW_LOT_SLITTING_SERIES_func('" + companyCode + "','')",
                        "ID_LOT_SLITTING_SERIES",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
            }
            else if (ulevel == "2")
            {
                if (!string.IsNullOrEmpty(lotID))
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "VIEW_LOT_SLITTING_SERIES_func('" + companyCode + "','" + lotID + "')",
                        "ID_LOT_SLITTING_SERIES",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
                else
                {
                    list = Library.Database.BLL.LotSlitting.List(
                        "VIEW_LOT_SLITTING_SERIES_func('" + companyCode + "','')",
                        "ID_LOT_SLITTING_SERIES",
                        SearchField, SearchValue, SortField,
                        int.Parse(SortDirection), PageNo,
                        ShowDeleted ? 1 : 0);
                }
            }
            else
            {
                list = Library.Database.BLL.LotSlitting.List(
                    "PV_VIEW_LOT_SLITTING_SERIES",
                    "ID_LOT_SLITTING_SERIES",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
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
            return Page();
        }

        public IActionResult OnPostExport()
        {
            ParseQueryString();

            string saved = SessionGet("LabelPlan_Selected") ?? "";
            var selected = new List<string>(saved.Split(',', StringSplitOptions.RemoveEmptyEntries));

            // Rebind to get data for export
            BindData();

            if (GridData == null || GridData.Rows.Count == 0)
                return Page();

            var sb = new StringBuilder();

            // CSV header
            sb.AppendLine("Ref No,Company Code,Production Line,PC1 Mother,PC2 Mother,Unit Weight (Before)," +
                          "Lot No,PC1 Customer,PC2 Customer,Unit Weight (After),Slit Lot No,Record Type,Print Status");

            foreach (DataRow row in GridData.Rows)
            {
                string slitLotNo = row["SLITLOTNO"].ToString();
                if (selected.Count > 0 && !selected.Contains(slitLotNo))
                    continue;

                sb.AppendLine(
                    "\"" + row["REFNO"] + "\"," +
                    "\"" + row["COMPANYCODE"] + "\"," +
                    "\"" + row["PRODLINE"] + "\"," +
                    "\"" + row["PC1_MOTHER"] + "\"," +
                    "\"" + row["PC2_MOTHER"] + "\"," +
                    "\"" + row["UNITWEIGHT_BEFORE"] + "\"," +
                    "\"" + row["LOTNO"] + "\"," +
                    "\"" + row["PC1_CUSTOMER"] + "\"," +
                    "\"" + row["PC2_CUSTOMER"] + "\"," +
                    "\"" + row["UNITWEIGHT_AFTER"] + "\"," +
                    "\"" + slitLotNo + "\"," +
                    "\"" + row["RECTYPE"] + "\"," +
                    "\"" + row["PRINTSTATUS"] + "\"");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "LabelPlan_Export.csv");
        }
    }
}
