using System;

namespace Library.Database.BLL
{
    public class USER_LOGIN
    {
        public static string[] UserLogin(string pstr_UserID, string pstr_Password, int stage, string ipAddress = "", string maxLoginAttempts = "5")
        {
            using (var _dal = new DAL.USER_LOGIN())
            {
                return _dal.UserLogin(pstr_UserID, pstr_Password, ipAddress, maxLoginAttempts, stage);
            }
        }
    }
}
