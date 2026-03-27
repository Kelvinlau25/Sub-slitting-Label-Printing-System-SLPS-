<%@ Page Language="VB" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="false" CodeFile="MM_PC2_MOTHER_Dtl.aspx.vb" Inherits="MasterMaint_MM_PC2_MOTHER_Dtl" title="MM_PC2_MOTHER_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style3
        {
            width: 1176px;
        }
        .style5
        {
            width: 197px;
        }
        .style6
        {
            width: 983px;
        }
        .detailinfo
        {
            margin-right: 128px;
        }
        .style7
        {
            width: 1142px;
            height: 46px;
        }
        .style9
        {
            width: 1176px;
            height: 46px;
        }
        .style10
        {
            width: 1142px;
        }
        .style11
        {
            width: 1142px;
            height: 45px;
        }
        .style12
        {
            width: 1176px;
            height: 45px;
        }
        .style13
        {
            width: 1142px;
            height: 48px;
        }
        .style14
        {
            width: 1176px;
            height: 48px;
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
    
    <asp:Panel ID="Cdisplay" runat="server" Height="166px">
    <table class="detailinfo">
        <tr>
            <td class="style5"><asp:Label ID="Label11" runat="server" Text="PC2 "></asp:Label>
                <asp:Label ID="Label12" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblPC2" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label3" runat="server" Text="Thickness "></asp:Label>
                <asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblThickness" runat="server" Text=""></asp:Label></td>
            <td class="style3">
                 <asp:Label ID="Label15" runat="server" Text="Type "></asp:Label>
                 <asp:Label ID="Label16" runat="server" Text=" : "></asp:Label>
                 <asp:Label ID="lblType" runat="server" Text=""></asp:Label></td>
        </tr>
      
        <tr>
            <td class="style5"><asp:Label ID="Label19" runat="server" Text="Width "></asp:Label>
                <asp:Label ID="Labe20" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblWidth" runat="server" Text=""></asp:Label></td> 
       
            <td class="style3">
                <asp:Label ID="Label7" runat="server" Text="Length "></asp:Label>
                <asp:Label ID="Label8" runat="server" Text=" : "></asp:Label>
                <asp:Label ID="lblLength" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label1" runat="server" Text="Packing Code "></asp:Label>
                <asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblPackCode" runat="server" Text=""></asp:Label></td>
            <td class="style3">
                <asp:Label ID="Label6" runat="server" Text="Grade "></asp:Label>
                <asp:Label ID="Label9" runat="server" Text=" : "></asp:Label>
                <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label5" runat="server" Text="Core Code "></asp:Label>
                <asp:Label ID="Label10" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCoreCode" runat="server" Text=""></asp:Label></td> 
       
            <td class="style3">
                <asp:Label ID="Label14" runat="server" Text="Machine "></asp:Label>
                <asp:Label ID="Label17" runat="server" Text=" : "></asp:Label>
                <asp:Label ID="lblMachine" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style5"><asp:Label ID="Label13" runat="server" Text="Unit Weight "></asp:Label>
                <asp:Label ID="Label18" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblUnitWeight" runat="server" Text=""></asp:Label></td> 
       
            <td class="style3">
                <asp:Label ID="Label21" runat="server" Text="No. Per Pack "></asp:Label>
                <asp:Label ID="Label22" runat="server" Text=" : "></asp:Label>
                <asp:Label ID="lblNumPerPack" runat="server" Text=""></asp:Label>
            </td> 
        </tr>
        
          <tr>
            <td class="style5"><asp:Label ID="Label20" runat="server" Text="Remarks "></asp:Label>
                <asp:Label ID="Label41" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label></td> 
          </tr>
       
    </table>
    </asp:Panel>
    
 <asp:Panel ID="Cmodify" runat="server">
    <table class="detailinfo">
      <tr>
         <td colspan="1"> 
              <table id="tblPC2" runat="server" class="detailinfo"> 
                    <tr>
                        <td class="style10"><asp:Label ID="Label23" runat="server" Text="* " ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label24" runat="server" Text="PC2 "></asp:Label>
                            <asp:Label ID="LabelPC2" runat="server" Text=" : "></asp:Label>
                            <asp:TextBox ID="txtPC2" runat="server" Width="197px" style="margin-left: 119px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfPC2" runat="server" ControlToValidate="txtPC2" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
                        </td>  
                    </tr>
              </table>
         </td>
       </tr>
       
       <tr>        
         <td class="style7"><asp:Label ID="Label26" runat="server" Text="* " ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label27" runat="server" Text="Thickness "></asp:Label>
                            <asp:Label ID="LabelThickness" runat="server" Text=" : "></asp:Label>
                            <asp:TextBox ID="txtThickness" runat="server" Width="76px" Height="30px" style="margin-left: 86px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfThickness" runat="server" ControlToValidate="txtThickness" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
         </td>   
      
         <td class="style9"><asp:Label ID="Label29" runat="server" Text="* " ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label30" runat="server" Text="Type "></asp:Label>
                            <asp:Label ID="Label31" runat="server" Text=" : "></asp:Label>
                            <asp:TextBox ID="txtType" runat="server" Height="25px" Width="93px" style="margin-left: 69px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfType" runat="server" ControlToValidate="txtType" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
         </td>   
       </tr>
        
       <tr>        
         <td class="style11"><asp:Label ID="Label28" runat="server" Text="* " ForeColor="Red"></asp:Label>
                             <asp:Label ID="Label32" runat="server" Text="Width "></asp:Label>
                             <asp:Label ID="LabelWidth" runat="server" Text=" : "></asp:Label>
                             <asp:TextBox ID="txtWidth" runat="server" Width="79px" Height="30px" style="margin-left: 111px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfWidth" runat="server" ControlToValidate="txtWidth" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
         </td>   
        
         <td class="style12"><asp:Label ID="Label34" runat="server" Text="* " ForeColor="Red"></asp:Label>
                             <asp:Label ID="Label35" runat="server" Text="Length "></asp:Label>
                             <asp:Label ID="LabelLength" runat="server" Text=" : "></asp:Label>
                             <asp:TextBox ID="txtLength" runat="server" Width="95px" Height="25px" style="margin-left: 59px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfLength" runat="server" ControlToValidate="txtLength" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
         </td>   
       </tr>
        
       <tr>        
         <td class="style7"><asp:Label ID="Label33" runat="server" Text="* " ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label36" runat="server" Text="Packing Code "></asp:Label>
                            <asp:Label ID="LabelPackCode" runat="server" Text=" : "></asp:Label>
                            <asp:TextBox ID="txtPackCode" runat="server" Width="76px" Height="30px" style="margin-left: 61px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfPackCode" runat="server" ControlToValidate="txtPackCode" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
          </td>   
        
          <td class="style9"><asp:Label ID="Label38" runat="server" Text="* " ForeColor="Red"></asp:Label>
                 <asp:Label ID="Label39" runat="server" Text="Grade "></asp:Label>
                 <asp:Label ID="LabelGrade" runat="server" Text=" : "></asp:Label>
                 <asp:TextBox ID="txtGrade" runat="server" Width="94px" Height="25px" style="margin-left: 62px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfGrade" runat="server" ControlToValidate="txtGrade" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
          </td>   
        </tr>
        
        <tr>        
          <td class="style13"><asp:Label ID="Label42" runat="server" Text="Core Code "></asp:Label>
                              <asp:Label ID="LabelCoreCode" runat="server" Text=" : "></asp:Label>
                              <asp:TextBox ID="txtCoreCode" runat="server" Width="75px" Height="30px" style="margin-left: 86px"></asp:TextBox></td>   
          <td class="style14"><asp:Label ID="Label44" runat="server" Text="* " ForeColor="Red"></asp:Label>
                              <asp:Label ID="Label45" runat="server" Text="Machine "></asp:Label>
                              <asp:Label ID="LabelMachine" runat="server" Text=" : "></asp:Label>
                              <asp:TextBox ID="txtMachine" runat="server" Width="96px" Height="25px" style="margin-left: 47px"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="rfMachine" runat="server" ControlToValidate="txtMachine" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
          </td>   
        </tr>
        
        <tr>        
          <td class="style7"><asp:Label ID="Label40" runat="server" Text="* " ForeColor="Red"></asp:Label>
                             <asp:Label ID="Label43" runat="server" Text="Unit Weight "></asp:Label>
                             <asp:Label ID="LabelUnitWeight" runat="server" Text=" : "></asp:Label>
                             <asp:TextBox ID="txtUnitWeight" runat="server" Width="73px" Height="30px" style="margin-left: 78px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfUnitWeight" runat="server" ControlToValidate="txtUnitWeight" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
          </td>   
      
          <td class="style9"><asp:Label ID="Label47" runat="server" Text="* " ForeColor="Red"></asp:Label>
                             <asp:Label ID="Label48" runat="server" Text="No. Per Pack "></asp:Label>
                             <asp:Label ID="LabelNumPack" runat="server" Text=" : "></asp:Label>
                             <asp:TextBox ID="txtNumPack" runat="server" Width="96px" Height="25px" style="margin-left: 15px" ></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfNumPack" runat="server" ControlToValidate="txtNumPack" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
          </td>   
        </tr>
        
        <tr>
          <td class="style10"><asp:Label ID="Label49" runat="server" Text="Remarks "></asp:Label>
                              <asp:Label ID="LabelRemarks" runat="server" Text=" : "></asp:Label>
                              <asp:TextBox ID="txtRemarks" runat="server" Width="533px" style="margin-left: 104px"></asp:TextBox>
          </td>  
       </tr>
      
    </table>
    </asp:Panel>
    
    
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>
    
</asp:Content>

