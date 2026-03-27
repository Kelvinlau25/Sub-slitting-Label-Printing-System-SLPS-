using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class MM_LABEL_ISSUING : Library.SQLServer.Connection
    {
        public MM_LABEL_ISSUING() : base("PFR_Label_DB") { }

        internal DataTable Get_Print_Label()
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_Get_PRINT_LABEL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }
    }
}
