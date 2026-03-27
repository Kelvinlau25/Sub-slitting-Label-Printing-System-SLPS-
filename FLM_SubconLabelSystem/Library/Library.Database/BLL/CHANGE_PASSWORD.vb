Namespace BLL

    Public Class CHANGE_PASSWORD

        Public Shared Function chg_password(ByVal pstr_UserID As String, ByVal curr_Password As String, ByVal new_Password As String, ByVal duplicatepass As Integer) As String

            Using _dal As New DAL.CHANGE_PASSWORD

                Return _dal.chg_password(pstr_UserID, curr_Password, new_Password, duplicatepass, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

            End Using

        End Function

        Public Shared Function retrieve_pass_arr(ByVal pstr_UserID As String) As String()

            Using _dal As New DAL.CHANGE_PASSWORD

                Return _dal.retrieve_pass_arr(pstr_UserID)

            End Using

        End Function

    End Class

End Namespace


