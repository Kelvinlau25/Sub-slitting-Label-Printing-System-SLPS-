using System;
using System.Configuration;
using Library.Root.Control;

public partial class MasterMaint_MM_COMPANY : Control.Base
{
    private Library.Database.ListCollection _list;
    private readonly string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_MM_COMPANY()
    {
        SetupKey = "MM_COMPANY";
        DefaultSort = "ID_MM_COMPANY";
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

        if (Session["ULEVEL"] != null &&
            (Session["ULEVEL"].ToString() == "3" || Session["ULEVEL"].ToString() == "2"))
        {
            DeleteControl = false;
        }
        else
        {
            DeleteControl = true;
        }
    }

    public override void BindData()
    {
        if (Session["ULEVEL"] != null &&
            (Session["ULEVEL"].ToString() == "3" || Session["ULEVEL"].ToString() == "2"))
        {
            _list = Library.Database.BLL.Company.List(
                "MM_COMPANY_func('" + Session["COMPANYCODE"] + "')",
                "ID_MM_COMPANY",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
        }
        else
        {
            _list = Library.Database.BLL.Company.List(
                "PV_MM_COMPANY",
                "ID_MM_COMPANY",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
        }

        grdResult.DataSource = _list.Data;
        grdResult.DataBind();

        UCFooter.TotalRecords = _list.TotalRow;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Reserved for additional load logic
        }
    }
}