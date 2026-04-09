<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" title="Change_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type ="text/javascript">
        
        function LogOut()
        {
        window.open('../Default.aspx', '_parent', '');
        }  
        
        
		$(document).ready(function(){
        var pswexpired = '<%=Session["pswexpired"]%>';

        if (pswexpired == 1){
        alert('Password exceeded validity period. Please change password now.');
        }
        });
    </script>
    
    <style type="text/css" >
        html,body {
	        margin:0;
	        padding:0;
	        height:100%;
	        overflow:hidden;
        }
        #ChangedPassword {
    	    font-size:11px;
        }
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
<tr>
    <td>
    
            <table width="500px" style="height:auto;margin:auto;" border="0" cellpadding="1" cellspacing="0">
                    <tr>
                        <td align="center" colspan="3"><img src="../image/ChgPassword.jpg" alt="Change Password"/></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" ></asp:Label>&nbsp;Password</td>
                        <td>:</td>
                        <td><asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Width="250px" MaxLength="15" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfpassword" runat="server" ValidationGroup="login" ControlToValidate="txtpassword" ForeColor="White">.</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="repassword" runat="server" ValidationGroup="login" ControlToValidate="txtpassword" ForeColor="White">.</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" ></asp:Label>&nbsp;New Password</td>
                        <td>:</td>
                        <td><asp:TextBox ID="txtnewpassword" runat="server" TextMode="Password" Width="250px" MaxLength="15" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfnewpassword" runat="server" ValidationGroup="login" ControlToValidate="txtnewpassword" ForeColor="White">.</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="renewpassword" runat="server" ValidationGroup="login" ControlToValidate="txtnewpassword" ForeColor="White">.</asp:RegularExpressionValidator></td>
                            <asp:CompareValidator ID="nePassword" ValidationGroup="login" ControlToCompare="txtpassword" ControlToValidate="txtnewpassword" runat="server" SetFocusOnError="true" Operator="NotEqual" ForeColor="White" >.</asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red" ></asp:Label>&nbsp;Confirm Password</td>
                        <td>:</td>
                        <td><asp:TextBox ID="txtconfirmpassword" runat="server" TextMode="Password" Width="250px" MaxLength="15" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfconfirmpassword" runat="server" ValidationGroup="login" ControlToValidate="txtconfirmpassword" ForeColor="White">.</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reconfirmpassword" runat="server" ValidationGroup="login" ControlToValidate="txtconfirmpassword" ForeColor="White">.</asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="cvPassword" ValidationGroup="login" ControlToCompare="txtnewpassword" ControlToValidate="txtconfirmpassword" ForeColor="White" runat="server" SetFocusOnError="true" Operator="Equal" >.</asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                            <td><asp:Button ID="btnupdate" runat="server" Text="Change Password" ValidationGroup="login" OnClick="btnupdate_Click" />&nbsp;<asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" />&nbsp;<asp:Button ID="btnBack" runat="server" Text="Cancel" AutoPostBack="true" OnClick="btnBack_Click" />
                            <asp:ValidationSummary ID="vssummary" runat="server" ValidationGroup="login" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" /></td>
                    </tr>
                    <tr>
                    <td colspan="4"><br /><span style="color:Red;">* Passwords Must Contain at least 1 Alphabet and 1 Number.</span><br /><span style="color:Red;">* Passwords Must Contain a Minimum of 9 Characters.</span><br /><span style="color:Red;">* Passwords will expire after <%=Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Max_Password_Age"])%> days.</span><br /><span style="color:Red;">* Users are not allow to reuse any of their previous 5 passwords.</span></td>
                    </tr>
            </table>

    </td>
</tr>
</table>
</asp:Content>