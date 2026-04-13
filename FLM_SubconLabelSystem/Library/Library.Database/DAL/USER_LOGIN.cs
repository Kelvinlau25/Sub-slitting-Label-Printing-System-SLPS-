using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Library.Database.DAL
{
    public class USER_LOGIN : Library.SQLServer.Connection
    {
        //public USER_LOGIN() : base("PFR_Label_DB") { }
        public USER_LOGIN() : base("ACL") { }

        internal string[] UserLogin(string pstr_UserID, string pstr_Password, string ip_address, string max_attempts, object stage)
        {
            DataTable _obj_dt = new DataTable();
            string[] _arr_str_return_value = new string[7];
            _arr_str_return_value[0] = "";
            _arr_str_return_value[1] = "";
            _arr_str_return_value[2] = "";
            _arr_str_return_value[3] = "";
            _arr_str_return_value[4] = "";
            _arr_str_return_value[5] = "";
            _arr_str_return_value[6] = "";

            try
            {
                base._cmd.CommandText = "SP_USER_LOGIN";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@str_UserID", pstr_UserID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@str_UserPassword", pstr_Password)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@usr_ip_address", ip_address)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@max_attempts", max_attempts)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@stage", stage)).Direction = ParameterDirection.Input;

                base._cmd.Transaction.Commit();
                _obj_dt.Load(base._cmd.ExecuteReader());

                if (_obj_dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(stage) == 0)
                    {
                        _arr_str_return_value[0] = _obj_dt.Rows[0]["PASSWORD"].ToString().Trim();
                    }
                    else
                    {
                        _arr_str_return_value[0] = _obj_dt.Rows[0]["USERID"].ToString().Trim();
                        _arr_str_return_value[1] = _obj_dt.Rows[0]["ULEVEL"].ToString().Trim();
                        _arr_str_return_value[2] = _obj_dt.Rows[0]["COMPANYCODE"].ToString().Trim();
                        _arr_str_return_value[3] = _obj_dt.Rows[0]["NAME"].ToString().Trim();
                        _arr_str_return_value[4] = _obj_dt.Rows[0]["STATUS"].ToString().Trim();
                        _arr_str_return_value[5] = _obj_dt.Rows[0]["PWD_DATE"].ToString().Trim();
                    }
                }

                return _arr_str_return_value;
            }
            catch (Exception ex)
            {
                _arr_str_return_value[6] = ex.Message;
                return _arr_str_return_value;
            }
        }
    }
}
