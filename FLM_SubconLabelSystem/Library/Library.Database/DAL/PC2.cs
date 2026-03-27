using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class PC2 : Library.SQLServer.Connection
    {
        public PC2() : base("PFR_Label_DB") { }

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

            base._cmd.CommandText = "SP_MM_PC2_SEL";
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

        internal string Maint(string ID, string CompanyCode, string PC2, string Thickness, string Type, string Width,
                               string Length, string PackCode, string Grade,
                               string CoreCode, string UnitWeight,
                               string NumPerPack, string Remarks,
                               string RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_MM_PC2_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompanyCode", CompanyCode)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2", PC2)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pThickness", Thickness)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pType", Type)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pWidth", Width)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLength", Length)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPackCode", PackCode)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pGrade", Grade)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCoreCode", CoreCode)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUnitWeight", UnitWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pNumPerPack", NumPerPack)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRemarks", Remarks)).Direction = ParameterDirection.Input;
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
