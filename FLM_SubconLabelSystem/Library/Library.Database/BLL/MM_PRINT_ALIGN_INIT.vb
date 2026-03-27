Namespace BLL

    Public Class MM_PRINT_ALIGN_INIT
        Inherits Library.Root.Other.BusinessLogicBase

        Public Shared Function Print_Align_init() As DataTable
            Dim dt As New DataTable
            Using _dal As New DAL.MM_PRINT_ALIGN_INIT
                dt = _dal.Print_Align_init()
            End Using
            Return dt
        End Function

    End Class

End Namespace

