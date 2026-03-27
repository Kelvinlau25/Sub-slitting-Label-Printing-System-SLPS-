using System.Data;

namespace Library.Database.BLL
{
    public class MenuListing
    {
        public static DataTable Load_Menu_Listing(string var_1)
        {
            using (var _dal = new DAL.MenuListing())
            {
                return _dal.Load_Menu_Listing(var_1);
            }
        }
    }
}
