using System;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class SSR_SEARCHModel : BasePageModel
    {
        public bool ShowAddButton { get; set; } = true;

        public SSR_SEARCHModel()
        {
            SetupKey = "PV_SUBSLIT_REQUEST_LIST";
            DefaultSort = "UPDATED_DATE";
            SortDirection = "1";
            GridViewCheckColumn = false;
            PrintControl = false;
            DeleteControl = true;
            GridViewRadioColumn = false;
            ViewHistoryControl = false;
            RecordTypeColumn = 7;
        }

        public override void BindData()
        {
            string companyCode = SessionGet("COMPANYCODE") ?? string.Empty;
            int showDeleted = ShowDeleted ? 1 : 0;
            string ulevel = SessionGet("ULEVEL") ?? "";

            if (ulevel == "3")
            {
                DeleteControl = false;
                ShowAddButton = false;

                var list = Library.Database.BLL.user.List(
                    "MM_SUBSLIT_func('" + companyCode + "')",
                    "UPDATED_DATE",
                    SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, showDeleted);

                GridData = list.Data;
                TotalRecords = list.TotalRow;
            }
            else
            {
                DeleteControl = true;

                var list = Library.Database.BLL.user.List(
                    "PV_SUBSLIT_REQUEST_LIST",
                    "UPDATED_DATE",
                    SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, showDeleted);

                GridData = list.Data;
                TotalRecords = list.TotalRow;
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

        public IActionResult OnGetSelect(string refno, string idssr, string reqStatus)
        {
            ParseQueryString();
            string ulevel = SessionGet("ULEVEL") ?? "";

            if (ulevel == "3" && reqStatus != "Submitted" && reqStatus != "Cancel")
            {
                BindData();
                PopulateSearchViewData();
                return Page();
            }

            if (reqStatus == "New")
            {
                return Redirect("~/Transactions/SUBSLIT_REQ_?itm1=" + refno + "&itm2= " + idssr);
            }
            else
            {
                return Redirect("~/Transactions/SSR_SEARCH_Dtl?itm1=" + refno + "&itm2= " + idssr);
            }
        }
    }
}
