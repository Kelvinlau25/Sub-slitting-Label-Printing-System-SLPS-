using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class PC1 : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.PC1())
            {
                if (Direction != 1)
                {
                    Direction = 0;
                }
                return _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted);
            }
        }

        public static ListCollection List2(string refno, string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                           int Page, int Deleted)
        {
            using (var _dal = new DAL.PC1())
            {
                if (Direction != 1)
                {
                    Direction = 0;
                }
                return _dal.List2(refno, Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted);
            }
        }

        public static DataTable GetData(string ID)
        {
            using (var _dal = new DAL.PC1())
            {
                return _dal.GetData(ID);
            }
        }

        public static DataTable GetDLLData(string Value, string ID)
        {
            using (var _dal = new DAL.PC1())
            {
                return _dal.GetDLLData(Value, ID);
            }
        }

        public static string Maint(string ID, string PC1, string CompanyCode, string NameDelivery, string RecType)
        {
            using (var _Dal = new DAL.PC1())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, PC1, CompanyCode, NameDelivery, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string Maint(string ID, string PC1, string CompanyCode, string NameDelivery, string RecType, string userId, string userIp)
        {
            using (var _Dal = new DAL.PC1())
            {
                string result = _Dal.Maint(ID, PC1, CompanyCode, NameDelivery, RecType, userId, userIp);

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
