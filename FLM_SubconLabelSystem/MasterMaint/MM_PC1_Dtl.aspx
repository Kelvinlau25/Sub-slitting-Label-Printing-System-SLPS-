<%@ Page Language="C#" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="true" CodeBehind="MM_PC1_Dtl.aspx.cs" Inherits="MasterMaint_MM_PC1_Dtl" title="MM_PC1_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 606px;
        }
        .style4
        {
            width: 10px;
        }
        .detailinfo
        {
            width: 686px;
        }
    </style>
<script type="text/javascript">

        function isDecimal(evt, element) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // "-" CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // "." CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
            return false;
            return true;
        }
        
     
</script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <control:Main ID="UCTitle" runat="server" />
    <control:Error ID="UCError" runat="server" ValidationGroup="Group1"/>
    
    <asp:Panel ID="Cdisplay" runat="server">
    <table class="detailinfo">
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="PC1 "></asp:Label></td>
            <td><asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblPC1" runat="server" Text=""></asp:Label></td>   
        </tr>       
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Description"></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblNameDelivery" runat="server" Text=""></asp:Label></td> 
        </tr>   
    </table>
    </asp:Panel>
    
 <asp:Panel ID="Cmodify" runat="server">
    <table class="detailinfo">        
        <tr>
            <td><asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label16" runat="server" Text="PC1 "></asp:Label></td>
            <td class="style4"><asp:Label ID="Label17" runat="server" Text=" : "></asp:Label></td>
            <td class="style3">
                <asp:Label ID="lbPC1" runat="server" Width="100" style="margin-left: 5px"></asp:Label>
                <asp:TextBox ID="txtPC1" runat="server" Width="100" style="margin-left: 5px" MaxLength="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfPC1" runat="server" ControlToValidate="txtPC1" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>  
        </tr>
        
        <tr>
            <td><asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label19" runat="server" Text="Description"></asp:Label></td>
            <td class="style4"><asp:Label ID="Label20" runat="server" Text=" : "></asp:Label></td>
            <td class="style3">
               <asp:TextBox ID="txtNameDelivery" runat="server" Width="700" style="margin-left: 4px" MaxLength="50"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfNameDelivery" runat="server" ControlToValidate="txtNameDelivery" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>   
        </tr>
        
    </table>
    </asp:Panel>
    
    
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>
    
</asp:Content>