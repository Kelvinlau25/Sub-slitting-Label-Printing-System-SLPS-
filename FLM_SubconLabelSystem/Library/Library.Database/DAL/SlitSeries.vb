Imports System.Data.SqlClient

Namespace DAL

    Public Class SlitSeries
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

        Friend Function GetData(ByVal ID As String) As DataTable
            GetData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_SLIT_SERIES_SEL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetData.Load(_rdr)
        End Function

        Friend Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
            GetDLLData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_MM_GETDDLDATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pValue", Value)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetDLLData.Load(_rdr)
        End Function

        Friend Function GetDDLData(ByVal Ind As String) As DataTable
            GetDDLData = New Data.DataTable

            With MyBase._cmd
                If Ind = 1 Then
                    .CommandText = "SP_MM_PC1_SEL"
                ElseIf Ind = 2 Then
                    .CommandText = "SP_MM_PRODLINE_SEL"
                ElseIf Ind = 3 Then
                    .CommandText = "SP_MM_REFNO_SEL"
                End If

                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pID", 0)).Direction = Data.ParameterDirection.Input

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetDDLData.Load(_rdr)
        End Function

        Friend Function GetRefByComp(ByVal Company_Code As String) As DataTable
            GetRefByComp = New Data.DataTable

            With MyBase._cmd
                
                .CommandText = "SP_MM_REFBYCOMP_SEL"


                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pCompanyCode", Company_Code)).Direction = Data.ParameterDirection.Input

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetRefByComp.Load(_rdr)
        End Function

        Friend Function GetDDLData2(ByVal refno As String) As DataTable
            GetDDLData2 = New Data.DataTable

            With MyBase._cmd

                .CommandText = "SP_MM_PC1_SEL2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pREFNO", refno)).Direction = Data.ParameterDirection.Input

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetDDLData2.Load(_rdr)
        End Function

        Friend Function GetDDLData2_Rev01(ByVal refno As String, ByVal str_PRODLINE_NO As String) As DataTable
            GetDDLData2_Rev01 = New Data.DataTable

            With MyBase._cmd

                .CommandText = "SP_MM_PC1_SEL2_Rev01"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pREFNO", refno)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_PRODLINE_NO", str_PRODLINE_NO)).Direction = Data.ParameterDirection.Input

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetDDLData2_Rev01.Load(_rdr)
        End Function

        Friend Function GetDDLPC1Cust(ByVal pRefNo As String, ByVal pPC2_Mother As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_PCCust"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID_SubSlit_Req", pPC2_Mother)).Direction = Data.ParameterDirection.Input


            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function GetDDLPC1Cust_REV01(ByVal pRefNo As String, ByVal pPC2_Mother As String, _
                                            ByVal str_PRODLINE_NO As String, ByVal str_PC1_MOTHER As String, _
                                            ByVal str_PC2_MOTHER As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_PCCust_REV01"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pRefNo", pRefNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID_SubSlit_Req", pPC2_Mother)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_PRODLINE_NO", str_PRODLINE_NO)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_PC1_MOTHER", str_PC1_MOTHER)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_PC2_MOTHER", str_PC2_MOTHER)).Direction = Data.ParameterDirection.Input

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

        Friend Function GetPCMOTHER() As DataTable
            GetPCMOTHER = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_PCMother"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0


                .Parameters.Clear()

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPCMOTHER.Load(_rdr)
        End Function

        Friend Function GetPCCUSTOMER() As DataTable
            GetPCCUSTOMER = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_PCCustomer"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPCCUSTOMER.Load(_rdr)
        End Function

        Friend Function GetPCMOTHER2(ByVal refno As String) As DataTable
            GetPCMOTHER2 = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_PCMother_2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0


                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pREFNO", refno)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPCMOTHER2.Load(_rdr)
        End Function


        Friend Function GetPC2CUST(ByVal refno As String) As DataTable
            GetPC2CUST = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_PC2Cust"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0


                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pREFNO", refno)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPC2CUST.Load(_rdr)
        End Function

        Friend Function GetPRODLINE2(ByVal refno As String) As DataTable
            GetPRODLINE2 = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_Prodline_2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0


                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pREFNO", refno)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetPRODLINE2.Load(_rdr)
        End Function

        Friend Function GetUNITWEIGHT2(ByVal pc2 As String) As DataTable
            GetUNITWEIGHT2 = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_UnitWeight_2"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0


                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@PC2", PC2)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetUNITWEIGHT2.Load(_rdr)
        End Function

        Friend Function Maint(ByVal ID As String, ByVal CompCode As String, ByVal RefNo As String, ByVal LotNo As String, ByVal PC1_Mother As String, ByVal PC2_Mother As String, _
                                     ByVal PC1_Cust As String, ByVal PC2_Cust As String, ByVal ProdLine As String, _
                                     ByVal No_Of_Slit As String, ByVal Plan_Year_Mth As String, ByVal Type_Of_Slit As String, _
                                     ByVal RecType As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            Maint = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_SLIT_SERIES_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCOMPANYCODE", CompCode)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRefNo", RefNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLOTNO", LotNo)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1_MOTHER", PC1_Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2_MOTHER", PC2_Mother)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1_CUST", PC1_Cust)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2_CUST", PC2_Cust)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPRODLINE_NO", ProdLine)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pNO_OF_SLIT", No_Of_Slit)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPLAN_YEAR_MONTH", Plan_Year_Mth)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pTYPE_OF_SLIT", Type_Of_Slit)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                End With

                MyBase._cmd.ExecuteNonQuery()

            Catch ex As Exception
                Maint = ex.Message
            End Try

        End Function

        Friend Function CreateSlitRec(ByVal D_Company_Code As String, ByVal D_ID_PC2_LOTNO As Integer, _
                                           ByVal D_TYPE_OF_SLIT As Integer, ByVal D_MATRIX_POS As Integer, ByVal D_MATRIX_INC As Integer, ByVal D_LOTNO As String, ByVal D_NO_OF_SLIT As Integer, ByVal D_User_ID As String, ByVal UpdatedLoc As String) As String
            CreateSlitRec = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_CREATE_SLITCODE"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pCOMPANYCODE", D_Company_Code)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pID_PC2_LOTNO", D_ID_PC2_LOTNO)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pTYPE_OF_SLIT", D_TYPE_OF_SLIT)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMATRIX_POS", D_MATRIX_POS)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMATRIX_INC", D_MATRIX_INC)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLOTNO", D_LOTNO)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pNO_OF_SLIT ", D_NO_OF_SLIT)).Direction = Data.ParameterDirection.Input


                    .Parameters.Add(New SqlParameter("@pCreatedBy", D_User_ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input

                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                End With
                MyBase._cmd.ExecuteNonQuery()

            Catch ex As Exception
                CreateSlitRec = ex.Message
            End Try

        End Function


    End Class
End Namespace