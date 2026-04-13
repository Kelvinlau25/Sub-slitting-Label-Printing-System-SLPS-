using System;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class HousekeepingModel : PageModel
    {
        public string StartupScript { get; set; } = string.Empty;

        [BindProperty]
        public string DataRetention { get; set; } = string.Empty;

        public string PurgeDate { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPostCalculateDate()
        {
            if (!string.IsNullOrEmpty(DataRetention))
            {
                DateTime reqDate = DateTime.Today.AddDays(-Convert.ToDouble(DataRetention));
                PurgeDate = reqDate.ToString("dd/MM/yyyy");
            }
            return Page();
        }

        public IActionResult OnPostUpdate(string purgeDate)
        {
            PurgeDate = purgeDate;
            if (string.IsNullOrEmpty(DataRetention) || string.IsNullOrEmpty(PurgeDate))
            {
                StartupScript = "alert('Please select Data Retention');";
                return Page();
            }

            string ulevel = HttpContext.Session.GetString("ULEVEL") ?? "";
            if (ulevel == "3")
            {
                return Page();
            }

            string[] preDate = PurgeDate.Split('/');
            string pDatePurge = preDate[2] + "-" + preDate[1] + "-" + preDate[0];
            string pCompany = " ";

            string childSucc = "Y", motSucc = "Y", lotSucc = "Y";
            string subslitDone = "N", lotNoDone = "N";

            // SUBSLIT PURGE - CHILD
            DataTable dtSubSlitChild = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSCHILD");
            for (int i = 0; i < dtSubSlitChild.Rows.Count; i++)
            {
                string temp = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtSubSlitChild.Rows[i]["ID_SUBSLIT_REQUEST_CHILD"].ToString(), "SSCHILD");
                if (temp != "1") { childSucc = "N"; StartupScript = "alert('There is an error on purging SUBSLIT CHILD!');"; }
            }

            // SUBSLIT PURGE - MOTHER
            if (childSucc == "Y")
            {
                DataTable dtSubSlitMother = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSMOTHER");
                for (int j = 0; j < dtSubSlitMother.Rows.Count; j++)
                {
                    string temp2 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtSubSlitMother.Rows[j]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString(), "SSMOTHER");
                    if (temp2 != "1") { motSucc = "N"; }
                }
            }

            // SUBSLIT PURGE - MAIN
            if (motSucc == "Y")
            {
                DataTable dtSubSlitMain = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSMAIN");
                for (int k = 0; k < dtSubSlitMain.Rows.Count; k++)
                {
                    string temp3 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtSubSlitMain.Rows[k]["ID_SUBSLIT_REQUEST"].ToString(), "SSMAIN");
                    if (temp3 == "1") subslitDone = "Y";
                }
            }

            // LOTNO PURGE
            DataTable dtLotSlit = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "LOTSLIT");
            for (int m = 0; m < dtLotSlit.Rows.Count; m++)
            {
                string temp4 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtLotSlit.Rows[m]["ID_LOT_SLITTING"].ToString(), "LOTSLIT");
                if (temp4 != "1") lotSucc = "N";
            }

            if (lotSucc == "Y")
            {
                DataTable dtPC2Lot = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "PC2LOT");
                for (int n = 0; n < dtPC2Lot.Rows.Count; n++)
                {
                    string temp5 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtPC2Lot.Rows[n]["ID_PC2_LOTNO"].ToString(), "PC2LOT");
                    if (temp5 == "1") lotNoDone = "Y";
                }
            }

            if (lotNoDone == "Y" && subslitDone == "Y")
                StartupScript = "alert('Successfully Purge Data');";
            else if (lotNoDone == "Y")
                StartupScript = "alert('Successfully Purge Lot No data');";
            else if (subslitDone == "Y")
                StartupScript = "alert('Successfully Purge Subslit data');";
            else
                StartupScript = "alert('No data was purge.');";

            return Page();
        }

        public IActionResult OnPostCancel()
        {
            return Redirect("~/Menu");
        }
    }
}
