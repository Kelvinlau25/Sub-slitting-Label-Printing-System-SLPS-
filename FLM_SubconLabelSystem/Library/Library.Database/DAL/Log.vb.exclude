Imports System.Data.SqlClient

Namespace DAL
    ''' <summary>
    ''' Log Data Access Layer
    ''' ------------------------------------------------
    ''' 15 March 2012  C.C.Yeon Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Log
        Inherits Library.SQLServer.Connection


        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        '''' <summary>
        '''' Retrieve List according selection
        '''' </summary>
        Public Function getLogList(ByVal Tablename As String, ByVal Key As String, ByVal FromNo As Integer, ByVal ToNo As Integer, ByVal Sort As String) As ListCollection
            getLogList = New ListCollection

            With MyBase._cmd
                .CommandText = "PSP_COMMON_LIST_LOG"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@pTable", Tablename)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pKey", Key)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pFromRowno", FromNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pToRowNo", ToNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pSort", Sort)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader

            getLogList.Data.Load(_rdr)

            While MyBase._rdr.Read
                getLogList.TotalRow = _rdr("COUNTER")
            End While

        End Function
        Friend Function GetGroupNo() As DataTable
            GetGroupNo = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_GROUPNO"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetGroupNo.Load(_rdr)

        End Function

    End Class
End Namespace
