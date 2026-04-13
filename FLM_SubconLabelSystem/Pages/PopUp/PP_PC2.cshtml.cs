using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace PFRLabelIssuing.Pages.PopUp
{
    public class PP_PC2Model : BasePageModel
    {
        private Library.Database.ListCollection _list;

        public string Pc2mother { get; set; } = string.Empty;
        public string Lblpc2mother { get; set; } = string.Empty;
        public string Refno { get; set; } = string.Empty;
        public string StrBtnName { get; set; } = string.Empty;
        public string StrHdnPC2Mother { get; set; } = string.Empty;
        public string StrHdnUnitWeightMother { get; set; } = string.Empty;

        public PP_PC2Model()
        {
            SetupKey = "PP_PC2";
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
            Pc2mother = Item1;
            Lblpc2mother = Item2;

            string refno = "0";
            string strProdLine = "0";
            string strPC1Mother = "0";
            StrBtnName = "";
            StrHdnPC2Mother = "";
            StrHdnUnitWeightMother = "";

            if (!string.IsNullOrEmpty(Item3))
                refno = Item3;

            if (!string.IsNullOrEmpty(Item4) && Item4 != ",")
            {
                strProdLine = Item4.Replace(",", "");
            }

            if (!string.IsNullOrEmpty(Item5) && Item5 != ",")
            {
                strPC1Mother = Item5.Replace(",", "");
            }

            if (!string.IsNullOrEmpty(Item6) && Item6 != ",")
            {
                StrBtnName = Item6.Replace(",", "");
            }

            if (!string.IsNullOrEmpty(Item7) && Item7 != ",")
            {
                StrHdnPC2Mother = Item7.Replace(",", "");
            }

            if (!string.IsNullOrEmpty(Item8) && Item8 != ",")
            {
                StrHdnUnitWeightMother = Item8.Replace(",", "");
            }

            string condition = " REFNO = '" + refno + "' AND PRODLINE_NO = '" + strProdLine + "' AND PC1_MOTHER = '" + strPC1Mother + "'";
            _list = Library.Database.BLL.PC1.List2(condition, "PV_MM_PC2_POPUPv1", "ID_MM_PC2", SearchField, SearchValue, SortField, Convert.ToInt32(SortDirection), PageNo, ShowDeleted ? 1 : 0);
            GridData = _list.Data;
            TotalRecords = _list.TotalRow;
        }
    }
}
