Imports System.Data.SqlClient
Imports System.Data

Partial Class Master_Default
    Inherits System.Web.UI.Page


    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CheckBoxUpdate()
        CsvDownload()

    End Sub

    Public Sub CheckBoxUpdate()

        If Session("SlitSlotNoArray") IsNot Nothing Then
            Dim SlitSlotNoArray As ArrayList = _
            DirectCast(Session("SlitSlotNoArray"), ArrayList)

            For j As Integer = 0 To SlitSlotNoArray.Count - 1

                Dim upd_stat As String = ""
                upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSel(SlitSlotNoArray(j), "1", "Update")

                If upd_stat.Equals("1") = False Then
                    'MsgBox("Update Print_Sel not successful")
                    'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Update Print_Sel not successful")
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, upd_stat)
                End If

            Next
        End If

    End Sub

    Protected Sub CsvDownload()

        Dim constr As String = ConfigurationManager.ConnectionStrings("PFR_Label_DB").ConnectionString

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select a.* from VIEW_LOT_SLITTING_SERIES a Inner join LOT_SLITTING b on a.SLIT_LOT_NO = b.SLIT_LOT_NO AND a.ID_LOT_SLITTING = b.ID_LOT_SLITTING where (b.PRINT_SEL = 1) And (a.REC_TYPE = 1 OR a.REC_TYPE = 3)")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        If (dt.Rows.Count > 0) Then

                            Dim csv As String = String.Empty

                            For Each column As DataColumn In dt.Columns
                                csv += column.ColumnName + ","c
                            Next

                            'Add new line.
                            csv += vbCr & vbLf

                            For Each row As DataRow In dt.Rows
                                For Each column As DataColumn In dt.Columns
                                    'Add the Data rows.
                                    If column.ColumnName = "LOTNO" Or column.ColumnName = "SLIT_LOT_NO" Then
                                        csv += row(column.ColumnName).ToString().Replace(",", ";") + "A" + ","c
                                    Else
                                        csv += row(column.ColumnName).ToString().Replace(",", ";") + ","c
                                    End If
                                Next
                                'Add new line.
                                csv += vbCr & vbLf
                            Next

                            CheckBoxInit()
                            'Download the CSV file.
                            Response.ClearContent()
                            Response.AddHeader("content-disposition", "attachment;filename=printlabel.csv")
                            Response.Charset = ""
                            Response.ContentType = "application/text"
                            Response.Output.Write(csv)
                            Response.End()

                        End If

                    End Using
                End Using
            End Using
        End Using

    End Sub


    Public Sub CheckBoxInit()

        If Session("SlitSlotNoArray") IsNot Nothing Then

            Dim SlitSlotNoArray As ArrayList = _
            DirectCast(Session("SlitSlotNoArray"), ArrayList)

            For j As Integer = 0 To SlitSlotNoArray.Count - 1
                Dim upd_stat As String = ""
                upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSel(SlitSlotNoArray(j), "1", "Init")
                If upd_stat.Equals("1") = False Then
                    'MsgBox("Update Print_Sel not successful")
                    'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Update Print_Sel not successful")
                    Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, upd_stat)
                End If
            Next

        End If

    End Sub

End Class
