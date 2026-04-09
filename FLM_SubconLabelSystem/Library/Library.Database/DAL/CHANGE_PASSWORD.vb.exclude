Imports System.Data.SqlClient

Namespace DAL

    Public Class CHANGE_PASSWORD
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function chg_password(ByVal pstr_UserID As String, ByVal curr_Password As String, ByVal new_Password As String, ByVal duplicatepass As Integer, ByVal UpdatedLoc As String) As String

            Dim _obj_dt As New DataTable
            Dim update_status As String = ""
            'Dim var_userid As String = ""
            'Dim var_password As String = ""

            With MyBase._cmd
                .CommandText = "SP_CHANGE_PASSWORD"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@str_UserID", pstr_UserID)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_currPassword", curr_Password)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_newPassword", new_Password)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@duplicatepass", duplicatepass)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_Updated_Loc", UpdatedLoc)).Direction = Data.ParameterDirection.Input

                _obj_dt.Load(MyBase._cmd.ExecuteReader)

                .Transaction.Commit()

            End With

            If _obj_dt.Rows.Count > 0 Then

                update_status = _obj_dt.Rows(0)("return_status").ToString.Trim
                'var_userid = _obj_dt.Rows(0)("USERID").ToString.Trim
                'var_password = _obj_dt.Rows(0)("PASSWORD").ToString.Trim

            End If

            _obj_dt.Dispose()

            Return update_status

        End Function


        Friend Function retrieve_pass_arr(ByVal pstr_UserID As String) As String()

            Dim _obj_dt As New DataTable

            Try

                With MyBase._cmd
                    .CommandText = "SP_RETRIEVE_PASS"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()

                    .Parameters.Add(New SqlParameter("@str_UserID", pstr_UserID)).Direction = Data.ParameterDirection.Input

                    _obj_dt.Load(MyBase._cmd.ExecuteReader)

                End With

                Dim _arr_str_return_value(_obj_dt.Rows.Count - 1) As String

                If _obj_dt.Rows.Count > 0 Then

                    For i As Integer = 0 To _obj_dt.Rows.Count - 1
                        _arr_str_return_value(i) = _obj_dt.Rows(i)(0)
                    Next

                End If

                Return _arr_str_return_value

            Catch ex As Exception
                Dim _arr_str_return_value(0) As String
                _arr_str_return_value(0) = ex.Message
                Return _arr_str_return_value
            End Try

        End Function

    End Class

End Namespace


