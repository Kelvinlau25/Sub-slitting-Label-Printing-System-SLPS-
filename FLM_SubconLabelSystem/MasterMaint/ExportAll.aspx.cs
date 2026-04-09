using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Master_Default : Page
{
    private string searchField = string.Empty;
    private string searchValue = string.Empty;
    private string addCondition = string.Empty;
    private string passType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SearchField"] != null && Session["SearchValue"] != null)
        {
            searchField = Session["SearchField"].ToString();
            searchValue = Session["SearchValue"].ToString();
            addCondition = Session["AddCondition"].ToString();
            passType = Session["passType"].ToString();
        }

        CheckBoxUpdate();
        CsvDownload();
    }

    public void CheckBoxUpdate()
    {
        string upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSelAll(true, "Update", searchField, searchValue, addCondition, passType);

        if (!upd_stat.Equals("1"))
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, upd_stat);
        }
    }

    protected void CsvDownload()
    {
        string constr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand("select a.* from VIEW_LOT_SLITTING_SERIES a Inner join LOT_SLITTING b on a.SLIT_LOT_NO = b.SLIT_LOT_NO AND a.ID_LOT_SLITTING = b.ID_LOT_SLITTING where (b.PRINT_SEL = 1) And (a.REC_TYPE = 1 OR a.REC_TYPE = 3)"))
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            cmd.Connection = con;
            sda.SelectCommand = cmd;

            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string csv = string.Empty;

                    // Add the Header row for CSV file.
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv += column.ColumnName + ",";
                    }

                    // Add new line.
                    csv += "\r\n";

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            // Add the Data rows.
                            if (column.ColumnName == "LOTNO" || column.ColumnName == "SLIT_LOT_NO")
                            {
                                csv += row[column.ColumnName].ToString().Replace(",", ";") + "A" + ",";
                            }
                            else
                            {
                                csv += row[column.ColumnName].ToString().Replace(",", ";") + ",";
                            }
                        }
                        // Add new line.
                        csv += "\r\n";
                    }

                    CheckBoxInit();

                    // Download the CSV file.
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=printlabel.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(csv);
                    Response.End();
                }
            }
        }
    }

    public void CheckBoxInit()
    {
        string upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSelAll(true, "Init", searchField, searchValue, addCondition, passType);

        if (!upd_stat.Equals("1"))
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, upd_stat);
        }
    }
}