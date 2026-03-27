using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class MM_PRINT_ALIGN_INIT : Library.SQLServer.Connection
    {
        public MM_PRINT_ALIGN_INIT() : base("PFR_Label_DB") { }

        internal DataTable Print_Align_init()
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_PRINT_ALIGN_INIT";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }
    }
}
