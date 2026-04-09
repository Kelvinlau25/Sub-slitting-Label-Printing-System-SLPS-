using System;
using System.Web.UI;

public partial class App_Module_Title : System.Web.UI.UserControl
{
    private bool _audit = false;
    public bool Audit
    {
        set { _audit = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!_audit)
        {
            Control.Base setting = (Control.Base)this.Page;
            lblFormTitle.Text = setting.DisplayTitle
                + (setting.Action != Control.Base.EnumAction.None ? " - " : string.Empty)
                + setting.ActionDesc;
        }
        else
        {
            Control.LogBase setting = (Control.LogBase)this.Page;
            lblFormTitle.Text = setting.DisplayTitle;
        }
    }
}