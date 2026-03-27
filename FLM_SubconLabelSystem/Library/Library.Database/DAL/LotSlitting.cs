using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class LotSlitting : Library.SQLServer.Connection
    {
        public LotSlitting() : base("PFR_Label_DB") { }

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
            base._cmd.CommandText = "SP_LOT_SLITTING_SEL";
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

        internal string Maint(string ID, string LOTNO, string var2, string var3, string var4,
                               string var5, string var6, string var7,
                               string var8, string var9, string var10,
                               string var11, string var12,
                               string RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_LOT_SLITTING_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();
                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLOTNO", LOTNO)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar2", var2)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar3", var3)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar4", var4)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar5", var5)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar6", var6)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar7", var7)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar8", var8)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar9", var9)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar10", var10)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar11", var11)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pvar12", var12)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;
                base._cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

        internal string UpdPrintSel(string SLITLOTNO, bool PrintSel, string RecUpd, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_UPDATE_PRINTSEL";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();
                base._cmd.Parameters.Add(new SqlParameter("@pSlitLotNo", SLITLOTNO)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPrintSel", PrintSel)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecUpd", RecUpd)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUpdatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUpdatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;
                base._cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

        internal string UpdPrintSelAll(bool PrintSel, string RecUpd, string filter, string filterField, string addCondition, string passType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_UPDATE_PRINTSEL_ALL";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();
                base._cmd.Parameters.Add(new SqlParameter("@pPrintSel", PrintSel)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecUpd", RecUpd)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pFilter", filter)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pFilterField", filterField)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pAddCondition", addCondition)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPassType", passType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUpdatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUpdatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;
                base._cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }
    }
}
