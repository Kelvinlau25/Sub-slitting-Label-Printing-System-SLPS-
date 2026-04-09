using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class PopUp_PP_PC2 : Control.Base
{
    private Library.Database.ListCollection _list;

    protected string pc2mother = string.Empty;
    protected string lblpc2mother = string.Empty;
    protected string refno = string.Empty;
    protected string str_BtnName = "";
    protected string str_hdn_PC2_Mother = "";
    protected string str_hdn_Unit_Weight_Mother = "";

    public PopUp_PP_PC2()
    {
        SetupKey = "PP_PC2";
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
        string refno = "0";
        string _str_ProdLine = "0";
        string _str_PC1Mother = "0";

        str_BtnName = "";
        str_hdn_PC2_Mother = "";
        str_hdn_Unit_Weight_Mother = "";

        if (Request.QueryString["itm3"] != null)
        {
            if (Request.QueryString["itm3"].ToString() != "")
            {
                refno = Request.QueryString["itm3"].ToString();
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please select the reference no.");
                return;
            }
        }

        if (Request.QueryString["itm4"] != null)
        {
            if (Request.QueryString["itm4"].ToString() != "" && Request.QueryString["itm4"].ToString() != ",")
            {
                _str_ProdLine = Request.QueryString["itm4"].ToString();
                _str_ProdLine = _str_ProdLine.Replace(",", "");
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please select the Production Line.");
                return;
            }
        }

        if (Request.QueryString["itm5"] != null)
        {
            if (Request.QueryString["itm5"].ToString() != "" && Request.QueryString["itm5"].ToString() != ",")
            {
                _str_PC1Mother = Request.QueryString["itm5"].ToString();
                _str_PC1Mother = _str_PC1Mother.Replace(",", "");
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please select the PC 1 Mother.");
                return;
            }
        }

        if (Request.QueryString["itm6"] != null)
        {
            if (Request.QueryString["itm6"].ToString() != "" && Request.QueryString["itm6"].ToString() != ",")
            {
                str_BtnName = Request.QueryString["itm6"].ToString();
                str_BtnName = str_BtnName.Replace(",", "");
            }
        }

        if (Request.QueryString["itm7"] != null)
        {
            if (Request.QueryString["itm7"].ToString() != "" && Request.QueryString["itm7"].ToString() != ",")
            {
                str_hdn_PC2_Mother = Request.QueryString["itm7"].ToString();
                str_hdn_PC2_Mother = str_hdn_PC2_Mother.Replace(",", "");
            }
        }

        if (Request.QueryString["itm8"] != null)
        {
            if (Request.QueryString["itm8"].ToString() != "" && Request.QueryString["itm8"].ToString() != ",")
            {
                str_hdn_Unit_Weight_Mother = Request.QueryString["itm8"].ToString();
                str_hdn_Unit_Weight_Mother = str_hdn_Unit_Weight_Mother.Replace(",", "");
            }
        }

        refno = " REFNO = '" + refno + "' AND PRODLINE_NO = '" + _str_ProdLine + "' AND PC1_MOTHER = '" + _str_PC1Mother + "'";
        _list = Library.Database.BLL.PC1.List2(refno, "PV_MM_PC2_POPUPv1", "ID_MM_PC2", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
        grdResult.DataSource = _list.Data;
        grdResult.DataBind();

        UCFooter.TotalRecords = _list.TotalRow;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pc2mother = Item1;
        lblpc2mother = Item2;
        refno = Item3;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)UCSearch.FindControl("ddlSearch");
        ddl.SelectedIndex = 1;
    }
}