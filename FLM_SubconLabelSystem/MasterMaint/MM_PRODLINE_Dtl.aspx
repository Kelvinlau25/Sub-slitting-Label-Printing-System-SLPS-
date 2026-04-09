<%@ Page Language="C#" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="true" CodeFile="MM_PRODLINE_Dtl.aspx.cs" Inherits="MasterMaint_MM_PRODLINE_Dtl" title="MM_PRODLINE_Dtl" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style5
        {
            width: 180px;
        }
        .style6
        {
            width: 242px;
        }
        .detailinfo
        {
            margin-right: 128px;
            width: 1225px;
        }
        .style7
        {
            width: 1567px;
            height: 46px;
        }
        .style9
        {
            width: 1176px;
            height: 46px;
        }
        .style10
        {
            width: 584px;
        }
        .style15
        {
            width: 1567px;
        }
        .style16
        {
            width: 1567px;
            height: 64px;
        }
        .style17
        {
            width: 1176px;
            height: 64px;
        }
        .style18
        {
            width: 1567px;
            height: 47px;
        }
        .style19
        {
            width: 1176px;
            height: 47px;
        }
        .style20
        {
            width: 429px;
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
    
    <asp:Panel ID="Cdisplay" runat="server" Height="156px">
    <table class="detailinfo">
        <tr>
            <td class="style5"><asp:Label ID="Label11" runat="server" Text="Product Line"></asp:Label></td>
            <td style="width:10px;"><asp:Label ID="Label12" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblProdLine" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr> 
            <td class="style5"></td>
        </tr>
        
        <tr>
            <td class="style5">
                <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblDesc" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    </asp:Panel>
    
 <asp:Panel ID="Cmodify" runat="server" Height="156px">

    <table class="detailinfo">
        <tr>
            <td class="style5"><asp:Label ID="Label23" runat="server" Text="* " ForeColor="Red"></asp:Label>
                               <asp:Label ID="Label24" runat="server" Text="Product Line"></asp:Label></td>
            <td style="width:10px;"><asp:Label ID="labelProdLine" runat="server" Text=" : "></asp:Label></td>   
            <td><asp:Label ID="lbProdLine" runat="server" Width="150px" Height="25px"></asp:Label>
                <asp:TextBox ID="txtProdLine" runat="server" Width="150px" Height="25px" MaxLength="15"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfProdLine" runat="server" ControlToValidate="txtProdLine" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator></td>  
        </tr>
        <tr>
            <td class="style5"></td>
            <td></td>
        </tr> 
        <tr>        
            <td class="style5"><asp:Label ID="Label21" runat="server" Text="* " ForeColor="White" ></asp:Label>
                               <asp:Label ID="Label27" runat="server" Text="Description "></asp:Label></td>
            <td><asp:Label ID="lbDesc" runat="server" Text=" : "></asp:Label></td>
            <td><asp:TextBox ID="txtDesc" runat="server" Width="300px" Height="25px" MaxLength="30"></asp:TextBox></td>   
        </tr>
    </table>

 </asp:Panel>
    
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>
    
</asp:Content>