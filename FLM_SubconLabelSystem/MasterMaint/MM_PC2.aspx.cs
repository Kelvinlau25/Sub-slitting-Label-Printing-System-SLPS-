using System;
using System.Configuration;
using Library.Root.Control;

public partial class MasterMaint_MM_PC2 : Control.Base
{
    private Library.Database.ListCollection _list;
    private readonly string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_MM_PC2()
    {
        SetupKey = "MM_PC2";
        DefaultSort = "ID_MM_PC2";
        SortDirection = "0";
        GridViewCheckColumn = false;
        PrintControl = false;
        DeleteControl = true;
        GridViewRadioColumn = false;
        ViewHistoryControl = true;
        RecordTypeColumn = 6;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        GridView = grdResult;

        DeleteControl = Convert.ToInt32(Session["ULEVEL"]) != 3;
    }

    public override void BindData()
    {
        _list = Library.Database.BLL.PC2.List(
            "PV_MM_PC2", "ID_MM_PC2",
            SearchField, SearchValue,
            SortField, Convert.ToInt32(SortDirection),
            PageNo, Convert.ToInt32(ShowDeleted));

        grdResult.DataSource = _list.Data;
        grdResult.DataBind();

        UCFooter.TotalRecords = _list.TotalRow;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
}