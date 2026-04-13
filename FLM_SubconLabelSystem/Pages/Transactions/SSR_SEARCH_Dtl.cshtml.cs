using System;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class SSR_SEARCH_DtlModel : PageModel
    {
        public string StartupScript { get; set; } = string.Empty;
        public string TitleSuffix { get; set; } = string.Empty;

        // Header fields
        public string CompCode { get; set; } = string.Empty;
        public string RefNo { get; set; } = string.Empty;
        public string Dept { get; set; } = string.Empty;
        public string DateReq { get; set; } = string.Empty;
        public string By { get; set; } = string.Empty;
        public string Rev { get; set; } = string.Empty;

        // Status
        [BindProperty]
        public string ReqStatSelected { get; set; } = "Submitted";
        [BindProperty]
        public string VenStatSelected { get; set; } = "N/A";
        public bool ShowReqStatDdl { get; set; } = true;
        public bool ShowVenStatDdl { get; set; } = true;
        public bool ReqStatEnabled { get; set; } = true;
        public bool VenStatEnabled { get; set; } = true;
        public string ReqStatLabel { get; set; } = string.Empty;
        public string VenStatLabel { get; set; } = string.Empty;

        // Grid
        public DataTable GridData { get; set; }
        public string MQty { get; set; } = "0";
        public string MTotalWeight { get; set; } = "0";
        public string CQty { get; set; } = "0";
        public string CTotalWeight { get; set; } = "0";
        public string SubSlitWaste { get; set; } = "0";

        // Buttons visibility
        public bool ShowUpdStatButton { get; set; } = false;
        public bool ShowNewRevButton { get; set; } = false;
        public bool ShowExportButton { get; set; } = false;
        public bool ShowCancelButton { get; set; } = false;

        // Delete mode
        public bool IsDeleteMode { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string UpdatedDate { get; set; } = string.Empty;
        [BindProperty]
        public string DeleteConfirm { get; set; } = "No";

        // Query string values
        public string Itm1 { get; set; } = string.Empty;
        public string Itm2 { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        private string CurrentUserId => HttpContext.Session.GetString("gstrUserID") ?? HttpContext.Session.GetString("USERID") ?? string.Empty;
        private string CurrentUserIp => HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        private string CurrentCompanyCode => HttpContext.Session.GetString("gstrUserComp") ?? HttpContext.Session.GetString("COMPANYCODE") ?? string.Empty;

        private string Compile2OneDecimal(string pstrValue)
        {
            return Decimal.Round(Convert.ToDecimal(pstrValue), 1).ToString("#,###,###,##0.0");
        }

        private void ParseQS()
        {
            Itm1 = Request.Query.ContainsKey("itm1") ? Request.Query["itm1"].ToString().Trim() : string.Empty;
            Itm2 = Request.Query.ContainsKey("itm2") ? Request.Query["itm2"].ToString().Trim() : string.Empty;
            Id = Request.Query.ContainsKey("id") ? Request.Query["id"].ToString().Trim() : string.Empty;

            // On POST, fall back to hidden form fields when query string values are missing
            if (Request.Method == "POST" && Request.HasFormContentType)
            {
                if (string.IsNullOrEmpty(Itm1) && Request.Form.ContainsKey("itm1"))
                    Itm1 = Request.Form["itm1"].ToString().Trim();
                if (string.IsNullOrEmpty(Itm2) && Request.Form.ContainsKey("itm2"))
                    Itm2 = Request.Form["itm2"].ToString().Trim();
                if (string.IsNullOrEmpty(Id) && Request.Form.ContainsKey("id"))
                    Id = Request.Form["id"].ToString().Trim();
            }
        }

        private void LoadData()
        {
            if (string.IsNullOrEmpty(Itm1) && string.IsNullOrEmpty(Itm2) && !string.IsNullOrEmpty(Id))
            {
                // Delete mode
                IsDeleteMode = true;
                int rIdSsr = Convert.ToInt32(Id);
                DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(string.Empty, rIdSsr);

                CompCode = dtSsr.Rows[0]["COMPANYTO"].ToString();
                RefNo = dtSsr.Rows[0]["REFNO"].ToString();
                DateReq = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"].ToString()).ToString("dd/MM/yyyy");
                Rev = dtSsr.Rows[0]["REVISIONCOUNT"].ToString();
                ReqStatLabel = dtSsr.Rows[0]["REQUEST_STATUS"].ToString();
                VenStatLabel = dtSsr.Rows[0]["VENDOR_STATUS"].ToString();
                ShowReqStatDdl = false;
                ShowVenStatDdl = false;
                Dept = dtSsr.Rows[0]["REQUEST_BY"].ToString();
                By = dtSsr.Rows[0]["DEPARTMENT"].ToString();

                DisplaySSRListing(RefNo, rIdSsr.ToString(), true);
                TitleSuffix = " Delete";
            }
            else if (!string.IsNullOrEmpty(Itm1) || !string.IsNullOrEmpty(Itm2))
            {
                // View/Edit mode
                IsDeleteMode = false;
                string rRefno = Itm1;
                int rIdSsr = Convert.ToInt32(Itm2);
                DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(rRefno, rIdSsr);

                CompCode = dtSsr.Rows[0]["COMPANYTO"].ToString();
                RefNo = dtSsr.Rows[0]["REFNO"].ToString();
                DateReq = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"].ToString()).ToString("dd/MM/yyyy");
                Rev = dtSsr.Rows[0]["REVISIONCOUNT"].ToString();
                Dept = dtSsr.Rows[0]["REQUEST_BY"].ToString();
                By = dtSsr.Rows[0]["DEPARTMENT"].ToString();

                ReqStatSelected = dtSsr.Rows[0]["REQUEST_STATUS"].ToString();
                VenStatSelected = dtSsr.Rows[0]["VENDOR_STATUS"].ToString();

                if (ReqStatSelected == "Cancel")
                {
                    VenStatEnabled = false;
                    ReqStatEnabled = false;
                    ShowUpdStatButton = false;
                }

                string ulevel = HttpContext.Session.GetString("ULEVEL") ?? "";
                if (ulevel == "3")
                {
                    ReqStatEnabled = false;
                }
                else
                {
                    VenStatEnabled = false;
                }

                DisplaySSRListing(rRefno, Itm2, false);

                if (ReqStatSelected != "Cancel")
                {
                    ShowUpdStatButton = true;
                }

                if (ulevel == "3")
                {
                    ShowNewRevButton = false;
                    ShowExportButton = true;
                }
                else
                {
                    ShowNewRevButton = true;
                    ShowExportButton = true;
                }
                ShowCancelButton = true;

                TitleSuffix = " Received";
            }
        }

        private void DisplaySSRListing(string rRefno, string rIdSsr, bool showAudit)
        {
            DataTable dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(rRefno, Convert.ToInt32(rIdSsr));

            if (dtSSRList.Rows.Count > 0)
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

                string prevPc2Mother = string.Empty;
                string prevPc1Mother = string.Empty;
                string prevProdLineNo = string.Empty;
                string prevSeqMother = string.Empty;

                int cQty = 0; decimal cTotalWeight = 0;
                int mQty = 0; decimal mTotalWeight = 0;
                decimal subSlitWaste = 0;

                for (int i = 0; i < dtSSRList.Rows.Count; i++)
                {
                    bool sameGroup = dtSSRList.Rows[i]["PC2_MOTHER"].ToString() == prevPc2Mother
                        && dtSSRList.Rows[i]["PC1_MOTHER"].ToString() == prevPc1Mother
                        && dtSSRList.Rows[i]["PRODLINE_NO"].ToString() == prevProdLineNo
                        && dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString() == prevSeqMother;

                    if (sameGroup)
                    {
                        DataRow dr = dtWBlank.NewRow();
                        dr["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                        dr["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                        dr["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];
                        dr["C_WEIGHT"] = Compile2OneDecimal(dtSSRList.Rows[i]["C_WEIGHT"].ToString());
                        dr["C_TOTAL_WEIGHT"] = Compile2OneDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString());
                        dr["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                        dtWBlank.Rows.Add(dr);

                        cQty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                        cTotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(prevPc2Mother))
                        {
                            dtWBlank.Rows.Add(dtWBlank.NewRow()); // separator row
                        }

                        DataRow dr = dtWBlank.NewRow();
                        dr["PRODLINE_NO"] = dtSSRList.Rows[i]["PRODLINE_NO"];
                        dr["PC1_MOTHER"] = dtSSRList.Rows[i]["PC1_MOTHER"];
                        dr["PC2_MOTHER"] = dtSSRList.Rows[i]["PC2_MOTHER"];
                        dr["QTY"] = dtSSRList.Rows[i]["QTY"];
                        dr["M_WEIGHT"] = Compile2OneDecimal(dtSSRList.Rows[i]["M_WEIGHT"].ToString());
                        dr["M_TOTAL_WEIGHT"] = Compile2OneDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"].ToString());
                        dr["PC1_CUST"] = dtSSRList.Rows[i]["PC1_CUST"];
                        dr["PC2_CUST"] = dtSSRList.Rows[i]["PC2_CUST"];
                        dr["C_QTY"] = dtSSRList.Rows[i]["C_QTY"];
                        dr["C_WEIGHT"] = Compile2OneDecimal(dtSSRList.Rows[i]["C_WEIGHT"].ToString());
                        dr["C_TOTAL_WEIGHT"] = Compile2OneDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString());
                        dr["SUBSLIT_WASTE"] = Compile2OneDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"].ToString());
                        dr["ETD"] = Convert.ToDateTime(dtSSRList.Rows[i]["ETD"].ToString()).ToString("dd/MM/yyyy");
                        dr["ETA"] = Convert.ToDateTime(dtSSRList.Rows[i]["ETA"].ToString()).ToString("dd/MM/yyyy");
                        dr["REMARK"] = dtSSRList.Rows[i]["REMARK"];
                        dtWBlank.Rows.Add(dr);

                        mQty += Convert.ToInt32(dtSSRList.Rows[i]["QTY"]);
                        mTotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["M_TOTAL_WEIGHT"]);
                        subSlitWaste += Convert.ToDecimal(dtSSRList.Rows[i]["SUBSLIT_WASTE"]);
                        cQty += Convert.ToInt32(dtSSRList.Rows[i]["C_QTY"]);
                        cTotalWeight += Convert.ToDecimal(dtSSRList.Rows[i]["C_TOTAL_WEIGHT"]);
                    }

                    prevPc2Mother = dtSSRList.Rows[i]["PC2_MOTHER"].ToString();
                    prevPc1Mother = dtSSRList.Rows[i]["PC1_MOTHER"].ToString();
                    prevProdLineNo = dtSSRList.Rows[i]["PRODLINE_NO"].ToString();
                    prevSeqMother = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();

                    if (showAudit)
                    {
                        CreatedBy = dtSSRList.Rows[i]["CREATED_BY"].ToString();
                        CreatedDate = dtSSRList.Rows[i]["CREATED_DATE"].ToString();
                        UpdatedBy = dtSSRList.Rows[i]["UPDATED_BY"].ToString();
                        UpdatedDate = dtSSRList.Rows[i]["CREATED_DATE"].ToString();
                    }
                }

                MQty = mQty.ToString("#,###,###,##0.0");
                MTotalWeight = Compile2OneDecimal(mTotalWeight.ToString());
                CQty = cQty.ToString();
                CTotalWeight = Compile2OneDecimal(cTotalWeight.ToString());
                SubSlitWaste = Compile2OneDecimal(subSlitWaste.ToString());
                GridData = dtWBlank;
            }
        }

        public void OnGet()
        {
            ParseQS();
            LoadData();
        }

        public IActionResult OnPostUpdateStatus()
        {
            ParseQS();
            int rIdSsr = Convert.ToInt32(Itm2);

            LoadData();

            string temp = Library.Database.BLL.SubSlitRequest.SSRUpdateStat(
                RefNo, rIdSsr, ReqStatSelected, VenStatSelected, CurrentUserId, CurrentUserIp);

            if (temp == "1")
                StartupScript = "alert('The Status of Sub Slittng Request under " + RefNo + " is updated successfully.');";
            else
                StartupScript = "alert('" + (temp == "0" ? "Operation failed" : temp.Replace("'", "\\'")) + "');";

            if (ReqStatSelected == "Cancel")
            {
                VenStatEnabled = false;
                ReqStatEnabled = false;
            }

            return Page();
        }

        public IActionResult OnPostExport()
        {
            ParseQS();
            string rRefno = Itm1;
            int rIdSsr = Convert.ToInt32(Itm2);
            string userid = HttpContext.Session.GetString("USERID") ?? string.Empty;

            LoadData();

            string ssrStr = Library.Database.BLL.SubSlitRequest.GET_SSR_TO_EXCEL(
                rRefno, rIdSsr, userid,
                Convert.ToInt32(MQty.Replace(",", "")),
                Convert.ToDecimal(MTotalWeight.Replace(",", "")),
                Convert.ToInt32(CQty.Replace(",", "")),
                Convert.ToDecimal(CTotalWeight.Replace(",", "")),
                Convert.ToDecimal(SubSlitWaste.Replace(",", "")),
                GridData);

            string fileName = "SubSlittingRequest" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            byte[] bytes = Encoding.UTF8.GetBytes(ssrStr);
            return File(bytes, "application/vnd.ms-excel", fileName);
        }

        public IActionResult OnPostCancel()
        {
            return Redirect("~/Transactions/SSR_SEARCH");
        }

        public IActionResult OnPostNewRevision()
        {
            ParseQS();
            string rRefno = Itm1;
            int rIdSsr = Convert.ToInt32(Itm2);
            DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(rRefno, rIdSsr);

            int revCount = Convert.ToInt32(dtSsr.Rows[0]["REVISIONCOUNT"]) + 1;
            string transDate = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"].ToString()).ToString("MM/dd/yyyy");

            Library.Database.BLL.SubSlitRequest.SubSlitDup(
                "0", dtSsr.Rows[0]["COMPANYFROM"].ToString(),
                dtSsr.Rows[0]["COMPANYTO"].ToString(), dtSsr.Rows[0]["REFNO"].ToString(),
                revCount, transDate, "New", "N/A", 1, CurrentUserId, CurrentUserIp, CurrentCompanyCode);

            DataTable dtIdSsr = Library.Database.BLL.SubSlitRequest.GetIDSSR(dtSsr.Rows[0]["REFNO"].ToString(), revCount);
            int vIdSsr = 0;

            if (dtIdSsr.Rows.Count > 0)
                vIdSsr = Convert.ToInt32(dtIdSsr.Rows[0]["ID_SUBSLIT_REQUEST"]);
            else
            {
                LoadData();
                StartupScript = "alert('Error - Could not find Sub Slitting Request ID');";
                return Page();
            }

            DataTable dtSSRList = Library.Database.BLL.SubSlitRequest.SSRListExist(rRefno, rIdSsr);
            if (dtSSRList.Rows.Count > 0)
            {
                string prevSeqMother = string.Empty;
                int vMothSeq = 0;

                for (int i = 0; i < dtSSRList.Rows.Count; i++)
                {
                    if (dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString() == prevSeqMother)
                    {
                        Library.Database.BLL.SubSlitRequest.SubSlitChildDup(
                            vMothSeq, rRefno,
                            dtSSRList.Rows[i]["PC1_CUST"].ToString(),
                            dtSSRList.Rows[i]["PC2_CUST"].ToString(),
                            dtSSRList.Rows[i]["C_QTY"].ToString(),
                            dtSSRList.Rows[i]["C_WEIGHT"].ToString(),
                            dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString(),
                            dtSSRList.Rows[i]["REMARK"].ToString(), "0", 1, CurrentUserId, CurrentUserIp);
                    }
                    else
                    {
                        string vEtd = Convert.ToDateTime(dtSSRList.Rows[i]["ETD"].ToString()).ToString("MM/dd/yyyy");
                        string vEta = Convert.ToDateTime(dtSSRList.Rows[i]["ETA"].ToString()).ToString("MM/dd/yyyy");

                        string temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDup(
                            vIdSsr.ToString(), dtSSRList.Rows[i]["REFNO"].ToString(),
                            dtSSRList.Rows[i]["PC1_MOTHER"].ToString(),
                            dtSSRList.Rows[i]["PC2_MOTHER"].ToString(),
                            dtSSRList.Rows[i]["PRODLINE_NO"].ToString(),
                            dtSSRList.Rows[i]["QTY"].ToString(),
                            dtSSRList.Rows[i]["M_WEIGHT"].ToString(),
                            dtSSRList.Rows[i]["M_TOTAL_WEIGHT"].ToString(),
                            dtSSRList.Rows[i]["SUBSLIT_WASTE"].ToString(), vEtd, vEta, 1, CurrentUserId, CurrentUserIp);

                        if (int.TryParse(temp2, out int chkint) && temp2 != "0")
                        {
                            DataTable dtMothSeq = Library.Database.BLL.SubSlitRequest.GetMotherSeq(vIdSsr, temp2);
                            if (dtMothSeq.Rows.Count > 0)
                                vMothSeq = Convert.ToInt32(dtMothSeq.Rows[0]["SUBSLIT_REQ_MOTHER_SEQNO"]);
                        }

                        Library.Database.BLL.SubSlitRequest.SubSlitChildDup(
                            vMothSeq, rRefno,
                            dtSSRList.Rows[i]["PC1_CUST"].ToString(),
                            dtSSRList.Rows[i]["PC2_CUST"].ToString(),
                            dtSSRList.Rows[i]["C_QTY"].ToString(),
                            dtSSRList.Rows[i]["C_WEIGHT"].ToString(),
                            dtSSRList.Rows[i]["C_TOTAL_WEIGHT"].ToString(),
                            dtSSRList.Rows[i]["REMARK"].ToString(), "0", 1, CurrentUserId, CurrentUserIp);
                    }

                    prevSeqMother = dtSSRList.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();
                }

                return Redirect("~/Transactions/SUBSLIT_REQ_?itm1=" + rRefno + "&itm2= " + vIdSsr);
            }

            LoadData();
            StartupScript = "alert('No Sub Slitting Request records found.');";
            return Page();
        }

        public IActionResult OnPostDeleteConfirm()
        {
            ParseQS();
            if (DeleteConfirm == "Yes")
            {
                string temp = Library.Database.BLL.SubSlitRequest.SubSlitMaint(
                    Id, string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, 5, CurrentUserId, CurrentUserIp, CurrentCompanyCode);

                if (temp == "1")
                    return Redirect("~/Transactions/SSR_SEARCH");
            }
            else
            {
                StartupScript = "alert('Please Choose Yes to confirm delete');";
            }

            LoadData();
            return Page();
        }
    }
}
