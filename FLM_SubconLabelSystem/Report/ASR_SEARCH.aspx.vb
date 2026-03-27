Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Configuration

Partial Class Transactions_ASR_SEARCH
    Inherits System.Web.UI.Page
    Dim _list As Library.Database.ListCollection
    Private str_MSSQL_Connstr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("ULEVEL") = 3 Then
                Dim r_companycode As String = Session("COMPANYCODE")
                Dim _refno As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetASRDDL(r_companycode.ToString())

                ddlRefNo.Items.Add(New ListItem("--Select--", "0"))
                If _refno.Rows.Count > 0 Then
                    For i As Integer = 0 To _refno.Rows.Count - 1
                        ddlRefNo.Items.Add(New ListItem(_refno.Rows(i).Item("REFNO").ToString, _refno.Rows(i).Item("REFNO").ToString))
                    Next
                    ddlRefNo.DataBind()
                End If
            Else
                Dim _refno2 As Data.DataTable = Library.Database.BLL.SubSlitRequest.GetASRDDL2()
                ddlRefNo.Items.Add(New ListItem("--Select--", "0"))
                If _refno2.Rows.Count > 0 Then
                    For i As Integer = 0 To _refno2.Rows.Count - 1
                        ddlRefNo.Items.Add(New ListItem(_refno2.Rows(i).Item("REFNO").ToString, _refno2.Rows(i).Item("REFNO").ToString))
                    Next
                    ddlRefNo.DataBind()
                End If
            End If
        End If
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        Dim r_Refno As String = ddlRefNo.SelectedValue.ToString().Trim
        Dim _list As String = String.Empty

        _list = Library.Database.BLL.SubSlitRequest.GET_ASR_TO_EXCEL(r_Refno)

        Dim _str_fileName = "AfterSlittingReport " & DateTime.Now.ToString("yyyyMMdd HHmm") & ".xls"
        _str_fileName = "attachment;filename=" & _str_fileName
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", _str_fileName)
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Response.Output.Write(Library.Database.BLL.SubSlitRequest.GET_ASR_TO_EXCEL(r_Refno))
        Response.Flush()
        Response.End()

    End Sub

End Class
