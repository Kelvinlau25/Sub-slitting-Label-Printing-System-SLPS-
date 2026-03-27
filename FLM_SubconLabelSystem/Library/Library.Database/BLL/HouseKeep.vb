Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HouseKeep
        Inherits Library.Root.Other.BusinessLogicBase

        Public Shared Function GetSubSlitChild(ByVal Company As String, ByVal datePurge As String, ByVal pPurgeTable As String) As DataTable
            Using _dal As New DAL.HouseKeep
                GetSubSlitChild = _dal.GetSubSlitChild(Company, datePurge, pPurgeTable)
            End Using
        End Function

        Public Shared Function DelSubSlitChild(ByVal pID As String, ByVal pHKTable As String) As String
            Using _Dal As New DAL.HouseKeep

                DelSubSlitChild = _Dal.DelSubSlitChild(pID, pHKTable)

                If DelSubSlitChild = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace

