using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class PopUp_PP_PC2SUB : Control.Base
{
    private Library.Database.ListCollection _list;

    protected string pc2mother = string.Empty;
    protected string lblpc2mother = string.Empty;

    public PopUp_PP_PC2SUB()
    {
        SetupKey = "PP_PC2SUB";
        DefaultSort = "ID_MM_PC2";
        SortDirection = "0";
        GridViewCheckColumn = false;
        PrintControl = false;
        DeleteControl = false;
        GridViewRadioColumn = false;
        ViewHistoryControl = false;
        RecordTypeColumn = -1;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
    }

    public override void BindData()
    {
        if (Request.QueryString["itm1"] == "pc2mother")
        {
            lblTittle.Text = "PC2 Mother";
        }
        else
        {
            lblTittle.Text = "PC2 Child";
        }

        if (SearchField != "")
        {
            _list = Library.Database.BLL.PC1.List("PV_MM_PC2SUB_POPUP", "ID_MM_PC2", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
            grdResult.DataSource = _list.Data;
            grdResult.DataBind();

            UCFooter.TotalRecords = _list.TotalRow;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pc2mother = Item1;
        lblpc2mother = Item2;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)UCSearch.FindControl("ddlSearch");
        ddl.SelectedIndex = 1;
    }
}