<%@ Page Language="VB" AutoEventWireup="false" CodeFile="test.aspx.vb" Inherits="Transactions_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
          <form id="form1" runat="server">
          <asp:Label ID="Label23" runat="server" Text="Password : "></asp:Label><asp:TextBox ID="txtpassword" runat="server" Width="200"></asp:TextBox>
          <asp:Button ID="Button1" runat="server" Text="Encrypt" />
          <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label><br />
          <asp:Button ID="Button2" runat="server" Text="Decrypt" style="height: 26px" /> 
          <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label><br />
          <asp:Label ID="Label1" runat="server" Text="CipherPass : "></asp:Label><asp:TextBox ID="txtcipher" runat="server" Width="200"></asp:TextBox>
          <asp:Button ID="Button3" runat="server" Text="Decrypt" style="height: 26px" />
          <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label> 
          </form>
</body>
</html>
