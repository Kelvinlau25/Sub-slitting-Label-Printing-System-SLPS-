Imports System.Data
Partial Class Transactions_Housekeeping
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        rfddlDataRet.ErrorMessage = String.Format(Resources.Message.FieldEmpty, "")
       
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.IsPostBack) Then
            ddlDataRet.SelectedValue = ""
        End If

    End Sub
    Protected Sub ddlDataRet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDataRet.SelectedIndexChanged

        Dim Curr_Date As Date = Date.Today
        If ddlDataRet.SelectedValue = "" Then
            lbCurrDate.Text = ""
        Else

            Dim Req_Date As Date = Curr_Date.AddDays(-ddlDataRet.SelectedValue)

            lbCurrDate.Text = Format(Req_Date, "dd/MM/yyyy")

        End If
    End Sub

    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim _childSucc As String = "Y"      '_temp
        Dim _motSucc As String = "Y"        '_temp2
        Dim _LotSucc As String = "Y"        '_temp4
        Dim _SubslitDone As String = "N"
        Dim _lotNoDone As String = "N"

        If ddlDataRet.SelectedValue = "" Or lbCurrDate.Text = "" Then
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Please select Data Retention")
        Else
            Dim pDatePurge As String
            Dim preDate As String()
            preDate = (lbCurrDate.Text).Split("/")
            pDatePurge = preDate(2) & "-" & preDate(1) & "-" & preDate(0)

            Dim pCompany As String
            If Session("ULEVEL") = "3" Then
                Exit Sub
            Else
                pCompany = " "
            End If

            '---------- SUBSLIT PURGE ----------------'
            '**** CHILD ****'
            Dim _dtSubSlitChild As Data.DataTable = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSCHILD")
            For i As Integer = 0 To _dtSubSlitChild.Rows.Count - 1
                Dim _temp As String = ""
                _temp = Library.Database.BLL.HouseKeep.DelSubSlitChild(_dtSubSlitChild.Rows(i)("ID_SUBSLIT_REQUEST_CHILD").ToString(), "SSCHILD")

                If _temp = "1" Then
                    'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Data Purging SUBSLIT CHILD Is successful")
                Else
                    _childSucc = "N"
                    If _temp = "0" Then
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "There is an error on purging SUBSLIT CHILD!")
                    Else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp)
                    End If
                End If
            Next

            '**** MOTHER ****'
            If _childSucc = "Y" Then
                Dim _dtSubSlitMother As Data.DataTable = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSMOTHER")
                For j As Integer = 0 To _dtSubSlitMother.Rows.Count - 1
                    Dim _temp2 As String = ""
                    _temp2 = Library.Database.BLL.HouseKeep.DelSubSlitChild(_dtSubSlitMother.Rows(j)("SUBSLIT_REQ_MOTHER_SEQNO").ToString(), "SSMOTHER")

                    If _temp2 = "1" Then
                        'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Data Purging SUBSLIT MOTHER Is successful")
                    Else
                        _motSucc = "N"
                        If _temp2 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "There is an error on purging SUBSLIT MOTHER!")
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp2)
                        End If
                    End If
                Next
            End If

            '**** MAIN ****'
            If _motSucc = "Y" Then
                Dim _dtSubSlitMain As Data.DataTable = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSMAIN")
                For k As Integer = 0 To _dtSubSlitMain.Rows.Count - 1
                    Dim _temp3 As String = ""
                    _temp3 = Library.Database.BLL.HouseKeep.DelSubSlitChild(_dtSubSlitMain.Rows(k)("ID_SUBSLIT_REQUEST").ToString(), "SSMAIN")

                    If _temp3 = "1" Then
                        'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Successfully Purge Data Subslit.")
                        _SubslitDone = "Y"
                    Else
                        If _temp3 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "There is an error on purging SUBSLIT MOTHER!")
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp3)
                        End If
                    End If
                Next
            End If

            '------------------ LOTNO PURGE ---------------------'

            Dim _dtLotSlit As Data.DataTable = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "LOTSLIT")
            For m As Integer = 0 To _dtLotSlit.Rows.Count - 1
                Dim _temp4 As String = ""
                _temp4 = Library.Database.BLL.HouseKeep.DelSubSlitChild(_dtLotSlit.Rows(m)("ID_LOT_SLITTING").ToString(), "LOTSLIT")

                If _temp4 = "1" Then
                    'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Data Purging SUBSLIT CHILD Is successful")
                Else
                    _LotSucc = "N"
                    If _temp4 = "0" Then
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "There is an error on purging LOT SLITTING!")
                    Else
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp4)
                    End If
                End If
            Next

            '**** pc2_lotno ****'
            If _LotSucc = "Y" Then
                Dim _dtPC2Lot As Data.DataTable = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "PC2LOT")
                For n As Integer = 0 To _dtPC2Lot.Rows.Count - 1
                    Dim _temp5 As String = ""
                    _temp5 = Library.Database.BLL.HouseKeep.DelSubSlitChild(_dtPC2Lot.Rows(n)("ID_PC2_LOTNO").ToString(), "PC2LOT")

                    If _temp5 = "1" Then
                        'Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Successfully Purge Data Lot No")
                        _lotNoDone = "Y"
                    Else
                        If _temp5 = "0" Then
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "There is an error on purging PC2 LOTNO!")
                        Else
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, _temp5)
                        End If
                    End If
                Next
            End If

            If _lotNoDone = "Y" And _SubslitDone = "Y" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Successfully Purge Data")
            ElseIf _lotNoDone = "Y" And _SubslitDone = "N" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Successfully Purge Lot No data")
            ElseIf _lotNoDone = "N" And _SubslitDone = "Y" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "Successfully Purge Subslit data")
            ElseIf _lotNoDone = "N" And _SubslitDone = "N" Then
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Me.Page, "No data was purge.")
            End If
        End If
    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Response.End()
    End Sub
End Class
