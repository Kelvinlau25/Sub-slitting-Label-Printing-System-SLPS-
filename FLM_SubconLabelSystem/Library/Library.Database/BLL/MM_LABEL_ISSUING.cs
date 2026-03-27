using System.Data;

namespace Library.Database.BLL
{
    public class MM_LABEL_ISSUING : Library.Root.Other.BusinessLogicBase
    {
        public static DataTable Get_Print_Label()
        {
            DataTable dt = new DataTable();
            using (var _dal = new DAL.MM_LABEL_ISSUING())
            {
                dt = _dal.Get_Print_Label();
            }
            return dt;
        }
    }
}
