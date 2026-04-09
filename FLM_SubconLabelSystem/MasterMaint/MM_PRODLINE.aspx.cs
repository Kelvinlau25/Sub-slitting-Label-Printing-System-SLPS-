using System;
using System.Configuration;

public partial class MasterMaint_MM_PRODLINE : Control.Base
{
    private Library.Database.ListCollection _list;
    private string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_MM_PRODLINE()
    {
        SetupKey = "MM_PRODLINE";
        DefaultSort = "ID_MM_PRODLINE";
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

        if (Convert.ToString(Session["ULEVEL"]) == "3" || Convert.ToString(Session["ULEVEL"]) == "2")
            DeleteControl = false;
        else
            DeleteControl = true;
    }

    public override void BindData()
    {
        _list = Library.Database.BLL.MM_PRODLINE.List("PV_MM_PRODLINE", "ID_MM_PRODLINE", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));

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