Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PC1
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.PC1
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function List2(ByVal refno As String, ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                            ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.PC1
                'Validation the parameter value
                If Direction <> 1 Then
                    Direction = 0
                End If

                List2 = _dal.List2(refno, Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.PC1
                GetData = _dal.GetData(ID)
            End Using
        End Function

        Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
            Using _dal As New DAL.PC1
                GetDLLData = _dal.GetDLLData(Value, ID)
            End Using
        End Function

        Public Shared Function Maint(ByVal ID As String, ByVal PC1 As String, ByVal CompanyCode As String, ByVal NameDelivery As String, _
                                     ByVal RecType As String) As String
            Using _Dal As New DAL.PC1
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString

                Maint = _Dal.Maint(ID, PC1, CompanyCode, NameDelivery, RecType, Str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace

