using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class PrintAlignInit : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.PrintAlignInit())
            {
                if (Direction != 1)
                {
                    Direction = 0;
                }
                return _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted);
            }
        }

        public static DataTable GetData(string ID)
        {
            using (var _dal = new DAL.PrintAlignInit())
            {
                return _dal.GetData(ID);
            }
        }

        public static string Maint(string ID, string PrinterName, string TextFont, string WidthX, string TextFontSize,
                                    string WidthY, string BarcodeFont, string LengthHeaderX, string BarcodeFontSize,
                                    string LengthHeaderY, string PackCodeX, string UnitWeightX, string PackCodeY,
                                    string UnitWeightY, string NumPerPackX, string SlitLotNoX, string NumPerPackY,
                                    string SlitLotNoY, string PC1X, string GradeX, string PC1Y,
                                    string GradeY, string LengthX, string CoreCodeX, string LengthY,
                                    string CoreCodeY, string txtThicknessX, string txtBarcodeX, string txtThicknessY,
                                    string txtBarcodeY, string txtTypeX, string txtTypeY, bool RadioButton1,
                                    bool RadioButton2, string Company_Code, string RecType)
        {
            using (var _Dal = new DAL.PrintAlignInit())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, PrinterName, TextFont, WidthX, TextFontSize, WidthY, BarcodeFont, LengthHeaderX, BarcodeFontSize,
                                           LengthHeaderY, PackCodeX, UnitWeightX, PackCodeY, UnitWeightY, NumPerPackX, SlitLotNoX, NumPerPackY,
                                           SlitLotNoY, PC1X, GradeX, PC1Y, GradeY, LengthX, CoreCodeX, LengthY, CoreCodeY, txtThicknessX, txtBarcodeX,
                                           txtThicknessY, txtBarcodeY, txtTypeX, txtTypeY, RadioButton1, RadioButton2, Company_Code,
                                           RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

                if (result == "1")
                {
                    _Dal.Commit();
                }
                else
                {
                    _Dal.Rollback();
                }
                return result;
            }
        }
    }
}
