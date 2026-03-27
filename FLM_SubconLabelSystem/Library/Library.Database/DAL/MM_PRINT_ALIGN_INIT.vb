Imports System.Data.SqlClient
Imports System.Text
Namespace DAL

    Public Class MM_PRINT_ALIGN_INIT
        Inherits Library.SQLServer.Connection
        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function Print_Align_init()

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_PRINT_ALIGN_INIT"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function


    End Class

End Namespace
