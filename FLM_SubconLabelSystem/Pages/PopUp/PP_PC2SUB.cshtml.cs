using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.PopUp
{
    public class PP_PC2SUBModel : BasePageModel
    {
        private Library.Database.ListCollection _list;

        public string Pc2mother { get; set; } = string.Empty;
        public string Lblpc2mother { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;

        public PP_PC2SUBModel()
        {
            SetupKey = "PP_PC2SUB";
            DefaultSort = "ID_MM_PC2";
            SortDirection = "0";
            GridViewCheckColumn = false;
            PrintControl = false;
            DeleteControl = false;
            GridViewRadioColumn = false;
            ViewHistoryControl = false;
            RecordTypeColumn = -1;
        }

        public override void BindData()
        {
            Pc2mother = Item1;
            Lblpc2mother = Item2;

            SubTitle = (Item1 == "pc2mother") ? "PC2 Mother" : "PC2 Child";

            if (!string.IsNullOrEmpty(SearchField))
            {
                _list = Library.Database.BLL.PC1.List("PV_MM_PC2SUB_POPUP", "ID_MM_PC2", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
                GridData = _list.Data;
                TotalRecords = _list.TotalRow;
            }
        }
    }
}
