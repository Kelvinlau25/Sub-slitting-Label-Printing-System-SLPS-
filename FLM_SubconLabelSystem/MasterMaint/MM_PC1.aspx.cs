using System;
using System.Configuration;
using Library.Root.Control;

public partial class MasterMaint_MM_PC1 : Control.Base
{
    private Library.Database.ListCollection _list;
    private readonly string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_MM_PC1()
    {
        SetupKey = "MM_PC1";
        DefaultSort = "ID_MM_PC1";
        SortDirection = "0";
        GridViewCheckColumn = false;
        PrintControl = false;
        DeleteControl = true;
        GridViewRadioColumn = false;
        ViewHistoryControl = true;
        RecordTypeColumn = 2;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        GridView = grdResult;

        int uLevel = Convert.ToInt32(Session["ULEVEL"]);
        DeleteControl = !(uLevel == 3 || uLevel == 2);
    }

    public override void BindData()
    {
        _list = Library.Database.BLL.PC1.List(
            "PV_MM_PC1", "ID_MM_PC1",
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