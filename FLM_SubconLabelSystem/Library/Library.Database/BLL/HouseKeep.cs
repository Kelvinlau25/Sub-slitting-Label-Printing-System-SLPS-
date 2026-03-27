using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class HouseKeep : Library.Root.Other.BusinessLogicBase
    {
        public static DataTable GetSubSlitChild(string Company, string datePurge, string pPurgeTable)
        {
            using (var _dal = new DAL.HouseKeep())
            {
                return _dal.GetSubSlitChild(Company, datePurge, pPurgeTable);
            }
        }

        public static string DelSubSlitChild(string pID, string pHKTable)
        {
            using (var _Dal = new DAL.HouseKeep())
            {
                string result = _Dal.DelSubSlitChild(pID, pHKTable);

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
