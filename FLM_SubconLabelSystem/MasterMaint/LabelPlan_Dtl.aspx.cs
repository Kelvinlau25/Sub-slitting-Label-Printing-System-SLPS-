using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class MasterMaint_LabelPlan_Dtl : Control.Base
{
    public MasterMaint_LabelPlan_Dtl()
    {
        SetupKey = "VIEW_LOT_SLITTING_SERIES";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
    }

    protected void UCAction_DisplayMode(object sender, EventArgs e)
    {
        Cdisplay.Visible = true;

        DataTable _datatable = Library.Database.BLL.LotSlitting.GetData(Key);

        lblCompCode.Text = _datatable.Rows[0]["COMPANYCODE"].ToString();
        lblPlanYrMth.Text = _datatable.Rows[0]["PLAN_YEAR_MONTH"].ToString();
        lblProdLine.Text = _datatable.Rows[0]["PRODLINE_NO"].ToString();

        lblPC1Mother.Text = _datatable.Rows[0]["PC1_MOTHER"].ToString();
        lblPC2Mother.Text = _datatable.Rows[0]["PC2_MOTHER"].ToString();
        lblUnitWeightMother.Text = _datatable.Rows[0]["M_UNITWEIGHT"].ToString();

        lblPC1Customer.Text = _datatable.Rows[0]["PC1_CUST"].ToString();
        lblPC2Customer.Text = _datatable.Rows[0]["PC2_CUST"].ToString();
        lblUnitWeightCustomer.Text = _datatable.Rows[0]["C_UNITWEIGHT"].ToString();

        lblLotNo.Text = _datatable.Rows[0]["LOTNO"].ToString();
        lblLotSlitNo.Text = _datatable.Rows[0]["SLIT_LOT_NO"].ToString();
        lblStatus.Text = _datatable.Rows[0]["STATUS"].ToString();

        UCAction.CreatedBy = "";
        UCAction.CreatedDate = new DateTime();
        UCAction.CreatedLoc = "";

        DateTime tmpCreateDate;
        if (DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
        {
            UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();
            UCAction.CreatedDate = tmpCreateDate;
            UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
        }

        UCAction.UpdatedBy = "";
        UCAction.UpdatedDate = new DateTime();
        UCAction.UpdatedLoc = "";

        DateTime tmpUpdateDate;
        if (DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
        {
            UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();
            UCAction.UpdatedDate = tmpUpdateDate;
            UCAction.UpdatedLoc = _datatable.Rows[0]["UPDATED_LOC"].ToString();
        }

        UCAction.EditMode = _datatable.Rows[0]["REC_TYPE"].ToString() != "5";

        if (Action == EnumAction.Delete)
        {
            Button btnSubmit = (Button)UCAction.FindControl("btnSubmit");
            btnSubmit.Visible = true;
        }

        if (Action == EnumAction.View)
        {
            Button btnSubmit = (Button)UCAction.FindControl("btnSubmit");
            Button btnDelete = (Button)UCAction.FindControl("btnDelete");
            btnSubmit.Visible = false;
            if (Session["ULEVEL"].ToString() == "2")
            {
                btnDelete.Visible = false;
            }
            else
            {
                btnDelete.Visible = true;
            }
        }

        HyperLink hpLink = (HyperLink)UCAction.FindControl("hpLink");
        hpLink.Visible = false;
    }

    protected void UCAction_AddResetAction(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void UCAction_EditResetAction(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    /// <summary>
    /// Delete Action
    /// </summary>
    protected void UCAction_DeleteAction(object sender, EventArgs e)
    {
        string _temp = Library.Database.BLL.LotSlitting.Maint(Key, lblLotNo.Text, "", "", "",
                                                               "", "", "", "",
                                                               "", "", "", "", ((int)Library.Root.Control.Base.EnumAction.Delete).ToString());
        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else
        {
            if (_temp == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}