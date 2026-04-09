using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PRODLINEModel : BasePageModel
    {
        public MM_PRODLINEModel()
        {
            SetupKey = "MM_PRODLINE";
            DefaultSort = "ID_MM_PRODLINE";
            SortDirection = "0";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = true;
            RecordTypeColumn = 6;
        }

        public override void BindData()
        {
            var list = Library.Database.BLL.MM_PRODLINE.List(
                "PV_MM_PRODLINE",
                "ID_MM_PRODLINE",
                SearchField, SearchValue, SortField,
                int.Parse(SortDirection), PageNo,
                ShowDeleted ? 1 : 0);

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
