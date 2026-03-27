Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net
Imports System.Diagnostics

Partial Class MasterMaint_PRINT_ALIGN_INIT
    Inherits Control.Base

    Private WithEvents httpclient As WebClient
    Dim _list As Library.Database.ListCollection
    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Public Sub New()

        MyBase.SetupKey = "PRINT_ALIGN_INIT"
        MyBase.DefaultSort = "ID_Print_Align_Init"
        MyBase.SortDirection = 0
        MyBase.GridViewCheckColumn = False
        MyBase.PrintControl = False
        MyBase.DeleteControl = True
        MyBase.GridViewRadioColumn = False
        MyBase.ViewHistoryControl = True
        MyBase.RecordTypeColumn = 6

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        MyBase.GridView = grdResult

        If Session("ULEVEL") = 2 Then
            MyBase.DeleteControl = False
        Else
            MyBase.DeleteControl = True
        End If

    End Sub

    Public Overrides Sub BindData()

        Dim Company_Code As String = Session("COMPANYCODE")

        If Session("ULEVEL") = 3 Then
            _list = Library.Database.BLL.PrintAlignInit.List("Print_Align_Init_func(" + "'" + Company_Code + "'" + ")", "ID_Print_Align_Init", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        Else
            _list = Library.Database.BLL.PC1.List("PV_PRINT_ALIGN_INIT", "ID_Print_Align_Init", MyBase.SearchField, MyBase.SearchValue, MyBase.SortField, MyBase.SortDirection, MyBase.PageNo, MyBase.ShowDeleted)
        End If

        grdResult.DataSource = _list.Data
        grdResult.DataBind()
        UCFooter.TotalRecords = _list.TotalRow

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If
    End Sub

    Protected Sub herebutton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles herebutton.Click

        download_exe()

    End Sub

    Private Sub DownloadCSV()
        Dim constr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from PRINT_ALIGN_INIT Where Default_Printer = 1 And REC_TYPE != 5")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        'Build the CSV file data as a Comma separated string.
                        Dim csv As String = String.Empty

                        For Each column As DataColumn In dt.Columns
                            'Add the Header row for CSV file.
                            csv += column.ColumnName + ","c
                        Next

                        'Add new line.
                        csv += vbCr & vbLf

                        For Each row As DataRow In dt.Rows
                            For Each column As DataColumn In dt.Columns
                                'Add the Data rows.
                                csv += row(column.ColumnName).ToString().Replace(",", ";") + ","c
                            Next

                            'Add new line.
                            csv += vbCr & vbLf
                        Next

                        'Download the CSV file.
                        Response.Clear()
                        Response.Buffer = True
                        Response.AddHeader("content-disposition", "attachment;filename=settings.csv")
                        Response.Charset = ""
                        Response.ContentType = "application/text"
                        Response.Output.Write(csv)
                        Response.Flush()
                        Response.End()

                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub download_exe()
        'Download.Enabled = False
        'httpclient = New WebClient
        ''AddHandler httpclient.DownloadFileCompleted, AddressOf Downloaded
        'AddHandler httpclient.DownloadFileCompleted, AddressOf Downloaded
        'httpclient.DownloadFileAsync(New Uri("http://10.200.0.44:85/printlabelling.zip"), ("C:\subconlabelprinter\printlabelling.zip"))
        'Response.Redirect("http://10.200.0.44:85/printlabelling.zip")
        Response.Redirect(ResolveUrl("~/printlabelling.zip"))
    End Sub

    Private Sub Downloaded()
        'If System.IO.File.Exists("C:\download\printlabelling.exe") = True Then
        '    Process.Start("C:\download\printlabelling.exe")
        'Else
        '    MsgBox("PrintLabelling execute file is not found, please download again", 64, "Open")
        'End If
    End Sub

    Protected Sub hereButton1_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles hereButton1.Click
        DownloadCSV()
    End Sub

End Class
