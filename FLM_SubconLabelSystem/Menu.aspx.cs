using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using ACL.MenuBar.Object;

public partial class Menu : System.Web.UI.Page
{
    protected LeftMenuItemList _list;
    protected string _words;
    private bool _pointer = false;

    private string _signOutURL = string.Empty;
    protected string SignOutURL
    {
        get { return _signOutURL; }
    }

    private string _homeURL = string.Empty;
    protected string HomeURL
    {
        get { return _homeURL; }
    }

    private string _resetURL = string.Empty;
    protected string ResetURL
    {
        get { return _resetURL; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["gstrUserID"] = Session["USERID"];
        Session["gstrUserComp"] = Session["COMPANYCODE"];
        Session["U_Level"] = Session["ULEVEL"];
        Session["gettemp"] = Session["USERNAME"];
        Session["LoginHis"] = DateTime.Now.ToString("dd MMMM yyyy");

        this._signOutURL = ResolveUrl("~/Default.aspx");
        this._homeURL = ResolveUrl("~/Menu.aspx");
        this._resetURL = ResolveUrl("~/MasterMaint/ChangePassword.aspx");

        if (Session["gstrUserID"] == null)
        {
            Response.Redirect("~/SessionExpired.aspx?ReturnURL=" + Server.UrlEncode(Request.RawUrl));
        }

        if (Session["pswlenerr"] != null && Convert.ToBoolean(Session["pswlenerr"]))
        {
            Session.Remove("pswlenerr");
            resetpwd.Visible = true;
            resetpwd.Attributes.Add("Style", "font-weight:900;");
        }
        else
        {
            resetpwd.Visible = false;
        }

        if (_list == null)
        {
            _list = new LeftMenuItemList();
        }

        DataTable _menulist = Library.Database.BLL.MenuListing.Load_Menu_Listing("");

        StringBuilder mylistHtml = new StringBuilder();
        int mycounter = 0;
        string _str_Category = "";
        string _str_MenuName = "";
        int _int_left_menu_id = 0;
        ArrayList _obj_List = new ArrayList();
        string _str_menu_id = "";

        foreach (DataRow dr in _menulist.Rows)
        {
            mycounter += 1;
            _str_MenuName = dr["MENU_NAME"].ToString().Trim();

            if (!_str_Category.Equals(dr["CATEGORY"].ToString().Trim()) && !Session["ULEVEL"].Equals("3"))
            {
                _str_Category = dr["CATEGORY"].ToString().Trim();

                if (_int_left_menu_id > 0)
                {
                    liItems.Text += string.Format("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>", _str_menu_id, mylistHtml);

                    mylistHtml.Length = 0;
                    mylistHtml.Capacity = 0;
                }

                _str_menu_id = "left_menu_" + _int_left_menu_id;
                _int_left_menu_id += 1;

                if (Convert.ToString(Session["ULEVEL"]) == "3" && _str_Category == "HouseKeeping")
                {
                    // skip
                }
                else
                {
                    _list.AddItem(new LeftMenuItem(_str_menu_id, _str_Category, false));
                }
            }

            if (!_str_Category.Equals(dr["CATEGORY"].ToString().Trim()) && Session["ULEVEL"].Equals("3") && !_str_MenuName.Equals("Sub-Slittting Request - Add"))
            {
                _str_Category = dr["CATEGORY"].ToString().Trim();

                if (_int_left_menu_id > 0)
                {
                    liItems.Text += string.Format("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>", _str_menu_id, mylistHtml);

                    mylistHtml.Length = 0;
                    mylistHtml.Capacity = 0;
                }

                _str_menu_id = "left_menu_" + _int_left_menu_id;
                _int_left_menu_id += 1;

                if (Convert.ToString(Session["ULEVEL"]) == "3" && _str_Category == "HouseKeeping")
                {
                    // skip
                }
                else
                {
                    _list.AddItem(new LeftMenuItem(_str_menu_id, _str_Category, false));
                }
            }

            if (!Session["ULEVEL"].Equals("3") || (Session["ULEVEL"].Equals("3") && !_str_MenuName.Equals("Sub-Slittting Request - Add")))
            {
                mylistHtml.AppendFormat("<li class='{2}'><a {3} href='{0}'>{1}</a></li>",
                    GenerateKeywords(Convert.ToString(dr["MENU_LINK"]), Convert.ToString(Session["gstrUserID"]), Convert.ToString(Session["gstrUserComp"]), Convert.ToString(Session["gettemp"]), Convert.ToString(dr["MENU_NAME"])),
                    dr["MENU_NAME"],
                    mycounter % 2 == 0 ? "alt" : "nor",
                    "target='page'");
            }
        }

        liItems.Text += string.Format("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>", _str_menu_id, mylistHtml);

        if (DateTime.Now.Hour < 12)
        {
            _words = "Good Morning";
        }
        else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 17)
        {
            _words = "Good Afternoon";
        }
        else
        {
            _words = "Good Evening";
        }
    }

    public string GenerateKeywords(string URL, string ID, string Company, string Name, string System)
    {
        return Server.HtmlEncode(ResolveUrl(URL));
    }

    private void systemCheck(string Systemname)
    {
        // Validate the system
        Session["system"] = 0;

        if (Convert.ToInt32(Session["system"]) == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Invalid System');", true);
            return;
        }
    }
}