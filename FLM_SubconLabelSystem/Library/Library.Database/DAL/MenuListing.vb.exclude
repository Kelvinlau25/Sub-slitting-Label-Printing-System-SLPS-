Imports System.Data.SqlClient

Namespace DAL

    Public Class MenuListing
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function Load_Menu_Listing(ByVal var_1 As String) As DataTable

            Dim _obj_dt As New DataTable

            With MyBase._cmd
                .CommandText = "SP_GET_MENU_LIST"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.Clear()

                '.Parameters.Add(New SqlParameter("@int_ID_MM_USER_GROUP", pstr_ID_MM_USER_GROUP)).Direction = Data.ParameterDirection.Input

            End With

            _obj_dt.Load(MyBase._cmd.ExecuteReader)

            Return _obj_dt

        End Function

    End Class

End Namespace
