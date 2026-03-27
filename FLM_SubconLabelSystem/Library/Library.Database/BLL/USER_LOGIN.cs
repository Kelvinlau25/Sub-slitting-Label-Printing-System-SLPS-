using System;

namespace Library.Database.BLL
{
    public class USER_LOGIN
    {
        public static string[] UserLogin(string pstr_UserID, string pstr_Password, int stage)
        {
            using (var _dal = new DAL.USER_LOGIN())
            {
                return _dal.UserLogin(pstr_UserID, pstr_Password, System.Web.HttpContext.Current.Request.UserHostAddress.ToString(), Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Max_Login_Attempts"]), stage);
            }
        }
    }
}
