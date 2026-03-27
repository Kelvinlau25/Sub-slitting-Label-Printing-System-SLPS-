using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class PrintAlignInit : Library.SQLServer.Connection
    {
        public PrintAlignInit() : base("PFR_Label_DB") { }

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

            base._cmd.CommandText = "SP_Print_Align_Init_SEL";
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

        internal string Maint(string ID, string PrinterName, string TextFont, string WidthX, string TextFontSize,
                               string WidthY, string BarcodeFont, string LengthHeaderX, string BarcodeFontSize,
                               string LengthHeaderY, string PackCodeX, string UnitWeightX, string PackCodeY,
                               string UnitWeightY, string NumPerPackX, string SlitLotNoX, string NumPerPackY,
                               string SlitLotNoY, string PC1X, string GradeX, string PC1Y,
                               string GradeY, string LengthX, string CoreCodeX, string LengthY,
                               string CoreCodeY, string ThicknessX, string BarcodeX, string ThicknessY,
                               string BarcodeY, string TypeX, string TypeY, bool RadioButton1,
                               bool RadioButton2, string Company_Code,
                               string RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_Print_Align_Init_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPrinterName", PrinterName)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pTextFont", TextFont)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pWidthX", WidthX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pTextFontSize", TextFontSize)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pWidthY", WidthY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pBarcodeFont", BarcodeFont)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLengthHeaderX", LengthHeaderX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pBarcodeFontSize", BarcodeFontSize)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLengthHeaderY", LengthHeaderY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPackCodeX", PackCodeX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUnitWeightX", UnitWeightX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPackCodeY", PackCodeY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pUnitWeightY", UnitWeightY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pNumPerPackX", NumPerPackX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pSlitLotNoX", SlitLotNoX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pNumPerPackY", NumPerPackY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pSlitLotNoY", SlitLotNoY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1X", PC1X)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pGradeX", GradeX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Y", PC1Y)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pGradeY", GradeY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLengthX", LengthX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCoreCodeX", CoreCodeX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pLengthY", LengthY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCoreCodeY", CoreCodeY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pThicknessX", ThicknessX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pBarcodeX", BarcodeX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pThicknessY", ThicknessY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pBarcodeY", BarcodeY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pTypeX", TypeX)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pTypeY", TypeY)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRadioButton1", RadioButton1)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRadioButton2", RadioButton2)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompanyCode", Company_Code)).Direction = ParameterDirection.Input;
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
