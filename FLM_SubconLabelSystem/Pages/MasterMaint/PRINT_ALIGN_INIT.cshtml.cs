using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class PRINT_ALIGN_INITModel : BasePageModel
    {
        public PRINT_ALIGN_INITModel()
        {
            SetupKey = "PRINT_ALIGN_INIT";
            DefaultSort = "ID_Print_Align_Init";
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

            if (ulevel == "3")
            {
                list = Library.Database.BLL.PrintAlignInit.List(
                    "Print_Align_Init_func('" + companyCode + "')",
                    "ID_Print_Align_Init",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }
            else
            {
                list = Library.Database.BLL.PC1.List(
                    "PV_PRINT_ALIGN_INIT",
                    "ID_Print_Align_Init",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
