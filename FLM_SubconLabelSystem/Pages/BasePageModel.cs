using System;
using System.Linq;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace PFRLabelIssuing.Pages
{
    /// <summary>
    /// Base PageModel for Razor Pages – replaces Control.Base (which extended
    /// Library.Root.Control.Base : System.Web.UI.Page).
    ///
    /// Provides the same query-string parsing, URL generation, sort / search /
    /// paging state, and BindData() lifecycle that the Web-Forms pages relied on.
    /// </summary>
    public abstract class BasePageModel : PageModel
    {
        // ── EnumAction (same values as the Web-Forms version) ──────────────
        public enum EnumAction
        {
            None = 0,
            Add = 1,
            Edit = 3,
            Delete = 5,
            View = 7,
            History = 9
        }

        // ── Setup / configuration ──────────────────────────────────────────
        public string SetupKey { get; set; } = string.Empty;
        public string DefaultSort { get; set; } = string.Empty;
        public string SortDirection { get; set; } = "1";
        public string SortField { get; set; } = string.Empty;
        public int PageNo { get; set; } = 1;
        public bool ShowDeleted { get; set; } = false;
        public bool FunctionControl { get; set; } = true;
        public bool DeleteControl { get; set; } = true;
        public bool AddControl { get; set; } = true;
        public bool PrintControl { get; set; } = true;
        public bool GridViewCheckColumn { get; set; } = true;
        public bool GridViewRadioColumn { get; set; } = false;
        public bool ViewHistoryControl { get; set; } = true;
        public bool ShowDeletedControl { get; set; } = true;
        public bool CustomTitle { get; set; } = false;
        public bool DetailListingFunction { get; set; } = false;
        public bool ReturnURLControl { get; set; } = false;
        public bool DeleteRedirectList { get; set; } = false;
        public int RecordTypeColumn { get; set; } = -1;

        // ── Key / Action / Search state ────────────────────────────────────
        public string Key { get; protected set; } = string.Empty;
        public EnumAction Action { get; protected set; } = EnumAction.None;

        private string _searchField = string.Empty;
        public string SearchField
        {
            get => WebUtility.UrlDecode(_searchField);
            set => _searchField = WebUtility.UrlEncode(value);
        }

        private string _searchValue = string.Empty;
        public string SearchValue
        {
            get => WebUtility.UrlDecode(_searchValue);
            set
            {
                string v = value ?? string.Empty;
                if (v.IndexOf("AND ") != -1 || v.IndexOf("OR ") != -1)
                {
                    if (v.Length >= 4 && v.Substring(0, 4).IndexOf("AND ") != -1)
                        v = v.Substring(3);
                    else if (v.Length >= 3 && v.Substring(0, 3).IndexOf("OR ") != -1)
                        v = v.Substring(2);
                }
                _searchValue = WebUtility.UrlEncode(v);
            }
        }

        public string Type { get; protected set; } = string.Empty;
        public string Item1 { get; set; } = string.Empty;
        public string Item2 { get; set; } = string.Empty;
        public string Item3 { get; set; } = string.Empty;
        public string Item4 { get; set; } = string.Empty;
        public string Item5 { get; set; } = string.Empty;
        public string Item6 { get; set; } = string.Empty;
        public string Item7 { get; set; } = string.Empty;
        public string Item8 { get; set; } = string.Empty;
        public string Item9 { get; set; } = string.Empty;
        public string Item10 { get; set; } = string.Empty;
        public string Item11 { get; set; } = string.Empty;
        public string Item12 { get; set; } = string.Empty;

        // ── Grid data (replaces GridView data source) ──────────────────────
        public DataTable GridData { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / Library.Root.Other.BusinessLogicBase.MaxQuantityPerPage);

        // ── Abstract / Virtual members ─────────────────────────────────────
        public abstract void BindData();

        public virtual string DetailPage =>
            (string)GetGlobalResource("DetailPage", SetupKey);

        public virtual string DisplayTitle =>
            (string)GetGlobalResource("Title", SetupKey);

        public virtual string ListPage =>
            (string)GetGlobalResource("ListPage", SetupKey);

        public virtual string LogPage =>
            (string)GetGlobalResource("ListPage", "History");

        public virtual string PrintPage =>
            (string)GetGlobalResource("ListPage", "Print");

        // ── Page title helper ──────────────────────────────────────────────
        public string ActionDesc
        {
            get
            {
                switch (Action)
                {
                    case EnumAction.Add: return "Add";
                    case EnumAction.Delete: return "Delete";
                    case EnumAction.Edit: return "Edit";
                    case EnumAction.View: return "View";
                    default: return string.Empty;
                }
            }
        }

        public string PageTitle =>
            DisplayTitle
            + (Action != EnumAction.None ? " - " : string.Empty)
            + ActionDesc;

        // ── URL generation (same logic as Library.Root.Control.Base) ───────
        public string GenerateList
        {
            get
            {
                string listPage = ListPage ?? string.Empty;
                return Url.Content(listPage)
                    + "?sort=" + (SortField == string.Empty ? DefaultSort : SortField)
                    + "&dic=" + (SortDirection == string.Empty ? "0" : SortDirection)
                    + "&page=" + PageNo
                    + "&fld=" + WebUtility.UrlEncode(_searchField)
                    + "&vl=" + WebUtility.UrlEncode(_searchValue)
                    + "&type=" + Type
                    + "&itm1=" + WebUtility.UrlEncode(Item1)
                    + "&itm2=" + Item2 + "&itm3=" + Item3 + "&itm4=" + Item4
                    + "&itm5=" + Item5 + "&itm6=" + Item6 + "&itm7=" + Item7
                    + "&itm8=" + Item8 + "&itm9=" + Item9
                    + "&itm10=" + Item10 + "&itm11=" + Item11 + "&itm12=" + Item12
                    + "&dlt=" + ShowDeleted;
            }
        }

        public string GetUrl(EnumAction action, string key = "")
        {
            if (SetupKey == string.Empty)
            {
                HttpContext.Session.Remove("Setkey");
                return string.Empty;
            }
            HttpContext.Session.SetString("Setkey", SetupKey);

            string k = string.IsNullOrEmpty(key) ? Key : key;
            string tempurl;

            switch (action)
            {
                case EnumAction.Add:
                    tempurl = Url.Content(DetailPage + "?action=" + (int)EnumAction.Add);
                    break;
                case EnumAction.Delete:
                    tempurl = DeleteRedirectList
                        ? GenerateList + "&action=" + (int)EnumAction.Delete + "&id=" + k
                        : Url.Content(DetailPage + "?action=" + (int)EnumAction.Delete + "&id=" + k);
                    break;
                case EnumAction.Edit:
                    tempurl = Url.Content(DetailPage + "?action=" + (int)EnumAction.Edit + "&id=" + k);
                    break;
                case EnumAction.View:
                    tempurl = Url.Content(DetailPage + "?action=" + (int)EnumAction.View + "&id=" + k);
                    break;
                case EnumAction.History:
                    tempurl = Url.Content(LogPage + "?id=" + k + "&key=" + SetupKey + "&act=" + Action + "&page=1");
                    break;
                default:
                    tempurl = Url.Content(ListPage ?? string.Empty);
                    break;
            }

            if (ReturnURLControl && !string.IsNullOrEmpty(tempurl))
                tempurl += (tempurl.Contains("?") ? "&" : "?") + "ReturnURL=" + WebUtility.UrlEncode(Request.Path + Request.QueryString);

            return tempurl;
        }

        public string GeneratePrintPage()
        {
            return Url.Content(PrintPage + "?type=" + SetupKey + "&itm1=" + Item1);
        }

        // ── Item list helpers (for checkbox selections) ────────────────────
        public void AddItem(string id)
        {
            string[] idlist = Item1.Split(',');
            if (idlist.Contains(id)) return;
            Item1 = Item1 == string.Empty ? id : Item1 + "," + id;
        }

        public void RemoveItem(string id)
        {
            string[] idlist = Item1.Split(',');
            if (!idlist.Contains(id)) return;
            Item1 = string.Empty;
            foreach (string s in idlist)
            {
                if (s != id)
                    Item1 = Item1 == string.Empty ? s : Item1 + "," + s;
            }
        }

        // ── Query-string parsing (called in OnGet / OnPost) ───────────────
        protected void ParseQueryString()
        {
            var qs = Request.Query;
            if (qs.ContainsKey("sort")) SortField = qs["sort"];
            if (qs.ContainsKey("dic")) SortDirection = qs["dic"];
            if (qs.ContainsKey("page") && int.TryParse(qs["page"], out int p)) PageNo = p;
            if (qs.ContainsKey("fld")) _searchField = qs["fld"];
            if (qs.ContainsKey("vl")) _searchValue = qs["vl"];
            if (qs.ContainsKey("type")) Type = qs["type"];
            if (qs.ContainsKey("itm1")) Item1 = WebUtility.UrlDecode(qs["itm1"]);
            if (qs.ContainsKey("itm2")) Item2 = qs["itm2"];
            if (qs.ContainsKey("itm3")) Item3 = qs["itm3"];
            if (qs.ContainsKey("itm4")) Item4 = qs["itm4"];
            if (qs.ContainsKey("itm5")) Item5 = qs["itm5"];
            if (qs.ContainsKey("itm6")) Item6 = qs["itm6"];
            if (qs.ContainsKey("itm7")) Item7 = qs["itm7"];
            if (qs.ContainsKey("itm8")) Item8 = qs["itm8"];
            if (qs.ContainsKey("itm9")) Item9 = qs["itm9"];
            if (qs.ContainsKey("itm10")) Item10 = qs["itm10"];
            if (qs.ContainsKey("itm11")) Item11 = qs["itm11"];
            if (qs.ContainsKey("itm12")) Item12 = qs["itm12"];
            if (qs.ContainsKey("dlt") && bool.TryParse(qs["dlt"], out bool d)) ShowDeleted = d;
            if (qs.ContainsKey("id")) Key = qs["id"];

            // Parse action
            if (qs.ContainsKey("action") && int.TryParse(qs["action"], out int a))
            {
                switch (a)
                {
                    case 1: Action = EnumAction.Add; break;
                    case 3: Action = EnumAction.Edit; break;
                    case 5: Action = EnumAction.Delete; break;
                    case 7: Action = EnumAction.View; break;
                    default: Action = EnumAction.None; break;
                }
            }
        }

        /// <summary>
        /// Standard OnGet: parse query string, check URL, bind data.
        /// Override in subclass if you need custom logic.
        /// </summary>
        public virtual IActionResult OnGet()
        {
            ParseQueryString();

            // Redirect to ensure sort is in the URL (same as CheckURL in Web Forms)
            if (FunctionControl && !string.IsNullOrEmpty(DefaultSort) && string.IsNullOrEmpty(SortField))
                return Redirect(GenerateList);

            BindData();
            return Page();
        }

        // ── Resource helper (replaces GetGlobalResourceObject) ─────────────
        protected string GetGlobalResource(string section, string key)
        {
            try
            {
                var rm = new System.Resources.ResourceManager(
                    "Resources." + section,
                    typeof(BasePageModel).Assembly);
                return rm.GetString(key) ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        // ── Session helpers ────────────────────────────────────────────────
        protected string SessionGet(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        protected void SessionSet(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        // ── Script injection helper (replaces ClientScript / ScriptManager) ─
        public string StartupScript { get; set; } = string.Empty;

        public void RegisterStartupScript(string script)
        {
            StartupScript += script + "\n";
        }
    }
}
