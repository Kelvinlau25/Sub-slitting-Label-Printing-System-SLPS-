Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Log
        Inherits Library.Root.Other.BusinessLogicBase
        ''' <summary>
        ''' Retrieve the log (Common Function)
        ''' Based on the Setup Key, the View's Name will be retrieved from the resource page
        ''' Select the data based on the key, and the page no. was applied
        ''' </summary>
        Public Shared Function GetLogList(ByVal Table As String, ByVal Key As String, ByVal Page As Integer, Optional ByVal Sort As String = "") As ListCollection
            GetLogList = New ListCollection

            If Table <> String.Empty Then
                Using _dal As New DAL.Log
                    GetLogList = _dal.getLogList(Table, Key, FromRowNo(Page), ToRowNo(Page), Sort)
                End Using
            End If
        End Function
        Public Shared Function GetGroupNo() As DataTable
            Using _Dal As New DAL.Log

                GetGroupNo = _Dal.GetGroupNo()

            End Using
        End Function

    End Class
End Namespace
