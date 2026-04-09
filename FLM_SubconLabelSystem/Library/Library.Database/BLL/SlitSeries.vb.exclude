Namespace BLL
    ''' <summary>
    ''' Business Logic Layer
    ''' ---------------------------------
    ''' 18 Feb 2012   Yeon    Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SlitSeries
        Inherits Library.Root.Other.BusinessLogicBase


        Public Shared Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                                    ByVal Page As Integer, ByVal Deleted As Integer) As ListCollection
            Using _dal As New DAL.SlitSeries
                'Validation the parameter value                
                If Direction <> 1 Then
                    Direction = 0
                End If
                List = _dal.List(Table, TableID, SearchField, SearchValue, SortField, Direction, FromRowNo(Page), ToRowNo(Page), Deleted)
            End Using
        End Function


        Public Shared Function GetData(ByVal ID As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetData = _dal.GetData(ID)
            End Using
        End Function


        Public Shared Function GetDDLData(ByVal Ind As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetDDLData = _dal.GetDDLData(Ind)
            End Using
        End Function

        Public Shared Function GetRefByComp(ByVal Comp As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetRefByComp = _dal.GetRefByComp(Comp)
            End Using
        End Function

        Public Shared Function GetDDLData2(ByVal refno As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetDDLData2 = _dal.GetDDLData2(refno)
            End Using
        End Function

        Public Shared Function GetDDLData2_Rev01(ByVal refno As String, ByVal str_PRODLINE_NO As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetDDLData2_Rev01 = _dal.GetDDLData2_Rev01(refno, str_PRODLINE_NO)
            End Using
        End Function

        Public Shared Function GetDDLPC1Cust(ByVal pRefNo As String, ByVal pPC2_Mother As String) As DataTable

            Using _dal As New DAL.SlitSeries

                Return _dal.GetDDLPC1Cust(pRefNo, pPC2_Mother)

            End Using

        End Function

        Public Shared Function GetDDLPC1Cust_Rev01(ByVal pRefNo As String, ByVal pPC2_Mother As String, _
                                            ByVal str_PRODLINE_NO As String, ByVal str_PC1_MOTHER As String, _
                                            ByVal str_PC2_MOTHER As String) As DataTable

            Using _dal As New DAL.SlitSeries

                Return _dal.GetDDLPC1Cust_REV01(pRefNo, pPC2_Mother, str_PRODLINE_NO, str_PC1_MOTHER, str_PC2_MOTHER)

            End Using

        End Function

        Public Shared Function GetPCMOTHER() As DataTable
            Using _dal As New DAL.SlitSeries
                GetPCMOTHER = _dal.GetPCMOTHER()
            End Using
        End Function

        Public Shared Function GetPCCUSTOMER() As DataTable
            Using _dal As New DAL.SlitSeries
                GetPCCUSTOMER = _dal.GetPCCUSTOMER()
            End Using
        End Function

        Public Shared Function GetPC2CUST(ByVal refno As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetPC2CUST = _dal.GetPC2CUST(refno)
            End Using
        End Function

        Public Shared Function GetPCMOTHER2(ByVal refno As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetPCMOTHER2 = _dal.GetPCMOTHER2(refno)
            End Using
        End Function

        Public Shared Function GetPRODLINE2(ByVal refno As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetPRODLINE2 = _dal.GetPRODLINE2(refno)
            End Using
        End Function

        Public Shared Function GetUNITWEIGHT2(ByVal pc2 As String) As DataTable
            Using _dal As New DAL.SlitSeries
                GetUNITWEIGHT2 = _dal.GetUNITWEIGHT2(pc2)
            End Using
        End Function


        'Public Shared Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
        '    Using _dal As New DAL.user
        '        GetDLLData = _dal.GetDLLData(Value, ID)
        '    End Using
        'End Function

        Public Shared Function Maint(ByVal ID As String, ByVal CompCode As String, ByVal RefNo As String, ByVal LotNo As String, ByVal PC1_Mother As String, ByVal PC2_Mother As String, _
                                     ByVal PC1_Cust As String, ByVal PC2_Cust As String, ByVal ProdLine As String, _
                                     ByVal No_Of_Slit As String, ByVal Plan_Year_Mth As String, ByVal Type_Of_Slit As String, _
                                     ByVal RecType As String) As String
            Using _Dal As New DAL.SlitSeries
                Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString
                Maint = _Dal.Maint(ID, CompCode, RefNo, LotNo, PC1_Mother, PC2_Mother, PC1_Cust, PC2_Cust, ProdLine, No_Of_Slit, Plan_Year_Mth, Type_Of_Slit, RecType, str, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If Maint = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function


        Public Shared Function CreateSlitRec(ByVal B_Company_Code As String, ByVal B_ID_PC2_LOTNO As Integer, _
                                           ByVal B_TYPE_OF_SLIT As Integer, ByVal B_MATRIX_POS As Integer, ByVal B_MATRIX_INC As Integer, ByVal B_LOTNO As String, ByVal B_NO_OF_SLIT As Integer, ByVal B_User_ID As String) As String
            Using _Dal As New DAL.SlitSeries
                'Dim str As String = System.Web.HttpContext.Current.Session("gstrUserID").ToString
                'Dim cc As String = System.Web.HttpContext.Current.Session("gstrUserCompCode").ToString

                CreateSlitRec = _Dal.CreateSlitRec(B_Company_Code, B_ID_PC2_LOTNO, B_TYPE_OF_SLIT, B_MATRIX_POS, B_MATRIX_INC, B_LOTNO, B_NO_OF_SLIT, B_User_ID, System.Web.HttpContext.Current.Request.UserHostAddress.ToString)

                If CreateSlitRec = "1" Then
                    _Dal.Commit()
                Else
                    _Dal.Rollback()
                End If
            End Using
        End Function

    End Class
End Namespace


