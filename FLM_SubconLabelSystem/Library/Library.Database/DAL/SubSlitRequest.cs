using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Database.DAL
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class SubSlitRequest : Library.SQLServer.Connection
    {
        public SubSlitRequest() : base("PFR_Label_DB") { }

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

        internal DataTable GetUserData(string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_SSR_USER_DATA";
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

            base._cmd.CommandText = "SP_SSR_GETDDLDATA";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pValue", Value)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPC2Data(string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_SSR_PC2_DATA";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetProdlineIDData(string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_SSR_GETPRODLINEIDDATA";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@PRODLINE", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPC1IDData(string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_SSR_GETPC1IDDATA";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@PC1", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetPC2IDData(string ID)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_SSR_GETPC2IDDATA";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@PC2", ID)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable chkRefNo(string RefNo)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_SSR_CHK_REFNO";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable chkPC2Mother(string RefNo, string PC2Mother, string ProdLine, string PC1Mother)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_PC2_MOTHER_Validate";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC2_MOTHER_ID", PC2Mother)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPRODLINE_NO", ProdLine)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC1_MOTHER_TEXT", PC1Mother)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetIDSSR(string RefNo, int RevCount)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_ID_SSR";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pRev", RevCount)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetMotherSeq(int IDSSR, string SeqMother)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_Mother_Seq";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pIDSSR", IDSSR)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@SeqMother", SeqMother)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetSSR_INFO(string RefNo, int IDSSR)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_GET_REQ_STATUS";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pID_SSR", IDSSR)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable chkPC2Child(string RefNo, string PC2Mother, string PC2Child)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_PC2_Child_Validate";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC2_MOTHER_ID", PC2Mother)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC2_Child_ID", PC2Child)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal string SubSlitMaint(string ID, string pCompFrom, string pCompTo, string pRefNo, string pRev, string pDateReq, string pReqStat, string pVenStat, int RecType, string UpdatedBy, string UpdatedLoc, string UpdatedCC)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SUBSLIT_REQUEST_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompFrom", pCompFrom)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompTo", pCompTo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRev", pRev)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pDateReq", pDateReq)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pReqStat", pReqStat)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pVenStat", pVenStat)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitDup(string ID, string pCompFrom, string pCompTo, string pRefNo, int pRev, string pDateReq, string pReqStat, string pVenStat, int RecType, string UpdatedBy, string UpdatedLoc, string UpdatedCC)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SUBSLIT_REQUEST_DUP";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompFrom", pCompFrom)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCompTo", pCompTo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRev", pRev)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pDateReq", pDateReq)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pReqStat", pReqStat)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pVenStat", pVenStat)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitMotherMaint(string ID, string RefNo, string pPC1Mom, string pPC2Mom,
                                            string pProdLine, string pQty, string pMWeight, string pMTotWeight,
                                            string pSubWaste, string pETD, string pETA, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "0";
            try
            {
                base._cmd.CommandText = "SP_SUBSLIT_REQUEST_MOTHER_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Mom", pPC1Mom)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mom", pPC2Mom)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pProdLine", pProdLine)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pQty", pQty)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pMWeight", pMWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pMTotWeight", pMTotWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pSubWaste", pSubWaste)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pETD", pETD)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pETA", pETA)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int)).Direction = ParameterDirection.Output;

                base._cmd.ExecuteReader();

                if (base._cmd.Parameters["@ReturnID"].Value.ToString() != "")
                {
                    result = base._cmd.Parameters["@ReturnID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitMotherDup(string ID, string RefNo, string pPC1Mom, string pPC2Mom,
                                          string pProdLine, string pQty, string pMWeight, string pMTotWeight,
                                          string pSubWaste, string pETD, string pETA, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "0";
            try
            {
                base._cmd.CommandText = "SP_SUBSLIT_REQUEST_MOTHER_DUP";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Mom", pPC1Mom)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mom", pPC2Mom)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pProdLine", pProdLine)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pQty", pQty)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pMWeight", pMWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pMTotWeight", pMTotWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pSubWaste", pSubWaste)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pETD", pETD)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pETA", pETA)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int)).Direction = ParameterDirection.Output;

                base._cmd.ExecuteReader();

                if (base._cmd.Parameters["@ReturnID"].Value.ToString() != "")
                {
                    result = base._cmd.Parameters["@ReturnID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitChildMaint(string ID, string RefNo, string pPC1Cust, string pPC2Cust, string pCQty,
                                           string pCUnitWeight, string pCTotWeight, string pRemark, string pPC2Mother, string pProdLineNo, string pPC1Mother, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SUBSLIT_REQUEST_CHILD_MAINT";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Cust", pPC1Cust)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Cust", pPC2Cust)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCQty", pCQty)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCUnitWeight", pCUnitWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCTotWeight", pCTotWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRemark", pRemark)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mother", pPC2Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pProdLine_No", pProdLineNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Mother", pPC1Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitChildDup(int ID, string RefNo, string pPC1Cust, string pPC2Cust, string pCQty,
                                         string pCUnitWeight, string pCTotWeight, string pRemark, string pPC2Mother, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SUBSLIT_REQUEST_CHILD_DUP";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Cust", pPC1Cust)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Cust", pPC2Cust)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCQty", pCQty)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCUnitWeight", pCUnitWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCTotWeight", pCTotWeight)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRemark", pRemark)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mother", pPC2Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitChildDel(string pRefNo, string pIdSubMomReq, string pPC2Mother, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SSR_CHILD_Del";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pIdSubMomReq", pIdSubMomReq)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mother", pPC2Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitChildDelFrList(string RefNo, string pPC2Mother, string pPC1Mother, string pProdLineNo, string pSeqMother, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SSR_CHILDDEL_FRLIST";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mother", pPC2Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Mother", pPC1Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pProdLineNo", pProdLineNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pSeqMother", pSeqMother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SubSlitMotherDel(string RefNo, string pPC2Mother, string pPC1Mother, string pProdLineNo, string pSeqMother, int RecType, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SSR_MOTHER_Del";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", RefNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC2Mother", pPC2Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1Mother", pPC1Mother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pProdLineNo", pProdLineNo)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pSeqMother", pSeqMother)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pRecType", RecType)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string UpdateReq(string Refno, int Revision, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_Update_Req";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@RefNo", Refno)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@Revision", Revision)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal string SSRUpdateStat(string Refno, int ID_SSR, string Req_Status, string Vend_Status, string UpdatedBy, string UpdatedLoc)
        {
            string result = "1";
            try
            {
                base._cmd.CommandText = "SP_SSR_Update_Stat";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();

                base._cmd.Parameters.Add(new SqlParameter("@pRefNo", Refno)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pID_SSR", ID_SSR)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pReq_Status", Req_Status)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pVendor_Status", Vend_Status)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedBy", UpdatedBy)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = ParameterDirection.Input;

                base._cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal DataTable SSRList(string pRefNo, string pSeqMother)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_SSR_LIST";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pSeqMother", pSeqMother)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable SSRList_02(string pRefNo, string pPC2_Mother, string pstr_ProLine)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_SSR_LIST_02";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC2_Mother", pPC2_Mother)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@str_ProLine", pstr_ProLine)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable SSRListExist(string pRefNo, int pID_SubSlit_Req)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_SSR_LISTExist";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pID_SubSlit_Req", pID_SubSlit_Req)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable GetProdLineID(string pProdLineNo)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_ID_MM_PRODLINE";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pProdLine", pProdLineNo)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable GetPC1ID(string pPC1Mother)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_ID_MM_PC1";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pPC1", pPC1Mother)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable GetPC2ID(string pPC2Mother)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_ID_MM_PC2";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pPC2", pPC2Mother)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable CHECK_SUBMITTED_REQ(string RefNo, int Revision)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_CHECK_SUBMITTED_REQ";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@RefNo", RefNo)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@Revision", Revision)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable chkPC2Mom(string ID, string pRefNo)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_CHK_PC2_MOTHER";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pID", ID)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetASRDDL(string CompanyCode)
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_ASR_SEARCH_REF_NO_SEL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pCompanyCode", CompanyCode)).Direction = ParameterDirection.Input;

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable GetASRDDL2()
        {
            DataTable result = new DataTable();

            base._cmd.CommandText = "SP_ASR_SEARCH_REF_NO_SEL2";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._rdr = base._cmd.ExecuteReader();
            result.Load(base._rdr);
            return result;
        }

        internal DataTable ASRList(string pRefNo)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_ASR_SEARCH_SEL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable ASRListALL()
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_ASR_SEARCH_SEL_ALL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable GetRefNoList()
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_REF_NO_LIST";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal string Chk_next(string ProdLine, string PC1)
        {
            string result = string.Empty;
            try
            {
                base._cmd.CommandText = "SP_CHK_REQ_FORM";
                base._cmd.CommandType = CommandType.StoredProcedure;
                base._cmd.CommandTimeout = 0;
                base._cmd.Parameters.Clear();
                base._cmd.Parameters.Add(new SqlParameter("@pProdLine", ProdLine)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@pPC1", PC1)).Direction = ParameterDirection.Input;
                base._cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int)).Direction = ParameterDirection.Output;

                base._cmd.ExecuteReader();

                if (base._cmd.Parameters["@ReturnID"].Value.ToString() != "")
                {
                    result = base._cmd.Parameters["@ReturnID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        internal DataTable Chk_label(string ind, string ProdLine, string pc1, string pc2)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_CHK_LABEL";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();
            base._cmd.Parameters.Add(new SqlParameter("@pInd", ind)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pProdLine", ProdLine)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC1", pc1)).Direction = ParameterDirection.Input;
            base._cmd.Parameters.Add(new SqlParameter("@pPC2", pc2)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }

        internal DataTable Chk_final(string pRefNo)
        {
            DataTable _obj_dt = new DataTable();

            base._cmd.CommandText = "SP_SUBSLIT_CHECK";
            base._cmd.CommandType = CommandType.StoredProcedure;
            base._cmd.CommandTimeout = 0;
            base._cmd.Parameters.Clear();

            base._cmd.Parameters.Add(new SqlParameter("@pRefNo", pRefNo)).Direction = ParameterDirection.Input;

            _obj_dt.Load(base._cmd.ExecuteReader());
            return _obj_dt;
        }
    }
}
