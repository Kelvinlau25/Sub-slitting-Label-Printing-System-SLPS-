Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PC2Mother
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.PC2Mother
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.PC2Mother
                GetData = _dal.GetData(ID)
            End Using
        End Function

        'Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
        '    Using _dal As New DAL.user
        '        GetDLLData = _dal.GetDLLData(Value, ID)
        '    End Using
        'End Function

        Public Shared Function Maint(ByVal ID As String, ByVal PC2 As String, ByVal Thickness As String, ByVal Type As String, ByVal Width As String, _
                                     ByVal Length As String, ByVal PackCode As String, ByVal Grade As String, _
                                     ByVal CoreCode As String, ByVal Machine As String, ByVal UnitWeight As String, _
                                     ByVal NumPerPack As String, ByVal Remarks As String, ByVal RecType As String) As String
            Using _Dal As New DAL.PC2Mother
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                Maint = _Dal.Maint(ID, PC2, Thickness, Type, Width, Length, PackCode, Grade, CoreCode, Machine, UnitWeight, NumPerPack, Remarks, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace

