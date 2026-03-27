using System.Data;

namespace Library.Database.BLL
{
    public class MM_PRINT_ALIGN_INIT : Library.Root.Other.BusinessLogicBase
    {
        public static DataTable Print_Align_init()
        {
            DataTable dt = new DataTable();
            using (var _dal = new DAL.MM_PRINT_ALIGN_INIT())
            {
                dt = _dal.Print_Align_init();
            }
            return dt;
        }
    }
}
