using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

public partial class MasterMaint_PRINT_ALIGN_INIT : Control.Base
{
    private WebClient httpclient;
    private Library.Database.ListCollection _list;
    private string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_PRINT_ALIGN_INIT()
    {
        SetupKey = "PRINT_ALIGN_INIT";
        DefaultSort = "ID_Print_Align_Init";
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
        DeleteControl = uLevel != 2;
    }

    public override void BindData()
    {
        string companyCode = Session["COMPANYCODE"] != null ? Session["COMPANYCODE"].ToString() : string.Empty;
        int uLevel = Convert.ToInt32(Session["ULEVEL"]);

        if (uLevel == 3)
        {
            _list = Library.Database.BLL.PrintAlignInit.List(
                "Print_Align_Init_func('" + companyCode + "')",
                "ID_Print_Align_Init",
                SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
        }
        else
        {
            _list = Library.Database.BLL.PC1.List(
                "PV_PRINT_ALIGN_INIT",
                "ID_Print_Align_Init",
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

    protected void herebutton_Click(object sender, EventArgs e)
    {
        DownloadExe();
    }

    private void DownloadCSV()
    {
        string constr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select * from PRINT_ALIGN_INIT Where Default_Printer = 1 And REC_TYPE != 5"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        string csv = string.Empty;

                        foreach (DataColumn column in dt.Columns)
                        {
                            csv += column.ColumnName + ",";
                        }

                        csv += "\r\n";

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {
                                csv += row[column.ColumnName].ToString().Replace(",", ";") + ",";
                            }

                            csv += "\r\n";
                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=settings.csv");
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        Response.Output.Write(csv);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }

    private void DownloadExe()
    {
        Response.Redirect(ResolveUrl("~/printlabelling.zip"));
    }

    private void Downloaded()
    {
    }

    protected void hereButton1_Click(object sender, EventArgs e)
    {
        DownloadCSV();
    }
}