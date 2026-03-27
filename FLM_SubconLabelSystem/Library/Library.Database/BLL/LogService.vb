Imports System.Web

Namespace BLL
    Public Class LogService
        Inherits Library.Root.Other.BusinessLogicBase
        '''' <summary>
        '''' Retrieve the log (Common Function)
        '''' Based on the Setup Key, the View's Name will be retrieved from the resource page
        '''' Select the data based on the key, and the page no. was applied
        '''' </summary>
        'Public Shared Function GetLogList(ByVal Table As String, ByVal Key As String, ByVal Page As Integer, Optional ByVal Sort As String = "") As Library.Root.Other.GenericCollection(Of Library.Root.[Object].Log)
        Public Shared Function GetLogList(ByVal Table As String, ByVal Key As String, ByVal Page As Integer, Optional ByVal Sort As String = "") As ListCollection
            'GetLogList = New Library.Root.Other.GenericCollection(Of Library.Root.[Object].Log)
            GetLogList = New ListCollection

            Using _dal As New DAL.Log
                GetLogList = _dal.getLogList(Table, Key, FromRowNo(Page), ToRowNo(Page), Sort)
            End Using
        End Function
        'Public Shared Function GetLogList(ByVal Table As String, ByVal Key As String, ByVal Page As Integer, Optional ByVal Sort As String = "") As ListCollection
        '    'Using _dal As New DAL.Log
        '    Using _dal As New DAL.PAB
        '        GetLogList = _dal.getLogList(Table, Key, FromRowNo(Page), ToRowNo(Page), Sort)
        '    End Using
        'End Function

    End Class
End Namespace

