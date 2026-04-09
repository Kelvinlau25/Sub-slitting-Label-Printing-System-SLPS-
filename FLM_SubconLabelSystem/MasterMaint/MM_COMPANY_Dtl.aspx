<%@ Page Language="C#" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="true" CodeBehind="MM_COMPANY_Dtl.aspx.cs" Inherits="MasterMaint_MM_COMPANY_Dtl" title="MM_COMPANY_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style2
        {
            width: 250px;
        }
    </style>
    <script type="text/javascript">
        function isDecimal(evt, element) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&
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
            <td style="width:180px;"><asp:Label ID="Label1" runat="server" Text="Company Code "></asp:Label></td>
            <td><asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCompCode" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Company Name"></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCompName" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Address"></asp:Label></td>
            <td><asp:Label ID="Label6" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label7" runat="server" Text="Telephone"></asp:Label></td>
            <td><asp:Label ID="Label8" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblTelephone" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label12" runat="server" Text="Email"></asp:Label></td>
            <td><asp:Label ID="Label13" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label29" runat="server" Text="Slit Code"></asp:Label></td>
            <td><asp:Label ID="Label30" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblslit" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="Cmodify" runat="server">
    <table class="detailinfo">
        <tr>
            <td><asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label16" runat="server" Text="Company Code "></asp:Label></td>
            <td><asp:Label ID="Label17" runat="server" Text=" : "></asp:Label></td>
            <td>
               <asp:Label ID="lbCompCode" runat="server" Width="200" style="margin-left: 5px"></asp:Label>
               <asp:TextBox ID="txtCompCode" runat="server" Width="200"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfCompCode" runat="server" ControlToValidate="txtCompCode" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label19" runat="server" Text="Company Name"></asp:Label></td>
            <td><asp:Label ID="Label20" runat="server" Text=" : "></asp:Label></td>
            <td>
               <asp:TextBox ID="txtCompName" runat="server" Width="200"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfCompName" runat="server" ControlToValidate="txtCompName" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label22" runat="server" Text="Address"></asp:Label></td>
            <td><asp:Label ID="Label23" runat="server" Text=" : "></asp:Label></td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" Width="200" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfAddress" runat="server" ControlToValidate="txtAddress" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td style="width:180px;"><asp:Label ID="Label10" runat="server" Text="Telephone"></asp:Label></td>
            <td><asp:Label ID="Label11" runat="server" Text=" : "></asp:Label></td>
            <td>
                <asp:TextBox ID="txtTelephone" runat="server" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfTelephone" runat="server" ControlToValidate="txtTelephone" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label24" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label25" runat="server" Text="Email"></asp:Label></td>
            <td><asp:Label ID="Label26" runat="server" Text=" : "></asp:Label></td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="200" ValidationGroup="Group1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfEmail2" runat="server" ControlToValidate="txtEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Group1" Display="None"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label14" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label27" runat="server" Text="Slit Code"></asp:Label></td>
            <td><asp:Label ID="Label28" runat="server" Text=" : "></asp:Label></td>
            <td>
                <asp:TextBox ID="txtSlit" runat="server" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfSlit" runat="server" ControlToValidate="txtSlit" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    </asp:Panel>

    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>

</asp:Content>