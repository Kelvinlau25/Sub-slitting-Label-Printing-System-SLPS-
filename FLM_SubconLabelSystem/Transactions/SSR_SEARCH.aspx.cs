using System;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Transactions_SUBSLITREQSEARCH : Control.Base
{
    private Library.Database.ListCollection _list;
    private string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public Transactions_SUBSLITREQSEARCH()
    {
        SetupKey = "PV_SUBSLIT_REQUEST_LIST";
        DefaultSort = "UPDATED_DATE";
        SortDirection = "1";
        GridViewCheckColumn = false;
        PrintControl = false;
        DeleteControl = true;
        GridViewRadioColumn = false;
        ViewHistoryControl = false;
        RecordTypeColumn = 7;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        GridView = grdResult;

        if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "3")
        {
            DeleteControl = false;
            Add_Button.Visible = false;
        }
        else
        {
            DeleteControl = true;
        }
    }

    public override void BindData()
    {
        string companyCode = Session["COMPANYCODE"] != null ? Session["COMPANYCODE"].ToString() : string.Empty;
        int showDeleted = ShowDeleted ? 1 : 0;

        if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "3")
        {
            _list = Library.Database.BLL.user.List(
                "MM_SUBSLIT_func('" + companyCode + "')",
                "UPDATED_DATE",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, showDeleted);
        }
        else
        {
            _list = Library.Database.BLL.user.List(
                "PV_SUBSLIT_REQUEST_LIST",
                "UPDATED_DATE",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, showDeleted);
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

    protected void grdResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdResult, "Select$" + e.Row.RowIndex);
        }
    }

    protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        string vRefno = string.Empty;
        string vIdSsr = string.Empty;
        string vReqStatus = string.Empty;

        if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "3")
        {
            vRefno = grdResult.SelectedRow.Cells[8].Text;
            vIdSsr = grdResult.SelectedRow.Cells[9].Text;
            vReqStatus = grdResult.SelectedRow.Cells[10].Text;
        }
        else
        {
            vRefno = grdResult.SelectedRow.Cells[9].Text;
            vIdSsr = grdResult.SelectedRow.Cells[10].Text;
            vReqStatus = grdResult.SelectedRow.Cells[11].Text;
        }

        if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "3"
            && vReqStatus != "Submitted" && vReqStatus != "Cancel")
        {
            return;
        }

        if (vReqStatus == "New")
        {
            string url = "~/Transactions/SUBSLIT_REQ_.aspx?itm1=" + vRefno + "&itm2= " + vIdSsr;
            Response.Redirect(url);
        }
        else
        {
            string url = "~/Transactions/SSR_SEARCH_Dtl.aspx?itm1=" + vRefno + "&itm2= " + vIdSsr;
            Response.Redirect(url);
        }
    }

    protected void SSRRefNo_Click(object sender, EventArgs e)
    {
        // MsgBox equivalent - no-op in web context (was debug code in VB)
    }

    protected void Add_Button_Click(object sender, EventArgs e)
    {
        string url = "~/Transactions/SUBSLIT_REQ_.aspx";
        Response.Redirect(url);
    }
}