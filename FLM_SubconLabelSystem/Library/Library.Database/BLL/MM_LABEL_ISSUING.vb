Namespace BLL

    Public Class MM_LABEL_ISSUING
        Inherits Library.Root.Other.BusinessLogicBase
        Public Shared Function Get_Print_Label() As DataTable
            Dim dt As New DataTable
            Using _dal As New DAL.MM_LABEL_ISSUING
                dt = _dal.Get_Print_Label()
            End Using
            Return dt
        End Function
    End Class
End Namespace
