using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class Transactions_ASR_SEARCH : System.Web.UI.Page
{
    private string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Session["ULEVEL"]) == 3)
            {
                string r_companycode = Session["COMPANYCODE"].ToString();
                DataTable _refno = Library.Database.BLL.SubSlitRequest.GetASRDDL(r_companycode);

                ddlRefNo.Items.Add(new ListItem("--Select--", "0"));
                if (_refno.Rows.Count > 0)
                {
                    for (int i = 0; i < _refno.Rows.Count; i++)
                    {
                        ddlRefNo.Items.Add(new ListItem(
                            _refno.Rows[i]["REFNO"].ToString(),
                            _refno.Rows[i]["REFNO"].ToString()));
                    }
                    ddlRefNo.DataBind();
                }
            }
            else
            {
                DataTable _refno2 = Library.Database.BLL.SubSlitRequest.GetASRDDL2();
                ddlRefNo.Items.Add(new ListItem("--Select--", "0"));
                if (_refno2.Rows.Count > 0)
                {
                    for (int i = 0; i < _refno2.Rows.Count; i++)
                    {
                        ddlRefNo.Items.Add(new ListItem(
                            _refno2.Rows[i]["REFNO"].ToString(),
                            _refno2.Rows[i]["REFNO"].ToString()));
                    }
                    ddlRefNo.DataBind();
                }
            }
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string r_Refno = ddlRefNo.SelectedValue.ToString().Trim();

        string _str_fileName = "AfterSlittingReport " + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xls";
        _str_fileName = "attachment;filename=" + _str_fileName;
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", _str_fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.Output.Write(Library.Database.BLL.SubSlitRequest.GET_ASR_TO_EXCEL(r_Refno));
        Response.Flush();
        Response.End();
    }
}