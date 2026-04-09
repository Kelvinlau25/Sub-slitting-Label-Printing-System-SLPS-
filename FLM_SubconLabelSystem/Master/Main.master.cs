using System;

public partial class master_main : System.Web.UI.MasterPage
{
    private bool _pointer = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        //Session["gstrUserID"] = "Admin";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString[ACL.Control.URL.URLEMPLOYEEID] != null)
        {
            Session["gstrUserID"] = ACL.Security.Encryption.Decrypt(Request.QueryString[ACL.Control.URL.URLEMPLOYEEID]);
            _pointer = true;
        }

        if (Request.QueryString[ACL.Control.URL.URLCOMPANYID] != null)
        {
            Session["gstrUserCom"] = Request.QueryString[ACL.Control.URL.URLCOMPANYID];
            _pointer = true;
        }

        if (Session["gstrUserID"] == null)
        {
            Response.Redirect("~/SessionExpired.aspx");
        }
    }
}