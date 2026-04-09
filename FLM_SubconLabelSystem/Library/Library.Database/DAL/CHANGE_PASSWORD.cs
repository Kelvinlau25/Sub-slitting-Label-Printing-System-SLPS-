using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Library.Database.DAL
{
    public class CHANGE_PASSWORD : Library.SQLServer.Connection
    {
        public CHANGE_PASSWORD() : base("PFR_Label_DB") { }

        internal string chg_password(string pstr_UserID, string curr_Password, string new_Password, int duplicatepass, string UpdatedLoc)
        {
            DataTable _obj_dt = new DataTable();
            string update_status = "";

            base._cmd.CommandText = "SP_CHANGE_PASSWORD";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@str_UserID", pstr_UserID)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_currPassword", curr_Password)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_newPassword", new_Password)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@duplicatepass", duplicatepass)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_Updated_Loc", UpdatedLoc)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            base._cmd.Transaction.Commit();

            if (_obj_dt.Rows.Count > 0)
            {
                update_status = _obj_dt.Rows[0]["return_status"].ToString().Trim();
            }

            _obj_dt.Dispose();
            return update_status;
        }

        internal string[] retrieve_pass_arr(string pstr_UserID)
        {
            DataTable _obj_dt = new DataTable();

            try
            {
                base._cmd.CommandText = "SP_RETRIEVE_PASS";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@str_UserID", pstr_UserID)).Direction = ParameterDirection.Input;

                _obj_dt.Load(base._cmd.ExecuteReader());

                string[] _arr_str_return_value = new string[_obj_dt.Rows.Count];

                if (_obj_dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= _obj_dt.Rows.Count - 1; i++)
                    {
                        _arr_str_return_value[i] = _obj_dt.Rows[i][0].ToString();
                    }
                }

                return _arr_str_return_value;
            }
            catch (Exception ex)
            {
                string[] _arr_str_return_value = new string[1];
                _arr_str_return_value[0] = ex.Message;
                return _arr_str_return_value;
            }
        }
    }
}
