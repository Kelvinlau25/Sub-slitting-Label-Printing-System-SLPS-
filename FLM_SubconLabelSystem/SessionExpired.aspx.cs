using System;

public partial class SessionExpired : System.Web.UI.Page
{
    public string ReturnURL
    {
        get { return "Default.aspx"; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        ClientScript.RegisterStartupScript(this.GetType(), "Load", "<script type='text/javascript'>window.parent.location.href='Default.aspx'; </script>");
        // Response.Redirect(ResolveUrl("Default.aspx"));
    }
}