using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_COMPANYModel : BasePageModel
    {
        public MM_COMPANYModel()
        {
            SetupKey = "MM_COMPANY";
            DefaultSort = "ID_MM_COMPANY";
            SortDirection = "0";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = true;
            RecordTypeColumn = 6;
        }

        public override void BindData()
        {
            string ulevel = SessionGet("ULEVEL") ?? "";
            string companyCode = SessionGet("ABORESSION") ?? "";

            Library.Database.ListCollection list;

            if (ulevel == "3" || ulevel == "2")
            {
                DeleteControl = true;
                list = Library.Database.BLL.Company.List(
                    "MM_COMPANY_func('" + companyCode + "')",
                    "ID_MM_COMPANY",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }
            else
            {
                DeleteControl = false;
                list = Library.Database.BLL.Company.List(
                    "PV_MM_COMPANY",
                    "ID_MM_COMPANY",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
