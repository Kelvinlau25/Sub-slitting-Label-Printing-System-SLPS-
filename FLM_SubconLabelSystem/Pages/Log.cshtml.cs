using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages
{
    public class LogModel : LogBasePageModel
    {
        public DataTable GridData { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalRecords /
                Library.Root.Other.BusinessLogicBase.MaxQuantityPerPage);

        protected override void BindData()
        {
            Library.Database.ListCollection _list;
            _list = Library.Database.BLL.Log.GetLogList(base.LogTable, base.Key, base.PageNo);

            GridData = _list.Data;
            TotalRecords = _list.TotalRow;
        }
    }
}
