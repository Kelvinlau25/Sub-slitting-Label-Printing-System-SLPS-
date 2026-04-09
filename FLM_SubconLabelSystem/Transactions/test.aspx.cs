using System;
using System.Web.UI;

public partial class Transactions_test : Page
{
    protected void Button1_Click(object sender, EventArgs e)
    {
        string encryptedtext = GlobalFunctions.Encrypt(txtpassword.Text);
        Label24.Text = encryptedtext;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string decrytedtext = GlobalFunctions.Decrypt(Label24.Text);
        Label25.Text = decrytedtext;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string decrytedtext = GlobalFunctions.Decrypt(txtcipher.Text);
        Label2.Text = decrytedtext;
    }
}