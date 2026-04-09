Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PrintAlignInit
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.PrintAlignInit
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function

        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.PrintAlignInit
                GetData = _dal.GetData(ID)
            End Using
        End Function

        'Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
        '    Using _dal As New DAL.user
        '        GetDLLData = _dal.GetDLLData(Value, ID)
        '    End Using
        'End Function

        Public Shared Function Maint(ByVal ID As String, ByVal PrinterName As String, ByVal TextFont As String, ByVal WidthX As String, ByVal TextFontSize As String, _
                                     ByVal WidthY As String, ByVal BarcodeFont As String, ByVal LengthHeaderX As String, ByVal BarcodeFontSize As String, _
                                     ByVal LengthHeaderY As String, ByVal PackCodeX As String, ByVal UnitWeightX As String, ByVal PackCodeY As String, _
                                     ByVal UnitWeightY As String, ByVal NumPerPackX As String, ByVal SlitLotNoX As String, ByVal NumPerPackY As String, _
                                     ByVal SlitLotNoY As String, ByVal PC1X As String, ByVal GradeX As String, ByVal PC1Y As String, _
                                     ByVal GradeY As String, ByVal LengthX As String, ByVal CoreCodeX As String, ByVal LengthY As String, _
                                     ByVal CoreCodeY As String, ByVal txtThicknessX As String, ByVal txtBarcodeX As String, ByVal txtThicknessY As String, _
                                     ByVal txtBarcodeY As String, ByVal txtTypeX As String, ByVal txtTypeY As String, ByVal RadioButton1 As Boolean, _
                                     ByVal RadioButton2 As Boolean, ByVal Company_Code As String, ByVal RecType As String) As String

            Using _Dal As New DAL.PrintAlignInit
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                Maint = _Dal.Maint(ID, PrinterName, TextFont, WidthX, TextFontSize, WidthY, BarcodeFont, LengthHeaderX, BarcodeFontSize, _
                                   LengthHeaderY, PackCodeX, UnitWeightX, PackCodeY, UnitWeightY, NumPerPackX, SlitLotNoX, NumPerPackY, _
                                   SlitLotNoY, PC1X, GradeX, PC1Y, GradeY, LengthX, CoreCodeX, LengthY, CoreCodeY, txtThicknessX, txtBarcodeX, _
                                   txtThicknessY, txtBarcodeY, txtTypeX, txtTypeY, RadioButton1, RadioButton2, Company_Code, _
                                   RecType, Str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function
    End Class
End Namespace

