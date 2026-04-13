using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PFRLabelIssuing.Pages.Report
{
    public class ASR_SEARCHModel : PageModel
    {
        [BindProperty]
        public string SelectedRefNo { get; set; } = "0";

        public List<SelectListItem> RefNoItems { get; set; } = new();

        public string StartupScript { get; set; } = string.Empty;

        public void OnGet()
        {
            LoadDropDown();
        }

        public IActionResult OnPostGenerate()
        {
            LoadDropDown();

            if (string.IsNullOrEmpty(SelectedRefNo) || SelectedRefNo == "0")
            {
                StartupScript = "alert('Please select a Ref No.');";
                return Page();
            }

            string fileName = "AfterSlittingReport " + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xls";
            string content = Library.Database.BLL.SubSlitRequest.GET_ASR_TO_EXCEL(SelectedRefNo);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);
            return File(bytes, "application/vnd.ms-excel", fileName);
        }

        private void LoadDropDown()
        {
            RefNoItems = new List<SelectListItem>
            {
                new SelectListItem("--Select--", "0")
            };

            int userLevel = 0;
            string uLevelStr = HttpContext.Session.GetString("ULEVEL");
            if (!string.IsNullOrEmpty(uLevelStr))
                int.TryParse(uLevelStr, out userLevel);

            DataTable refnoTable;
            if (userLevel == 3)
            {
                string companyCode = HttpContext.Session.GetString("COMPANYCODE") ?? string.Empty;
                refnoTable = Library.Database.BLL.SubSlitRequest.GetASRDDL(companyCode);
            }
            else
            {
                refnoTable = Library.Database.BLL.SubSlitRequest.GetASRDDL2();
            }

            if (refnoTable != null && refnoTable.Rows.Count > 0)
            {
                for (int i = 0; i < refnoTable.Rows.Count; i++)
                {
                    string refno = refnoTable.Rows[i]["REFNO"].ToString();
                    RefNoItems.Add(new SelectListItem(refno, refno));
                }
            }
        }
    }
}
