<%@ Page Language="C#" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="true" CodeBehind="MM_PC2_Dtl.aspx.cs" Inherits="MasterMaint_MM_PC2_Dtl" title="MM_PC2_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style5
        {
            width: 180px;
        }
        .detailinfo
        {
            margin-right: 128px;
            width: 1084px;
            height: 19px;
        }
        .style25
        {
            width: 3px;
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
            <td class="style5"><asp:Label ID="Label11" runat="server" Text="PC2 "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label14" runat="server" Text=":"></asp:Label></td>
            <td><asp:Label ID="lblPC2" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr><td></td></tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label3" runat="server" Text="Thickness "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblThickness" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr><td></td></tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label15" runat="server" Text="Type "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label16" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblType" runat="server" Text=""></asp:Label></td>
        </tr>
      
        <tr><td></td></tr>
      
        <tr>
            <td class="style5"><asp:Label ID="Label19" runat="server" Text="Width "></asp:Label></td>
            <td class="style25"><asp:Label ID="Labe20" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblWidth" runat="server" Text=""></asp:Label></td> 
        </tr>
        
          <tr><td></td></tr>
       
        <tr>
            <td class="style5"><asp:Label ID="Label7" runat="server" Text="Length "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label8" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblLength" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr><td></td></tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label6" runat="server" Text="Grade "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label9" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblGrade" runat="server" Text=""></asp:Label></td> 
        </tr>
        
          <tr><td></td></tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label1" runat="server" Text="Packing Code "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblPackCode" runat="server" Text=""></asp:Label></td>          
        </tr>
        
         <tr><td></td></tr>
        
         <tr>
            <td class="style5"><asp:Label ID="Label5" runat="server" Text="Core Code "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label10" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCoreCode" runat="server" Text=""></asp:Label></td> 
        </tr>
        
         <tr><td></td></tr>
        
         <tr>
            <td class="style5"><asp:Label ID="Label13" runat="server" Text="Unit Weight "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label18" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblUnitWeight" runat="server" Text=""></asp:Label></td> 
         </tr>
         
          <tr><td></td></tr>
        
          <tr>
            <td class="style5"><asp:Label ID="Label20" runat="server" Text="Remarks "></asp:Label></td>
            <td class="style25"><asp:Label ID="Label41" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label></td> 
          </tr>
    </table>
    </asp:Panel>

 <asp:Panel ID="Cmodify" runat="server">
    <table>
          
                <tr id="tblPC2" runat="server">
                    <td class="style5">
                    <asp:Label ID="Label23" runat="server" Text="* " ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label24" runat="server" Text="PC2 "></asp:Label></td>
                    <td style="width:10px;"><asp:Label ID="Label25" runat="server" Text=" : "></asp:Label></td>
                    <td><asp:Label ID="txtPC2" runat="server" Width="197px"></asp:Label></td>  
                </tr>
              
      <tr><td></td></tr>

       <tr>        
            <td class="style5">
                <asp:Label ID="Label26" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label27" runat="server" Text="Thickness "></asp:Label></td>
            <td><asp:Label ID="LabelThickness" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbThickness" runat="server"></asp:Label>
                <asp:TextBox ID="txtThickness" runat="server" Width="93px" Height="25px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfThickness" runat="server" ControlToValidate="txtThickness" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
            
        </tr>
        
          <tr><td></td></tr>
      
        <tr>
            <td class="style5"><asp:Label ID="Label29" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label30" runat="server" Text="Type "></asp:Label></td>
            <td><asp:Label ID="Label31" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbType" runat="server"></asp:Label>
                <asp:TextBox ID="txtType" runat="server" Height="25px" Width="93px" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfType" runat="server" ControlToValidate="txtType" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator></td>   
        </tr>
        
          <tr><td></td></tr>
        
        <tr>        
            <td class="style5">
                <asp:Label ID="Label28" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label32" runat="server" Text="Width "></asp:Label></td>
            <td><asp:Label ID="LabelWidth" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbWidth" runat="server"></asp:Label>
                <asp:TextBox ID="txtWidth" runat="server" Width="93px" Height="25px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfWidth" runat="server" ControlToValidate="txtWidth" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
      </tr>
      
       <tr><td></td></tr>
      
      <tr>        
            <td class="style5">
                <asp:Label ID="Label34" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label35" runat="server" Text="Length "></asp:Label></td>
            <td><asp:Label ID="LabelLength" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbLength" runat="server"></asp:Label>
                <asp:TextBox ID="txtLength" runat="server" Width="93px" Height="25px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfLength" runat="server" ControlToValidate="txtLength" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
        </tr>
        
          <tr><td></td></tr>
         
        <tr>
            <td class="style5">
                <asp:Label ID="Label38" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label39" runat="server" Text="Grade "></asp:Label></td>
            <td><asp:Label ID="LabelGrade" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbGrade" runat="server"></asp:Label>
                <asp:TextBox ID="txtGrade" runat="server" Width="93px" Height="25px" MaxLength="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfGrade" runat="server" ControlToValidate="txtGrade" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
        </tr>
        
           <tr><td></td></tr>
        
         <tr>        
            <td class="style5">
                <asp:Label ID="Label33" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label36" runat="server" Text="Packing Code "></asp:Label></td>
            <td><asp:Label ID="LabelPackCode" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbPackCode" runat="server"></asp:Label>
                <asp:TextBox ID="txtPackCode" runat="server" Width="93px" Height="25px" MaxLength="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfPackCode" runat="server" ControlToValidate="txtPackCode" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
         </tr>
         
         <tr><td></td></tr>
        
        <tr>        
            <td class="style5">
                <asp:Label ID="Label37" Text="* " runat="server" ForeColor="White"></asp:Label>
                <asp:Label ID="Label42" runat="server" Text="Core Code "></asp:Label></td>
            <td><asp:Label ID="LabelCoreCode" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbCoreCode" runat="server"></asp:Label>
                <asp:TextBox ID="txtCoreCode" runat="server" Width="93px" Height="25px" MaxLength="5"></asp:TextBox></td>   
        </tr>
        
         <tr><td></td></tr>
        
         <tr>        
            <td class="style5">
                <asp:Label ID="Label40" runat="server" Text="* " ForeColor="Red"></asp:Label>
                <asp:Label ID="Label43" runat="server" Text="Unit Weight "></asp:Label></td>
            <td><asp:Label ID="LabelUnitWeight" runat="server" Text=" : "></asp:Label></td>
            <td><asp:TextBox ID="txtUnitWeight" runat="server" Width="93px" Height="25px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfUnitWeight" runat="server" ControlToValidate="txtUnitWeight" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="reUnitWeight" runat="server" 
                     ControlToValidate="txtUnitWeight" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
            </td>  
         </tr> 
      
       <tr><td></td></tr>  
        
        <tr>
            <td class="style5">
                <asp:Label ID="Label49" runat="server" Text="Remarks "></asp:Label></td>
            <td><asp:Label ID="LabelRemarks" runat="server" Text=" : "></asp:Label></td>
            <td><asp:TextBox ID="txtRemarks" runat="server" Width="350px" Height="25px"></asp:TextBox></td>  
       </tr>
       
        <tr><td></td></tr>
       
    </table>
    </asp:Panel>
      
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>
    
</asp:Content>