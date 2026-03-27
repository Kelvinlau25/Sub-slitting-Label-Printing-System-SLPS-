using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class CHECK_LOTNO_DUP : Library.SQLServer.Connection
    {
        public CHECK_LOTNO_DUP() : base("PFR_Label_DB") { }

        internal string check_lotno_dup(string pstr_CompanyCode, string pstr_LotNo)
        {
            DataTable _obj_dt = new DataTable();
            string update_status = "";

            base._cmd.CommandText = "SP_CHECK_LOTNO_DUP";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@str_CompanyCode", pstr_CompanyCode)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_LotNo", pstr_LotNo)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            base._cmd.Transaction.Commit();

            if (_obj_dt.Rows.Count > 0)
            {
                update_status = _obj_dt.Rows[0]["return_status"].ToString().Trim();
            }

            _obj_dt.Dispose();
            return update_status;
        }
    }
}
