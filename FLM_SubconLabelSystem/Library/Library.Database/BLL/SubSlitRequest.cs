using System;
using System.Data;
using System.Text;

namespace Library.Database.BLL
{
    /// <summary>
    /// Business Logic Layer
    /// ---------------------------------
    /// 18 Feb 2012   Yeon    Initial Version
    /// </summary>
    public class SubSlitRequest : Library.Root.Other.BusinessLogicBase
    {
        private static int chkint = 0;

        public static DataTable GetUserData(string ID)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetUserData(ID);
            }
        }

        public static DataTable GetDLLData(string Value, string ID)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetDLLData(Value, ID);
            }
        }

        public static DataTable GetPC2Data(string ID)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetPC2Data(ID);
            }
        }

        public static DataTable GetProdlineIDData(string ID)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetProdlineIDData(ID);
            }
        }

        public static DataTable GetPC1IDData(string ID)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetPC1IDData(ID);
            }
        }

        public static DataTable GetPC2IDData(string ID)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetPC2IDData(ID);
            }
        }

        public static DataTable chkRefNo(string RefNo)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.chkRefNo(RefNo);
            }
        }

        public static DataTable chkPC2Mother(string IDSubSlitReq, string PC2Mother, string ProdLine, string PC1Mother)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.chkPC2Mother(IDSubSlitReq, PC2Mother, ProdLine, PC1Mother);
            }
        }

        public static DataTable GetIDSSR(string Refno, int RevCount)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetIDSSR(Refno, RevCount);
            }
        }

        public static DataTable GetMotherSeq(int IDSSR, string SeqMother)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetMotherSeq(IDSSR, SeqMother);
            }
        }

        public static DataTable chkPC2Child(string IDSubSlitReq, string PC2Mother, string PC2Child)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.chkPC2Child(IDSubSlitReq, PC2Mother, PC2Child);
            }
        }

        public static string SubSlitMaint(string ID, string pCompFrom, string pCompTo, string pRefNo, string pRev, string pDateReq, string pReqStat, string pVenStat, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitMaint(ID, pCompFrom, pCompTo, pRefNo, pRev, pDateReq, pReqStat, pVenStat, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString(), cc);

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

        public static string SubSlitMaint(string ID, string pCompFrom, string pCompTo, string pRefNo, string pRev, string pDateReq, string pReqStat, string pVenStat, int RecType, string userId, string userIp, string companyCode)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitMaint(ID, pCompFrom, pCompTo, pRefNo, pRev, pDateReq, pReqStat, pVenStat, RecType, userId, userIp, companyCode);

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

        public static string SubSlitDup(string ID, string pCompFrom, string pCompTo, string pRefNo, int pRev, string pDateReq, string pReqStat, string pVenStat, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitDup(ID, pCompFrom, pCompTo, pRefNo, pRev, pDateReq, pReqStat, pVenStat, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString(), cc);

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

        public static string SubSlitDup(string ID, string pCompFrom, string pCompTo, string pRefNo, int pRev, string pDateReq, string pReqStat, string pVenStat, int RecType, string userId, string userIp, string companyCode)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitDup(ID, pCompFrom, pCompTo, pRefNo, pRev, pDateReq, pReqStat, pVenStat, RecType, userId, userIp, companyCode);

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

        public static string SubSlitMotherMaint(string ID, string pIdSubReq, string pPC1Mom, string pPC2Mom,
                                                 string pProdLine, string pQty, string pMWeight, string pMTotWeight,
                                                 string pSubWaste, string pETD, string pETA, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitMotherMaint(ID, pIdSubReq, pPC1Mom, pPC2Mom, pProdLine, pQty, pMWeight, pMTotWeight, pSubWaste, pETD, pETA, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

                if (int.TryParse(result, out chkint) && result != "0")
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

        public static string SubSlitMotherMaint(string ID, string pIdSubReq, string pPC1Mom, string pPC2Mom,
                                                 string pProdLine, string pQty, string pMWeight, string pMTotWeight,
                                                 string pSubWaste, string pETD, string pETA, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitMotherMaint(ID, pIdSubReq, pPC1Mom, pPC2Mom, pProdLine, pQty, pMWeight, pMTotWeight, pSubWaste, pETD, pETA, RecType, userId, userIp);

                if (int.TryParse(result, out chkint) && result != "0")
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

        public static string SubSlitMotherDup(string ID, string pIdSubReq, string pPC1Mom, string pPC2Mom,
                                               string pProdLine, string pQty, string pMWeight, string pMTotWeight,
                                               string pSubWaste, string pETD, string pETA, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitMotherDup(ID, pIdSubReq, pPC1Mom, pPC2Mom, pProdLine, pQty, pMWeight, pMTotWeight, pSubWaste, pETD, pETA, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

                if (int.TryParse(result, out chkint) && result != "0")
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

        public static string SubSlitMotherDup(string ID, string pIdSubReq, string pPC1Mom, string pPC2Mom,
                                               string pProdLine, string pQty, string pMWeight, string pMTotWeight,
                                               string pSubWaste, string pETD, string pETA, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitMotherDup(ID, pIdSubReq, pPC1Mom, pPC2Mom, pProdLine, pQty, pMWeight, pMTotWeight, pSubWaste, pETD, pETA, RecType, userId, userIp);

                if (int.TryParse(result, out chkint) && result != "0")
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

        public static string SubSlitChildDel(string pRefNo, string pIdSubMomReq, string pPC2Mother, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitChildDel(pRefNo, pIdSubMomReq, pPC2Mother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string SubSlitChildDel(string pRefNo, string pIdSubMomReq, string pPC2Mother, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitChildDel(pRefNo, pIdSubMomReq, pPC2Mother, RecType, userId, userIp);

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

        public static string SubSlitChildDelFrList(string pIdSubMomReq, string pPC2Mother, string pPC1Mother, string pProdLineNo, string pSeqMother, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitChildDelFrList(pIdSubMomReq, pPC2Mother, pPC1Mother, pProdLineNo, pSeqMother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string SubSlitChildDelFrList(string pIdSubMomReq, string pPC2Mother, string pPC1Mother, string pProdLineNo, string pSeqMother, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitChildDelFrList(pIdSubMomReq, pPC2Mother, pPC1Mother, pProdLineNo, pSeqMother, RecType, userId, userIp);

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

        public static string SubSlitMotherDel(string pIdSubMomReq, string pPC2Mother, string pPC1Mother, string pProdLineNo, string pSeqMother, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitMotherDel(pIdSubMomReq, pPC2Mother, pPC1Mother, pProdLineNo, pSeqMother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string SubSlitMotherDel(string pIdSubMomReq, string pPC2Mother, string pPC1Mother, string pProdLineNo, string pSeqMother, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitMotherDel(pIdSubMomReq, pPC2Mother, pPC1Mother, pProdLineNo, pSeqMother, RecType, userId, userIp);

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

        public static string SubSlitChildMaint(string ID, string pIdSubMomReq, string pPC1Cust, string pPC2Cust, string pCQty, string pCUnitWeight, string pCTotWeight, string pRemark, string pPC2Mother, string pProdLineNo, string pPC1Mother, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitChildMaint(ID, pIdSubMomReq, pPC1Cust, pPC2Cust, pCQty, pCUnitWeight, pCTotWeight, pRemark, pPC2Mother, pProdLineNo, pPC1Mother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string SubSlitChildMaint(string ID, string pIdSubMomReq, string pPC1Cust, string pPC2Cust, string pCQty, string pCUnitWeight, string pCTotWeight, string pRemark, string pPC2Mother, string pProdLineNo, string pPC1Mother, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitChildMaint(ID, pIdSubMomReq, pPC1Cust, pPC2Cust, pCQty, pCUnitWeight, pCTotWeight, pRemark, pPC2Mother, pProdLineNo, pPC1Mother, RecType, userId, userIp);

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

        public static string SubSlitChildDup(int ID, string pIdSubMomReq, string pPC1Cust, string pPC2Cust, string pCQty, string pCUnitWeight, string pCTotWeight, string pRemark, string pPC2Mother, int RecType)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string cc = System.Web.HttpContext.Current.Session["gstrUserComp"].ToString();
                string result = _Dal.SubSlitChildDup(ID, pIdSubMomReq, pPC1Cust, pPC2Cust, pCQty, pCUnitWeight, pCTotWeight, pRemark, pPC2Mother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string SubSlitChildDup(int ID, string pIdSubMomReq, string pPC1Cust, string pPC2Cust, string pCQty, string pCUnitWeight, string pCTotWeight, string pRemark, string pPC2Mother, int RecType, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SubSlitChildDup(ID, pIdSubMomReq, pPC1Cust, pPC2Cust, pCQty, pCUnitWeight, pCTotWeight, pRemark, pPC2Mother, RecType, userId, userIp);

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

        public static string UpdateReq(string RefNo, int Revision)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.UpdateReq(RefNo, Revision, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string UpdateReq(string RefNo, int Revision, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.UpdateReq(RefNo, Revision, userId, userIp);

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

        public static string SSRUpdateStat(string RefNo, int ID_SSR, string Req_Status, string Vend_Status)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string str = System.Web.HttpContext.Current.Session["gstrUserID"].ToString();
                string result = _Dal.SSRUpdateStat(RefNo, ID_SSR, Req_Status, Vend_Status, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString());

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

        public static string SSRUpdateStat(string RefNo, int ID_SSR, string Req_Status, string Vend_Status, string userId, string userIp)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.SSRUpdateStat(RefNo, ID_SSR, Req_Status, Vend_Status, userId, userIp);

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

        public static DataTable SSRListExist(string pRefNo, int pID_SubSlit_Req)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.SSRListExist(pRefNo, pID_SubSlit_Req);
            }
        }

        public static DataTable SSRList(string pRefNo, string pSeqMother)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.SSRList(pRefNo, pSeqMother);
            }
        }

        public static DataTable SSRList_02(string pRefNo, string pPC2_Mother, string pstr_ProLine)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.SSRList_02(pRefNo, pPC2_Mother, pstr_ProLine);
            }
        }

        public static DataTable GetProdLineID(string pProdLineNo)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetProdLineID(pProdLineNo);
            }
        }

        public static DataTable GetPC1ID(string pPC1Mother)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetPC1ID(pPC1Mother);
            }
        }

        public static DataTable GetPC2ID(string pPC2Mother)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetPC2ID(pPC2Mother);
            }
        }

        public static DataTable CHECK_SUBMITTED_REQ(string RefNo, int Revision)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.CHECK_SUBMITTED_REQ(RefNo, Revision);
            }
        }

        public static DataTable GetSSR_INFO(string RefNo, int IDSSR)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetSSR_INFO(RefNo, IDSSR);
            }
        }

        public static DataTable chkPC2Mom(string ID, string pRefNo)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.chkPC2Mom(ID, pRefNo);
            }
        }

        public static ListCollection List(string Table, string TableID, string SearchField, string SearchValue, string SortField, int Direction,
                                          int Page, int Deleted)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                if (Direction != 1)
                {
                    Direction = 0;
                }
                return _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted);
            }
        }

        public static DataTable GetASRDDL(string CompanyCode)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetASRDDL(CompanyCode);
            }
        }

        public static DataTable GetASRDDL2()
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetASRDDL2();
            }
        }

        public static string GET_SSR_TO_EXCEL_BK_30MAR2018(string pRefNo, int pID_SubSlit_Req, string pUserID, int M_Qty,
                                                            decimal M_Total_Weight, int C_Qty, decimal C_Total_Weight, decimal SubSlitWaste)
        {
            DataTable _obj_dt;
            DataTable _obj_dt_1;
            DataTable _obj_dt_2;

            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                _obj_dt = _dal.SSRListExist(pRefNo, pID_SubSlit_Req);
                _obj_dt_1 = _dal.GetUserData(pUserID);
                _obj_dt_2 = _dal.SSRListExist(pRefNo, pID_SubSlit_Req);
            }

            StringBuilder _obj_sb = new StringBuilder();
            string q = "\"";

            string _str_table = "<table>";
            string _str_detail = "";
            string _str_detail_2 = "";
            string _str_detail_3 = "";
            string _str_detail_4 = "";
            string _str_detail_5 = "";

            _str_detail = "<tr><td></td>";
            _str_detail += "<td></td>";
            _str_detail += string.Format("<td align={0}center{0}><h3>Sub-Slitting Request</h3></td>", q);
            _str_detail += "<td></td>";
            _str_detail += "<td></td>";
            _str_detail += "<td></td></tr>";

            _str_detail += string.Format("<tr><td width={0}130px{0}>To</td>", q);
            _str_detail += string.Format("<td width={0}10{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0}>_obj1</td>", q);
            _str_detail += string.Format("<td width={0}130px{0}>Ref No</td>", q);
            _str_detail += string.Format("<td width={0}10{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0}>_obj2</td></tr>", q);

            _str_detail += string.Format("<tr><td width={0}130px{0}>Department</td>", q);
            _str_detail += string.Format("<td width={0}10px{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0}>_obj3</td>", q);
            _str_detail += string.Format("<td width={0}130px{0}>Date</td>", q);
            _str_detail += string.Format("<td width={0}10px{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0} align={0}left{0}>_obj4</td></tr>", q);

            _str_detail += string.Format("<tr><td width={0}130px{0}>By</td>", q);
            _str_detail += string.Format("<td width={0}10px{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0}>_obj5</td>", q);
            _str_detail += string.Format("<td width={0}130px{0}>Rev</td>", q);
            _str_detail += string.Format("<td width={0}10px{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0} align={0}left{0}>_obj6</td></tr>", q);

            _str_detail += string.Format("<tr><td width={0}100px{0}>Requestor Status</td>", q);
            _str_detail += string.Format("<td width={0}10px{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0}>_obj7</td>", q);
            _str_detail += string.Format("<td width={0}130px{0}>Vendor Status</td>", q);
            _str_detail += string.Format("<td width={0}10px{0}>:</td>", q);
            _str_detail += string.Format("<td width={0}280px{0}>_obj8</td></tr>", q);

            _str_detail = _str_detail.Replace("_obj1", "{0}");
            _str_detail = _str_detail.Replace("_obj2", "{1}");
            _str_detail = _str_detail.Replace("_obj3", "{2}");
            _str_detail = _str_detail.Replace("_obj4", "{3}");
            _str_detail = _str_detail.Replace("_obj5", "{4}");
            _str_detail = _str_detail.Replace("_obj6", "{5}");
            _str_detail = _str_detail.Replace("_obj7", "{6}");
            _str_detail = _str_detail.Replace("_obj8", "{7}");

            _str_detail_4 = string.Format("<table><tr><td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", q);
            for (int i = 0; i < 13; i++) _str_detail_4 += string.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", q);
            _str_detail_4 += string.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td></tr></table>", q);

            _str_detail_3 = string.Format("<tr><td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PRODUCTION LINE</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC1 MOTHER</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC2 MOTHER</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>QTY (ROLL)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>UNIT WEIGHT (KG)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>TOTAL WEIGHT (KG)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC1 CUSTOMER</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC2 CUSTOMER</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>QTY (ROLL)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>UNIT WEIGHT (KG)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>TOTAL WEIGHT (KG)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>SUB-SLIT WASTE (KG)</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>ETD PFR</td></strong>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>ETA SUBSLIT CONTRACTOR</strong></td>", q);
            _str_detail_3 += string.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>REMARK</strong></td></tr>", q);

            _str_detail_2 = "<tr><td>_obj1</td>";
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj2</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj3</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj4</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj5</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj6</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj7</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj8</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj9</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj_10</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj_11</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj_12</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj_13</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj_14</td>", q);
            _str_detail_2 += string.Format("<td align={0}center{0}>_obj_15</td>", q);
            _str_detail_2 += "</tr>";

            _str_detail_2 = _str_detail_2.Replace("_obj1", "{0}");
            _str_detail_2 = _str_detail_2.Replace("_obj2", "{1}");
            _str_detail_2 = _str_detail_2.Replace("_obj3", "{2}");
            _str_detail_2 = _str_detail_2.Replace("_obj4", "{3}");
            _str_detail_2 = _str_detail_2.Replace("_obj5", "{4}");
            _str_detail_2 = _str_detail_2.Replace("_obj6", "{5}");
            _str_detail_2 = _str_detail_2.Replace("_obj7", "{6}");
            _str_detail_2 = _str_detail_2.Replace("_obj8", "{7}");
            _str_detail_2 = _str_detail_2.Replace("_obj9", "{8}");
            _str_detail_2 = _str_detail_2.Replace("_obj_10", "{9}");
            _str_detail_2 = _str_detail_2.Replace("_obj_11", "{10}");
            _str_detail_2 = _str_detail_2.Replace("_obj_12", "{11}");
            _str_detail_2 = _str_detail_2.Replace("_obj_13", "{12}");
            _str_detail_2 = _str_detail_2.Replace("_obj_14", "{13}");
            _str_detail_2 = _str_detail_2.Replace("_obj_15", "{14}");

            _str_detail_5 = "<tr><td>&nbsp</td>";
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>Total</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>" + M_Qty.ToString() + "</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>" + M_Total_Weight.ToString() + "</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>" + C_Qty.ToString() + "</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>" + C_Total_Weight.ToString() + "</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>" + SubSlitWaste.ToString() + "</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>_&nbsp</td>", q);
            _str_detail_5 += string.Format("<td align={0}center{0}>&nbsp</td>", q);
            _str_detail_5 += "</tr>";

            _obj_sb.Append(_str_table);

            if (_obj_dt.Rows.Count > 0)
            {
                _obj_sb.Append(string.Format(_str_detail,
                    _obj_dt.Rows[0]["COMPANYTO"].ToString().Trim(),
                    _obj_dt.Rows[0]["REFNO"].ToString().Trim(),
                    _obj_dt_1.Rows[0]["DEPARTMENT"].ToString().Trim(),
                    _obj_dt.Rows[0]["DATEREQ"].ToString().Trim(),
                    _obj_dt_1.Rows[0]["NAME"].ToString().Trim(),
                    _obj_dt.Rows[0]["REVISIONCOUNT"].ToString().Trim(),
                    _obj_dt.Rows[0]["REQUEST_STATUS"].ToString().Trim(),
                    _obj_dt.Rows[0]["VENDOR_STATUS"].ToString().Trim()));

                _obj_sb.Append("</TABLE>");
                _obj_sb.Append(string.Format(_str_detail_4));
                _obj_sb.Append(string.Format("<table border={0}1px solid black{0}>", q));

                if (_obj_dt_2.Rows.Count > 0)
                {
                    _obj_sb.Append(string.Format(_str_detail_3));

                    for (int _int_iLoop = 0; _int_iLoop <= (_obj_dt_2.Rows.Count - 1); _int_iLoop++)
                    {
                        _obj_sb.Append(string.Format(_str_detail_2,
                            _obj_dt_2.Rows[_int_iLoop]["PRODLINE_NO"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["PC1_MOTHER"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["PC2_MOTHER"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["QTY"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["M_WEIGHT"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["M_TOTAL_WEIGHT"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["PC1_CUST"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["PC2_CUST"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["C_QTY"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["C_WEIGHT"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["C_TOTAL_WEIGHT"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["SUBSLIT_WASTE"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["ETD"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["ETA"].ToString().Trim(),
                            _obj_dt_2.Rows[_int_iLoop]["REMARK"].ToString().Trim()));
                    }

                    _obj_sb.Append(string.Format(_str_detail_5));
                    _obj_sb.Append("</TABLE>");
                }

                _str_detail = _obj_sb.ToString();
            }
            else
            {
                _str_detail = "";
            }

            _obj_dt.Dispose();
            return _str_detail;
        }

        public static string GET_SSR_TO_EXCEL(string pRefNo, int pID_SubSlit_Req, string pUserID,
                                               int M_Qty, decimal M_Total_Weight, int C_Qty,
                                               decimal C_Total_Weight, decimal SubSlitWaste,
                                               DataTable pobj_data)
        {
            StringBuilder _obj_sb = new StringBuilder();
            DataTable _obj_dt;
            DataTable _obj_dt_1;

            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                _obj_dt = _dal.SSRListExist(pRefNo, pID_SubSlit_Req);
                _obj_dt_1 = _dal.GetUserData(pUserID);
            }

            _obj_sb.AppendLine("<table>");
            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td align='center'><h3>Sub-Slitting Request</h3></td><td></td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("</tr>");

            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td width='130px'>To</td>");
            _obj_sb.AppendLine("<td width='10'>:</td>");
            _obj_sb.AppendLine("<td width='280px'>" + _obj_dt.Rows[0]["COMPANYTO"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td width='130px'>Ref No</td>");
            _obj_sb.AppendLine("<td width='10'>:</td>");
            _obj_sb.AppendLine("<td width='280px'>" + _obj_dt.Rows[0]["REFNO"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("</tr>");

            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td width='130px'>Department</td>");
            _obj_sb.AppendLine("<td width='10px'>:</td>");
            _obj_sb.AppendLine("<td width='280px'>" + _obj_dt_1.Rows[0]["DEPARTMENT"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td width='130px'>Date</td>");
            _obj_sb.AppendLine("<td width='10px'>:</td>");
            _obj_sb.AppendLine(@"<td width='280px' align='left' style='mso-number-format:""Short Date"";'>" + _obj_dt.Rows[0]["DATEREQ"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("</tr>");

            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td width='130px'>By</td>");
            _obj_sb.AppendLine("<td width='10px'>:</td>");
            _obj_sb.AppendLine("<td width='280px'>" + _obj_dt_1.Rows[0]["NAME"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td width='130px'>Rev</td>");
            _obj_sb.AppendLine("<td width='10px'>:</td>");
            _obj_sb.AppendLine("<td width='280px' align='left'>" + _obj_dt.Rows[0]["REVISIONCOUNT"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("</tr>");

            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td width='100px'>Requestor Status</td>");
            _obj_sb.AppendLine("<td width='10px'>:</td>");
            _obj_sb.AppendLine("<td width='280px'>" + _obj_dt.Rows[0]["REQUEST_STATUS"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("<td></td>");
            _obj_sb.AppendLine("<td width='130px'>Vendor Status</td>");
            _obj_sb.AppendLine("<td width='10px'>:</td>");
            _obj_sb.AppendLine("<td width='280px'>" + _obj_dt.Rows[0]["VENDOR_STATUS"].ToString().Trim() + "</td>");
            _obj_sb.AppendLine("</tr>");
            _obj_sb.AppendLine("</TABLE>");

            _obj_sb.AppendLine("<table>");
            _obj_sb.AppendLine("<tr>");
            for (int i = 0; i < 15; i++) _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>");
            _obj_sb.AppendLine("</tr>");
            _obj_sb.AppendLine("</table>");

            _obj_sb.AppendLine("<table border='1px solid black'>");
            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PRODUCTION LINE</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC1 MOTHER</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC2 MOTHER</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>QTY (ROLL)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>UNIT WEIGHT (KG)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>TOTAL WEIGHT (KG)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC1 CUSTOMER</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC2 CUSTOMER</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>QTY (ROLL)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>UNIT WEIGHT (KG)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>TOTAL WEIGHT (KG)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>SUB-SLIT WASTE (KG)</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>ETD PFR</td></strong>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>ETA SUBSLIT CONTRACTOR</strong></td>");
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>REMARK</strong></td>");
            _obj_sb.AppendLine("</tr>");

            for (int _int_iResult = 0; _int_iResult <= (pobj_data.Rows.Count - 1); _int_iResult++)
            {
                _obj_sb.AppendLine("<tr>");
                _obj_sb.AppendLine("<td>" + pobj_data.Rows[_int_iResult]["PRODLINE_NO"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["PC1_MOTHER"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["PC2_MOTHER"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["QTY"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine(@"<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" + pobj_data.Rows[_int_iResult]["M_WEIGHT"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine(@"<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" + pobj_data.Rows[_int_iResult]["M_TOTAL_WEIGHT"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["PC1_CUST"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["PC2_CUST"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["C_QTY"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine(@"<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" + pobj_data.Rows[_int_iResult]["C_WEIGHT"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine(@"<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" + pobj_data.Rows[_int_iResult]["C_TOTAL_WEIGHT"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["SUBSLIT_WASTE"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["ETD"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["ETA"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("<td align='center'>" + pobj_data.Rows[_int_iResult]["REMARK"].ToString().Trim() + "</td>");
                _obj_sb.AppendLine("</tr>");
            }

            _obj_sb.AppendLine("<tr>");
            _obj_sb.AppendLine("<td>&nbsp</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine("<td align='center'>Total</td>");
            _obj_sb.AppendLine("<td align='center'>" + M_Qty.ToString() + "</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine(@"<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" + M_Total_Weight.ToString() + "</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine("<td align='center'>" + C_Qty.ToString() + "</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine(@"<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" + C_Total_Weight.ToString() + "</td>");
            _obj_sb.AppendLine("<td align='center'>" + SubSlitWaste.ToString() + "</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine("<td align='center'>_&nbsp</td>");
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>");
            _obj_sb.AppendLine("</tr>");
            _obj_sb.AppendLine("</table>");

            return _obj_sb.ToString();
        }

        public static string GET_ASR_TO_EXCEL(string pRefNo)
        {
            DataTable _obj_dt;

            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                _obj_dt = _dal.ASRList(pRefNo);
            }

            StringBuilder _obj_sb = new StringBuilder();
            string q = "\"";

            string _str_table = string.Format("<table border={0}1px solid #000000{0}>", q);
            string _str_detail = "";

            _str_detail = string.Format("<tr><td align={0}left{0} >_obj1</td>", q);
            _str_detail += string.Format("<td>_obj2</td>", q);
            _str_detail += string.Format("<td>_obj3</td>", q);
            _str_detail += string.Format("<td>_obj4</td>", q);
            _str_detail += string.Format("<td>_obj5</td>", q);
            _str_detail += string.Format("<td align={0}left{0}>_obj6</td>", q);
            _str_detail += string.Format("<td>_obj7</td>", q);
            _str_detail += string.Format("<td style={0}mso-number-format:\\@;{0}>_obj8</td>", q);
            _str_detail += string.Format("<td align={0}left{0}>_obj9</td>", q);
            _str_detail += string.Format("<td>_obj_10</td>", q);
            _str_detail += string.Format("<td style={0}mso-number-format:\\@;{0}>_obj_11</td>", q);
            _str_detail += string.Format("<td>_obj_12</td>", q);
            _str_detail += string.Format("<td>_obj_13</td></tr>", q);

            _str_detail = _str_detail.Replace("_obj1", "{0}");
            _str_detail = _str_detail.Replace("_obj2", "{1}");
            _str_detail = _str_detail.Replace("_obj3", "{2}");
            _str_detail = _str_detail.Replace("_obj4", "{3}");
            _str_detail = _str_detail.Replace("_obj5", "{4}");
            _str_detail = _str_detail.Replace("_obj6", "{5}");
            _str_detail = _str_detail.Replace("_obj7", "{6}");
            _str_detail = _str_detail.Replace("_obj8", "{7}");
            _str_detail = _str_detail.Replace("_obj9", "{8}");
            _str_detail = _str_detail.Replace("_obj_10", "{9}");
            _str_detail = _str_detail.Replace("_obj_11", "{10}");
            _str_detail = _str_detail.Replace("_obj_12", "{11}");
            _str_detail = _str_detail.Replace("_obj_13", "{12}");

            _obj_sb.Append(_str_table);
            _obj_sb.Append(string.Format(_str_detail, "DELIVER_TO", "REF_NO", "ETD_PFR", "ETA",
                                         "PROD_LINE", "PC1 _MOTHER_ROLL", "PC2_MOTHER_ROLL", "MOTHER _LOT_ NO",
                                         "PC1_SUB_SLIT_CUSTOMER ROLL", "PC2_SUB_SLIT_CUSTOMER ROLL", "SUB_SLIT_LOT_NO", "PALLET_NO", "ETD_Collection"));

            if (_obj_dt.Rows.Count > 0)
            {
                for (int _int_iLoop = 0; _int_iLoop <= (_obj_dt.Rows.Count - 1); _int_iLoop++)
                {
                    _obj_sb.Append(string.Format(_str_detail,
                        _obj_dt.Rows[_int_iLoop]["DELIVERTO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["REFNO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["ETD_PFR_DESC"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["ETA_DESC"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PRODLINE_NO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC1_MOTHER"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC_MOTHER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["MOTHER_LOT_NO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC1_SUB_SLIT_CUSTOMER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC2_SUB_SLIT_CUSTOMER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["SUB_SLIT_CUSTOMER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PALLET_NO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["ETD_COLLECTION"].ToString().Trim()));
                }

                _obj_sb.Append("</TABLE>");
                _str_detail = _obj_sb.ToString();
            }
            else
            {
                _str_detail = "";
            }

            _obj_dt.Dispose();
            return _str_detail;
        }

        public static string GET_ASR_TO_EXCEL_ALL()
        {
            DataTable _obj_dt;

            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                _obj_dt = _dal.ASRListALL();
            }

            StringBuilder _obj_sb = new StringBuilder();
            string q = "\"";

            string _str_table = string.Format("<table border={0}1px solid #000000{0}>", q);
            string _str_detail = "";

            _str_detail = string.Format("<tr><td align={0}left{0} >_obj1</td>", q);
            _str_detail += string.Format("<td>_obj2</td>", q);
            _str_detail += string.Format("<td>_obj3</td>", q);
            _str_detail += string.Format("<td>_obj4</td>", q);
            _str_detail += string.Format("<td>_obj5</td>", q);
            _str_detail += string.Format("<td align={0}left{0}>_obj6</td>", q);
            _str_detail += string.Format("<td>_obj7</td>", q);
            _str_detail += string.Format("<td style={0}mso-number-format:\\@;{0}>_obj8</td>", q);
            _str_detail += string.Format("<td align={0}left{0}>_obj9</td>", q);
            _str_detail += string.Format("<td>_obj_10</td>", q);
            _str_detail += string.Format("<td style={0}mso-number-format:\\@;{0}>_obj_11</td>", q);
            _str_detail += string.Format("<td>_obj_12</td>", q);
            _str_detail += string.Format("<td>_obj_13</td></tr>", q);

            _str_detail = _str_detail.Replace("_obj1", "{0}");
            _str_detail = _str_detail.Replace("_obj2", "{1}");
            _str_detail = _str_detail.Replace("_obj3", "{2}");
            _str_detail = _str_detail.Replace("_obj4", "{3}");
            _str_detail = _str_detail.Replace("_obj5", "{4}");
            _str_detail = _str_detail.Replace("_obj6", "{5}");
            _str_detail = _str_detail.Replace("_obj7", "{6}");
            _str_detail = _str_detail.Replace("_obj8", "{7}");
            _str_detail = _str_detail.Replace("_obj9", "{8}");
            _str_detail = _str_detail.Replace("_obj_10", "{9}");
            _str_detail = _str_detail.Replace("_obj_11", "{10}");
            _str_detail = _str_detail.Replace("_obj_12", "{11}");
            _str_detail = _str_detail.Replace("_obj_13", "{12}");

            _obj_sb.Append(_str_table);
            _obj_sb.Append(string.Format(_str_detail, "DELIVER_TO", "REF_NO", "ETD_PFR", "ETA",
                                         "PROD_LINE", "PC1 _MOTHER_ROLL", "PC2_MOTHER_ROLL", "MOTHER _LOT_ NO",
                                         "PC1_SUB_SLIT_CUSTOMER ROLL", "PC2_SUB_SLIT_CUSTOMER ROLL", "SUB_SLIT_LOT_NO", "PALLET_NO", "ETD_Collection"));

            if (_obj_dt.Rows.Count > 0)
            {
                for (int _int_iLoop = 0; _int_iLoop <= (_obj_dt.Rows.Count - 1); _int_iLoop++)
                {
                    _obj_sb.Append(string.Format(_str_detail,
                        _obj_dt.Rows[_int_iLoop]["DELIVERTO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["REFNO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["ETD_PFR_DESC"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["ETA_DESC"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PRODLINE_NO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC1_MOTHER"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC_MOTHER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["MOTHER_LOT_NO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC1_SUB_SLIT_CUSTOMER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PC2_SUB_SLIT_CUSTOMER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["SUB_SLIT_CUSTOMER_ROLL"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["PALLET_NO"].ToString().Trim(),
                        _obj_dt.Rows[_int_iLoop]["ETD_COLLECTION"].ToString().Trim()));
                }

                _obj_sb.Append("</TABLE>");
                _str_detail = _obj_sb.ToString();
            }
            else
            {
                _str_detail = "";
            }

            _obj_dt.Dispose();
            return _str_detail;
        }

        public static DataTable GetRefNoList()
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.GetRefNoList();
            }
        }

        public static string Chk_next(string ProdLine, string PC1)
        {
            using (var _Dal = new Library.Database.DAL.SubSlitRequest())
            {
                string result = _Dal.Chk_next(ProdLine, PC1);

                if (result != "")
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

        public static DataTable Chk_label(string ind, string ProdLine, string pc1, string pc2)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.Chk_label(ind, ProdLine, pc1, pc2);
            }
        }

        public static DataTable Chk_final(string pRefNo)
        {
            using (var _dal = new Library.Database.DAL.SubSlitRequest())
            {
                return _dal.Chk_final(pRefNo);
            }
        }
    }
}