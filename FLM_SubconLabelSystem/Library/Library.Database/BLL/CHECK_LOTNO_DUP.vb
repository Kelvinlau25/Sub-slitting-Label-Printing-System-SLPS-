Namespace BLL

    Public Class CHECK_LOTNO_DUP

        Public Shared Function check_lotno_dup(ByVal pstr_CompanyCode As String, ByVal pstr_LotNo As String) As String

            Using _dal As New DAL.CHECK_LOTNO_DUP

                Return _dal.check_lotno_dup(pstr_CompanyCode, pstr_LotNo)

            End Using

        End Function

    End Class

End Namespace


