using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PC1Model : BasePageModel
    {
        public MM_PC1Model()
        {
            SetupKey = "MM_PC1";
            DefaultSort = "ID_MM_PC1";
            SortDirection = "0";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = true;
            RecordTypeColumn = 2;
        }

        public override void BindData()
        {
            var list = Library.Database.BLL.PC1.List(
                "PV_MM_PC1",
                "ID_MM_PC1",
                SearchField, SearchValue, SortField,
                int.Parse(SortDirection), PageNo,
                ShowDeleted ? 1 : 0);

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
