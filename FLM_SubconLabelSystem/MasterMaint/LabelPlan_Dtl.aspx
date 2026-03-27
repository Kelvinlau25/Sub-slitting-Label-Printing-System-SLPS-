<%@ Page Language="VB" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="false" CodeFile="LabelPlan_Dtl.aspx.vb" Inherits="MasterMaint_LabelPlan_Dtl" title="LabelPlan_Dtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
     
        .style1
        {
            width: 143px;
        }
        .style2
        {
            width: 12px;
            text-align: right;
        }
        .style3
        {
            width: 143px;
            height: 23px;
        }
        .style4
        {
            width: 12px;
            text-align: right;
            height: 23px;
        }
        .style5
        {
            height: 23px;
        }
     

    </style>
    
    <script type="text/javascript">
        function isDecimal(evt, element) {
                    var charCode = (evt.which) ? evt.which : event.keyCode
                    if (
                        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                        (charCode < 48 || charCode > 57))
                    return false;
                    return true;
                }
    </script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Error ID="UCError" runat="server" ValidationGroup="Group1"/>
    
    <asp:Panel ID="Cdisplay" runat="server" Height="400px">
    
    <table width="100%">
        <tr>
            <td class="style1"><asp:Label ID="Label13" runat="server" Text="Company Code "></asp:Label></td>
            <td class="style2"><asp:Label ID="Label14" runat="server" Text=" : "></asp:Label></td> 
            <td><asp:Label ID="lblCompCode" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
            <td class="style1"><asp:Label ID="Label11" runat="server" Text="Planning Year Month "></asp:Label></td>
            <td class="style2"><asp:Label ID="Label12" runat="server" Text=" : "></asp:Label></td> 
            <td><asp:Label ID="lblPlanYrMth" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
            <td class="style1"><asp:Label ID="Label3" runat="server" Text="Production Line "></asp:Label></td>
            <td class="style2"><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblProdLine" runat="server" Text=""></asp:Label></td>
        </tr>
      
        <tr>
            <td class="style1"><asp:Label ID="Label19" runat="server" Text="PC 1 Mother "></asp:Label></td>
            <td class="style2">  <asp:Label ID="Labe20" runat="server" Text=" : "></asp:Label></td> 
            <td><asp:Label ID="lblPC1Mother" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
            <td class="style3"><asp:Label ID="Label1" runat="server" Text="PC 2 Mother "></asp:Label></td>
            <td class="style4"><asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td> 
            <td class="style5"><asp:Label ID="lblPC2Mother" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
            <td class="style1"><asp:Label ID="Label15" runat="server" Text="Unit Weight "></asp:Label></td>
            <td class="style2"><asp:Label ID="Label16" runat="server" Text=" : "></asp:Label></td> 
            <td><asp:Label ID="lblUnitWeightMother" runat="server" Text=""></asp:Label></td>
        </tr>
   
        <tr>
            <td class="style1"><asp:Label ID="Label17" runat="server" Text="PC 1 Customer "></asp:Label></td>
            <td class="style2"><asp:Label ID="Label18" runat="server" Text=" : "></asp:Label></td> 
            <td><asp:Label ID="lblPC1Customer" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
            <td class="style1"><asp:Label ID="Label6" runat="server" Text="PC 2 Customer "></asp:Label></td>
            <td class="style2"><asp:Label ID="Label9" runat="server" Text=" : "></asp:Label></td> 
            <td><asp:Label ID="lblPC2Customer" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
           <td class="style1"><asp:Label ID="Label5" runat="server" Text="Unit Weight "></asp:Label></td>
           <td class="style2"><asp:Label ID="Label10" runat="server" Text=" : "></asp:Label></td> 
           <td><asp:Label ID="lblUnitWeightCustomer" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>
           <td class="style1"><asp:Label ID="Label21" runat="server" Text="Lot No "></asp:Label></td>
           <td class="style2"><asp:Label ID="Label22" runat="server" Text=" : "></asp:Label></td> 
           <td><asp:Label ID="lblLotNo" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
           <td class="style1"><asp:Label ID="Label20" runat="server" Text="Lot Slit No "></asp:Label></td>
           <td class="style2"><asp:Label ID="Label41" runat="server" Text=" : "></asp:Label></td> 
           <td><asp:Label ID="lblLotSlitNo" runat="server" Text=""></asp:Label></td>
        </tr>
          
        <tr>
           <td class="style1"><asp:Label ID="Label7" runat="server" Text="Status "></asp:Label></td>
           <td class="style2"><asp:Label ID="Label8" runat="server" Text=" : "></asp:Label></td> 
           <td><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
        </tr>
       
    </table>
    </asp:Panel>
      
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>
    
</asp:Content>


