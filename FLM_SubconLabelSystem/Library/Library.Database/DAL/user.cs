using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class user : Library.SQLServer.Connection
    {
        public user() : base("PFR_Label_DB") { }

        internal ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                     int FromRowNo, int ToRowNo, int Deleted)
        {
            ListCollection result = new ListCollection();

            base._cmd.CommandText = "PSP_COMMON_LIST";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@Table", Table)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@TableID", TableID)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@Search", SearchField)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@Value", SearchValue)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@SortField", SortField)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@Direction", Direction)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@FrmRowno", FromRowNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@ToRowno", ToRowNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@Deleted", Deleted)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Data.Load(base._rdr);

            while (base._rdr.Read())
            {
                result.TotalRow = (int)base._rdr["COUNTER"];
            }
            return result;
        }

        internal DataTable GetData(string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_MM_USER_SEL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetDLLData(string Value, string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_MM_GETDDLDATA";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pValue", Value)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal string Maint(string id, string CompName, string Name, string UserID, string Department, string Email,
                               bool Ulevel, bool Ulevel2, bool Ulevel3, string Psword, string Stats,
                               string RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_MM_USER_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", id)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompName", CompName)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pName", Name)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUserID", UserID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pDepartment", Department)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pEmail", Email)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUlevel", Ulevel)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUlevel2", Ulevel2)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUlevel3", Ulevel3)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPassword", Psword)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pStatus", Stats)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string ResetPass(string id, string Psword, string RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_MM_USER_RESETPASS";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", id)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPassword", Psword)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

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
