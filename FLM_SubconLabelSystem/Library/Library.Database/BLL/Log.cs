using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class Log : Library.Root.Other.BusinessLogicBase
    {
        /// <summary>
        /// Retrieve the log (Common Function)
        /// Based on the Setup Key, the View's Name will be retrieved from the resource page
        /// Select the data based on the key, and the page no. was applied
        /// </summary>
        public static ListCollection GetLogList(string Table, string Key, int Page, string Sort = "")
        {
            ListCollection result = new ListCollection();

            if (Table != string.Empty)
            {
                using (var _dal = new DAL.Log())
                {
                    result = _dal.getLogList(Table, Key, FromRowNo(Page), ToRowNo(Page), Sort);
                }
            }
            return result;
        }

        public static DataTable GetGroupNo()
        {
            using (var _Dal = new DAL.Log())
            {
                return _Dal.GetGroupNo();
            }
        }
    }
}
