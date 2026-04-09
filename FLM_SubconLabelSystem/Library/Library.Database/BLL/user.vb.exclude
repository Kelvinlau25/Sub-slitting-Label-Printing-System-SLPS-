Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class user
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.user
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.user
                GetData = _dal.GetData(ID)
            End Using
        End Function

        Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
            Using _dal As New DAL.user
                GetDLLData = _dal.GetDLLData(Value, ID)
            End Using
        End Function

        Public Shared Function Maint(ByVal ID As String, ByVal CompName As String, ByVal Name As String, ByVal UserID As String, ByVal Department As String, ByVal Email As String, _
                                     ByVal Ulevel As Boolean, ByVal Ulevel2 As Boolean, ByVal Ulevel3 As Boolean, ByVal Psword As String, ByVal Stats As String, ByVal RecType As String) As String
            Using _Dal As New DAL.user
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                Maint = _Dal.Maint(ID, CompName, Name, UserID, Department, Email, Ulevel, Ulevel2, Ulevel3, Psword, Stats, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function ResetPass(ByVal ID As String, ByVal Psword As String, ByVal RecType As String) As String
            Using _Dal As New DAL.user
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                ResetPass = _Dal.ResetPass(ID, Psword, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If ResetPass = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace

