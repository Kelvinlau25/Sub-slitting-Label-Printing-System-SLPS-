using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages
{
    public class SessionExpiredModel : PageModel
    {
        public string ReturnURL => "~/";

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }
}
