namespace Library.Database.BLL
{
    public class CHANGE_PASSWORD
    {
        public static string chg_password(string pstr_UserID, string curr_Password, string new_Password, int duplicatepass)
        {
            using (var _dal = new DAL.CHANGE_PASSWORD())
            {
                return _dal.chg_password(pstr_UserID, curr_Password, new_Password, duplicatepass, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());
            }
        }

        public static string[] retrieve_pass_arr(string pstr_UserID)
        {
            using (var _dal = new DAL.CHANGE_PASSWORD())
            {
                return _dal.retrieve_pass_arr(pstr_UserID);
            }
        }
    }
}
