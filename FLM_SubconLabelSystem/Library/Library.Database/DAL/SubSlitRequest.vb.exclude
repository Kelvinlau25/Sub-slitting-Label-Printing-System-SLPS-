Imports System.Data.SqlClient

Namespace DAL
    ''' <summary>
    ''' CapexCapital Data Access Layer
    ''' ------------------------------------------------
    ''' 15 March 2012  C.C.Yeon Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SubSlitRequest
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                            ByVal FromRowNo As Integer, ByVal ToRowNo As Integer, ByVal Deleted As Integer) As ListCollection
            List = New ListCollection

            Dim dt As New DataTable
            Dim ct As New DataTable
            Dim ds As New DataSet

            With MyBase._cmd
                .CommandText = "PSP_COMMON_LIST"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@Table", Table)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@TableID", TableID)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Search", SearchField)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Value", SearchValue)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@SortField", SortField)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Direction", Direction)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@FrmRowno", FromRowNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@ToRowno", ToRowNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Deleted", Deleted)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            List.Data.Load(_rdr)

            While MyBase._rdr.Read
                List.TotalRow = _rdr("COUNTER")
            End While
            'MsgBox("end dal")
        End Function

        Friend Function GetUserData(ByVal ID As String) As DataTable
            GetUserData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_USER_DATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetUserData.Load(_rdr)
        End Function

        Friend Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
            GetDLLData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_GETDDLDATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pValue", Value)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetDLLData.Load(_rdr)
        End Function

        Friend Function GetPC2Data(ByVal ID As String) As DataTable
            GetPC2Data = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_PC2_DATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPC2Data.Load(_rdr)
        End Function

        Friend Function GetProdlineIDData(ByVal ID As String) As DataTable
            GetProdlineIDData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_GETPRODLINEIDDATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@PRODLINE", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetProdlineIDData.Load(_rdr)
        End Function

        Friend Function GetPC1IDData(ByVal ID As String) As DataTable
            GetPC1IDData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_GETPC1IDDATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@PC1", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPC1IDData.Load(_rdr)
        End Function

        Friend Function GetPC2IDData(ByVal ID As String) As DataTable
            GetPC2IDData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_GETPC2IDDATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@PC2", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPC2IDData.Load(_rdr)
        End Function

        Friend Function chkRefNo(ByVal RefNo As String) As DataTable
            chkRefNo = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_CHK_REFNO"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            chkRefNo.Load(_rdr)
        End Function

        Friend Function chkPC2Mother(ByVal RefNo As String, ByVal PC2Mother As String, ByVal ProdLine As String, ByVal PC1Mother As String) As DataTable
            chkPC2Mother = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_PC2_MOTHER_Validate"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC2_MOTHER_ID", PC2Mother)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPRODLINE_NO", ProdLine)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC1_MOTHER_TEXT", PC1Mother)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            chkPC2Mother.Load(_rdr)
        End Function

        Friend Function GetIDSSR(ByVal RefNo As String, ByVal RevCount As Integer) As DataTable
            GetIDSSR = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_ID_SSR"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pRev", RevCount)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetIDSSR.Load(_rdr)
        End Function

        Friend Function GetMotherSeq(ByVal IDSSR As Integer, ByVal SeqMother As String) As DataTable
            GetMotherSeq = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_Mother_Seq"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pIDSSR", IDSSR)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@SeqMother", SeqMother)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetMotherSeq.Load(_rdr)
        End Function

        Friend Function GetSSR_INFO(ByVal RefNo As String, ByVal IDSSR As Integer) As DataTable
            GetSSR_INFO = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_REQ_STATUS"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID_SSR", IDSSR)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetSSR_INFO.Load(_rdr)
        End Function

        Friend Function chkPC2Child(ByVal RefNo As String, ByVal PC2Mother As String, ByVal PC2Child As String) As DataTable
            chkPC2Child = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_PC2_Child_Validate"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC2_MOTHER_ID", PC2Mother)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC2_Child_ID", PC2Child)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            chkPC2Child.Load(_rdr)

        End Function

        Friend Function SubSlitMaint(ByVal ID As String, ByVal pCompFrom As String, ByVal pCompTo As String, ByVal pRefNo As String, ByVal pRev As String, ByVal pDateReq As String, ByVal pReqStat As String, ByVal pVenStat As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String, ByVal UpdatedCC As String) As String
            SubSlitMaint = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SUBSLIT_REQUEST_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCompFrom", pCompFrom)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCompTo", pCompTo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRev", pRev)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pDateReq", pDateReq)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pReqStat", pReqStat)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pVenStat", pVenStat)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitMaint = ex.Message
            End Try
        End Function

        Friend Function SubSlitDup(ByVal ID As String, ByVal pCompFrom As String, ByVal pCompTo As String, ByVal pRefNo As String, ByVal pRev As Integer, ByVal pDateReq As String, ByVal pReqStat As String, ByVal pVenStat As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String, ByVal UpdatedCC As String) As String
            SubSlitDup = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SUBSLIT_REQUEST_DUP"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCompFrom", pCompFrom)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCompTo", pCompTo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRev", pRev)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pDateReq", pDateReq)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pReqStat", pReqStat)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pVenStat", pVenStat)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitDup = ex.Message
            End Try
        End Function

        Friend Function SubSlitMotherMaint(ByVal ID As String, ByVal RefNo As String, ByVal pPC1Mom As String, ByVal pPC2Mom As String, _
                                           ByVal pProdLine As String, ByVal pQty As String, ByVal pMWeight As String, ByVal pMTotWeight As String, _
                                           ByVal pSubWaste As String, ByVal pETD As String, ByVal pETA As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitMotherMaint = "0"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SUBSLIT_REQUEST_MOTHER_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Mom", pPC1Mom)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Mom", pPC2Mom)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pProdLine", pProdLine)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pQty", pQty)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMWeight", pMWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMTotWeight", pMTotWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pSubWaste", pSubWaste)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pETD", pETD)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pETA", pETA)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@ReturnID", SqlDbType.Int)).Direction = Data.ParameterDirection.Output

                End With
                MyBase._cmd.ExecuteReader()

                If MyBase._cmd.Parameters("@ReturnID").Value.ToString <> "" Then
                    SubSlitMotherMaint = MyBase._cmd.Parameters("@ReturnID").Value.ToString()
                End If

            Catch ex As Exception
                SubSlitMotherMaint = ex.Message
            End Try
        End Function

        Friend Function SubSlitMotherDup(ByVal ID As String, ByVal RefNo As String, ByVal pPC1Mom As String, ByVal pPC2Mom As String, _
                                           ByVal pProdLine As String, ByVal pQty As String, ByVal pMWeight As String, ByVal pMTotWeight As String, _
                                           ByVal pSubWaste As String, ByVal pETD As String, ByVal pETA As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitMotherDup = "0"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SUBSLIT_REQUEST_MOTHER_DUP"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Mom", pPC1Mom)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Mom", pPC2Mom)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pProdLine", pProdLine)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pQty", pQty)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMWeight", pMWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMTotWeight", pMTotWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pSubWaste", pSubWaste)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pETD", pETD)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pETA", pETA)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@ReturnID", SqlDbType.Int)).Direction = Data.ParameterDirection.Output

                End With
                MyBase._cmd.ExecuteReader()

                If MyBase._cmd.Parameters("@ReturnID").Value.ToString <> "" Then
                    SubSlitMotherDup = MyBase._cmd.Parameters("@ReturnID").Value.ToString()
                End If

            Catch ex As Exception
                SubSlitMotherDup = ex.Message
            End Try

        End Function

        Friend Function SubSlitChildMaint(ByVal ID As String, ByVal RefNo As String, ByVal pPC1Cust As String, ByVal pPC2Cust As String, ByVal pCQty As String, _
                                          ByVal pCUnitWeight As String, ByVal pCTotWeight As String, ByVal pRemark As String, ByVal pPC2Mother As String, ByVal pProdLineNo As String, ByVal pPC1Mother As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitChildMaint = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SUBSLIT_REQUEST_CHILD_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Cust", pPC1Cust)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Cust", pPC2Cust)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCQty", pCQty)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCUnitWeight", pCUnitWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCTotWeight", pCTotWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRemark", pRemark)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Mother", pPC2Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pProdLine_No", pProdLineNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Mother", pPC1Mother)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitChildMaint = ex.Message
            End Try
        End Function

        Friend Function SubSlitChildDup(ByVal ID As Integer, ByVal RefNo As String, ByVal pPC1Cust As String, ByVal pPC2Cust As String, ByVal pCQty As String, _
                                          ByVal pCUnitWeight As String, ByVal pCTotWeight As String, ByVal pRemark As String, ByVal pPC2Mother As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitChildDup = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SUBSLIT_REQUEST_CHILD_DUP"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Cust", pPC1Cust)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Cust", pPC2Cust)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCQty", pCQty)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCUnitWeight", pCUnitWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCTotWeight", pCTotWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRemark", pRemark)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Mother", pPC2Mother)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitChildDup = ex.Message
            End Try
        End Function

        Friend Function SubSlitChildDel(ByVal pRefNo As String, ByVal pIdSubMomReq As String, ByVal pPC2Mother As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitChildDel = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SSR_CHILD_Del"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()

                    .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pIdSubMomReq", pIdSubMomReq)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2Mother", pPC2Mother)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitChildDel = ex.Message
            End Try
        End Function

        Friend Function SubSlitChildDelFrList(ByVal RefNo As String, ByVal pPC2Mother As String, ByVal pPC1Mother As String, ByVal pProdLineNo As String, ByVal pSeqMother As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitChildDelFrList = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SSR_CHILDDEL_FRLIST"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()

                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pPC2Mother", pPC2Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Mother", pPC1Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pProdLineNo", pProdLineNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pSeqMother", pSeqMother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitChildDelFrList = ex.Message
            End Try
        End Function

        Friend Function SubSlitMotherDel(ByVal RefNo As String, ByVal pPC2Mother As String, ByVal pPC1Mother As String, ByVal pProdLineNo As String, ByVal pSeqMother As String, ByVal RecType As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SubSlitMotherDel = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SSR_MOTHER_Del"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()

                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pPC2Mother", pPC2Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Mother", pPC1Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pProdLineNo", pProdLineNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pSeqMother", pSeqMother)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SubSlitMotherDel = ex.Message
            End Try
        End Function

        Friend Function UpdateReq(ByVal Refno As String, ByVal Revision As Integer, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            UpdateReq = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_Update_Req"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@RefNo", Refno)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@Revision", Revision)).Direction = Data.ParameterDirection.Input
                   
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                UpdateReq = ex.Message
            End Try
        End Function

        Friend Function SSRUpdateStat(ByVal Refno As String, ByVal ID_SSR As Integer, ByVal Req_Status As String, ByVal Vend_Status As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            SSRUpdateStat = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SSR_Update_Stat"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pRefNo", Refno)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pID_SSR", ID_SSR)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pReq_Status", Req_Status)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pVendor_Status", Vend_Status)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input

                End With
                MyBase._cmd.ExecuteReader()

            Catch ex As Exception
                SSRUpdateStat = ex.Message
            End Try
        End Function

        Friend Function SSRList(ByVal pRefNo As String, ByVal pSeqMother As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_LIST"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pSeqMother", pSeqMother)).Direction = Data.ParameterDirection.Input


            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function SSRList_02(ByVal pRefNo As String, ByVal pPC2_Mother As String, ByVal pstr_ProLine As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_LIST_02"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC2_Mother", pPC2_Mother)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_ProLine", pstr_ProLine)).Direction = Data.ParameterDirection.Input


            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function SSRListExist(ByVal pRefNo As String, ByVal pID_SubSlit_Req As Integer) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_SSR_LISTExist"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID_SubSlit_Req", pID_SubSlit_Req)).Direction = Data.ParameterDirection.Input


            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function GetProdLineID(ByVal pProdLineNo As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_ID_MM_PRODLINE"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pProdLine", pProdLineNo)).Direction = Data.ParameterDirection.Input

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function GetPC1ID(ByVal pPC1Mother As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_ID_MM_PC1"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pPC1", pPC1Mother)).Direction = Data.ParameterDirection.Input

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function GetPC2ID(ByVal pPC2Mother As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_ID_MM_PC2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pPC2", pPC2Mother)).Direction = Data.ParameterDirection.Input

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function CHECK_SUBMITTED_REQ(ByVal RefNo As String, ByVal Revision As Integer) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_CHECK_SUBMITTED_REQ"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@RefNo", RefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Revision", Revision)).Direction = Data.ParameterDirection.Input
            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function chkPC2Mom(ByVal ID As String, ByVal pRefNo As String) As DataTable
            chkPC2Mom = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_CHK_PC2_MOTHER"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            chkPC2Mom.Load(_rdr)
        End Function

        Friend Function GetASRDDL(ByVal CompanyCode As String) As DataTable
            GetASRDDL = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_ASR_SEARCH_REF_NO_SEL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pCompanyCode", CompanyCode)).Direction = Data.ParameterDirection.Input

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetASRDDL.Load(_rdr)
        End Function

        'without company code
        Friend Function GetASRDDL2() As DataTable
            GetASRDDL2 = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_ASR_SEARCH_REF_NO_SEL2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                '.Parameters.Add(New SqlParameter("@pCompanyCode", CompanyCode)).Direction = Data.ParameterDirection.Input

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetASRDDL2.Load(_rdr)
        End Function

        Friend Function ASRList(ByVal pRefNo As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_ASR_SEARCH_SEL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input


            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function ASRListALL() As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_ASR_SEARCH_SEL_ALL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function GetRefNoList() As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_REF_NO_LIST"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function Chk_next(ByVal ProdLine As String, ByVal PC1 As String) As String
            Chk_next = String.Empty
            Try
                With MyBase._cmd
                    .CommandText = "SP_CHK_REQ_FORM"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0
                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pProdLine", ProdLine)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1", PC1)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@ReturnID", SqlDbType.Int)).Direction = Data.ParameterDirection.Output
                End With
                MyBase._cmd.ExecuteReader()
                If MyBase._cmd.Parameters("@ReturnID").Value.ToString <> "" Then
                    Chk_next = MyBase._cmd.Parameters("@ReturnID").Value.ToString()
                End If
            Catch ex As Exception
                Chk_next = ex.Message
            End Try
        End Function
        Friend Function Chk_label(ByVal ind As String, ByVal ProdLine As String, ByVal pc1 As String, ByVal pc2 As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_CHK_LABEL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pInd", ind)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pProdLine", ProdLine)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC1", pc1)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pPC2", pc2)).Direction = Data.ParameterDirection.Input
            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function
        Friend Function Chk_final(ByVal pRefNo As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_SUBSLIT_CHECK"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                '.Parameters.Add(New SqlParameter("@pPC2_Mother", pPC2_Mother)).Direction = Data.ParameterDirection.Input


            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function
    End Class
End Namespace