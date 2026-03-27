using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class HouseKeep : Library.SQLServer.Connection
    {
        public HouseKeep() : base("PFR_Label_DB") { }

        internal DataTable GetSubSlitChild(string Company, string datePurge, string purgeTable)
        {
            DataTable result = new DataTable();

            if (purgeTable == "SSCHILD")
                base._cmd.CommandText = "SP_HK_GET_SUB_CHILD";
            else if (purgeTable == "SSMOTHER")
                base._cmd.CommandText = "SP_HK_GET_SUB_MOTHER";
            else if (purgeTable == "SSMAIN")
                base._cmd.CommandText = "SP_HK_GET_SUB_MAIN";
            else if (purgeTable == "LOTSLIT")
                base._cmd.CommandText = "SP_HK_GET_LOT_SLIT";
            else if (purgeTable == "PC2LOT")
                base._cmd.CommandText = "SP_HK_GET_PC2_LOT";

            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pCompany", Company)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pDatePurge", datePurge)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal string DelSubSlitChild(string pid, string pHKTable)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_HOUSEKEEPING";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();
                base._cmd.Parameters.Add(new SqlParameter("@pID", pid)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@HouseKeepTable", pHKTable)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
