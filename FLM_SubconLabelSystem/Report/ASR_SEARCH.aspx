<%@ Page Language="VB" MasterPageFile="~/master/Main.master" AutoEventWireup="false" CodeFile="ASR_SEARCH.aspx.vb" Inherits="Transactions_ASR_SEARCH" title="After Slitting Report" EnableEventValidation = "false"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="<%= ResolveUrl("~/js/jquery-3.4.1.js") %>"  type="text/javascript"></script>
    <%--<script src="<%= ResolveUrl("~/js/jquery-1.4.2.min.js") %>" type="text/javascript"></script>--%>
    <link href="<%= ResolveUrl("~/css/overcast/jquery-ui-1.8.18.custom.css") %>" rel="stylesheet" type="text/css"/>
    <script src="<%= ResolveUrl("~/js/jquery-ui-1.8.9.custom.min.js") %>"  type="text/javascript"></script>
    
         <style type="text/css">
            .style1
            {
                width: 57px;
            }
             .styler
            {
                color :Red ;
            }
             .style2
             {
                 width: 13px;
             }
        </style>

   	    <script language="javascript" type="text/javascript">
            $(document).ready(function(){           
               $('[id*=txtDateFrom]').datepicker({
                   onSelect: function(dateText, inst) { },
                    dateFormat: 'dd/mm/yy'
                });
            });        
	    </script>
	    
        <script language="javascript" type="text/javascript">
            $(document).ready(function(){
               $('[id*=txtDateTo]').datepicker({
                   onSelect: function(dateText, inst) { },
                    dateFormat: 'dd/mm/yy'
                });
            });       
	    </script>
	    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
<tr>
    <td>


<div>
<table width="100%">

<tr>
<td>
 <h2><asp:Label ID="Label9" runat="server" Text="After Slitting Report"></asp:Label></h2><hr />
</td>
</tr>
</table>
</div>
 <br />
<div>
                 <table>  
                 <tr>
                    <td class="style1">         
                        <asp:Label ID="Label14" runat="server" Text="Ref No."></asp:Label>
                    </td>
                    <td class="style2"><asp:Label ID="Label15" runat="server" Text=" : " Font-Bold="True"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlRefNo" runat="server" AutoPostBack="False" 
                            Height="26px" Width="200px"></asp:DropDownList>    
                    </td> 
                    <td>
                    <div>
                       <asp:Button ID="btnGenerate" runat="server" Text="Generate" Font-Bold="True"/>
                   </div>
                    </td>      
                </tr>
                </table>
        
                </div>      
                 <br />   

                </td>
        
</tr>
</table>
</asp:Content>
