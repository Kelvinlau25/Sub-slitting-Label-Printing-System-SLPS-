Imports System.Data.SqlClient

Namespace DAL
    ''' <summary>
    ''' CapexCapital Data Access Layer
    ''' ------------------------------------------------
    ''' 15 March 2012  C.C.Yeon Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HouseKeep
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function GetSubSlitChild(ByVal Company As String, ByVal datePurge As String, ByVal purgeTable As String) As DataTable
            GetSubSlitChild = New Data.DataTable

            With MyBase._cmd
                If purgeTable = "SSCHILD" Then
                    .CommandText = "SP_HK_GET_SUB_CHILD"
                ElseIf purgeTable = "SSMOTHER" Then
                    .CommandText = "SP_HK_GET_SUB_MOTHER"
                ElseIf purgeTable = "SSMAIN" Then
                    .CommandText = "SP_HK_GET_SUB_MAIN"
                ElseIf purgeTable = "LOTSLIT" Then
                    .CommandText = "SP_HK_GET_LOT_SLIT"
                ElseIf purgeTable = "PC2LOT" Then
                    .CommandText = "SP_HK_GET_PC2_LOT"
                End If
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pCompany", Company)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pDatePurge", datePurge)).Direction = Data.ParameterDirection.Input
                '.Parameters.Add(New SqlParameter("SREFData", Oracle.DataAccess.Client.OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetSubSlitChild.Load(_rdr)
        End Function
        Friend Function DelSubSlitChild(ByVal pid As String, ByVal pHKTable As String) As String
            DelSubSlitChild = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_HOUSEKEEPING"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", pid)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@HouseKeepTable", pHKTable)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                End With
                MyBase._cmd.ExecuteNonQuery()

            Catch ex As Exception
                DelSubSlitChild = ex.Message
            End Try
        End Function


    End Class
End Namespace