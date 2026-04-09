Imports System.Data.SqlClient
Imports System.Text
Imports Library.SQLServer


Namespace DAL
    Public Class MM_LABEL_ISSUING
        Inherits Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function Get_Print_Label()

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_Get_PRINT_LABEL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function


    End Class
End Namespace

