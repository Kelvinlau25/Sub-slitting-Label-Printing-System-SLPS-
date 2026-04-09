Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MM_PRODLINE
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.MM_PRODLINE
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.MM_PRODLINE
                GetData = _dal.GetData(ID)
            End Using
        End Function

        Public Shared Function Maint(ByVal ID As String, ByVal ProdLine As String, ByVal Desc As String, _
                                     ByVal RecType As String) As String

            Using _Dal As New DAL.MM_PRODLINE
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                Maint = _Dal.Maint(ID, ProdLine, Desc, _
                                   RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace

