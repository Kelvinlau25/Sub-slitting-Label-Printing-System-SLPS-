Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LotSlitting
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.LotSlitting
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.LotSlitting
                GetData = _dal.GetData(ID)
            End Using
        End Function

        'Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
        '    Using _dal As New DAL.user
        '        GetDLLData = _dal.GetDLLData(Value, ID)
        '    End Using
        'End Function

        Public Shared Function Maint(ByVal ID As String, ByVal LOTNO As String, ByVal var2 As String, ByVal var3 As String, ByVal var4 As String, _
                                     ByVal var5 As String, ByVal var6 As String, ByVal var7 As String, _
                                     ByVal var8 As String, ByVal var9 As String, ByVal var10 As String, _
                                     ByVal var11 As String, ByVal var12 As String, ByVal RecType As String) As String
            Using _Dal As New DAL.LotSlitting
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                Maint = _Dal.Maint(ID, LOTNO, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function UpdPrintSel(ByVal SLITLOTNO As String, ByVal PrintSel As Boolean, _
                                           ByVal RecUpd As String) As String
            Using _Dal As New DAL.LotSlitting
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                UpdPrintSel = _Dal.UpdPrintSel(SLITLOTNO, PrintSel, RecUpd, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If UpdPrintSel = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

        Public Shared Function UpdPrintSelAll(ByVal PrintSel As Boolean, _
                                           ByVal RecUpd As String, ByVal filter As String, ByVal filterfield As String, ByVal addCondition As String, ByVal passType As String) As String
            Using _Dal As New DAL.LotSlitting
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                UpdPrintSelAll = _Dal.UpdPrintSelAll(PrintSel, RecUpd, filter, filterfield, addCondition, passType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If UpdPrintSelAll = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace


