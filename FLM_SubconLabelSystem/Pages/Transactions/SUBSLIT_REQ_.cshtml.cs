using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class ChildRowModel
    {
        public string PC1Cust { get; set; } = string.Empty;
        public string PC2Cust { get; set; } = string.Empty;
        public string CQty { get; set; } = string.Empty;
        public string CWeight { get; set; } = string.Empty;
        public string CTotalWeight { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class SUBSLIT_REQ_Model : PageModel
    {
        // ?? Form fields ??????????????????????????????????????????????
        [BindProperty] public string CompCodeValue { get; set; } = "0";
        [BindProperty] public string RefNo { get; set; } = string.Empty;
        [BindProperty] public string TransDate { get; set; } = string.Empty;
        [BindProperty] public string ProdLine { get; set; } = string.Empty;
        [BindProperty] public string PC1Value { get; set; } = string.Empty;
        [BindProperty] public string PC2Value { get; set; } = string.Empty;
        [BindProperty] public string Qty { get; set; } = string.Empty;
        [BindProperty] public string UnitWeight { get; set; } = string.Empty;
        [BindProperty] public string TotWeight { get; set; } = string.Empty;
        [BindProperty] public string SubSlitWaste { get; set; } = string.Empty;
        [BindProperty] public string ETD { get; set; } = string.Empty;
        [BindProperty] public string ETA { get; set; } = string.Empty;
        [BindProperty] public string RevisionCount { get; set; } = "0";
        [BindProperty] public string ReqStatus { get; set; } = "New";
        [BindProperty] public string VenStatus { get; set; } = "N/A";
        [BindProperty] public string SeqMother { get; set; } = string.Empty;
        [BindProperty] public string EditFlag { get; set; } = "N";
        [BindProperty] public string InpHide { get; set; } = "0";
        [BindProperty] public string SelectedSeqMother { get; set; } = string.Empty;
        [BindProperty] public List<ChildRowModel> ChildRows { get; set; }

        // ?? Display properties ???????????????????????????????????????
        public string Department { get; set; } = string.Empty;
        public string ByUser { get; set; } = string.Empty;
        public bool ShowChildPanel { get; set; }
        public bool ShowListPanel { get; set; }
        public bool ShowSubmitCancel { get; set; }
        public bool ShowMotherPanel { get; set; } = true;
        public DataTable ListData { get; set; }
        public List<SelectListItem> CompCodeItems { get; set; } = new();
        public string[] ProdLineAutoComplete { get; set; } = Array.Empty<string>();
        public string[] PC1AutoComplete { get; set; } = Array.Empty<string>();
        public string[] PC2AutoComplete { get; set; } = Array.Empty<string>();
        public string[] PC1ChildAutoComplete { get; set; } = Array.Empty<string>();
        public string[] PC2ChildAutoComplete { get; set; } = Array.Empty<string>();
        public string StartupScript { get; set; } = string.Empty;

        private string CurrentUserId => HttpContext.Session.GetString("gstrUserID") ?? HttpContext.Session.GetString("USERID") ?? string.Empty;
        private string CurrentUserIp => HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        private string CurrentCompanyCode => HttpContext.Session.GetString("gstrUserComp") ?? HttpContext.Session.GetString("COMPANYCODE") ?? string.Empty;

        // ?? Lifecycle ????????????????????????????????????????????????
        private void LoadCommon()
        {
            // Company dropdown
            CompCodeItems = new List<SelectListItem> { new("--Select--", "0") };
            DataTable dtComp = Library.Database.BLL.SubSlitRequest.GetDLLData("CompanyCode", "");
            foreach (DataRow r in dtComp.Rows)
                CompCodeItems.Add(new SelectListItem(r["CompanyCode"].ToString(), r["ID_MM_COMPANY"].ToString()));

            // Autocomplete data
            DataTable dtProd = Library.Database.BLL.SubSlitRequest.GetDLLData("PRODLINE", "");
            ProdLineAutoComplete = dtProd.AsEnumerable().Select(r => "\"" + r["PRODLINE_NO"] + "\"").ToArray();

            DataTable dtPC1 = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "");
            PC1AutoComplete = dtPC1.AsEnumerable().Select(r => "\"" + r["PC1"] + "\"").ToArray();

            DataTable dtPC2 = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "");
            PC2AutoComplete = dtPC2.AsEnumerable().Select(r => "\"" + r["PC2"] + "\"").ToArray();

            DataTable dtPC1C = Library.Database.BLL.SubSlitRequest.GetDLLData("PC1", "");
            PC1ChildAutoComplete = dtPC1C.AsEnumerable().Select(r => "\"" + r["PC1"] + "\"").ToArray();

            DataTable dtPC2C = Library.Database.BLL.SubSlitRequest.GetDLLData("PC2", "");
            PC2ChildAutoComplete = dtPC2C.AsEnumerable().Select(r => "\"" + r["PC2"] + "\"").ToArray();

            // User info
            string userId = HttpContext.Session.GetString("USERID") ?? string.Empty;
            DataTable dtUser = Library.Database.BLL.SubSlitRequest.GetUserData(userId);
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                Department = dtUser.Rows[0]["DEPARTMENT"].ToString();
                ByUser = dtUser.Rows[0]["NAME"].ToString();
            }

            // Restore child rows from session
            if (ChildRows == null)
            {
                string json = HttpContext.Session.GetString("SUBSLIT_ChildRows");
                if (!string.IsNullOrEmpty(json))
                    ChildRows = JsonSerializer.Deserialize<List<ChildRowModel>>(json);
            }

            ShowChildPanel = HttpContext.Session.GetString("SUBSLIT_ShowChild") == "1";
            ShowListPanel = HttpContext.Session.GetString("SUBSLIT_ShowList") == "1";
            ShowSubmitCancel = HttpContext.Session.GetString("SUBSLIT_ShowSubmit") == "1";
            ShowMotherPanel = HttpContext.Session.GetString("SUBSLIT_ShowMother") != "0";

            // Restore list data
            string listJson = HttpContext.Session.GetString("SUBSLIT_ListData");
            if (!string.IsNullOrEmpty(listJson))
            {
                // Deserialize as list of dictionaries and rebuild DataTable
                var rows = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(listJson);
                ListData = RebuildDataTable(rows);
            }
        }

        private void SaveChildToSession()
        {
            if (ChildRows != null)
                HttpContext.Session.SetString("SUBSLIT_ChildRows", JsonSerializer.Serialize(ChildRows));
            else
                HttpContext.Session.Remove("SUBSLIT_ChildRows");
        }

        private void SaveListToSession()
        {
            if (ListData != null)
            {
                var rows = new List<Dictionary<string, object>>();
                foreach (DataRow dr in ListData.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn col in ListData.Columns)
                        dict[col.ColumnName] = dr[col] == DBNull.Value ? null : dr[col];
                    rows.Add(dict);
                }
                HttpContext.Session.SetString("SUBSLIT_ListData", JsonSerializer.Serialize(rows));
            }
            else
                HttpContext.Session.Remove("SUBSLIT_ListData");
        }

        private static DataTable RebuildDataTable(List<Dictionary<string, object>> rows)
        {
            if (rows == null || rows.Count == 0) return new DataTable();
            var dt = new DataTable();
            string[] colNames = { "REFNO","PRODLINE_NO","PC1_MOTHER","PC2_MOTHER","QTY","M_WEIGHT","M_TOTAL_WEIGHT","PC1_CUST","PC2_CUST","C_QTY","C_WEIGHT","C_TOTAL_WEIGHT","SUBSLIT_WASTE","ETD","ETA","REMARK","SEQ","CHK" };
            foreach (string c in colNames) dt.Columns.Add(c, typeof(string));
            foreach (var dict in rows)
            {
                DataRow dr = dt.NewRow();
                foreach (string c in colNames)
                    dr[c] = dict.ContainsKey(c) && dict[c] != null ? dict[c].ToString() : "";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void SetPanelVisibility(bool showChild, bool showList, bool showSubmitCancel)
        {
            ShowChildPanel = showChild;
            ShowListPanel = showList;
            ShowSubmitCancel = showSubmitCancel;
            // Hide the mother roll description section when the listing is showing
            // and the child panel is not active (i.e. not in edit/add-child mode).
            ShowMotherPanel = !(showList && !showChild);
            HttpContext.Session.SetString("SUBSLIT_ShowChild", showChild ? "1" : "0");
            HttpContext.Session.SetString("SUBSLIT_ShowList", showList ? "1" : "0");
            HttpContext.Session.SetString("SUBSLIT_ShowSubmit", showSubmitCancel ? "1" : "0");
            HttpContext.Session.SetString("SUBSLIT_ShowMother", ShowMotherPanel ? "1" : "0");
        }

        // ?? Lifecycle ????????????????????????????????????????????????
        public IActionResult OnGet()
        {
            string itm1 = Request.Query.ContainsKey("itm1") ? Request.Query["itm1"].ToString().Trim() : null;
            string itm2 = Request.Query.ContainsKey("itm2") ? Request.Query["itm2"].ToString().Trim() : null;

            // Clear session state for fresh start
            HttpContext.Session.Remove("SUBSLIT_ChildRows");
            HttpContext.Session.Remove("SUBSLIT_ListData");

            if (string.IsNullOrEmpty(itm1) || string.IsNullOrEmpty(itm2))
            {
                SetPanelVisibility(false, false, false);
            }
            else
            {
                string decodedRefNo = System.Net.WebUtility.HtmlDecode(itm1);
                int idSsr = Convert.ToInt32(itm2);
                DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(decodedRefNo, idSsr);

                if (dtSsr != null && dtSsr.Rows.Count > 0)
                {
                    CompCodeValue = dtSsr.Rows[0]["COMPTOID"].ToString();
                    RefNo = dtSsr.Rows[0]["REFNO"].ToString();
                    TransDate = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"]).ToString("dd/MM/yyyy");
                    RevisionCount = dtSsr.Rows[0]["REVISIONCOUNT"].ToString();
                    VenStatus = dtSsr.Rows[0]["VENDOR_STATUS"].ToString();

                    DisplaySSRListing(RefNo);
                    SetPanelVisibility(false, true, true);
                    SaveListToSession();
                }
            }

            LoadCommon();
            return Page();
        }

        public IActionResult OnPostNext()
        {
            LoadCommon();

            if (CompCodeValue == "0") { StartupScript = "alert('Please Select To which Company.');"; return Page(); }
            if (string.IsNullOrEmpty(RefNo)) { StartupScript = "alert('Please Insert Ref No.');"; return Page(); }
            if (string.IsNullOrEmpty(TransDate)) { StartupScript = "alert('Please Select Date.');"; return Page(); }
            if (string.IsNullOrEmpty(ProdLine)) { StartupScript = "alert('Please Select Production Line.');"; return Page(); }
            if (string.IsNullOrEmpty(PC1Value)) { StartupScript = "alert('Please Select PC1.');"; return Page(); }
            if (string.IsNullOrEmpty(PC2Value)) { StartupScript = "alert('Please Select PC2.');"; return Page(); }

            string chk = Library.Database.BLL.SubSlitRequest.Chk_next(ProdLine, PC1Value);
            if (chk == "1") { StartupScript = "alert('Invalid PC1 Mother');"; PC1Value = ""; return Page(); }
            if (chk == "2") { StartupScript = "alert('Invalid Production Line No');"; return Page(); }
            if (chk != "0") { StartupScript = "alert('Invalid Production Line No and PC1 Mother');"; return Page(); }

            if (string.IsNullOrEmpty(Qty) || Qty == "0" || !decimal.TryParse(Qty, out _))
            { StartupScript = "alert('Please enter Qty more than 0.');"; return Page(); }
            if (string.IsNullOrEmpty(ETD)) { StartupScript = "alert('Please Select ETD PFR Date.');"; return Page(); }
            if (string.IsNullOrEmpty(ETA)) { StartupScript = "alert('Please Select ETA SUB-SLIT CONTRACTOR.');"; return Page(); }

            CalculateMotherWeight();

            ChildRows = new List<ChildRowModel> { new ChildRowModel() };
            SaveChildToSession();
            SetPanelVisibility(true, ShowListPanel, ShowSubmitCancel);

            return Page();
        }

        public IActionResult OnPostReset()
        {
            HttpContext.Session.Remove("SUBSLIT_ChildRows");
            HttpContext.Session.Remove("SUBSLIT_ListData");
            SetPanelVisibility(false, false, false);
            return RedirectToPage();
        }

        public IActionResult OnPostAddRow()
        {
            LoadCommon();
            CalculateMotherWeight();
            RecalcChildRows();

            if (ChildRows == null) ChildRows = new List<ChildRowModel>();
            ChildRows.Add(new ChildRowModel());
            SaveChildToSession();
            SetPanelVisibility(true, ShowListPanel, ShowSubmitCancel);

            return Page();
        }

        public IActionResult OnPostDeleteRow(int deleteIndex)
        {
            LoadCommon();
            CalculateMotherWeight();

            if (ChildRows != null && deleteIndex >= 0 && deleteIndex < ChildRows.Count)
            {
                ChildRows.RemoveAt(deleteIndex);
                if (ChildRows.Count == 0) ChildRows = null;
            }
            RecalcChildRows();
            SaveChildToSession();
            SetPanelVisibility(ChildRows != null, ShowListPanel, ShowSubmitCancel);

            return Page();
        }

        public IActionResult OnPostCalculate()
        {
            LoadCommon();
            CalculateMotherWeight();
            RecalcChildRows();
            SaveChildToSession();
            return Page();
        }

        public IActionResult OnPostChildCalculate()
        {
            LoadCommon();
            CalculateMotherWeight();
            RecalcChildRows();
            SaveChildToSession();
            return Page();
        }

        public IActionResult OnPostSave()
        {
            LoadCommon();
            CalculateMotherWeight();
            RecalcChildRows();

            if (!CheckRefNo()) return Page();

            double subSlitVal;
            if (double.TryParse((SubSlitWaste ?? "0").Replace(",", ""), out subSlitVal) && subSlitVal < 0)
            {
                StartupScript = "showDialogue('" + SubSlitWaste + "');";
                SetPanelVisibility(true, ShowListPanel, ShowSubmitCancel);
                SaveChildToSession();
                return Page();
            }

            DoSave();
            return Page();
        }

        public IActionResult OnPostConfirmSave()
        {
            LoadCommon();
            CalculateMotherWeight();
            RecalcChildRows();

            if (InpHide == "1")
                DoSave();

            return Page();
        }

        public IActionResult OnPostEdit()
        {
            LoadCommon();

            if (string.IsNullOrEmpty(SelectedSeqMother))
            {
                StartupScript = "alert('Please click the required PC2 Mother/Child for edit.');";
                return Page();
            }

            string[] parts = SelectedSeqMother.Split('|');
            string seqMother = parts.Length > 0 ? parts[0] : "";

            EditFlag = "Y";
            SeqMother = seqMother;

            DataTable dtSSR = Library.Database.BLL.SubSlitRequest.SSRList(RefNo, seqMother);
            if (dtSSR.Rows.Count > 0)
            {
                var firstRow = dtSSR.Rows[0];
                ProdLine = firstRow["PRODLINE_NO"].ToString();
                PC1Value = firstRow["PC1_MOTHER"].ToString();
                PC2Value = firstRow["PC2_MOTHER"].ToString();
                Qty = firstRow["QTY"].ToString();
                UnitWeight = Convert.ToDecimal(firstRow["M_WEIGHT"]).ToString("#,###,###,##0.0");
                TotWeight = Convert.ToDecimal(firstRow["M_TOTAL_WEIGHT"]).ToString("#,###,###,##0.0");
                SubSlitWaste = Convert.ToDecimal(firstRow["SUBSLIT_WASTE"]).ToString("#,###,###,##0.0");
                ETD = Convert.ToDateTime(firstRow["ETD"]).ToString("dd/MM/yyyy");
                ETA = Convert.ToDateTime(firstRow["ETA"]).ToString("dd/MM/yyyy");

                ChildRows = new List<ChildRowModel>();
                for (int i = 0; i < dtSSR.Rows.Count; i++)
                {
                    var row = dtSSR.Rows[i];
                    ChildRows.Add(new ChildRowModel
                    {
                        PC1Cust = row["PC1_CUST"].ToString(),
                        PC2Cust = row["PC2_CUST"].ToString(),
                        CQty = row["C_QTY"].ToString(),
                        CWeight = Convert.ToDecimal(row["C_WEIGHT"]).ToString("#,###,###,##0.0"),
                        CTotalWeight = Convert.ToDecimal(row["C_TOTAL_WEIGHT"]).ToString("#,###,###,##0.0"),
                        Remark = row["REMARK"].ToString()
                    });
                }
            }

            SaveChildToSession();
            SetPanelVisibility(true, true, ShowSubmitCancel);

            return Page();
        }

        public IActionResult OnPostDelete()
        {
            LoadCommon();

            if (string.IsNullOrEmpty(SelectedSeqMother))
            {
                StartupScript = "alert('Please click radio button for the required PC2 Mother/Child.');";
                return Page();
            }

            string[] parts = SelectedSeqMother.Split('|');
            string seqMother = parts.Length > 0 ? parts[0] : "";
            string pc2Mother = parts.Length > 1 ? parts[1] : "";
            string pc1Mother = parts.Length > 2 ? parts[2] : "";
            string prodLineNo = parts.Length > 3 ? parts[3] : "";

            string temp1 = Library.Database.BLL.SubSlitRequest.SubSlitChildDelFrList(RefNo, pc2Mother, pc1Mother, prodLineNo, seqMother, 2, CurrentUserId, CurrentUserIp);
            if (temp1 == "1")
            {
                string temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherDel(RefNo, pc2Mother, pc1Mother, prodLineNo, seqMother, 2, CurrentUserId, CurrentUserIp);
                if (temp2 == "1")
                    StartupScript = "alert('This selected PC2 Mother/Child " + pc2Mother.Replace("'", "\\'") + " is deleted successfully.');";
                else
                    StartupScript = "alert('" + (temp2 == "0" ? "Delete failed" : temp2.Replace("'", "\\'")) + "');";
            }
            else
            {
                StartupScript = "alert('" + (temp1 == "0" ? "Delete failed" : temp1.Replace("'", "\\'")) + "');";
            }

            DisplaySSRListing(RefNo);
            bool hasData = ListData != null && ListData.Rows.Count > 0;
            SetPanelVisibility(false, hasData, hasData);
            SaveListToSession();

            return Page();
        }

        public IActionResult OnPostSubmit()
        {
            LoadCommon();

            DataTable dtR = Library.Database.BLL.SubSlitRequest.CHECK_SUBMITTED_REQ(RefNo, Convert.ToInt32(RevisionCount));
            if (dtR.Rows.Count > 0)
            {
                StartupScript = "alert('This RefNo " + RefNo + " and Revision " + RevisionCount + " exists in the system.');";
                return Page();
            }

            string result = Library.Database.BLL.SubSlitRequest.UpdateReq(RefNo, Convert.ToInt32(RevisionCount), CurrentUserId, CurrentUserIp);
            if (result == "1")
                StartupScript = "alert('This RefNo " + RefNo + " and Revision " + RevisionCount + " is submitted successfully.');";
            else
                StartupScript = "alert('" + (result == "0" ? "Submit failed" : result.Replace("'", "\\'")) + "');";

            HttpContext.Session.Remove("SUBSLIT_ChildRows");
            HttpContext.Session.Remove("SUBSLIT_ListData");
            SetPanelVisibility(false, false, false);

            RefNo = ""; CompCodeValue = "0"; TransDate = ""; ProdLine = ""; PC1Value = ""; PC2Value = "";
            Qty = ""; UnitWeight = ""; TotWeight = ""; SubSlitWaste = ""; ETD = ""; ETA = "";
            RevisionCount = "0";

            return Page();
        }

        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("SUBSLIT_ChildRows");
            HttpContext.Session.Remove("SUBSLIT_ListData");
            SetPanelVisibility(false, false, false);

            string itm1 = Request.Query.ContainsKey("itm1") ? Request.Query["itm1"].ToString() : null;
            if (!string.IsNullOrEmpty(itm1))
                return RedirectToPage("/Transactions/SSR_SEARCH");

            return Page();
        }

        // ?? Helper methods ???????????????????????????????????????????

        private void CalculateMotherWeight()
        {
            if (!string.IsNullOrEmpty(PC2Value))
            {
                DataTable dtPC2 = Library.Database.BLL.SubSlitRequest.GetPC2ID(PC2Value);
                if (dtPC2 != null && dtPC2.Rows.Count > 0)
                {
                    string pc2Id = dtPC2.Rows[0]["ID_MM_PC2"].ToString();
                    DataTable dtUW = Library.Database.BLL.SubSlitRequest.GetPC2Data(pc2Id);
                    if (dtUW != null && dtUW.Rows.Count > 0)
                    {
                        decimal unitW = Convert.ToDecimal(dtUW.Rows[0]["UNIT_WEIGHT"]);
                        UnitWeight = unitW.ToString("#,###,###,##0.0");
                    }
                }
            }

            if (decimal.TryParse(Qty, out decimal qty) && decimal.TryParse(UnitWeight?.Replace(",", ""), out decimal uw))
            {
                TotWeight = Math.Round(qty * uw, 1).ToString("#,###,###,##0.0");
            }
        }

        private void RecalcChildRows()
        {
            if (ChildRows == null) return;

            decimal momSubslit = 0;
            if (decimal.TryParse(TotWeight?.Replace(",", ""), out decimal totW))
                momSubslit = totW;

            foreach (var row in ChildRows)
            {
                if (!string.IsNullOrEmpty(row.PC2Cust))
                {
                    DataTable dtPC2 = Library.Database.BLL.SubSlitRequest.GetPC2IDData(row.PC2Cust);
                    if (dtPC2 != null && dtPC2.Rows.Count > 0)
                    {
                        string id = dtPC2.Rows[0]["ID_MM_PC2"].ToString();
                        DataTable dtUW = Library.Database.BLL.SubSlitRequest.GetPC2Data(id);
                        if (dtUW != null && dtUW.Rows.Count > 0)
                        {
                            decimal cUnitW = Convert.ToDecimal(dtUW.Rows[0]["UNIT_WEIGHT"]);
                            row.CWeight = cUnitW.ToString("#,###,###,##0.0");
                        }
                    }
                }

                if (decimal.TryParse(row.CQty, out decimal cQty) && decimal.TryParse(row.CWeight?.Replace(",", ""), out decimal cUW))
                {
                    decimal cTot = Math.Round(cQty * cUW, 1);
                    row.CTotalWeight = cTot.ToString("#,###,###,##0.0");
                    momSubslit -= cTot;
                }
            }

            SubSlitWaste = momSubslit.ToString("#,###,###,##0.0");
        }

        private bool CheckRefNo()
        {
            if (string.IsNullOrEmpty(RefNo))
            {
                StartupScript = "alert('Ref No cannot be empty.');";
                return false;
            }

            DataTable dtMax = Library.Database.BLL.SubSlitRequest.chkRefNo(RefNo);
            if (dtMax != null && dtMax.Rows.Count > 0)
            {
                if (dtMax.Rows[0]["REQUEST_STATUS"].ToString() != "New")
                {
                    StartupScript = "alert('This Refno is already submitted/cancelled. Please re-enter new RefNo');";
                    RefNo = "";
                    return false;
                }
                else
                {
                    DataTable dtSsr = Library.Database.BLL.SubSlitRequest.GetSSR_INFO(RefNo, Convert.ToInt32(dtMax.Rows[0]["ID_SUBSLIT_REQUEST"]));
                    if (dtSsr != null && dtSsr.Rows.Count > 0)
                    {
                        CompCodeValue = dtSsr.Rows[0]["COMPTOID"].ToString();
                        TransDate = Convert.ToDateTime(dtSsr.Rows[0]["DATEREQ"]).ToString("dd/MM/yyyy");
                        RevisionCount = dtSsr.Rows[0]["REVISIONCOUNT"].ToString();
                        VenStatus = dtSsr.Rows[0]["VENDOR_STATUS"].ToString();
                    }
                }
            }
            return true;
        }

        private void DoSave()
        {
            string transDate = TransDate.Substring(6, 4) + "-" + TransDate.Substring(3, 2) + "-" + TransDate.Substring(0, 2);
            string temp = Library.Database.BLL.SubSlitRequest.SubSlitMaint("0", CompCodeValue, CompCodeValue, RefNo, RevisionCount, transDate, ReqStatus, VenStatus, 1, CurrentUserId, CurrentUserIp, CurrentCompanyCode);

            if (temp != "1")
            {
                StartupScript = "alert('" + (temp == "0" ? "Operation failed" : temp.Replace("'", "\\'")) + "');";
                return;
            }

            string etdDate = ETD.Substring(6, 4) + "-" + ETD.Substring(3, 2) + "-" + ETD.Substring(0, 2);
            string etaDate = ETA.Substring(6, 4) + "-" + ETA.Substring(3, 2) + "-" + ETA.Substring(0, 2);

            DataTable dtProd = Library.Database.BLL.SubSlitRequest.GetProdlineIDData(ProdLine);
            DataTable dtPC1 = Library.Database.BLL.SubSlitRequest.GetPC1IDData(PC1Value);
            DataTable dtPC2 = Library.Database.BLL.SubSlitRequest.GetPC2IDData(PC2Value);

            if (dtProd == null || dtProd.Rows.Count == 0)
            {
                StartupScript = "alert('Invalid Production Line. No matching record found.');";
                return;
            }
            if (dtPC1 == null || dtPC1.Rows.Count == 0)
            {
                StartupScript = "alert('Invalid PC1. No matching record found.');";
                return;
            }
            if (dtPC2 == null || dtPC2.Rows.Count == 0)
            {
                StartupScript = "alert('Invalid PC2. No matching record found.');";
                return;
            }

            string prodLineId = dtProd.Rows[0]["ID_MM_PRODLINE"].ToString();
            string pc1Id = dtPC1.Rows[0]["ID_MM_PC1"].ToString();
            string pc2Id = dtPC2.Rows[0]["ID_MM_PC2"].ToString();

            string temp2;
            if (EditFlag == "Y")
            {
                temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherMaint(SeqMother, RefNo, pc1Id, pc2Id, prodLineId, Qty,
                    UnitWeight.Replace(",", ""), TotWeight.Replace(",", ""), SubSlitWaste.Replace(",", ""), etdDate, etaDate, 3, CurrentUserId, CurrentUserIp);
            }
            else
            {
                temp2 = Library.Database.BLL.SubSlitRequest.SubSlitMotherMaint("0", RefNo, pc1Id, pc2Id, prodLineId, Qty,
                    UnitWeight.Replace(",", ""), TotWeight.Replace(",", ""), SubSlitWaste.Replace(",", ""), etdDate, etaDate, 1, CurrentUserId, CurrentUserIp);
            }

            if (int.TryParse(temp2, out int chk) && temp2 != "0")
            {
                if (EditFlag == "Y")
                {
                    string temp4 = Library.Database.BLL.SubSlitRequest.SubSlitChildDel(RefNo, SeqMother, pc2Id, 2, CurrentUserId, CurrentUserIp);
                    if (temp4 != "1")
                    {
                        StartupScript = "alert('" + (temp4 == "0" ? "Delete child failed" : temp4.Replace("'", "\\'")) + "');";
                        return;
                    }
                }

                InsertChildren(temp2, pc2Id);

                DisplaySSRListing(RefNo);
                SetPanelVisibility(false, true, true);
                SaveListToSession();

                ChildRows = null;
                SaveChildToSession();
                EditFlag = "N";

                ProdLine = ""; PC1Value = ""; PC2Value = ""; Qty = "";
                UnitWeight = ""; TotWeight = ""; SubSlitWaste = "";
                ETD = ""; ETA = "";
            }
            else
            {
                StartupScript = "alert('" + (temp2 == "0" ? "Operation failed" : temp2.Replace("'", "\\'")) + "');";
            }
        }

        private void InsertChildren(string seqMother, string motherPC2Id)
        {
            if (ChildRows == null) return;

            string result = "0";
            foreach (var row in ChildRows)
            {
                if (string.IsNullOrEmpty(row.PC1Cust)) break;

                DataTable dtPC1C = Library.Database.BLL.SubSlitRequest.GetPC1IDData(row.PC1Cust);
                DataTable dtPC2C = Library.Database.BLL.SubSlitRequest.GetPC2IDData(row.PC2Cust);

                if (dtPC1C == null || dtPC1C.Rows.Count == 0)
                {
                    StartupScript = "alert('Invalid Child PC1: " + (row.PC1Cust ?? "").Replace("'", "\\'") + ". No matching record found.');";
                    return;
                }
                if (dtPC2C == null || dtPC2C.Rows.Count == 0)
                {
                    StartupScript = "alert('Invalid Child PC2: " + (row.PC2Cust ?? "").Replace("'", "\\'") + ". No matching record found.');";
                    return;
                }

                string pc1ChildId = dtPC1C.Rows[0]["ID_MM_PC1"].ToString();
                string pc2ChildId = dtPC2C.Rows[0]["ID_MM_PC2"].ToString();

                if (EditFlag == "Y")
                {
                    result = Library.Database.BLL.SubSlitRequest.SubSlitChildMaint(seqMother, RefNo, pc1ChildId, pc2ChildId,
                        row.CQty, row.CWeight?.Replace(",", ""), row.CTotalWeight?.Replace(",", ""),
                        row.Remark, motherPC2Id, ProdLine, PC1Value, 3, CurrentUserId, CurrentUserIp);
                }
                else
                {
                    result = Library.Database.BLL.SubSlitRequest.SubSlitChildMaint(seqMother, RefNo, pc1ChildId, pc2ChildId,
                        row.CQty, row.CWeight?.Replace(",", ""), row.CTotalWeight?.Replace(",", ""),
                        row.Remark, "0", ProdLine, PC1Value, 1, CurrentUserId, CurrentUserIp);
                }

                if (result != "1")
                {
                    StartupScript = "alert('" + (result == "0" ? "Insert child failed" : result.Replace("'", "\\'")) + "');";
                    return;
                }
            }

            string msg = EditFlag == "Y"
                ? "PC2 Mother and/or its Child are updated successfully."
                : "SubSlit Request is added successfully.";
            StartupScript = "alert('" + msg + "');";
        }

        private void DisplaySSRListing(string refNo)
        {
            DataTable dtSSR = Library.Database.BLL.SubSlitRequest.SSRList(refNo, "0");
            ListData = BuildListTable(dtSSR);
        }

        private DataTable BuildListTable(DataTable dtSSR)
        {
            DataTable dtW = new DataTable();
            dtW.Columns.Add("REFNO", typeof(string));
            dtW.Columns.Add("PRODLINE_NO", typeof(string));
            dtW.Columns.Add("PC1_MOTHER", typeof(string));
            dtW.Columns.Add("PC2_MOTHER", typeof(string));
            dtW.Columns.Add("QTY", typeof(string));
            dtW.Columns.Add("M_WEIGHT", typeof(string));
            dtW.Columns.Add("M_TOTAL_WEIGHT", typeof(string));
            dtW.Columns.Add("PC1_CUST", typeof(string));
            dtW.Columns.Add("PC2_CUST", typeof(string));
            dtW.Columns.Add("C_QTY", typeof(string));
            dtW.Columns.Add("C_WEIGHT", typeof(string));
            dtW.Columns.Add("C_TOTAL_WEIGHT", typeof(string));
            dtW.Columns.Add("SUBSLIT_WASTE", typeof(string));
            dtW.Columns.Add("ETD", typeof(string));
            dtW.Columns.Add("ETA", typeof(string));
            dtW.Columns.Add("REMARK", typeof(string));
            dtW.Columns.Add("SEQ", typeof(string));
            dtW.Columns.Add("CHK", typeof(string));

            if (dtSSR == null || dtSSR.Rows.Count == 0) return dtW;

            string prevPC2 = "", prevPC1 = "", prevProd = "", prevSeq = "";

            for (int i = 0; i < dtSSR.Rows.Count; i++)
            {
                bool same = dtSSR.Rows[i]["PC2_MOTHER"].ToString() == prevPC2
                    && dtSSR.Rows[i]["PC1_MOTHER"].ToString() == prevPC1
                    && dtSSR.Rows[i]["PRODLINE_NO"].ToString() == prevProd
                    && dtSSR.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString() == prevSeq;

                if (same)
                {
                    DataRow dr = dtW.NewRow();
                    dr["REFNO"] = dtSSR.Rows[i]["REFNO"];
                    dr["PC1_CUST"] = dtSSR.Rows[i]["PC1_CUST"];
                    dr["PC2_CUST"] = dtSSR.Rows[i]["PC2_CUST"];
                    dr["C_QTY"] = dtSSR.Rows[i]["C_QTY"];
                    dr["C_WEIGHT"] = Convert.ToDecimal(dtSSR.Rows[i]["C_WEIGHT"]).ToString("#,###,###,##0.0");
                    dr["C_TOTAL_WEIGHT"] = Convert.ToDecimal(dtSSR.Rows[i]["C_TOTAL_WEIGHT"]).ToString("#,###,###,##0.0");
                    dr["REMARK"] = dtSSR.Rows[i]["REMARK"];
                    dr["PRODLINE_NO"] = ""; dr["PC1_MOTHER"] = ""; dr["PC2_MOTHER"] = "";
                    dr["CHK"] = "0";
                    dtW.Rows.Add(dr);
                }
                else
                {
                    if (!string.IsNullOrEmpty(prevPC2))
                    {
                        DataRow blank = dtW.NewRow();
                        blank["PC2_MOTHER"] = ""; blank["CHK"] = "0";
                        dtW.Rows.Add(blank);
                    }

                    DataRow dr = dtW.NewRow();
                    dr["REFNO"] = dtSSR.Rows[i]["REFNO"];
                    dr["PRODLINE_NO"] = dtSSR.Rows[i]["PRODLINE_NO"];
                    dr["PC1_MOTHER"] = dtSSR.Rows[i]["PC1_MOTHER"];
                    dr["PC2_MOTHER"] = dtSSR.Rows[i]["PC2_MOTHER"];
                    dr["QTY"] = dtSSR.Rows[i]["QTY"];
                    dr["M_WEIGHT"] = Convert.ToDecimal(dtSSR.Rows[i]["M_WEIGHT"]).ToString("#,###,###,##0.0");
                    dr["M_TOTAL_WEIGHT"] = Convert.ToDecimal(dtSSR.Rows[i]["M_TOTAL_WEIGHT"]).ToString("#,###,###,##0.0");
                    dr["PC1_CUST"] = dtSSR.Rows[i]["PC1_CUST"];
                    dr["PC2_CUST"] = dtSSR.Rows[i]["PC2_CUST"];
                    dr["C_QTY"] = dtSSR.Rows[i]["C_QTY"];
                    dr["C_WEIGHT"] = Convert.ToDecimal(dtSSR.Rows[i]["C_WEIGHT"]).ToString("#,###,###,##0.0");
                    dr["C_TOTAL_WEIGHT"] = Convert.ToDecimal(dtSSR.Rows[i]["C_TOTAL_WEIGHT"]).ToString("#,###,###,##0.0");
                    dr["SUBSLIT_WASTE"] = Convert.ToDecimal(dtSSR.Rows[i]["SUBSLIT_WASTE"]).ToString("#,###,###,##0.0");
                    dr["ETD"] = Convert.ToDateTime(dtSSR.Rows[i]["ETD"]).ToString("dd/MM/yyyy");
                    dr["ETA"] = Convert.ToDateTime(dtSSR.Rows[i]["ETA"]).ToString("dd/MM/yyyy");
                    dr["REMARK"] = dtSSR.Rows[i]["REMARK"];
                    dr["SEQ"] = dtSSR.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"];
                    dr["CHK"] = "1";
                    dtW.Rows.Add(dr);
                }

                prevPC2 = dtSSR.Rows[i]["PC2_MOTHER"].ToString();
                prevPC1 = dtSSR.Rows[i]["PC1_MOTHER"].ToString();
                prevProd = dtSSR.Rows[i]["PRODLINE_NO"].ToString();
                prevSeq = dtSSR.Rows[i]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString();
            }

            return dtW;
        }
    }
}
