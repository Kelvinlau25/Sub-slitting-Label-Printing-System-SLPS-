<%@ Page Language="VB" MasterPageFile="~/master/Main.master" AutoEventWireup="false" CodeFile="Housekeeping.aspx.vb" Inherits="Transactions_Housekeeping" title="House_Keeping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css" >
            html,body {
	            margin:0;
	            padding:0;
	            height:100%;
	            overflow:hidden;
            }
            #ChangedPassword
            {
    	        font-size:11px;
            }
            .style2
            {
                width: 533px;
            }
            .style3
            {
                width: 359px;
            }
            .style4
            {
                width: 359px;
                height: 34px;
            }
            .style5
            {
                height: 34px;
            }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="center"><h3>HOUSEKEEPING</h3></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%; margin:50px;">
                            <tr>
                                <td class="style3">Data Retention :</td>
                                <td>
                                    <asp:DropDownList ID="ddlDataRet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDataRet_SelectedIndexChanged">
                                    <asp:ListItem Selected ="True" Text ="-- Days --" Value="" />
                                    <asp:ListItem Text="90 days" Value ="90" />
                                    <asp:ListItem Text="180 days" Value ="180" />  
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfddlDataRet" runat="server" 
                                        ControlToValidate="ddlDataRet" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>  
                                 </td>                                    
                            </tr>
                            <tr>
                                <td class="style3">
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" >Purge transaction before : </td>
                                <td class="style5">
                                    <asp:Label ID="lbCurrDate" runat="server" Width="123px"  ></asp:Label></td>     
                            </tr>
                            
                            <tr>
                                <td class="style3">
                                </td>
                            </tr>
                            
                            <tr>
                                <td></td>
                            </tr>
                            
                            <tr>
                                <td>Note: Records with updated date before the above date</td>
                            </tr>
                            
                            <tr>
                                <td>
                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          will be purged except Maintenance and Setting tables
                                </td>
                            </tr>
                            
                            <tr>
                                <td></td>
                            </tr>
                            
                            <tr>
                                <td></td>
                            </tr>
                            
                            <tr>
                                <td></td>
                            </tr>
                            
                            <tr>
                                
                                <td class="style3"></td>
                                <td class="style2">&nbsp;<asp:Button ID="btnupdate" runat="server" onclientclick="return confirm('Do you confirm to purge all record before this date?');"
                                        Text="OK" ValidationGroup="login" Width="107px" />
                               &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnreset" runat="server" Text="Cancel" Width="106px" />&nbsp;
                              </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                                                                                
</table>
</asp:Content>