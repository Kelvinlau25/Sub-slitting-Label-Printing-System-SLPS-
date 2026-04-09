using System;
using System.Web.UI;

/// <summary>
/// Add User Control
///
/// Additional
/// ----------------------------------------
/// If the URL does not contain the Sort Direction and Sort Field then will generate and redirect to default value
///
/// Remark: Based on previous version and modified the way of the binding
/// ----------------------------------------
/// C.C.Yeon    25 April 2011  Modified
/// </summary>
public partial class UserControl_GridHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindHyperLink();

        Control.Base _page = (Control.Base)this.Page;
        hypAdd.Visible = _page.AddControl;
        ddlAction.Visible = _page.PrintControl;

        string setKey = Session["Setkey"] != null ? Session["Setkey"].ToString() : null;
        int uLevel = Convert.ToInt32(Session["ULEVEL"]);

        if (setKey == "PC2_LOTNO" || setKey == "VIEW_LOT_SLITTING_SERIES" || setKey == "PRINT_ALIGN_INIT")
        {
            hypAdd.Visible = uLevel == 3 || uLevel == 1;
        }
        else
        {
            if (uLevel == 3 || uLevel == 2)
            {
                hypAdd.Visible = uLevel == 2 && setKey == "MM_PC2";
            }
            else
            {
                hypAdd.Visible = true;
            }
        }
    }

    protected void BindHyperLink()
    {
        ddlAction.Visible = false;
        Control.Base setting = (Control.Base)this.Page;
        string addUrl = setting.GetUrl(Control.Base.EnumAction.Add);

        if (!string.IsNullOrEmpty(addUrl))
            hypAdd.HRef = ResolveUrl(addUrl);
        else
            hypAdd.Visible = false;
    }

    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue == "PRINT")
        {
            Control.Base setting = (Control.Base)this.Page;
            ddlAction.SelectedIndex = 0;

            if (setting.Item1 == string.Empty)
            {
                raiseNoRecordSelectedMsg();
                return;
            }

            string strScript = "popwindow('" + setting.GeneratePrintPage() + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", strScript, true);
        }
    }

    public void raiseNoRecordSelectedMsg()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoRecordFound", "alert('No selected records to print');", true);
    }
}