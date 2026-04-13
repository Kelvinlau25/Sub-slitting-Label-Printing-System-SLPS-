using System;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class SLIT_SERIES_DtlModel : BasePageModel
    {
        public string StartupScript { get; set; } = string.Empty;

        // Display fields
        public string DisplayCompCode { get; set; } = string.Empty;
        public string DisplayRefNo { get; set; } = string.Empty;
        public string DisplayPlanYrMth { get; set; } = string.Empty;
        public string DisplayProdLine { get; set; } = string.Empty;
        public string DisplayPC1Mother { get; set; } = string.Empty;
        public string DisplayPC2Mother { get; set; } = string.Empty;
        public string DisplayUnitWeightMthr { get; set; } = string.Empty;
        public string DisplayLotNo { get; set; } = string.Empty;
        public string DisplayPC1Customer { get; set; } = string.Empty;
        public string DisplayPC2Customer { get; set; } = string.Empty;
        public string DisplayUnitWeightCust { get; set; } = string.Empty;
        public string DisplayNumOfSlit { get; set; } = string.Empty;
        public string DisplayTypeOfSlit { get; set; } = string.Empty;
        public string DisplayLotSlitStatus { get; set; } = string.Empty;

        // Audit
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
        public bool EditMode { get; set; } = true;

        // Modify fields
        [BindProperty] public string CompanyCode { get; set; } = string.Empty;
        [BindProperty] public string SelectedRefNo { get; set; } = string.Empty;
        [BindProperty] public string PlanDate { get; set; } = string.Empty;
        [BindProperty] public string SelectedProdLine { get; set; } = string.Empty;
        [BindProperty] public string SelectedPC1Mother { get; set; } = string.Empty;
        [BindProperty] public string PC2Mother { get; set; } = string.Empty;
        [BindProperty] public string UnitWeightMother { get; set; } = string.Empty;
        [BindProperty] public string LotNo { get; set; } = string.Empty;
        [BindProperty] public string SelectedPC1Customer { get; set; } = string.Empty;
        [BindProperty] public string PC2Customer { get; set; } = string.Empty;
        [BindProperty] public string UnitWeightCustomer { get; set; } = string.Empty;
        [BindProperty] public string NoOfSlit { get; set; } = string.Empty;
        [BindProperty] public string TypeOfSlit { get; set; } = string.Empty;
        [BindProperty] public string MatrixPos { get; set; } = string.Empty;
        [BindProperty] public string MatrixInc { get; set; } = string.Empty;
        public string LotSlitStatus { get; set; } = string.Empty;

        // Dropdowns
        public DataTable RefNoList { get; set; }
        public DataTable ProdLineList { get; set; }
        public DataTable PC1MotherList { get; set; }
        public DataTable PC1CustomerList { get; set; }

        public bool IsDisplayMode => Action == EnumAction.View || Action == EnumAction.Delete;
        public bool IsModifyMode => Action == EnumAction.Add || Action == EnumAction.Edit;

        public SLIT_SERIES_DtlModel()
        {
            SetupKey = "PC2_LOTNO";
            FunctionControl = false;
        }

        public override void BindData() { }

        public override IActionResult OnGet()
        {
            ParseQueryString();

            if (IsDisplayMode)
                LoadDisplayData();
            else if (IsModifyMode)
                LoadModifyData();

            return Page();
        }

        private void LoadDisplayData()
        {
            DataTable dt = Library.Database.BLL.SlitSeries.GetData(Key);
            if (dt == null || dt.Rows.Count == 0) return;

            DisplayCompCode = dt.Rows[0]["COMPANYCODE"].ToString();
            DisplayRefNo = dt.Rows[0]["REFNO"].ToString();
            DisplayPlanYrMth = dt.Rows[0]["PLAN_YEAR_MONTH"].ToString();
            DisplayProdLine = dt.Rows[0]["PRODLINE_NO"].ToString();
            DisplayPC1Mother = dt.Rows[0]["PC1_MOTHER"].ToString();
            DisplayPC2Mother = dt.Rows[0]["PC2_MOTHER"].ToString();
            DisplayUnitWeightMthr = dt.Rows[0]["UNIT_WEIGHT_MOTHER"].ToString();
            DisplayLotNo = dt.Rows[0]["LOTNO"].ToString();
            DisplayPC1Customer = dt.Rows[0]["PC1_CUST"].ToString();
            DisplayPC2Customer = dt.Rows[0]["PC2_CUST"].ToString();
            DisplayUnitWeightCust = dt.Rows[0]["UNIT_WEIGHT_CUSTOMER"].ToString();
            DisplayNumOfSlit = dt.Rows[0]["NO_OF_SLIT"].ToString();

            string[] slitParts = dt.Rows[0]["TYPE_OF_SLIT"].ToString().Split(',');
            string typeofslit = slitParts[0];
            string matrixpos = slitParts.Length > 1 ? slitParts[1] : "0";
            string matrixinc = slitParts.Length > 2 ? slitParts[2] : "0";

            if (typeofslit == "1") DisplayTypeOfSlit = "Sequence";
            else if (typeofslit == "2") DisplayTypeOfSlit = "Even";
            else if (typeofslit == "3") DisplayTypeOfSlit = "Odd";
            else if (typeofslit == "4") DisplayTypeOfSlit = "Matrix (Position: " + matrixpos + " Increment: " + matrixinc + ")";

            DisplayLotSlitStatus = dt.Rows[0]["LOT_STATUS"].ToString();

            CreatedBy = dt.Rows[0]["CREATED_BY"].ToString();
            if (!Convert.IsDBNull(dt.Rows[0]["CREATED_DATE"]) && DateTime.TryParse(dt.Rows[0]["CREATED_DATE"].ToString(), out DateTime cd))
                CreatedDate = cd;
            UpdatedBy = dt.Rows[0]["UPDATED_BY"].ToString();
            if (!Convert.IsDBNull(dt.Rows[0]["UPDATED_DATE"]) && DateTime.TryParse(dt.Rows[0]["UPDATED_DATE"].ToString(), out DateTime ud))
                UpdatedDate = ud;

            EditMode = dt.Rows[0]["REC_TYPE"].ToString() != "5";
        }

        private void LoadModifyData()
        {
            CompanyCode = SessionGet("COMPANYCODE") ?? string.Empty;

            string companyCode = SessionGet("COMPANYCODE") ?? string.Empty;
            DataTable dfn = Library.Database.BLL.SlitSeries.GetRefByComp(companyCode);
            RefNoList = dfn;

            if (Action == EnumAction.Edit)
            {
                DataTable dt = Library.Database.BLL.SlitSeries.GetData(Key);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CompanyCode = dt.Rows[0]["COMPANYCODE"].ToString();
                    SelectedRefNo = dt.Rows[0]["REFNO"].ToString();
                    PlanDate = dt.Rows[0]["PLAN_YEAR_MONTH"].ToString();
                    SelectedProdLine = dt.Rows[0]["PRODLINE_NO"].ToString();
                    SelectedPC1Mother = dt.Rows[0]["PC1_MOTHER"].ToString();
                    PC2Mother = dt.Rows[0]["PC2_MOTHER"].ToString();
                    UnitWeightMother = dt.Rows[0]["UNIT_WEIGHT_MOTHER"].ToString();
                    SelectedPC1Customer = dt.Rows[0]["PC1_CUST"].ToString();
                    PC2Customer = dt.Rows[0]["PC2_CUST"].ToString();
                    UnitWeightCustomer = dt.Rows[0]["UNIT_WEIGHT_CUSTOMER"].ToString();
                    LotNo = dt.Rows[0]["LOTNO"].ToString();
                    NoOfSlit = dt.Rows[0]["NO_OF_SLIT"].ToString();
                    LotSlitStatus = dt.Rows[0]["LOT_STATUS"].ToString();

                    string[] slitParts = dt.Rows[0]["TYPE_OF_SLIT"].ToString().Split(',');
                    TypeOfSlit = slitParts[0];
                    MatrixPos = slitParts.Length > 1 ? slitParts[1] : "0";
                    MatrixInc = slitParts.Length > 2 ? slitParts[2] : "0";

                    // Load dependent dropdowns
                    ProdLineList = Library.Database.BLL.SlitSeries.GetPRODLINE2(SelectedRefNo);
                    PC1MotherList = Library.Database.BLL.SlitSeries.GetDDLData2_Rev01(SelectedRefNo, SelectedProdLine);
                    PC1CustomerList = LoadPC1CustomerList(PC2Mother);
                }
            }
        }

        private DataTable LoadPC1CustomerList(string pc2Mother)
        {
            DataTable dtmaxRev = Library.Database.BLL.SubSlitRequest.chkRefNo(SelectedRefNo);
            string idSubSlit = dtmaxRev.Rows.Count > 0 ? dtmaxRev.Rows[0]["ID_SUBSLIT_REQUEST"].ToString() : "";
            return Library.Database.BLL.SlitSeries.GetDDLPC1Cust_Rev01(SelectedRefNo, idSubSlit, SelectedProdLine, SelectedPC1Mother, pc2Mother);
        }

        public IActionResult OnPostSubmit()
        {
            ParseQueryString();

            string cbtypeOfSlit = "";
            string matrixvariable = "0,0";

            if (TypeOfSlit == "1") cbtypeOfSlit = "1," + matrixvariable;
            else if (TypeOfSlit == "3") cbtypeOfSlit = "3," + matrixvariable;
            else if (TypeOfSlit == "2") cbtypeOfSlit = "2," + matrixvariable;
            else if (TypeOfSlit == "4")
            {
                if (string.IsNullOrEmpty(MatrixInc)) MatrixInc = "0";
                matrixvariable = MatrixPos + "," + MatrixInc;
                cbtypeOfSlit = "4," + matrixvariable;
            }

            string temp;
            if (Action == EnumAction.Edit)
            {
                temp = Library.Database.BLL.SlitSeries.Maint(Key, CompanyCode.Trim(), SelectedRefNo, LotNo.Trim(),
                    SelectedPC1Mother, PC2Mother, SelectedPC1Customer, PC2Customer, SelectedProdLine,
                    NoOfSlit.Trim(), PlanDate, cbtypeOfSlit, ((int)Action).ToString());
            }
            else
            {
                // Check lot no dup for add
                string dupStatus = Library.Database.BLL.CHECK_LOTNO_DUP.check_lotno_dup(CompanyCode.Trim(), LotNo.Trim());
                if (dupStatus == "1")
                {
                    // Duplicate exists - proceed anyway (same as original showDialogue flow)
                }

                temp = Library.Database.BLL.SlitSeries.Maint("0", CompanyCode.Trim(), SelectedRefNo, LotNo.Trim(),
                    SelectedPC1Mother, PC2Mother, SelectedPC1Customer, PC2Customer, SelectedProdLine,
                    NoOfSlit.Trim(), PlanDate, cbtypeOfSlit, ((int)Action).ToString());
            }

            if (temp == "1")
                return Redirect(GetUrl(EnumAction.None));

            StartupScript = "alert('" + (temp == "0" ? "Operation failed" : temp.Replace("'", "\\'")) + "');";
            LoadModifyData();
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            ParseQueryString();
            LoadDisplayData();

            string temp = Library.Database.BLL.SlitSeries.Maint(Key, DisplayCompCode, DisplayRefNo, DisplayLotNo,
                DisplayProdLine, DisplayPC1Mother, DisplayPC2Mother, DisplayPC1Customer, DisplayPC2Customer,
                DisplayLotNo, DisplayNumOfSlit, DisplayTypeOfSlit,
                ((int)EnumAction.Delete).ToString());

            if (temp == "1")
                return Redirect(GetUrl(EnumAction.None));

            StartupScript = "alert('" + (temp == "0" ? "Delete failed" : temp.Replace("'", "\\'")) + "');";
            return Page();
        }

        public IActionResult OnPostReset()
        {
            return Redirect(Request.Path + Request.QueryString);
        }
    }
}
