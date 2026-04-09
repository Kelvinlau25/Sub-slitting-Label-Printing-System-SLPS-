Imports System.Data.SqlClient

Namespace DAL

    Public Class USER_LOGIN
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function UserLogin(ByVal pstr_UserID As String, ByVal pstr_Password As String, ByVal ip_address As String, ByVal max_attempts As String, ByVal stage As String) As String()

            Dim _obj_dt As New DataTable
            Dim _arr_str_return_value(6) As String
            _arr_str_return_value(0) = ""
            _arr_str_return_value(1) = ""
            _arr_str_return_value(2) = ""
            _arr_str_return_value(3) = ""
            _arr_str_return_value(4) = ""
            _arr_str_return_value(5) = ""
            _arr_str_return_value(6) = ""

            Try

                With MyBase._cmd
                    .CommandText = "SP_USER_LOGIN"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()

                    .Parameters.Add(New SqlParameter("@str_UserID", pstr_UserID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@str_UserPassword", pstr_Password)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@usr_ip_address", ip_address)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@max_attempts", max_attempts)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@stage", stage)).Direction = Data.ParameterDirection.Input

                    .Transaction.Commit()

                    _obj_dt.Load(MyBase._cmd.ExecuteReader)

                End With

                If _obj_dt.Rows.Count > 0 Then

                    If stage = 0 Then
                        _arr_str_return_value(0) = _obj_dt.Rows(0)("PASSWORD").ToString.Trim
                    Else
                        _arr_str_return_value(0) = _obj_dt.Rows(0)("USERID").ToString.Trim
                        _arr_str_return_value(1) = _obj_dt.Rows(0)("ULEVEL").ToString.Trim
                        _arr_str_return_value(2) = _obj_dt.Rows(0)("COMPANYCODE").ToString.Trim
                        _arr_str_return_value(3) = _obj_dt.Rows(0)("NAME").ToString.Trim
                        _arr_str_return_value(4) = _obj_dt.Rows(0)("STATUS").ToString.Trim
                        _arr_str_return_value(5) = _obj_dt.Rows(0)("PWD_DATE").ToString.Trim
                    End If

                End If

                Return _arr_str_return_value

            Catch ex As Exception
                _arr_str_return_value(6) = ex.Message
                Return _arr_str_return_value
            End Try

        End Function

    End Class

End Namespace


