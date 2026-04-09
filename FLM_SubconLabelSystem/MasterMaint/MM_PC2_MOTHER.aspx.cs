using System;
using System.Configuration;

public partial class MasterMaint_MM_PC2_MOTHER : Control.Base
{
    private Library.Database.ListCollection _list;
    private string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_MM_PC2_MOTHER()
    {
        SetupKey = "MM_PC2_MOTHER";
        DefaultSort = "ID_MM_PC2_MOTHER";
        SortDirection = "0";
        GridViewCheckColumn = false;
        PrintControl = false;
        DeleteControl = true;
        GridViewRadioColumn = false;
        ViewHistoryControl = true;
        RecordTypeColumn = 7;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        GridView = grdResult;
    }

    public override void BindData()
    {
        _list = Library.Database.BLL.PC2Mother.List("PV_MM_PC2_MOTHER", "ID_MM_PC2_MOTHER", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
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