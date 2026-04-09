using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Transactions_SlitSeries : Control.Base
{
    private Library.Database.ListCollection _list;
    private string v_Status;
    private string Lot_NO;
    private DataTable _datatable;
    private int v_ID_PC2_LOTNO;
    private string Company_Code;
    private int User_Level;
    private string User_ID;
    private int z;
    private string a_Redirect;
    private string b_LotNo;

    private readonly string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public Transactions_SlitSeries()
    {
        base.SetupKey = "PC2_LOTNO";
        base.DefaultSort = "ID_PC2_LOTNO";
        base.SortDirection = "0";
        base.GridViewCheckColumn = false;
        base.PrintControl = false;
        base.DeleteControl = true;
        base.GridViewRadioColumn = false;
        base.ViewHistoryControl = false;
        base.RecordTypeColumn = 11;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        base.GridView = grdResult;

        if (Convert.ToInt32(Session["ULEVEL"]) == 2)
        {
            base.DeleteControl = false;
        }
        else
        {
            base.DeleteControl = true;
        }

        if (Request.QueryString["action"] != null)
        {
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["action"].ToString().ToLower().Equals("create"))
                {
                    Create_Lot_Slittting(Request.QueryString["ID"].ToString().Trim());
                }
            }
        }
    }

    public override void BindData()
    {
        Company_Code = Session["COMPANYCODE"].ToString();
        User_Level = Convert.ToInt32(Session["ULEVEL"]);

        if (User_Level == 3)
        {
            _list = Library.Database.BLL.SlitSeries.List("|" + Company_Code, "ID_PC2_LOTNO", base.SearchField, base.SearchValue, "UPDATED_DATE", 1, base.PageNo, base.ShowDeleted ? 1 : 0);
        }
        else
        {
            _list = Library.Database.BLL.SlitSeries.List("PV_PC2_LOTNO", "ID_PC2_LOTNO", base.SearchField, base.SearchValue, "UPDATED_DATE", 1, base.PageNo, base.ShowDeleted ? 1 : 0);
        }

        DataTable _obj_DT = _list.Data;
        DataRow[] _obj_dr;

        _obj_DT.Columns.Add("Create_URL", typeof(string));

        _obj_dr = _obj_DT.Select("STATUS = 'Create'");

        if (_obj_dr.GetLength(0) > 0)
        {
            for (int _int_iCreate = 0; _int_iCreate <= (_obj_dr.GetLength(0) - 1); _int_iCreate++)
            {
                _obj_dr[_int_iCreate]["Create_URL"] = string.Format("SLIT_SERIES.aspx?action={0}&ID={1}", "create", _obj_dr[_int_iCreate]["ID_PC2_LOTNO"].ToString().Trim());
            }
        }

        grdResult.DataSource = _list.Data;
        grdResult.DataBind();

        UCFooter.TotalRecords = _list.TotalRow;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (!hdn_LotID.Value.Equals(""))
            {
                Lot_NO = hdn_LotID.Value;
            }
        }
    }

    public void Create_Lot_Slittting(string Lot)
    {
        Company_Code = Session["COMPANYCODE"].ToString();
        User_Level = Convert.ToInt32(Session["ULEVEL"]);

        DataTable dtData = Library.Database.BLL.SlitSeries.GetData(Lot);

        if (dtData.Rows[0]["LOT_STATUS"].ToString() == "Create")
        {
            string[] slitParts = dtData.Rows[0]["TYPE_OF_SLIT"].ToString().Split(',');
            int v_TYPE_OF_SLIT = Convert.ToInt32(slitParts[0]);
            int v_MATRIX_POS = Convert.ToInt32(slitParts[1]);
            int v_MATRIX_INC = Convert.ToInt32(slitParts[2]);
            string v_LOTNO = dtData.Rows[0]["LOTNO"].ToString();
            int v_NO_OF_SLIT = Convert.ToInt32(dtData.Rows[0]["NO_OF_SLIT"]);
            int v_ID_Lot = Convert.ToInt32(Lot);
            string v_CompanyCode = dtData.Rows[0]["COMPANYTO"].ToString();
            User_ID = Session["USERID"].ToString();
            Session["LOTNO"] = dtData.Rows[0]["LOTNO"].ToString();

            string upd_stat = "";
            if (User_Level == 1)
            {
                upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(v_CompanyCode, v_ID_Lot, v_TYPE_OF_SLIT, v_MATRIX_POS, v_MATRIX_INC, v_LOTNO, v_NO_OF_SLIT, User_ID);
            }

            if (User_Level == 2 && Company_Code.Equals(v_CompanyCode))
            {
                upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(Company_Code, v_ID_Lot, v_TYPE_OF_SLIT, v_MATRIX_POS, v_MATRIX_INC, v_LOTNO, v_NO_OF_SLIT, User_ID);
            }
            else
            {
                if (User_Level == 2 && !Company_Code.Equals(v_CompanyCode))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Creating Lot Slitting records for other company is not allowed");
                }
            }

            if (User_Level == 3)
            {
                upd_stat = Library.Database.BLL.SlitSeries.CreateSlitRec(Company_Code, v_ID_Lot, v_TYPE_OF_SLIT, v_MATRIX_POS, v_MATRIX_INC, v_LOTNO, v_NO_OF_SLIT, User_ID);
            }

            if (!upd_stat.Equals("1"))
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Creating Slitting records is not successful");
            }
            else
            {
                string url = "~/MasterMaint/LabelPlan.aspx?itm1=" + v_LOTNO;
                Response.Redirect(url);
            }
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "This Lot No Slitting is already completed");
        }
    }

    public void Redirect_Label_Plan()
    {
        Response.Redirect("~/MasterMaint/LabelPlan.aspx");
    }
}