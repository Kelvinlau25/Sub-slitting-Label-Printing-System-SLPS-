Imports System.Data.SqlClient

Namespace DAL
    ''' <summary>
    ''' CapexCapital Data Access Layer
    ''' ------------------------------------------------
    ''' 15 March 2012  C.C.Yeon Initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PC2Mother
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
                .CommandText = "SP_MM_PC2_MOTHER_SEL"
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

        Friend Function Maint(ByVal ID As String, ByVal PC2 As String, ByVal Thickness As String, ByVal Type As String, ByVal Width As String, _
                                     ByVal Length As String, ByVal PackCode As String, ByVal Grade As String, _
                                     ByVal CoreCode As String, ByVal Machine As String, ByVal UnitWeight As String, _
                                     ByVal NumPerPack As String, ByVal Remarks As String, _
                                     ByVal RecType As String, ByVal UpdatedBy As String, ByVal UpdatedLoc As String) As String
            Maint = "1"
            Try
                With MyBase._cmd
                    .CommandText = "SP_MM_PC2_MOTHER_MAINT"
                    .CommandType = CommandType.StoredProcedure
                    .CommandTimeout = 0

                    .Parameters.Clear()
                    .Parameters.Add(New SqlParameter("@pID", ID)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPC2", PC2)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pThickness", Thickness)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pType", Type)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pWidth", Width)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pLength", Length)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pPackCode", PackCode)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pGrade", Grade)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pCoreCode", CoreCode)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pMachine", Machine)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pUnitWeight", UnitWeight)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pNumPerPack", NumPerPack)).Direction = Data.ParameterDirection.Input
                    .Parameters.Add(New SqlParameter("@pRemarks", Remarks)).Direction = Data.ParameterDirection.Input



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