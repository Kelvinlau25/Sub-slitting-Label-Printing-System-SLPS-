using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    public class SlitSeries : Library.SQLServer.Connection
    {
        public SlitSeries() : base("PFR_Label_DB") { }

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

            base._cmd.CommandText = "SP_SLIT_SERIES_SEL";
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

        internal DataTable GetDDLData(string Ind)
        {
            DataTable result = new DataTable();

            if (Ind == "1")
                base._cmd.CommandText = "SP_MM_PC1_SEL";
            else if (Ind == "2")
                base._cmd.CommandText = "SP_MM_PRODLINE_SEL";
            else if (Ind == "3")
                base._cmd.CommandText = "SP_MM_REFNO_SEL";

            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pID", "0")).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetRefByComp(string Company_Code)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_MM_REFBYCOMP_SEL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pCompanyCode", Company_Code)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetDDLData2(string refno)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_MM_PC1_SEL2";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pREFNO", refno)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetDDLData2_Rev01(string refno, string str_PRODLINE_NO)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_MM_PC1_SEL2_Rev01";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pREFNO", refno)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_PRODLINE_NO", str_PRODLINE_NO)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetDDLPC1Cust(string pRefNo, string pPC2_Mother)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_GET_PCCust";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pID_SubSlit_Req", pPC2_Mother)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable GetDDLPC1Cust_REV01(string pRefNo, string pPC2_Mother,
                                                string str_PRODLINE_NO, string str_PC1_MOTHER,
                                                string str_PC2_MOTHER)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_GET_PCCust_REV01";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pID_SubSlit_Req", pPC2_Mother)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_PRODLINE_NO", str_PRODLINE_NO)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_PC1_MOTHER", str_PC1_MOTHER)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_PC2_MOTHER", str_PC2_MOTHER)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable GetPCMOTHER()
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_PCMother";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPCCUSTOMER()
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_PCCustomer";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPCMOTHER2(string refno)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_PCMother_2";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pREFNO", refno)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPC2CUST(string refno)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_PC2Cust";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pREFNO", refno)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPRODLINE2(string refno)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_Prodline_2";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pREFNO", refno)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetUNITWEIGHT2(string pc2)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_UnitWeight_2";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@PC2", pc2)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal string Maint(string ID, string CompCode, string RefNo, string LotNo, string PC1_Mother, string PC2_Mother,
                               string PC1_Cust, string PC2_Cust, string ProdLine,
                               string No_Of_Slit, string Plan_Year_Mth, string Type_Of_Slit,
                               string RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SLIT_SERIES_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCOMPANYCODE", CompCode)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLOTNO", LotNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1_MOTHER", PC1_Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2_MOTHER", PC2_Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1_CUST", PC1_Cust)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2_CUST", PC2_Cust)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPRODLINE_NO", ProdLine)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pNO_OF_SLIT", No_Of_Slit)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPLAN_YEAR_MONTH", Plan_Year_Mth)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pTYPE_OF_SLIT", Type_Of_Slit)).Direction = ParameterDirection.Input;
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

        internal string CreateSlitRec(string D_Company_Code, int D_ID_PC2_LOTNO,
                                       int D_TYPE_OF_SLIT, int D_MATRIX_POS, int D_MATRIX_INC,
                                       string D_LOTNO, int D_NO_OF_SLIT, string D_User_ID, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_CREATE_SLITCODE";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pCOMPANYCODE", D_Company_Code)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pID_PC2_LOTNO", D_ID_PC2_LOTNO)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pTYPE_OF_SLIT", D_TYPE_OF_SLIT)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pMATRIX_POS", D_MATRIX_POS)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pMATRIX_INC", D_MATRIX_INC)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLOTNO", D_LOTNO)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pNO_OF_SLIT ", D_NO_OF_SLIT)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", D_User_ID)).Direction = ParameterDirection.Input;
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
