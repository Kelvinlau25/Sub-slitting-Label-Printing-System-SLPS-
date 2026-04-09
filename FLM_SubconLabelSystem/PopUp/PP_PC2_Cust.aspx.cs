using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class PopUp_PP_PC2_Cust : Control.Base
{
    private Library.Database.ListCollection _list;

    protected string pc2cust = string.Empty;
    protected string lblpc2cust = string.Empty;
    protected string refno = string.Empty;
    protected string str_hdn_PC2Customer = "";
    protected string str_hdn_UnitWeightCustomer = "";
    protected string str_BtnName = "";

    public PopUp_PP_PC2_Cust()
    {
        SetupKey = "PP_PC2_Cust";
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
        string _str_PC2Mother = "0";
        string _str_PC1Customer = "0";

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
                _str_PC2Mother = Request.QueryString["itm6"].ToString();
                _str_PC2Mother = _str_PC2Mother.Replace(",", "");
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please select the PC 2 Mother.");
                return;
            }
        }

        if (Request.QueryString["itm7"] != null)
        {
            if (Request.QueryString["itm7"].ToString() != "" && Request.QueryString["itm7"].ToString() != ",")
            {
                _str_PC1Customer = Request.QueryString["itm7"].ToString();
                _str_PC1Customer = _str_PC1Customer.Replace(",", "");
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please select the PC 1 Customer.");
                return;
            }
        }

        if (Request.QueryString["itm8"] != null)
        {
            if (Request.QueryString["itm8"].ToString() != "" && Request.QueryString["itm8"].ToString() != ",")
            {
                str_BtnName = Request.QueryString["itm8"].ToString();
                str_BtnName = str_BtnName.Replace(",", "");
            }
        }

        if (Request.QueryString["itm9"] != null)
        {
            if (Request.QueryString["itm9"].ToString() != "" && Request.QueryString["itm9"].ToString() != ",")
            {
                str_hdn_PC2Customer = Request.QueryString["itm9"].ToString();
                str_hdn_PC2Customer = str_hdn_PC2Customer.Replace(",", "");
            }
        }

        if (Request.QueryString["itm10"] != null)
        {
            if (Request.QueryString["itm10"].ToString() != "" && Request.QueryString["itm10"].ToString() != ",")
            {
                str_hdn_UnitWeightCustomer = Request.QueryString["itm10"].ToString();
                str_hdn_UnitWeightCustomer = str_hdn_UnitWeightCustomer.Replace(",", "");
            }
        }

        refno = " REFNO = '" + refno + "' AND PC1_MOTHER = '" + _str_PC1Mother + "' AND PC2_MOTHER = '" + _str_PC2Mother + "' AND PC1_CUST = '" + _str_PC1Customer + "' AND PRODLINE_NO = '" + _str_ProdLine + "'";

        _list = Library.Database.BLL.PC1.List2(refno, "PV_MM_PC2CUST_POPUPv1", "ID_MM_PC2", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
        grdResult.DataSource = _list.Data;
        grdResult.DataBind();

        UCFooter.TotalRecords = _list.TotalRow;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pc2cust = Item1;
        lblpc2cust = Item2;
        refno = Item3;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)UCSearch.FindControl("ddlSearch");
        ddl.SelectedIndex = 1;
    }
}