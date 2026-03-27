using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class PC2Mother : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.PC2Mother())
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
            using (var _dal = new DAL.PC2Mother())
            {
                return _dal.GetData(ID);
            }
        }

        public static string Maint(string ID, string PC2, string Thickness, string Type, string Width,
                                    string Length, string PackCode, string Grade,
                                    string CoreCode, string Machine, string UnitWeight,
                                    string NumPerPack, string Remarks, string RecType)
        {
            using (var _Dal = new DAL.PC2Mother())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, PC2, Thickness, Type, Width, Length, PackCode, Grade, CoreCode, Machine, UnitWeight, NumPerPack, Remarks, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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
