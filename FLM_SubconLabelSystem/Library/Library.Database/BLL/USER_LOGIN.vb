Namespace BLL

    Public Class USER_LOGIN

        Public Shared Function UserLogin(ByVal pstr_UserID As String, ByVal pstr_Password As String, ByVal stage As Integer) As String()

            Using _dal As New DAL.USER_LOGIN

                Return _dal.UserLogin(pstr_UserID, pstr_Password, System.Web.HttpContext.Current.Request.UserHostAddress.ToString, Convert.ToString(System.Configuration.ConfigurationManager.AppSettings("Max_Login_Attempts")), stage)

            End Using

        End Function

    End Class

End Namespace


