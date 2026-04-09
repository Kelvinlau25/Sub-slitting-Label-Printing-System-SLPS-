using System.Data;
using Microsoft.Data.SqlClient;

namespace Library.Database.DAL
{
    public class Log : Library.SQLServer.Connection
    {
        public Log() : base("PFR_Label_DB") { }

        public ListCollection getLogList(string Tablename, string Key, int FromNo, int ToNo, string Sort)
        {
            ListCollection result = new ListCollection();

            base._cmd.CommandText = "PSP_COMMON_LIST_LOG";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pTable", Tablename)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pKey", Key)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pFromRowno", FromNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pToRowNo", ToNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pSort", Sort)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Data.Load(base._rdr);

            while (base._rdr.Read())
            {
                result.TotalRow = (int)base._rdr["COUNTER"];
            }
            return result;
        }

        internal DataTable GetGroupNo()
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_GROUPNO";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();  // Added: prevents stale parameters from prior calls

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }
    }
}
