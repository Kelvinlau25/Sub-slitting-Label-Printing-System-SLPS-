
Partial Class Transactions_test
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim encryptedtext = GlobalFunctions.encrypt(txtpassword.Text)
        Label24.Text = encryptedtext
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim decrytedtext = GlobalFunctions.decrypt(Label24.Text)
        Label25.Text = decrytedtext
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim decrytedtext = GlobalFunctions.decrypt(txtcipher.Text)
        Label2.Text = decrytedtext
    End Sub

End Class
