using System.Data;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class SlitSeries : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                if (Direction != 1)
                {
                    Direction = 0;
                }
                return _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted);
            }
        }

        public static DataTable GetData(string ID)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetData(ID);
            }
        }

        public static DataTable GetDDLData(string Ind)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetDDLData(Ind);
            }
        }

        public static DataTable GetRefByComp(string Comp)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetRefByComp(Comp);
            }
        }

        public static DataTable GetDDLData2(string refno)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetDDLData2(refno);
            }
        }

        public static DataTable GetDDLData2_Rev01(string refno, string str_PRODLINE_NO)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetDDLData2_Rev01(refno, str_PRODLINE_NO);
            }
        }

        public static DataTable GetDDLPC1Cust(string pRefNo, string pPC2_Mother)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetDDLPC1Cust(pRefNo, pPC2_Mother);
            }
        }

        public static DataTable GetDDLPC1Cust_Rev01(string pRefNo, string pPC2_Mother,
                                                     string str_PRODLINE_NO, string str_PC1_MOTHER,
                                                     string str_PC2_MOTHER)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetDDLPC1Cust_REV01(pRefNo, pPC2_Mother, str_PRODLINE_NO, str_PC1_MOTHER, str_PC2_MOTHER);
            }
        }

        public static DataTable GetPCMOTHER()
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetPCMOTHER();
            }
        }

        public static DataTable GetPCCUSTOMER()
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetPCCUSTOMER();
            }
        }

        public static DataTable GetPC2CUST(string refno)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetPC2CUST(refno);
            }
        }

        public static DataTable GetPCMOTHER2(string refno)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetPCMOTHER2(refno);
            }
        }

        public static DataTable GetPRODLINE2(string refno)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetPRODLINE2(refno);
            }
        }

        public static DataTable GetUNITWEIGHT2(string pc2)
        {
            using (var _dal = new DAL.SlitSeries())
            {
                return _dal.GetUNITWEIGHT2(pc2);
            }
        }

        public static string Maint(string ID, string CompCode, string RefNo, string LotNo, string PC1_Mother, string PC2_Mother,
                                    string PC1_Cust, string PC2_Cust, string ProdLine,
                                    string No_Of_Slit, string Plan_Year_Mth, string Type_Of_Slit,
                                    string RecType)
        {
            using (var _Dal = new DAL.SlitSeries())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.Maint(ID, CompCode, RefNo, LotNo, PC1_Mother, PC2_Mother, PC1_Cust, PC2_Cust, ProdLine, No_Of_Slit, Plan_Year_Mth, Type_Of_Slit, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

                if (result == "1")
                {
                    _Dal.Commit();
                }
                else
                {
                    _Dal.Rollback();
                }
                return result;
            }
        }

        public static string Maint(string ID, string CompCode, string RefNo, string LotNo, string PC1_Mother, string PC2_Mother,
                                    string PC1_Cust, string PC2_Cust, string ProdLine,
                                    string No_Of_Slit, string Plan_Year_Mth, string Type_Of_Slit,
                                    string RecType, string userId, string userIp)
        {
            using (var _Dal = new DAL.SlitSeries())
            {
                string result = _Dal.Maint(ID, CompCode, RefNo, LotNo, PC1_Mother, PC2_Mother, PC1_Cust, PC2_Cust,
                    ProdLine, No_Of_Slit, Plan_Year_Mth, Type_Of_Slit, RecType, userId, userIp);

                if (result == "1")
                {
                    _Dal.Commit();
                }
                else
                {
                    _Dal.Rollback();
                }
                return result;
            }
        }

        public static string CreateSlitRec(string B_Company_Code, int B_ID_PC2_LOTNO,
                                            int B_TYPE_OF_SLIT, int B_MATRIX_POS, int B_MATRIX_INC, string B_LOTNO, int B_NO_OF_SLIT, string B_User_ID)
        {
            using (var _Dal = new DAL.SlitSeries())
            {
                string result = _Dal.CreateSlitRec(B_Company_Code, B_ID_PC2_LOTNO, B_TYPE_OF_SLIT, B_MATRIX_POS, B_MATRIX_INC, B_LOTNO, B_NO_OF_SLIT, B_User_ID, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

                if (result == "1")
                {
                    _Dal.Commit();
                }
                else
                {
                    _Dal.Rollback();
                }
                return result;
            }
        }
    }
}
