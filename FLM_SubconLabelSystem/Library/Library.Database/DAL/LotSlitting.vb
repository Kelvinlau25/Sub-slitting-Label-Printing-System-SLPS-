Imports System.Data.SqlClient

Namespace DAL
    ''' <summary>
    ''' CapexCapital Data Access Layer
    ''' ------------------------------------------------
    ''' 15 March 2012  C.C.Yeon Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LotSlitting
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
                .CommandText = "SP_LOT_SLITTING_SEL"
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

        Friend Function Maint(ByVal ID As String, ByVal LOTNO As String, ByVal var2 As String, ByVal var3 As String, ByVal var4 As String, _
                                     ByVal var5 As String, ByVal var6 As String, ByVal var7 As String, _
                                     ByVal var8 As String, ByVal var9 As String, ByVal var10 As String, _
                                     ByVal var11 As String, ByVal var12 As String, _
                                     ByVal RecType As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            Maint = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_LOT_SLITTING_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLOTNO", LOTNO)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar2", var2)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar3", var3)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar4", var4)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar5", var5)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar6", var6)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar7", var7)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar8", var8)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar9", var9)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar10", var10)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar11", var11)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pvar12", var12)).Direction = Data.ParameterDirection.Input



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

        Friend Function UpdPrintSel(ByVal SLITLOTNO As String, ByVal PrintSel As Boolean, _
                                   ByVal RecUpd As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            UpdPrintSel = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_UPDATE_PRINTSEL"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pSlitLotNo", SLITLOTNO)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPrintSel", PrintSel)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRecUpd", RecUpd)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pUpdatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pUpdatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input

                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                End With
                MyBase._cmd.ExecuteNonQuery()

            Catch ex As Exception
                UpdPrintSel = ex.Message
            End Try

        End Function

        Friend Function UpdPrintSelAll(ByVal PrintSel As Boolean, _
                                   ByVal RecUpd As String, ByVal filter As String, ByVal filterField As String, ByVal addCondition As String, ByVal passType As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            UpdPrintSelAll = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_UPDATE_PRINTSEL_ALL"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()

                    .Parameters.Add(New SqlParameter("@pPrintSel", PrintSel)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRecUpd", RecUpd)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pFilter", filter)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pFilterField", filterField)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pAddCondition", addCondition)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPassType", passType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pUpdatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pUpdatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input

                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                End With
                MyBase._cmd.ExecuteNonQuery()

            Catch ex As Exception
                UpdPrintSelAll = ex.Message
            End Try

        End Function
    End Class
End Namespace