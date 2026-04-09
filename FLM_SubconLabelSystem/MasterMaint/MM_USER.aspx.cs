using System;
using System.Configuration;
using Library.Root.Control;

public partial class MasterMaint_MM_USER : Control.Base
{
    private Library.Database.ListCollection _list;
    private string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_MM_USER()
    {
        SetupKey = "MM_USER";
        DefaultSort = "COMPANYCODE, ULEVEL, USERID";
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

        int uLevel = Convert.ToInt32(Session["ULEVEL"]);
        DeleteControl = !(uLevel == 3 || uLevel == 2);
    }

    public override void BindData()
    {
        string companyCode = Session["COMPANYCODE"] != null ? Session["COMPANYCODE"].ToString() : string.Empty;
        int uLevel = Convert.ToInt32(Session["ULEVEL"]);

        if (uLevel == 3 || uLevel == 2)
        {
            _list = Library.Database.BLL.user.List(
                "MM_USER_func('" + companyCode + "')",
                "ID_MM_USERID",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
        }
        else
        {
            _list = Library.Database.BLL.user.List(
                "PV_MM_USER",
                "ID_MM_USERID",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
        }

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