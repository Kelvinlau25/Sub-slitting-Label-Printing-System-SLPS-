using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages
{
    /// <summary>
    /// Base PageModel for audit/log pages – replaces Control.LogBase.
    /// </summary>
    public abstract class LogBasePageModel : PageModel
    {
        public string SetupKey { get; set; } = string.Empty;
        public string Key { get; protected set; } = string.Empty;
        public int PageNo { get; set; } = 1;
        public string StartupScript { get; set; } = string.Empty;

        public virtual string LogPage =>
            GetGlobalResource("ListPage", "History");

        public virtual string LogTitle =>
            GetGlobalResource("Title", SetupKey);

        public string LogTable =>
            GetGlobalResource("Log", SetupKey);

        public string DisplayTitle => LogTitle;

        public string GenerateList
        {
            get
            {
                return Url.Content(LogPage)
                    + "?id=" + Key
                    + "&key=" + SetupKey
                    + "&page=" + PageNo;
            }
        }

        protected abstract void BindData();

        protected void ParseQueryString()
        {
            var qs = Request.Query;
            if (qs.ContainsKey("id")) Key = qs["id"];
            if (qs.ContainsKey("key")) SetupKey = qs["key"];
            if (qs.ContainsKey("page") && int.TryParse(qs["page"], out int p)) PageNo = p;
        }

        public virtual IActionResult OnGet()
        {
            ParseQueryString();
            BindData();
            return Page();
        }

        protected string GetGlobalResource(string section, string key)
        {
            try
            {
                var rm = new System.Resources.ResourceManager(
                    "Resources." + section,
                    typeof(LogBasePageModel).Assembly);
                return rm.GetString(key) ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        protected string SessionGet(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        protected void SessionSet(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public void RegisterStartupScript(string script)
        {
            StartupScript += script + "\n";
        }
    }
}
