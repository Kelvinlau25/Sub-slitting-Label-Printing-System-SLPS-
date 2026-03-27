Imports System.Text
Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SubSlitRequest
        Inherits Library.Root.Other.BusinessLogicBase
        Private Shared chkint As Integer = 0

        Public Shared Function GetUserData(ByVal ID As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetUserData = _dal.GetUserData(ID)
            End Using
        End Function

        Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetDLLData = _dal.GetDLLData(Value, ID)
            End Using
        End Function

        Public Shared Function GetPC2Data(ByVal ID As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetPC2Data = _dal.GetPC2Data(ID)
            End Using
        End Function

        Public Shared Function GetProdlineIDData(ByVal ID As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetProdlineIDData = _dal.GetProdlineIDData(ID)
            End Using
        End Function

        Public Shared Function GetPC1IDData(ByVal ID As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetPC1IDData = _dal.GetPC1IDData(ID)
            End Using
        End Function

        Public Shared Function GetPC2IDData(ByVal ID As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetPC2IDData = _dal.GetPC2IDData(ID)
            End Using
        End Function

        Public Shared Function chkRefNo(ByVal RefNo As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                chkRefNo = _dal.chkRefNo(RefNo)
            End Using
        End Function

        Public Shared Function chkPC2Mother(ByVal IDSubSlitReq As String, ByVal PC2Mother As String, ByVal ProdLine As String, ByVal PC1Mother As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                chkPC2Mother = _dal.chkPC2Mother(IDSubSlitReq, PC2Mother, ProdLine, PC1Mother)
            End Using
        End Function

        Public Shared Function GetIDSSR(ByVal Refno As String, ByVal RevCount As Integer) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetIDSSR = _dal.GetIDSSR(Refno, RevCount)
            End Using
        End Function

        Public Shared Function GetMotherSeq(ByVal IDSSR As Integer, ByVal SeqMother As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetMotherSeq = _dal.GetMotherSeq(IDSSR, SeqMother)
            End Using
        End Function

        Public Shared Function chkPC2Child(ByVal IDSubSlitReq As String, ByVal PC2Mother As String, ByVal PC2Child As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                chkPC2Child = _dal.chkPC2Child(IDSubSlitReq, PC2Mother, PC2Child)
            End Using
        End Function

        Public Shared Function SubSlitMaint(ByVal ID As String, ByVal pCompFrom As String, ByVal pCompTo As String, ByVal pRefNo As String, ByVal pRev As String, ByVal pDateReq As String, ByVal pReqStat As String, ByVal pVenStat As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitMaint = _Dal.SubSlitMaint(ID, pCompFrom, pCompTo, pRefNo, pRev, pDateReq, pReqStat, pVenStat, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString, cc)

                If SubSlitMaint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitDup(ByVal ID As String, ByVal pCompFrom As String, ByVal pCompTo As String, ByVal pRefNo As String, ByVal pRev As Integer, ByVal pDateReq As String, ByVal pReqStat As String, ByVal pVenStat As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitDup = _Dal.SubSlitDup(ID, pCompFrom, pCompTo, pRefNo, pRev, pDateReq, pReqStat, pVenStat, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString, cc)

                If SubSlitDup = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitMotherMaint(ByVal ID As String, ByVal pIdSubReq As String, ByVal pPC1Mom As String, ByVal pPC2Mom As String, _
                                                  ByVal pProdLine As String, ByVal pQty As String, ByVal pMWeight As String, ByVal pMTotWeight As String, _
                                                 ByVal pSubWaste As String, ByVal pETD As String, ByVal pETA As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitMotherMaint = _Dal.SubSlitMotherMaint(ID, pIdSubReq, pPC1Mom, pPC2Mom, pProdLine, pQty, pMWeight, pMTotWeight, pSubWaste, pETD, pETA, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Integer.TryParse(SubSlitMotherMaint, chkint) And SubSlitMotherMaint <> "0" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitMotherDup(ByVal ID As String, ByVal pIdSubReq As String, ByVal pPC1Mom As String, ByVal pPC2Mom As String, _
                                                  ByVal pProdLine As String, ByVal pQty As String, ByVal pMWeight As String, ByVal pMTotWeight As String, _
                                                 ByVal pSubWaste As String, ByVal pETD As String, ByVal pETA As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitMotherDup = _Dal.SubSlitMotherDup(ID, pIdSubReq, pPC1Mom, pPC2Mom, pProdLine, pQty, pMWeight, pMTotWeight, pSubWaste, pETD, pETA, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Integer.TryParse(SubSlitMotherDup, chkint) And SubSlitMotherDup <> "0" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitChildDel(ByVal pRefNo As String, ByVal pIdSubMomReq As String, ByVal pPC2Mother As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitChildDel = _Dal.SubSlitChildDel(pRefNo, pIdSubMomReq, pPC2Mother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If SubSlitChildDel = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitChildDelFrList(ByVal pIdSubMomReq As String, ByVal pPC2Mother As String, ByVal pPC1Mother As String, ByVal pProdLineNo As String, ByVal pSeqMother As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitChildDelFrList = _Dal.SubSlitChildDelFrList(pIdSubMomReq, pPC2Mother, pPC1Mother, pProdLineNo, pSeqMother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If SubSlitChildDelFrList = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitMotherDel(ByVal pIdSubMomReq As String, ByVal pPC2Mother As String, ByVal pPC1Mother As String, ByVal pProdLineNo As String, ByVal pSeqMother As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitMotherDel = _Dal.SubSlitMotherDel(pIdSubMomReq, pPC2Mother, pPC1Mother, pProdLineNo, pSeqMother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If SubSlitMotherDel = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        'SUBSLIT_REQ_MOTHER_SEQNO
        'PC1_CUST 
        'PC2_CUST 
        'C_QTY 
        'C_WEIGHT 
        'C_TOTAL_WEIGHT 
        'REMARK 

        Public Shared Function SubSlitChildMaint(ByVal ID As String, ByVal pIdSubMomReq As String, ByVal pPC1Cust As String, ByVal pPC2Cust As String, ByVal pCQty As String, ByVal pCUnitWeight As String, ByVal pCTotWeight As String, ByVal pRemark As String, ByVal pPC2Mother As String, ByVal pProdLineNo As String, ByVal pPC1Mother As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitChildMaint = _Dal.SubSlitChildMaint(ID, pIdSubMomReq, pPC1Cust, pPC2Cust, pCQty, pCUnitWeight, pCTotWeight, pRemark, pPC2Mother, pProdLineNo, pPC1Mother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If SubSlitChildMaint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SubSlitChildDup(ByVal ID As Integer, ByVal pIdSubMomReq As String, ByVal pPC1Cust As String, ByVal pPC2Cust As String, ByVal pCQty As String, ByVal pCUnitWeight As String, ByVal pCTotWeight As String, ByVal pRemark As String, ByVal pPC2Mother As String, ByVal RecType As Integer) As String
            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserComp").ToString
                SubSlitChildDup = _Dal.SubSlitChildDup(ID, pIdSubMomReq, pPC1Cust, pPC2Cust, pCQty, pCUnitWeight, pCTotWeight, pRemark, pPC2Mother, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If SubSlitChildDup = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function UpdateReq(ByVal RefNo As String, ByVal Revision As Integer) As String

            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                UpdateReq = _Dal.UpdateReq(RefNo, Revision, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If UpdateReq = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SSRUpdateStat(ByVal RefNo As String, ByVal ID_SSR As Integer, ByVal Req_Status As String, ByVal Vend_Status As String) As String

            Using _Dal As New DAL.SubSlitRequest
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString  'created by
                SSRUpdateStat = _Dal.SSRUpdateStat(RefNo, ID_SSR, Req_Status, Vend_Status, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If SSRUpdateStat = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function SSRListExist(ByVal pRefNo As String, ByVal pID_SubSlit_Req As Integer) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.SSRListExist(pRefNo, pID_SubSlit_Req)

            End Using

        End Function

        Public Shared Function SSRList(ByVal pRefNo As String, ByVal pSeqMother As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.SSRList(pRefNo, pSeqMother)

            End Using

        End Function

        Public Shared Function SSRList_02(ByVal pRefNo As String, ByVal pPC2_Mother As String, ByVal pstr_ProLine As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.SSRList_02(pRefNo, pPC2_Mother, pstr_ProLine)

            End Using

        End Function

        Public Shared Function GetProdLineID(ByVal pProdLineNo As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.GetProdLineID(pProdLineNo)
            End Using

        End Function

        Public Shared Function GetPC1ID(ByVal pPC1Mother As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.GetPC1ID(pPC1Mother)
            End Using

        End Function

        Public Shared Function GetPC2ID(ByVal pPC2Mother As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.GetPC2ID(pPC2Mother)
            End Using

        End Function

        Public Shared Function CHECK_SUBMITTED_REQ(ByVal RefNo As String, ByVal Revision As Integer) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.CHECK_SUBMITTED_REQ(RefNo, Revision)
            End Using

        End Function

        Public Shared Function GetSSR_INFO(ByVal RefNo As String, ByVal IDSSR As Integer) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.GetSSR_INFO(RefNo, IDSSR)
            End Using

        End Function


        Public Shared Function chkPC2Mom(ByVal ID As String, ByVal pRefNo As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                chkPC2Mom = _dal.chkPC2Mom(ID, pRefNo)
            End Using
        End Function

        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.SubSlitRequest
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetASRDDL(ByVal CompanyCode As String) As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetASRDDL = _dal.GetASRDDL(CompanyCode)
            End Using
        End Function

        Public Shared Function GetASRDDL2() As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetASRDDL2 = _dal.GetASRDDL2()
            End Using
        End Function


        Public Shared Function GET_SSR_TO_EXCEL_BK_30MAR2018(ByVal pRefNo As String, ByVal pID_SubSlit_Req As Integer, ByVal pUserID As String, ByVal M_Qty As Integer, _
                                                ByVal M_Total_Weight As Decimal, ByVal C_Qty As Integer, ByVal C_Total_Weight As Decimal, ByVal SubSlitWaste As Decimal) As String

            Dim _obj_dt As DataTable
            Dim _obj_dt_1 As DataTable
            Dim _obj_dt_2 As DataTable

            Using _dal As New DAL.SubSlitRequest

                _obj_dt = _dal.SSRListExist(pRefNo, pID_SubSlit_Req)
                _obj_dt_1 = _dal.GetUserData(pUserID)
                _obj_dt_2 = _dal.SSRListExist(pRefNo, pID_SubSlit_Req)

            End Using


            Dim _obj_sb As New StringBuilder

            Dim _str_table As String = "<table>"
            Dim _str_detail As String = ""
            Dim _str_detail_2 As String = ""
            Dim _str_detail_3 As String = ""
            Dim _str_detail_4 As String = ""
            Dim _str_detail_5 As String = ""

            _str_detail = "<tr><td></td>"
            _str_detail += "<td></td>"
            _str_detail += String.Format("<td align={0}center{0}><h3>Sub-Slitting Request</h3></td>", ControlChars.Quote)
            '_str_detail += "<td align="center"><big>Sub-Slitting Request</big> </td>"
            _str_detail += "<td></td>"
            _str_detail += "<td></td>"
            _str_detail += "<td></td></tr>"



            _str_detail += String.Format("<tr><td width={0}130px{0}>To</td>", ControlChars.Quote) '"<tr><td width="130px">To</td>"
            _str_detail += String.Format("<td width={0}10{0}>:</td>", ControlChars.Quote) '"<td width="10px">:</td>"
            _str_detail += String.Format("<td width={0}280px{0}>_obj1</td>", ControlChars.Quote) '"<td width="280px">{0}</td>"

            _str_detail += String.Format("<td width={0}130px{0}>Ref No</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0}>_obj2</td></tr>", ControlChars.Quote)

            _str_detail += String.Format("<tr><td width={0}130px{0}>Department</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10px{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0}>_obj3</td>", ControlChars.Quote)

            _str_detail += String.Format("<td width={0}130px{0}>Date</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10px{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0} align={0}left{0}>_obj4</td></tr>", ControlChars.Quote)

            _str_detail += String.Format("<tr><td width={0}130px{0}>By</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10px{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0}>_obj5</td>", ControlChars.Quote)

            _str_detail += String.Format("<td width={0}130px{0}>Rev</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10px{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0} align={0}left{0}>_obj6</td></tr>", ControlChars.Quote)

            _str_detail += String.Format("<tr><td width={0}100px{0}>Requestor Status</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10px{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0}>_obj7</td>", ControlChars.Quote)

            _str_detail += String.Format("<td width={0}130px{0}>Vendor Status</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}10px{0}>:</td>", ControlChars.Quote)
            _str_detail += String.Format("<td width={0}280px{0}>_obj8</td></tr>", ControlChars.Quote)




            _str_detail = _str_detail.Replace("_obj1", "{0}")
            _str_detail = _str_detail.Replace("_obj2", "{1}")
            _str_detail = _str_detail.Replace("_obj3", "{2}")
            _str_detail = _str_detail.Replace("_obj4", "{3}")
            _str_detail = _str_detail.Replace("_obj5", "{4}")
            _str_detail = _str_detail.Replace("_obj6", "{5}")
            _str_detail = _str_detail.Replace("_obj7", "{6}")
            _str_detail = _str_detail.Replace("_obj8", "{7}")

            _str_detail_4 = String.Format("<table><tr><td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_4 += String.Format("<td bgcolor={0}blue{0} border={0}none{0}>&nbsp</td></tr></table>", ControlChars.Quote)

            _str_detail_3 = String.Format("<tr><td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PRODUCTION LINE</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC1 MOTHER</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC2 MOTHER</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>QTY (ROLL)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>UNIT WEIGHT (KG)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>TOTAL WEIGHT (KG)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC1 CUSTOMER</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>PC2 CUSTOMER</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>QTY (ROLL)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>UNIT WEIGHT (KG)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>TOTAL WEIGHT (KG)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>SUB-SLIT WASTE (KG)</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>ETD PFR</td></strong>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>ETA SUBSLIT CONTRACTOR</strong></td>", ControlChars.Quote)
            _str_detail_3 += String.Format("<td width={0}80px{0} align={0}center{0} bgcolor={0}lightblue{0}><strong>REMARK</strong></td></tr>", ControlChars.Quote)

            _str_detail_2 = "<tr><td>_obj1</td>"
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj2</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj3</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj4</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj5</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj6</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj7</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj8</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj9</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj_10</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj_11</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj_12</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj_13</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj_14</td>", ControlChars.Quote)
            _str_detail_2 += String.Format("<td align={0}center{0}>_obj_15</td>", ControlChars.Quote)
            _str_detail_2 += "</tr>"

            _str_detail_2 = _str_detail_2.Replace("_obj1", "{0}")
            _str_detail_2 = _str_detail_2.Replace("_obj2", "{1}")
            _str_detail_2 = _str_detail_2.Replace("_obj3", "{2}")
            _str_detail_2 = _str_detail_2.Replace("_obj4", "{3}")
            _str_detail_2 = _str_detail_2.Replace("_obj5", "{4}")
            _str_detail_2 = _str_detail_2.Replace("_obj6", "{5}")
            _str_detail_2 = _str_detail_2.Replace("_obj7", "{6}")
            _str_detail_2 = _str_detail_2.Replace("_obj8", "{7}")
            _str_detail_2 = _str_detail_2.Replace("_obj9", "{8}")
            _str_detail_2 = _str_detail_2.Replace("_obj_10", "{9}")
            _str_detail_2 = _str_detail_2.Replace("_obj_11", "{10}")
            _str_detail_2 = _str_detail_2.Replace("_obj_12", "{11}")
            _str_detail_2 = _str_detail_2.Replace("_obj_13", "{12}")
            _str_detail_2 = _str_detail_2.Replace("_obj_14", "{13}")
            _str_detail_2 = _str_detail_2.Replace("_obj_15", "{14}")


            _str_detail_5 = "<tr><td>&nbsp</td>"
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>Total</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>" + M_Qty.ToString() + "</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>" + M_Total_Weight.ToString() + "</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>" + C_Qty.ToString() + "</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>" + C_Total_Weight.ToString() + "</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>" + SubSlitWaste.ToString() + "</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>_&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += String.Format("<td align={0}center{0}>&nbsp</td>", ControlChars.Quote)
            _str_detail_5 += "</tr>"


            '"DEPARTMENT","NAME"

            Dim _str_String As String = ""

            _obj_sb.Append(_str_table)


            If _obj_dt.Rows.Count > 0 Then


                _obj_sb.Append(String.Format(_str_detail, _
                                             _obj_dt.Rows(0)("COMPANYTO").ToString.Trim, _
                                             _obj_dt.Rows(0)("REFNO").ToString.Trim, _
                                             _obj_dt_1.Rows(0)("DEPARTMENT").ToString.Trim, _
                                             _obj_dt.Rows(0)("DATEREQ").ToString.Trim, _
                                             _obj_dt_1.Rows(0)("NAME").ToString.Trim, _
                                             _obj_dt.Rows(0)("REVISIONCOUNT").ToString.Trim, _
                                             _obj_dt.Rows(0)("REQUEST_STATUS").ToString.Trim, _
                                             _obj_dt.Rows(0)("VENDOR_STATUS").ToString.Trim))





                _obj_sb.Append("</TABLE>")

                '_obj_sb.Append(String.Format("<table width={0}100%{0}><tr><td bgcolor={0}Silver{0}> </td> </tr></table>", ControlChars.Quote))
                _obj_sb.Append(String.Format(_str_detail_4))

                _obj_sb.Append(String.Format("<table border={0}1px solid black{0}>", ControlChars.Quote))

                If _obj_dt_2.Rows.Count > 0 Then

                    _obj_sb.Append(String.Format(_str_detail_3))

                    For _int_iLoop As Integer = 0 To (_obj_dt_2.Rows.Count - 1)

                        _obj_sb.Append(String.Format(_str_detail_2, _
                                                     _obj_dt_2.Rows(_int_iLoop)("PRODLINE_NO").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("PC1_MOTHER").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("PC2_MOTHER").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("QTY").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("M_WEIGHT").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("M_TOTAL_WEIGHT").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("PC1_CUST").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("PC2_CUST").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("C_QTY").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("C_WEIGHT").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("C_TOTAL_WEIGHT").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("SUBSLIT_WASTE").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("ETD").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("ETA").ToString.Trim, _
                                                     _obj_dt_2.Rows(_int_iLoop)("REMARK").ToString.Trim))


                    Next


                    _obj_sb.Append(String.Format(_str_detail_5))


                    _obj_sb.Append("</TABLE>")

                End If

                _str_detail = _obj_sb.ToString

            Else

                _str_detail = ""

            End If

            _obj_dt.Dispose()

            Return _str_detail


        End Function

        Public Shared Function GET_SSR_TO_EXCEL(ByVal pRefNo As String, ByVal pID_SubSlit_Req As Integer, ByVal pUserID As String, _
                                                ByVal M_Qty As Integer, ByVal M_Total_Weight As Decimal, ByVal C_Qty As Integer, _
                                                ByVal C_Total_Weight As Decimal, ByVal SubSlitWaste As Decimal, _
                                                ByVal pobj_data As DataTable) As String

            Dim _obj_sb As New StringBuilder

            Dim _obj_dt As DataTable
            Dim _obj_dt_1 As DataTable
            'Dim _obj_dt_2 As DataTable

            Using _dal As New DAL.SubSlitRequest

                _obj_dt = _dal.SSRListExist(pRefNo, pID_SubSlit_Req)
                _obj_dt_1 = _dal.GetUserData(pUserID)
                '_obj_dt_2 = _dal.SSRListExist(pRefNo, pID_SubSlit_Req)

            End Using

            _obj_sb.AppendLine("<table>")

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td align='center'><h3>Sub-Slitting Request</h3></td><td></td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("</tr>")

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td width='130px'>To</td>")
            _obj_sb.AppendLine("<td width='10'>:</td>")
            _obj_sb.AppendLine("<td width='280px'>" & _obj_dt.Rows(0)("COMPANYTO").ToString.Trim & "</td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td width='130px'>Ref No</td>")
            _obj_sb.AppendLine("<td width='10'>:</td>")
            _obj_sb.AppendLine("<td width='280px'>" & _obj_dt.Rows(0)("REFNO").ToString.Trim & "</td>")
            _obj_sb.AppendLine("</tr>")

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td width='130px'>Department</td>")
            _obj_sb.AppendLine("<td width='10px'>:</td>")
            _obj_sb.AppendLine("<td width='280px'>" & _obj_dt_1.Rows(0)("DEPARTMENT").ToString.Trim & "</td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td width='130px'>Date</td>")
            _obj_sb.AppendLine("<td width='10px'>:</td>")
            _obj_sb.AppendLine("<td width='280px' align='left' style='mso-number-format:""Short Date"";'>" & _obj_dt.Rows(0)("DATEREQ").ToString.Trim & "</td>")
            _obj_sb.AppendLine("</tr>")

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td width='130px'>By</td>")
            _obj_sb.AppendLine("<td width='10px'>:</td>")
            _obj_sb.AppendLine("<td width='280px'>" & _obj_dt_1.Rows(0)("NAME").ToString.Trim & "</td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td width='130px'>Rev</td>")
            _obj_sb.AppendLine("<td width='10px'>:</td>")
            _obj_sb.AppendLine("<td width='280px' align='left'>" & _obj_dt.Rows(0)("REVISIONCOUNT").ToString.Trim & "</td>")
            _obj_sb.AppendLine("</tr>")

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td width='100px'>Requestor Status</td>")
            _obj_sb.AppendLine("<td width='10px'>:</td>")
            _obj_sb.AppendLine("<td width='280px'>" & _obj_dt.Rows(0)("REQUEST_STATUS").ToString.Trim & "</td>")
            _obj_sb.AppendLine("<td></td>")
            _obj_sb.AppendLine("<td width='130px'>Vendor Status</td>")
            _obj_sb.AppendLine("<td width='10px'>:</td>")
            _obj_sb.AppendLine("<td width='280px'>" & _obj_dt.Rows(0)("VENDOR_STATUS").ToString.Trim & "</td>")
            _obj_sb.AppendLine("</tr>")

            _obj_sb.AppendLine("</TABLE>")

            _obj_sb.AppendLine("<table>")
            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("<td bgcolor='blue' border='none'>&nbsp</td>")
            _obj_sb.AppendLine("</tr>")
            _obj_sb.AppendLine("</table>")

            _obj_sb.AppendLine("<table border='1px solid black'>")

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PRODUCTION LINE</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC1 MOTHER</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC2 MOTHER</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>QTY (ROLL)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>UNIT WEIGHT (KG)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>TOTAL WEIGHT (KG)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC1 CUSTOMER</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>PC2 CUSTOMER</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>QTY (ROLL)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>UNIT WEIGHT (KG)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>TOTAL WEIGHT (KG)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>SUB-SLIT WASTE (KG)</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>ETD PFR</td></strong>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>ETA SUBSLIT CONTRACTOR</strong></td>")
            _obj_sb.AppendLine("<td width='80px' align='center' bgcolor='lightblue'><strong>REMARK</strong></td>")
            _obj_sb.AppendLine("</tr>")

            For _int_iResult As Integer = 0 To (pobj_data.Rows.Count - 1)

                _obj_sb.AppendLine("<tr>")
                _obj_sb.AppendLine("<td>" & pobj_data.Rows(_int_iResult)("PRODLINE_NO").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("PC1_MOTHER").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("PC2_MOTHER").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("QTY").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" & pobj_data.Rows(_int_iResult)("M_WEIGHT").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" & pobj_data.Rows(_int_iResult)("M_TOTAL_WEIGHT").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("PC1_CUST").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("PC2_CUST").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("C_QTY").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" & pobj_data.Rows(_int_iResult)("C_WEIGHT").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" & pobj_data.Rows(_int_iResult)("C_TOTAL_WEIGHT").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("SUBSLIT_WASTE").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("ETD").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("ETA").ToString.Trim & "</td>")
                _obj_sb.AppendLine("<td align='center'>" & pobj_data.Rows(_int_iResult)("REMARK").ToString.Trim & "</td>")
                _obj_sb.AppendLine("</tr>")

            Next

            _obj_sb.AppendLine("<tr>")
            _obj_sb.AppendLine("<td>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center'>Total</td>")
            _obj_sb.AppendLine("<td align='center'>" & M_Qty.ToString() & "</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" & M_Total_Weight.ToString() & "</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center'>" & C_Qty.ToString() & "</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center' style='mso-number-format:""\#\,\#\#0\.0"";'>" & C_Total_Weight.ToString() & "</td>")
            _obj_sb.AppendLine("<td align='center'>" & SubSlitWaste.ToString() & "</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("<td align='center'>_&nbsp</td>")
            _obj_sb.AppendLine("<td align='center'>&nbsp</td>")
            _obj_sb.AppendLine("</tr>")

            _obj_sb.AppendLine("</table>")

            Return _obj_sb.ToString()


        End Function


        Public Shared Function GET_ASR_TO_EXCEL(ByVal pRefNo As String) As String

            Dim _obj_dt As DataTable

            Using _dal As New DAL.SubSlitRequest

                _obj_dt = _dal.ASRList(pRefNo)

            End Using


            Dim _obj_sb As New StringBuilder

            Dim _str_table As String = String.Format("<table border={0}1px solid #000000{0}>", ControlChars.Quote)
            Dim _str_detail As String = ""


            _str_detail = String.Format("<tr><td align={0}left{0} >_obj1</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj2</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj3</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj4</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj5</td>", ControlChars.Quote)
            _str_detail += String.Format("<td align={0}left{0}>_obj6</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj7</td>", ControlChars.Quote)
            _str_detail += String.Format("<td style={0}mso-number-format:\@;{0}>_obj8</td>", ControlChars.Quote)
            _str_detail += String.Format("<td align={0}left{0}>_obj9</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj_10</td>", ControlChars.Quote)
            _str_detail += String.Format("<td style={0}mso-number-format:\@;{0}>_obj_11</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj_12</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj_13</td></tr>", ControlChars.Quote)

            _str_detail = _str_detail.Replace("_obj1", "{0}")
            _str_detail = _str_detail.Replace("_obj2", "{1}")
            _str_detail = _str_detail.Replace("_obj3", "{2}")
            _str_detail = _str_detail.Replace("_obj4", "{3}")
            _str_detail = _str_detail.Replace("_obj5", "{4}")
            _str_detail = _str_detail.Replace("_obj6", "{5}")
            _str_detail = _str_detail.Replace("_obj7", "{6}")
            _str_detail = _str_detail.Replace("_obj8", "{7}")
            _str_detail = _str_detail.Replace("_obj9", "{8}")
            _str_detail = _str_detail.Replace("_obj_10", "{9}")
            _str_detail = _str_detail.Replace("_obj_11", "{10}")
            _str_detail = _str_detail.Replace("_obj_12", "{11}")
            _str_detail = _str_detail.Replace("_obj_13", "{12}")

            Dim _str_String As String = ""


            _obj_sb.Append(_str_table)
            _obj_sb.Append(String.Format(_str_detail, "DELIVER_TO", "REF_NO", "ETD_PFR", "ETA", _
                                        "PROD_LINE", "PC1 _MOTHER_ROLL", "PC2_MOTHER_ROLL", "MOTHER _LOT_ NO", _
                                        "PC1_SUB_SLIT_CUSTOMER ROLL", "PC2_SUB_SLIT_CUSTOMER ROLL", "SUB_SLIT_LOT_NO", "PALLET_NO", "ETD_Collection"))


            If _obj_dt.Rows.Count > 0 Then

                For _int_iLoop As Integer = 0 To (_obj_dt.Rows.Count - 1)

                    _obj_sb.Append(String.Format(_str_detail, _
                                                 _obj_dt.Rows(_int_iLoop)("DELIVERTO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("REFNO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("ETD_PFR_DESC").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("ETA_DESC").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PRODLINE_NO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC1_MOTHER").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC_MOTHER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("MOTHER_LOT_NO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC1_SUB_SLIT_CUSTOMER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC2_SUB_SLIT_CUSTOMER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("SUB_SLIT_CUSTOMER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PALLET_NO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("ETD_COLLECTION").ToString.Trim))

                Next

                _obj_sb.Append("</TABLE>")

                _str_detail = _obj_sb.ToString

            Else

                _str_detail = ""

            End If

            _obj_dt.Dispose()

            Return _str_detail


        End Function

        Public Shared Function GET_ASR_TO_EXCEL_ALL() As String

            Dim _obj_dt As DataTable

            Using _dal As New DAL.SubSlitRequest

                _obj_dt = _dal.ASRListALL()

            End Using


            Dim _obj_sb As New StringBuilder

            Dim _str_table As String = String.Format("<table border={0}1px solid #000000{0}>", ControlChars.Quote)
            Dim _str_detail As String = ""


            _str_detail = String.Format("<tr><td align={0}left{0} >_obj1</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj2</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj3</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj4</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj5</td>", ControlChars.Quote)
            _str_detail += String.Format("<td align={0}left{0}>_obj6</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj7</td>", ControlChars.Quote)
            _str_detail += String.Format("<td style={0}mso-number-format:\@;{0}>_obj8</td>", ControlChars.Quote)
            _str_detail += String.Format("<td align={0}left{0}>_obj9</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj_10</td>", ControlChars.Quote)
            _str_detail += String.Format("<td style={0}mso-number-format:\@;{0}>_obj_11</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj_12</td>", ControlChars.Quote)
            _str_detail += String.Format("<td>_obj_13</td></tr>", ControlChars.Quote)

            _str_detail = _str_detail.Replace("_obj1", "{0}")
            _str_detail = _str_detail.Replace("_obj2", "{1}")
            _str_detail = _str_detail.Replace("_obj3", "{2}")
            _str_detail = _str_detail.Replace("_obj4", "{3}")
            _str_detail = _str_detail.Replace("_obj5", "{4}")
            _str_detail = _str_detail.Replace("_obj6", "{5}")
            _str_detail = _str_detail.Replace("_obj7", "{6}")
            _str_detail = _str_detail.Replace("_obj8", "{7}")
            _str_detail = _str_detail.Replace("_obj9", "{8}")
            _str_detail = _str_detail.Replace("_obj_10", "{9}")
            _str_detail = _str_detail.Replace("_obj_11", "{10}")
            _str_detail = _str_detail.Replace("_obj_12", "{11}")
            _str_detail = _str_detail.Replace("_obj_13", "{12}")

            Dim _str_String As String = ""


            _obj_sb.Append(_str_table)
            _obj_sb.Append(String.Format(_str_detail, "DELIVER_TO", "REF_NO", "ETD_PFR", "ETA", _
                                        "PROD_LINE", "PC1 _MOTHER_ROLL", "PC2_MOTHER_ROLL", "MOTHER _LOT_ NO", _
                                        "PC1_SUB_SLIT_CUSTOMER ROLL", "PC2_SUB_SLIT_CUSTOMER ROLL", "SUB_SLIT_LOT_NO", "PALLET_NO", "ETD_Collection"))


            If _obj_dt.Rows.Count > 0 Then

                For _int_iLoop As Integer = 0 To (_obj_dt.Rows.Count - 1)

                    _obj_sb.Append(String.Format(_str_detail, _
                                                 _obj_dt.Rows(_int_iLoop)("DELIVERTO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("REFNO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("ETD_PFR_DESC").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("ETA_DESC").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PRODLINE_NO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC1_MOTHER").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC_MOTHER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("MOTHER_LOT_NO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC1_SUB_SLIT_CUSTOMER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PC2_SUB_SLIT_CUSTOMER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("SUB_SLIT_CUSTOMER_ROLL").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("PALLET_NO").ToString.Trim, _
                                                 _obj_dt.Rows(_int_iLoop)("ETD_COLLECTION").ToString.Trim))

                Next

                _obj_sb.Append("</TABLE>")

                _str_detail = _obj_sb.ToString

            Else

                _str_detail = ""

            End If

            _obj_dt.Dispose()

            Return _str_detail


        End Function

        Public Shared Function GetRefNoList() As DataTable
            Using _dal As New DAL.SubSlitRequest
                GetRefNoList = _dal.GetRefNoList()
            End Using
        End Function
        Public Shared Function Chk_next(ByVal ProdLine As String, ByVal PC1 As String) As String
            Using _Dal As New DAL.SubSlitRequest
                Chk_next = _Dal.Chk_next(ProdLine, PC1)

                If Chk_next <> "" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
        Public Shared Function Chk_label(ByVal ind As String, ByVal ProdLine As String, ByVal pc1 As String, ByVal pc2 As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.Chk_label(ind, ProdLine, pc1, pc2)
            End Using

        End Function
        Public Shared Function Chk_final(ByVal pRefNo As String) As DataTable

            Using _dal As New DAL.SubSlitRequest

                Return _dal.Chk_final(pRefNo)
            End Using

        End Function

    End Class
End Namespace

