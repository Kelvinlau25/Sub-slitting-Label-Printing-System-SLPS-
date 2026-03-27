using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class MenuListing : Library.SQLServer.Connection
    {
        public MenuListing() : base("PFR_Label_DB") { }

        internal DataTable Load_Menu_Listing(string var_1)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_GET_MENU_LIST";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }
    }
}
