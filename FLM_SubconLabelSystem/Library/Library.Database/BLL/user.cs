using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class user : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.user())
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
            using (var _dal = new DAL.user())
            {
                return _dal.GetData(ID);
            }
        }

        public static DataTable GetDLLData(string Value, string ID)
        {
            using (var _dal = new DAL.user())
            {
                return _dal.GetDLLData(Value, ID);
            }
        }

        public static string Maint(string ID, string CompName, string Name, string UserID, string Department, string Email,
                                    bool Ulevel, bool Ulevel2, bool Ulevel3, string Psword, string Stats, string RecType)
        {
            using (var _Dal = new DAL.user())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, CompName, Name, UserID, Department, Email, Ulevel, Ulevel2, Ulevel3, Psword, Stats, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string Maint(string ID, string CompName, string Name, string UserID, string Department, string Email,
                                    bool Ulevel, bool Ulevel2, bool Ulevel3, string Psword, string Stats, string RecType,
                                    string userID, string userHostAddress)
        {
            using (var _Dal = new DAL.user())
            {
                string result = _Dal.Maint(ID, CompName, Name, UserID, Department, Email, Ulevel, Ulevel2, Ulevel3, Psword, Stats, RecType, userID, userHostAddress);

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

        public static string ResetPass(string ID, string Psword, string RecType)
        {
            using (var _Dal = new DAL.user())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.ResetPass(ID, Psword, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string ResetPass(string ID, string Psword, string RecType, string userID, string userHostAddress)
        {
            using (var _Dal = new DAL.user())
            {
                string result = _Dal.ResetPass(ID, Psword, RecType, userID, userHostAddress);

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
