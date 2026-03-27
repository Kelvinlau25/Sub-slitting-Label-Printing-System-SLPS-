
Namespace BLL

    Public Class MenuListing

        Public Shared Function Load_Menu_Listing(ByVal var_1 As String) As DataTable

            Using _dal As New DAL.MenuListing

                Return _dal.Load_Menu_Listing(var_1)

            End Using

        End Function

    End Class

End Namespace
