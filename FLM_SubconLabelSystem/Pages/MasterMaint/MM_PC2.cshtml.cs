using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class MM_PC2Model : BasePageModel
    {
        public MM_PC2Model()
        {
            SetupKey = "MM_PC2";
            DefaultSort = "ID_MM_PC2";
            SortDirection = "0";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = true;
            RecordTypeColumn = 6;
        }

        public override void BindData()
        {
            var list = Library.Database.BLL.PC2.List(
                "PV_MM_PC2",
                "ID_MM_PC2",
                SearchField, SearchValue, SortField,
                int.Parse(SortDirection), PageNo,
                ShowDeleted ? 1 : 0);

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }
    }
}
