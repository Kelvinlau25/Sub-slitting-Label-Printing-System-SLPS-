Imports System.Data.SqlClient

Namespace DAL
    ''' <summary>
    ''' CapexCapital Data Access Layer
    ''' ------------------------------------------------
    ''' 15 March 2012  C.C.Yeon Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PrintAlignInit
        Inherits Library.SQLServer.Connection

        Public Sub New()
            MyBase.New("PFR_Label_DB")
        End Sub

        Friend Function List(ByVal Table As String, ByVal TableID As String, ByVal SearchField As String, ByVal SearchValue As String, ByVal SortField As String, ByVal Direction As Integer, _
                             ByVal FromRowNo As Integer, ByVal ToRowNo As Integer, ByVal Deleted As Integer) As ListCollection
            List = New ListCollection

            Dim dt As New DataTable
            Dim ct As New DataTable
            Dim ds As New DataSet

            With MyBase._cmd
                .CommandText = "PSP_COMMON_LIST"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()

                .Parameters.Add(New SqlParameter("@Table", Table)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@TableID", TableID)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Search", SearchField)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Value", SearchValue)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@SortField", SortField)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Direction", Direction)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@FrmRowno", FromRowNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@ToRowno", ToRowNo)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@Deleted", Deleted)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            List.Data.Load(_rdr)

            While MyBase._rdr.Read
                List.TotalRow = _rdr("COUNTER")
            End While
            'MsgBox("end dal")
        End Function

        Friend Function GetData(ByVal ID As String) As DataTable
            GetData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_Print_Align_Init_SEL"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetData.Load(_rdr)
        End Function

        Friend Function GetDLLData(ByVal Value As String, ByVal ID As String) As DataTable
            GetDLLData = New Data.DataTable

            With MyBase._cmd
                .CommandText = "SP_MM_GETDDLDATA"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                .Parameters.Clear()
                .Parameters.Add(New SqlParameter("@pValue", Value)).Direction = Data.ParameterDirection.Input
                .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
            End With

            MyBase._rdr = MyBase._cmd.ExecuteReader
            GetDLLData.Load(_rdr)
        End Function

        Friend Function Maint(ByVal ID As String, ByVal PrinterName As String, ByVal TextFont As String, ByVal WidthX As String, ByVal TextFontSize As String, _
                                     ByVal WidthY As String, ByVal BarcodeFont As String, ByVal LengthHeaderX As String, ByVal BarcodeFontSize As String, _
                                     ByVal LengthHeaderY As String, ByVal PackCodeX As String, ByVal UnitWeightX As String, ByVal PackCodeY As String, _
                                     ByVal UnitWeightY As String, ByVal NumPerPackX As String, ByVal SlitLotNoX As String, ByVal NumPerPackY As String, _
                                     ByVal SlitLotNoY As String, ByVal PC1X As String, ByVal GradeX As String, ByVal PC1Y As String, _
                                     ByVal GradeY As String, ByVal LengthX As String, ByVal CoreCodeX As String, ByVal LengthY As String, _
                                     ByVal CoreCodeY As String, ByVal ThicknessX As String, ByVal BarcodeX As String, ByVal ThicknessY As String, _
                                     ByVal BarcodeY As String, ByVal TypeX As String, ByVal TypeY As String, ByVal RadioButton1 As Boolean, _
                                     ByVal RadioButton2 As Boolean, ByVal Company_Code As String, _
                                     ByVal RecType As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            Maint = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_Print_Align_Init_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPrinterName", PrinterName)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pTextFont", TextFont)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pWidthX", WidthX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pTextFontSize", TextFontSize)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pWidthY", WidthY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pBarcodeFont", BarcodeFont)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLengthHeaderX", LengthHeaderX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pBarcodeFontSize", BarcodeFontSize)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLengthHeaderY", LengthHeaderY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPackCodeX", PackCodeX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pUnitWeightX", UnitWeightX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPackCodeY", PackCodeY)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pUnitWeightY", UnitWeightY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pNumPerPackX", NumPerPackX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pSlitLotNoX", SlitLotNoX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pNumPerPackY", NumPerPackY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pSlitLotNoY", SlitLotNoY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1X", PC1X)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pGradeX", GradeX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC1Y", PC1Y)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pGradeY", GradeY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLengthX", LengthX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCoreCodeX", CoreCodeX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLengthY", LengthY)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pCoreCodeY", CoreCodeY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pThicknessX", ThicknessX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pBarcodeX", BarcodeX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pThicknessY", ThicknessY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pBarcodeY", BarcodeY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pTypeX", TypeX)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pTypeY", TypeY)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRadioButton1", RadioButton1)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRadioButton2", RadioButton2)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCompanyCode", Company_Code)).Direction = Data.ParameterDirection.Input

                    .Parameters.Add(New SqlParameter("@pRecType", RecType)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedBy", UpdatedBy)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCreatedLoc", UpdatedLoc)).Direction = Data.ParameterDirection.Input
                    '.Parameters.Add(New SqlParameter("@pCreatedCC", UpdatedCC)).Direction = Data.ParameterDirection.Input
                End With
                MyBase._cmd.ExecuteNonQuery()

            Catch ex As Exception
                Maint = ex.Message
            End Try

        End Function

    End Class
End Namespace