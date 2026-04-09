using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ACL.MenuBar.Object;

namespace PFRLabelIssuing.Pages
{
    public class MenuModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public MenuModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Greeting { get; set; } = string.Empty;
        public string SignOutURL { get; set; } = string.Empty;
        public string HomeURL { get; set; } = string.Empty;
        public string ResetURL { get; set; } = string.Empty;
        public bool ShowResetPassword { get; set; }
        public string MenuItemsHtml { get; set; } = string.Empty;
        public string MenuListJson { get; set; } = "[]";

        public string PageTitle =>
            _configuration["AppSettings:title"] ?? string.Empty;

        public IActionResult OnGet()
        {
            HttpContext.Session.SetString("gstrUserID", HttpContext.Session.GetString("USERID") ?? "");
            HttpContext.Session.SetString("gstrUserComp", HttpContext.Session.GetString("COMPANYCODE") ?? "");
            HttpContext.Session.SetString("U_Level", HttpContext.Session.GetString("ULEVEL") ?? "");
            HttpContext.Session.SetString("gettemp", HttpContext.Session.GetString("USERNAME") ?? "");
            HttpContext.Session.SetString("LoginHis", DateTime.Now.ToString("dd MMMM yyyy"));

            SignOutURL = Url.Content("~/");
            HomeURL = Url.Content("~/Menu");
            ResetURL = Url.Content("~/MasterMaint/ChangePassword");

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("gstrUserID")))
            {
                return Redirect("~/SessionExpired?ReturnURL=" +
                    System.Net.WebUtility.UrlEncode(Request.Path + Request.QueryString));
            }

            // Check password length error
            var pswlenerr = HttpContext.Session.GetString("pswlenerr");
            if (!string.IsNullOrEmpty(pswlenerr) && pswlenerr == "1")
            {
                HttpContext.Session.Remove("pswlenerr");
                ShowResetPassword = true;
            }

            BuildMenu();
            SetGreeting();

            return Page();
        }

        private void BuildMenu()
        {
            var list = new LeftMenuItemList();
            DataTable menulist = Library.Database.BLL.MenuListing.Load_Menu_Listing("");

            var menuItemsHtml = new StringBuilder();
            var mylistHtml = new StringBuilder();
            int mycounter = 0;
            string strCategory = "";
            string strMenuName = "";
            int intLeftMenuId = 0;
            string strMenuId = "";
            string userLevel = HttpContext.Session.GetString("ULEVEL") ?? "";

            foreach (DataRow dr in menulist.Rows)
            {
                mycounter += 1;
                strMenuName = dr["MENU_NAME"].ToString().Trim();

                if (!strCategory.Equals(dr["CATEGORY"].ToString().Trim()) && !userLevel.Equals("3"))
                {
                    strCategory = dr["CATEGORY"].ToString().Trim();

                    if (intLeftMenuId > 0)
                    {
                        menuItemsHtml.AppendFormat("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>",
                            strMenuId, mylistHtml);
                        mylistHtml.Clear();
                    }

                    strMenuId = "left_menu_" + intLeftMenuId;
                    intLeftMenuId += 1;

                    if (!(userLevel == "3" && strCategory == "HouseKeeping"))
                    {
                        list.AddItem(new LeftMenuItem(strMenuId, strCategory, false));
                    }
                }

                if (!strCategory.Equals(dr["CATEGORY"].ToString().Trim()) &&
                    userLevel.Equals("3") &&
                    !strMenuName.Equals("Sub-Slittting Request - Add"))
                {
                    strCategory = dr["CATEGORY"].ToString().Trim();

                    if (intLeftMenuId > 0)
                    {
                        menuItemsHtml.AppendFormat("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>",
                            strMenuId, mylistHtml);
                        mylistHtml.Clear();
                    }

                    strMenuId = "left_menu_" + intLeftMenuId;
                    intLeftMenuId += 1;

                    if (!(userLevel == "3" && strCategory == "HouseKeeping"))
                    {
                        list.AddItem(new LeftMenuItem(strMenuId, strCategory, false));
                    }
                }

                if (!userLevel.Equals("3") ||
                    (userLevel.Equals("3") && !strMenuName.Equals("Sub-Slittting Request - Add")))
                {
                    string menuUrl = GenerateKeywords(
                        Convert.ToString(dr["MENU_LINK"]),
                        HttpContext.Session.GetString("gstrUserID") ?? "",
                        HttpContext.Session.GetString("gstrUserComp") ?? "",
                        HttpContext.Session.GetString("gettemp") ?? "",
                        Convert.ToString(dr["MENU_NAME"]));

                    mylistHtml.AppendFormat("<li class='{2}'><a {3} href='{0}'>{1}</a></li>",
                        menuUrl,
                        dr["MENU_NAME"],
                        mycounter % 2 == 0 ? "alt" : "nor",
                        "target='page'");
                }
            }

            menuItemsHtml.AppendFormat("<div class='bar_itms' id='{0}'><ul>{1}</ul></div>",
                strMenuId, mylistHtml);

            MenuItemsHtml = menuItemsHtml.ToString();
            MenuListJson = BuildMenuListJson(list);
        }

        private string BuildMenuListJson(LeftMenuItemList list)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            bool first = true;
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (!first) sb.Append(",");
                first = false;
                sb.AppendFormat("{{title:'{0}',contentEl:'{1}',collapsed:{2}}}",
                    EscapeJs(item.Text),
                    EscapeJs(item.ID),
                    item.Expanded ? "false" : "true");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string EscapeJs(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return value.Replace("\\", "\\\\").Replace("'", "\\'");
        }

        public string GenerateKeywords(string URL, string ID, string Company, string Name, string SystemName)
        {
            return global::System.Net.WebUtility.HtmlEncode(Url.Content(URL));
        }

        private void SetGreeting()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 12)
                Greeting = "Good Morning";
            else if (hour <= 17)
                Greeting = "Good Afternoon";
            else
                Greeting = "Good Evening";
        }
    }
}
