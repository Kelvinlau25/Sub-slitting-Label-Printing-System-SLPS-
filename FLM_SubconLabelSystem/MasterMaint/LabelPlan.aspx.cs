using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterMaint_LabelPlan : Control.Base
{
    private Library.Database.ListCollection _list;
    private readonly string str_MSSQL_Connstr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

    public MasterMaint_LabelPlan()
    {
        SetupKey = "VIEW_LOT_SLITTING_SERIES";
        DefaultSort = "CREATED_DATE";
        SortDirection = "1";
        GridViewCheckColumn = false;
        PrintControl = false;
        DeleteControl = true;
        GridViewRadioColumn = false;
        ViewHistoryControl = false;
        AddControl = false;
        RecordTypeColumn = 11;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        GridView = grdResult;

        if (Session["ULEVEL"].ToString() == "2")
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
        int userLevel = Convert.ToInt32(Session["ULEVEL"]);
        string companyCode = Session["COMPANYCODE"].ToString();
        string lotID = Item1;

        if (lotID == string.Empty)
        {
            Session["SearchField"] = SearchField;
            Session["SearchValue"] = SearchValue;
            Session["AddCondition"] = string.Empty;
            Session["passType"] = "1";
        }
        else
        {
            if (SearchField != string.Empty)
            {
                Session["SearchField"] = SearchField;
                Session["SearchValue"] = SearchValue;
                Session["AddCondition"] = " AND LOTNO = '" + Item1 + "'";
                Session["passType"] = "1";
            }
            else
            {
                Session["SearchField"] = "LOTNO";
                Session["SearchValue"] = Item1;
                Session["AddCondition"] = string.Empty;
                Session["passType"] = "0";
            }
        }

        if (userLevel == 3)
        {
            if (!lotID.Equals(string.Empty))
            {
                _list = Library.Database.BLL.LotSlitting.List("LabelPlan_ReDir_func(" + "'" + companyCode + "'" + ", " + "'" + lotID + "'" + ")", "CREATED_DATE", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
            }
            else
            {
                _list = Library.Database.BLL.LotSlitting.List("LabelPlan_func(" + "'" + companyCode + "'" + ")", "CREATED_DATE", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
            }
        }
        else
        {
            if (!lotID.Equals(string.Empty))
            {
                _list = Library.Database.BLL.LotSlitting.List("LabelPlan_ReDir_All_func(" + "'" + lotID + "'" + ")", "CREATED_DATE", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
            }
            else
            {
                _list = Library.Database.BLL.LotSlitting.List("LabelPlan_All_func(" + "'" + companyCode + "'" + ")", "CREATED_DATE", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, Convert.ToInt32(ShowDeleted));
            }
        }

        grdResult.DataSource = _list.Data;
        grdResult.DataBind();
        UCFooter.TotalRecords = _list.TotalRow;

        if (Session["CheckBoxArray"] != null)
        {
            ArrayList checkBoxArray = (ArrayList)Session["CheckBoxArray"];

            for (int i = 0; i <= grdResult.Rows.Count - 1; i++)
            {
                if (grdResult.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    int checkBoxIndex = grdResult.PageSize * _Pageno + (i + 1);
                    if (checkBoxArray.IndexOf(checkBoxIndex) != -1)
                    {
                        CheckBox chk = (CheckBox)grdResult.Rows[i].FindControl("PrintChkBox");
                        chk.Checked = true;
                    }
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ArrayList checkBoxArray;

        if (Session["CheckBoxArray"] != null)
        {
            checkBoxArray = (ArrayList)Session["CheckBoxArray"];
        }
        else
        {
            checkBoxArray = new ArrayList();
        }

        BindData();
    }

    protected void CsvDownload()
    {
        string constr = ConfigurationManager.ConnectionStrings["PFR_Label_DB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand("select a.* from VIEW_LOT_SLITTING_SERIES a Inner join LOT_SLITTING b on a.SLIT_LOT_NO = b.SLIT_LOT_NO where (b.PRINT_SEL = 1) And (a.REC_TYPE = 1 OR a.REC_TYPE = 3)"))
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            cmd.Connection = con;
            sda.SelectCommand = cmd;

            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "window.location='LabelPlan.aspx'", true);

                    // Build the CSV file data as a Comma separated string.
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
                    Session["CheckBoxArray"] = null;
                    Session["SlitSlotNoArray"] = null;
                    BindData();

                    // Download the CSV file.
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=printlabel.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(csv);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    public void CheckBoxUpdate()
    {
        if (Session["SlitSlotNoArray"] != null)
        {
            ArrayList slitSlotNoArray = (ArrayList)Session["SlitSlotNoArray"];

            for (int j = 0; j <= slitSlotNoArray.Count - 1; j++)
            {
                string upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSel(slitSlotNoArray[j].ToString(), true, "Update");

                if (!upd_stat.Equals("1"))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, upd_stat);
                }
            }
        }
    }

    public void CheckBoxInit()
    {
        if (Session["SlitSlotNoArray"] != null)
        {
            ArrayList slitSlotNoArray = (ArrayList)Session["SlitSlotNoArray"];

            for (int j = 0; j <= slitSlotNoArray.Count - 1; j++)
            {
                string upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSel(slitSlotNoArray[j].ToString(), true, "Init");

                if (!upd_stat.Equals("1"))
                {
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, upd_stat);
                }
            }
        }
    }

    protected void PrintChkBox_CheckedChanged(object sender, EventArgs e)
    {
        ArrayList checkBoxArray;
        ArrayList slitSlotNoArray;

        checkBoxArray = Session["CheckBoxArray"] != null
            ? (ArrayList)Session["CheckBoxArray"]
            : new ArrayList();

        slitSlotNoArray = Session["SlitSlotNoArray"] != null
            ? (ArrayList)Session["SlitSlotNoArray"]
            : new ArrayList();

        for (int i = 0; i <= grdResult.Rows.Count - 1; i++)
        {
            if (grdResult.Rows[i].RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)grdResult.Rows[i].Cells[15].FindControl("PrintChkBox");

                string slitLotNo = string.Empty;
                if (Session["ULEVEL"].ToString() == "2")
                {
                    slitLotNo = grdResult.Rows[i].Cells[11].Text;
                }
                else
                {
                    slitLotNo = grdResult.Rows[i].Cells[12].Text;
                }

                int checkBoxIndex = grdResult.PageSize * _Pageno + (i + 1);

                if (chk.Checked)
                {
                    if (checkBoxArray.IndexOf(checkBoxIndex) == -1)
                    {
                        checkBoxArray.Add(checkBoxIndex);
                        slitSlotNoArray.Add(slitLotNo);
                    }
                }
                else
                {
                    if (checkBoxArray.IndexOf(checkBoxIndex) != -1)
                    {
                        checkBoxArray.Remove(checkBoxIndex);
                        slitSlotNoArray.Remove(slitLotNo);
                    }
                }
            }
        }

        Session["CheckBoxArray"] = checkBoxArray;
        Session["SlitSlotNoArray"] = slitSlotNoArray;
    }

    protected void PrintChkBoxAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)grdResult.HeaderRow.FindControl("cbSelectAll");

        if (chkAll.Checked)
        {
            Session["ChkAll"] = 1;
            for (int i = 0; i <= grdResult.Rows.Count - 1; i++)
            {
                ((CheckBox)grdResult.Rows[i].FindControl("PrintChkBox")).Checked = true;
            }
        }
        else
        {
            Session["ChkAll"] = 0;
            for (int i = 0; i <= grdResult.Rows.Count - 1; i++)
            {
                ((CheckBox)grdResult.Rows[i].FindControl("PrintChkBox")).Checked = false;
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Session["CheckBoxArray"] = null;
        Session["SlitSlotNoArray"] = null;
        Session["ChkAll"] = 0;
        Response.Redirect("LabelPlan.aspx");
    }

    protected void grdResult_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView objGridView = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell = new TableCell();

            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(objgridviewrow, objtablecell, 4, "BEFORE", Color.DarkSalmon.Name);
            AddMergedCells(objgridviewrow, objtablecell, 4, "AFTER", Color.DarkSeaGreen.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, string.Empty, Color.AliceBlue.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void grdResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("cbSelectAll");
            if (Session["ChkAll"] != null && Session["ChkAll"].ToString() == "1")
            {
                chk.Checked = true;
            }
            else
            {
                chk.Checked = false;
                Session["ChkAll"] = 0;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("PrintChkBox");
            if (Session["ChkAll"] != null && Session["ChkAll"].ToString() == "1")
            {
                chk.Checked = true;
            }
            else
            {
                chk.Checked = false;
                Session["ChkAll"] = 0;
            }
        }
    }
}