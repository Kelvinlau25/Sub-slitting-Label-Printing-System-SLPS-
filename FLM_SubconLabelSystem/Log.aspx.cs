using System;

public partial class Log : Control.LogBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void BindData()
    {
        Library.Database.ListCollection _list;
        _list = Library.Database.BLL.Log.GetLogList(base.LogTable, base.Key, base.PageNo);

        grdResult.DataSource = _list.Data;
        grdResult.DataBind();

        UCFooter.TotalRecords = _list.TotalRow;
    }
}