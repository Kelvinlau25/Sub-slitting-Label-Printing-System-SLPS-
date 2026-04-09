using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_USERModel : BasePageModel
    {
        public MM_USERModel()
        {
            SetupKey = "MM_USER";
            DefaultSort = "COMPANYCODE, ULEVEL, USERID";
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
                list = Library.Database.BLL.user.List(
                    "MM_USER_func('" + companyCode + "')",
                    "ID_MM_USERID",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }
            else
            {
                DeleteControl = false;
                list = Library.Database.BLL.user.List(
                    "PV_MM_USER",
                    "ID_MM_USERID",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
