using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class LotSlitting : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.LotSlitting())
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
            using (var _dal = new DAL.LotSlitting())
            {
                return _dal.GetData(ID);
            }
        }

        public static string Maint(string ID, string LOTNO, string var2, string var3, string var4,
                                    string var5, string var6, string var7,
                                    string var8, string var9, string var10,
                                    string var11, string var12, string RecType)
        {
            using (var _Dal = new DAL.LotSlitting())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, LOTNO, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string UpdPrintSel(string SLITLOTNO, bool PrintSel, string RecUpd)
        {
            using (var _Dal = new DAL.LotSlitting())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.UpdPrintSel(SLITLOTNO, PrintSel, RecUpd, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string UpdPrintSelAll(bool PrintSel, string RecUpd, string filter, string filterfield, string addCondition, string passType)
        {
            using (var _Dal = new DAL.LotSlitting())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.UpdPrintSelAll(PrintSel, RecUpd, filter, filterfield, addCondition, passType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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
