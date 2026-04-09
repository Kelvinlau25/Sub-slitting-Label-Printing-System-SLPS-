Imports System.Data.SqlClient

Namespace DAL

    Public Class CHECK_LOTNO_DUP
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function check_lotno_dup(ByVal pstr_CompanyCode As String, ByVal pstr_LotNo As String) As String

            Dim _obj_dt As New DataTable
            Dim update_status As String = ""
            'Dim var_userid As String = ""
            'Dim var_password As String = ""

            With MyBase._cmd
                .CommandText = "SP_CHECK_LOTNO_DUP"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@str_CompanyCode", pstr_CompanyCode)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@str_LotNo", pstr_LotNo)).Direction = Data.ParameterDirection.Input

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

    End Class

End Namespace


