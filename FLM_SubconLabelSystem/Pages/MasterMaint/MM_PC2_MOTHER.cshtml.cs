using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PC2_MOTHERModel : BasePageModel
    {
        public MM_PC2_MOTHERModel()
        {
            SetupKey = "MM_PC2_MOTHER";
            DefaultSort = "ID_MM_PC2_MOTHER";
            SortDirection = "0";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = true;
            RecordTypeColumn = 7;
        }

        public override void BindData()
        {
            var list = Library.Database.BLL.PC2Mother.List(
                "PV_MM_PC2_MOTHER",
                "ID_MM_PC2_MOTHER",
                SearchField, SearchValue, SortField,
                int.Parse(SortDirection), PageNo,
                ShowDeleted ? 1 : 0);

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
