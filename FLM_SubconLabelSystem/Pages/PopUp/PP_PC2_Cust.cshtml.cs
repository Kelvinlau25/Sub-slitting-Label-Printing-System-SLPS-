using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.PopUp
{
    public class PP_PC2_CustModel : BasePageModel
    {
        private Library.Database.ListCollection _list;

        public string Pc2cust { get; set; } = string.Empty;
        public string Lblpc2cust { get; set; } = string.Empty;
        public string Refno { get; set; } = string.Empty;
        public string StrHdnPC2Customer { get; set; } = string.Empty;
        public string StrHdnUnitWeightCustomer { get; set; } = string.Empty;
        public string StrBtnName { get; set; } = string.Empty;

        public PP_PC2_CustModel()
        {
            SetupKey = "PP_PC2_Cust";
            DefaultSort = "ID_MM_PC2";
            SortDirection = "0";
            GridViewCheckColumn = false;
            PrintControl = false;
            DeleteControl = false;
            GridViewRadioColumn = false;
            ViewHistoryControl = false;
            RecordTypeColumn = -1;
        }

        public override void BindData()
        {
            Pc2cust = Item1;
            Lblpc2cust = Item2;

            string refno = "0";
            string strProdLine = "0";
            string strPC1Mother = "0";
            string strPC2Mother = "0";
            string strPC1Customer = "0";
            StrBtnName = "";
            StrHdnPC2Customer = "";
            StrHdnUnitWeightCustomer = "";

            if (!string.IsNullOrEmpty(Item3))
                refno = Item3;

            if (!string.IsNullOrEmpty(Item4) && Item4 != ",")
                strProdLine = Item4.Replace(",", "");

            if (!string.IsNullOrEmpty(Item5) && Item5 != ",")
                strPC1Mother = Item5.Replace(",", "");

            if (!string.IsNullOrEmpty(Item6) && Item6 != ",")
                strPC2Mother = Item6.Replace(",", "");

            if (!string.IsNullOrEmpty(Item7) && Item7 != ",")
                strPC1Customer = Item7.Replace(",", "");

            if (!string.IsNullOrEmpty(Item8) && Item8 != ",")
                StrBtnName = Item8.Replace(",", "");

            if (!string.IsNullOrEmpty(Item9) && Item9 != ",")
                StrHdnPC2Customer = Item9.Replace(",", "");

            if (!string.IsNullOrEmpty(Item10) && Item10 != ",")
                StrHdnUnitWeightCustomer = Item10.Replace(",", "");

            string condition = " REFNO = '" + refno + "' AND PC1_MOTHER = '" + strPC1Mother + "' AND PC2_MOTHER = '" + strPC2Mother + "' AND PC1_CUST = '" + strPC1Customer + "' AND PRODLINE_NO = '" + strProdLine + "'";

            _list = Library.Database.BLL.PC1.List2(condition, "PV_MM_PC2CUST_POPUPv1", "ID_MM_PC2", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
            GridData = _list.Data;
            TotalRecords = _list.TotalRow;
        }
    }
}
