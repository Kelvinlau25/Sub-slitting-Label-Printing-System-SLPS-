using System;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class SSR_VIEWModel : PageModel
    {
        public string StartupScript { get; set; } = string.Empty;
        public string[] RefNoList { get; set; } = Array.Empty<string>();

        // Header
        public string CompCode { get; set; } = string.Empty;
        public string Dept { get; set; } = string.Empty;
        public string ByName { get; set; } = string.Empty;
        public string DateReq { get; set; } = string.Empty;
        public string Rev { get; set; } = string.Empty;

        [BindProperty]
        public string RefNo { get; set; } = string.Empty;
        [BindProperty]
        public string SubReqId { get; set; } = string.Empty;
        [BindProperty]
        public string ReqStatSelected { get; set; } = "Submitted";
        [BindProperty]
        public string VenStatSelected { get; set; } = "N/A";

        public bool ReqStatEnabled { get; set; } = true;
        public bool VenStatEnabled { get; set; } = true;

        // Grid
        public DataTable GridData { get; set; }
        public string MQty { get; set; } = "0";
        public string MTotalWeight { get; set; } = "0";
        public string CQty { get; set; } = "0";
        public string CTotalWeight { get; set; } = "0";
        public string SubSlitWaste { get; set; } = "0";

        // Buttons
        public bool ShowUpdStatButton { get; set; } = false;
        public bool ShowNewRevButton { get; set; } = false;
        public bool ShowExportButton { get; set; } = false;
        public bool ShowCancelButton { get; set; } = false;

        private void LoadRefNoList()
        {
            DataTable dt_sel = Library.Database.BLL.SubSlitRequest.GetRefNoList();
            if (dt_sel.Rows.Count > 0)
            {
                RefNoList = new string[dt_sel.Rows.Count];
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                    RefNoList[i] = dt_sel.Rows[i]["REFNO"].ToString();
            }
        }

        private void LoadUserInfo()
        {
            string userid = HttpContext.Session.GetString("USERID") ?? "";
            DataTable _dtGetDept = Library.Database.BLL.SubSlitRequest.GetUserData(userid);
            Dept = _dtGetDept.Rows[0]["DEPARTMENT"].ToString();
            ByName = _dtGetDept.Rows[0]["NAME"].ToString();
        }

        public void OnGet()
        {
            LoadRefNoList();
            LoadUserInfo();
        }

        public IActionResult OnPost()
        {
            LoadRefNoList();
            LoadUserInfo();

            if (!string.IsNullOrEmpty(RefNo))
            {
                DataTable dtmaxRev = Library.Database.BLL.SubSlitRequest.chkRefNo(RefNo);

                if (dtmaxRev.Rows.Count > 0)
                {
                    if (dtmaxRev.Rows[0]["REQUEST_STATUS"].Equals("New"))
                    {
                        StartupScript = "alert('This Refno Status is New. Please go to Sub Slitting Request - Add screen');";
                        RefNo = "";
                        return Page();
                    }

                    SubReqId = dtmaxRev.Rows[0]["ID_SUBSLIT_REQUEST"].ToString();
                    CompCode = dtmaxRev.Rows[0]["COMPANYTO"].ToString();
                    DateReq = Convert.ToDateTime(dtmaxRev.Rows[0]["DATEREQ"].ToString()).ToString("dd-MM-yyyy");
                    Rev = dtmaxRev.Rows[0]["REVISIONCOUNT"].ToString();
                    ReqStatSelected = dtmaxRev.Rows[0]["REQUEST_STATUS"].ToString();
                    VenStatSelected = dtmaxRev.Rows[0]["VENDOR_STATUS"].ToString();

                    if (ReqStatSelected == "Cancel")
                    {
                        VenStatEnabled = false;
                        ReqStatEnabled = false;
                    }

                    DisplaySSRListing();
                }
                else
                {
                    StartupScript = "alert('This Refno does not exist.');";
                    RefNo = "";
                }
            }

            string ulevel = HttpContext.Session.GetString("ULEVEL") ?? "";
            if (ulevel == "2" || ulevel == "3")
                ReqStatEnabled = false;

            return Page();
        }

        private void DisplaySSRListing()
        {
            DataTable dtSSRList = Library.Database.BLL.SubSlitRequest.SSRList(RefNo, "0");

            if (dtSSRList.Rows.Count > 0 && !string.IsNullOrEmpty(RefNo))
            {
                if (dtSSRList.Rows[0]["REQUEST_STATUS"].Equals("Submitted") ||
                    dtSSRList.Rows[0]["REQUEST_STATUS"].Equals("Cancel"))
                {
                    DataTable dtWBlank = new DataTable();
                    dtWBlank.Columns.Add("PRODLINE_NO", typeof(string));
                    dtWBlank.Columns.Add("PC1_MOTHER", typeof(string));
                    dtWBlank.Columns.Add("PC2_MOTHER", typeof(string));
                    dtWBlank.Columns.Add("QTY", typeof(string));
                    dtWBlank.Columns.Add("M_WEIGHT", typeof(string));
                    dtWBlank.Columns.Add("M_TOTAL_WEIGHT", typeof(string));
                    dtWBlank.Columns.Add("PC1_CUST", typeof(string));
                    dtWBlank.Columns.Add("PC2_CUST", typeof(string));
                    dtWBlank.Columns.Add("C_QTY", typeof(string));
                    dtWBlank.Columns.Add("C_WEIGHT", typeof(string));
                    dtWBlank.Columns.Add("C_TOTAL_WEIGHT", typeof(string));
                    dtWBlank.Columns.Add("SUBSLIT_WASTE", typeof(string));
                    dtWBlank.Columns.Add("ETD", typeof(string));
                    dtWBlank.Columns.Add("ETA", typeof(string));
                    dtWBlank.Columns.Add("REMARK", typeof(string));

                    string prevPC2 = "";
                    int cQty = 0, mQty = 0;
                    decimal cTot = 0, mTot = 0, subW = 0;

                    for (int i = 0; i < dtSSRList.Rows.Count; i++)
                    {
                        if (dtSSRList.Rows[i]["PC2_MOTHER"].ToString() == prevPC2)
                        {
                            DataRow dr = dtWBlank.NewRow();
                            dr["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                            dr["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                            dr["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];
                            dr["C_WEIGHT"] = dtSSRList.Rows[i]["C_WEIGHT"];
                            dr["C_TOTAL_WEIGHT"] = dtSSRList.Rows[i]["C_TOTAL_WEIGHT"];
                            dr["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                            dtWBlank.Rows.Add(dr);
                            cQty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                            cTot += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(prevPC2))
                                dtWBlank.Rows.Add(dtWBlank.NewRow());

                            DataRow dr = dtWBlank.NewRow();
                            dr["PRODLINE_NO"] = dtSSRList.Rows[i]["PRODLINE_NO"];
                            dr["PC1_MOTHER"] = dtSSRList.Rows[i]["PC1_MOTHER"];
                            dr["PC2_MOTHER"] = dtSSRList.Rows[i]["PC2_MOTHER"];
                            dr["QTY"] = dtSSRList.Rows[i]["QTY"];
                            dr["M_WEIGHT"] = dtSSRList.Rows[i]["M_WEIGHT"];
                            dr["M_TOTAL_WEIGHT"] = dtSSRList.Rows[i]["M_TOTAL_WEIGHT"];
                            dr["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                            dr["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                            dr["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];
                            dr["C_WEIGHT"] = dtSSRList.Rows[i]["C_WEIGHT"];
                            dr["C_TOTAL_WEIGHT"] = dtSSRList.Rows[i]["C_TOTAL_WEIGHT"];
                            dr["SUBSLIT_WASTE"] = dtSSRList.Rows[i]["SUBSLIT_WASTE"];
                            dr["ETD"] = dtSSRList.Rows[i]["ETD"];
                            dr["ETA"] = dtSSRList.Rows[i]["ETA"];
                            dr["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                            dtWBlank.Rows.Add(dr);

                            mQty += Convert.ToInt32(dtSSRList.Rows[i]["QTY"]);
                            mTot += Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                            subW += Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                            cQty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                            cTot += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                        }
                        prevPC2 = dtSSRList.Rows[i]["PC2_MOTHER"].ToString();
                    }

                    MQty = mQty.ToString();
                    MTotalWeight = mTot.ToString();
                    CQty = cQty.ToString();
                    CTotalWeight = cTot.ToString();
                    SubSlitWaste = subW.ToString();
                    GridData = dtWBlank;

                    string ulevel = HttpContext.Session.GetString("ULEVEL") ?? "";
                    if (ulevel == "3")
                    {
                        ShowUpdStatButton = true;
                        ShowCancelButton = true;
                    }
                    else
                    {
                        ShowUpdStatButton = true;
                        ShowNewRevButton = true;
                        ShowExportButton = true;
                        ShowCancelButton = true;
                    }
                }
            }
        }

        public IActionResult OnPostUpdateStatus()
        {
            LoadRefNoList();
            LoadUserInfo();

            int rIdSsr = Convert.ToInt32(SubReqId);

            string temp = Library.Database.BLL.SubSlitRequest.SSRUpdateStat(
                RefNo, rIdSsr, ReqStatSelected, VenStatSelected);

            if (temp == "1")
                StartupScript = "alert('The Status of Sub Slittng Request under " + RefNo + " is updated successfully.');";
            else
                StartupScript = "alert('" + (temp == "0" ? "Operation failed" : temp.Replace("'", "\\'")) + "');";

            DisplaySSRListing();
            return Page();
        }

        public IActionResult OnPostExport()
        {
            LoadRefNoList();
            LoadUserInfo();

            string userid = HttpContext.Session.GetString("USERID") ?? "";

            DisplaySSRListing();

            string ssrStr = Library.Database.BLL.SubSlitRequest.GET_SSR_TO_EXCEL(
                RefNo, Convert.ToInt32(SubReqId), userid,
                Convert.ToInt32(MQty), Convert.ToDecimal(MTotalWeight),
                Convert.ToInt32(CQty), Convert.ToDecimal(CTotalWeight),
                Convert.ToDecimal(SubSlitWaste), GridData);

            string fileName = "SubSlittingRequest" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            byte[] bytes = Encoding.UTF8.GetBytes(ssrStr);
            return File(bytes, "application/vnd.ms-excel", fileName);
        }

        public IActionResult OnPostCancel()
        {
            return Redirect("~/Menu");
        }
    }
}
