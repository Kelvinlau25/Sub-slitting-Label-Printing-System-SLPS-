<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title><%=ConfigurationManager.AppSettings("title")%></title>
      <link href="css/SignIn.css" rel="stylesheet" type="text/css" />
      <style type="text/css">
          .style1
          {
              width: 247px;
          }
      </style>
</head>
<body>
    
   <table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table style="margin:auto;height:500px;" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td>
                                <form id="form2" runat="server">
                                    <asp:ValidationSummary ID="vsSummary" ValidationGroup="login" ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" runat="server" />
                                    <asp:CustomValidator runat="server" id="cusCustom" ValidationGroup="login" onservervalidate="cusCustom_ServerValidate" Display="None" errormessage="Invalid password and username!" />
                                    <div class="centered">
                                      <table>
                                        <tr>
                                            <td class="form" >
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td colspan="2" class="header"><img class="banner" src="image/LoginScreenWords_png.PNG" alt="" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <table class="login">
                                                                    <tr>
                                                                        <td class="style1">
                                                                              <span style="color:red">*</span>User ID : 
                                                                              <asp:TextBox ID="txtuserID" runat="server" MaxLength="50" CssClass="value" 
                                                                                  Height="27px" autocomplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style1">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                         <td class="style1">
                                                                         <span style="color:red">*</span>Password :
                                                                         <asp:TextBox ID="txtpassword" runat="server" MaxLength="15" 
                                                                                 CssClass="value" Height="27px" TextMode="Password" autocomplete="off"></asp:TextBox></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td class="style1">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                         <td class="style1">
                                                                         <span style="color:red">*</span>Captcha :
                                                                         <img style="WIDTH: 120px; HEIGHT: 32px" alt="" src="Captcha.aspx" />
                                                                         <asp:TextBox ID="txtCaptcha" runat="server" MaxLength="15" 
                                                                                 CssClass="value" Height="27px" autocomplete="off"></asp:TextBox></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td class="style1">&nbsp;</td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td class="style1">
                                                                            <asp:Button ID="LoginButton" runat="server" Text="Login" Width="83px" 
                                                                                Height="27px" ValidationGroup="login" />                                               
                                                                            &nbsp 
                                                                           <asp:Button ID="ClearButton" runat="server" Text="Clear" Width="80px" 
                                                                                Height="27px" /> 
                                                                         </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                      </table>
                                    </div>
                                </form>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
   </table>
</body>
</html>
