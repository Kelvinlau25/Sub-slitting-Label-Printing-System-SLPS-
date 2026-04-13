using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class MM_PRODLINE : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.MM_PRODLINE())
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
            using (var _dal = new DAL.MM_PRODLINE())
            {
                return _dal.GetData(ID);
            }
        }

        public static string Maint(string ID, string ProdLine, string Desc, string RecType)
        {
            using (var _Dal = new DAL.MM_PRODLINE())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, ProdLine, Desc,
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

        public static string Maint(string ID, string ProdLine, string Desc, string RecType, string updatedBy, string updatedLoc)
        {
            using (var _Dal = new DAL.MM_PRODLINE())
            {
                string result = _Dal.Maint(ID, ProdLine, Desc, RecType, updatedBy, updatedLoc);

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
