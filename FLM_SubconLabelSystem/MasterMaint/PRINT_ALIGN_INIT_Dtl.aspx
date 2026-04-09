<%@ Page Language="C#" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="true" CodeFile="PRINT_ALIGN_INIT_Dtl.aspx.cs" Inherits="MasterMaint_PRINT_ALIGN_INIT_Dtl" title="PRINT_ALIGN_INIT_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
            .style6
            {
                width: 242px;
            }
            .detailinfo
            {
                margin-right: 128px;
                width: 1335px;
                height: 900px;
            }
            .style8
            {
                height: 49px;
            }
            .style9
            {
                height: 49px;
                width: 242px;
            }
            .style10
            {
                width: 16px;
                height: 49px;
            }
            .style11
            {
                width: 16px;
                text-align: right;
                
            }
            .style12
            {
                width: 18px;
                height: 49px;
            }
            .style13
            {
                width: 18px;
            }
            .style17
            {
                width: 15px;
            }
            .style18
            {
                width: 141px;
            }
            .style19
            {
                width: 391px;
            }
            .style20
            {
                width: 135px;
            }
            .style21
            {
                width: 134px;
                height: 49px;
            }
            .style22
            {
                width: 134px;
            }
            .style23
            {
                width: 147px;
                height: 49px;
            }
            .style24
            {
                width: 147px;
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
    
    <asp:Panel ID="Cdisplay" runat="server" Height="950px">
    <table class="detailinfo">
        <tr>
             <td class="style23"><asp:Label ID="Label128" runat="server" Text="Company Code"></asp:Label></td>
             <td class="style11"><asp:Label ID="Label129" runat="server" Text=" : "></asp:Label></td>
             <td class="style9"><asp:Label ID="lblCompanyCode" runat="server" Text=""></asp:Label></td> 
             <td class="style21"><asp:Label ID="Label15" runat="server" Text="Width X "></asp:Label></td>
             <td class="style12"><asp:Label ID="Label16" runat="server" Text=" : "></asp:Label></td>
             <td class="style8"><asp:Label ID="lblWidthX" runat="server" Text=""></asp:Label></td>
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label11" runat="server" Text="Printer Name"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label12" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblPrinterName" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label7" runat="server" Text="Width Y "></asp:Label></td>
            <td class="style13"><asp:Label ID="Label8" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblWidthY" runat="server" Text=""></asp:Label></td>   
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label3" runat="server" Text="Text Font"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblTextFont" runat="server" Text=""></asp:Label></td>
            <td class="style22"><asp:Label ID="Label6" runat="server" Text="Length Header X "></asp:Label></td>
            <td class="style13"><asp:Label ID="Label9" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblLengthHeaderX" runat="server" Text=""></asp:Label></td>  
        </tr>
      
        <tr>
            <td class="style23"><asp:Label ID="Label19" runat="server" Text="Text Font Size "></asp:Label></td>
            <td class="style11"><asp:Label ID="Labe20" runat="server" Text=" : "></asp:Label></td>
            <td class="style9"><asp:Label ID="lblTextFontSize" runat="server" Text=""></asp:Label></td>  
            <td class="style21"><asp:Label ID="Label14" runat="server" Text="Length Header Y"></asp:Label></td>
            <td class="style12"><asp:Label ID="Label17" runat="server" Text=" : "></asp:Label></td>
            <td class="style8"><asp:Label ID="lblLengthHeaderY" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label1" runat="server" Text="Barcode Font "></asp:Label></td>
            <td class="style11"><asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblBarcodeFont" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label21" runat="server" Text="Unit Weigth X"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label22" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblUnitWeigthX" runat="server" Text=""></asp:Label></td>   
        </tr>
        
         <tr>
            <td class="style24"><asp:Label ID="Label5" runat="server" Text="Barcode Font Size"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label10" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblBarcodeFontSize" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label50" runat="server" Text="Unit Weigth Y"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label51" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblUnitWeigthY" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label13" runat="server" Text="Pack Code X"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label18" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblPackCodeX" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label54" runat="server" Text="Slit Lot No X"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label55" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblSlitLotNoX" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label25" runat="server" Text="Pack Code Y"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label37" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"> <asp:Label ID="LblPackCodeY" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label58" runat="server" Text="Slit Lot No Y"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label59" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblSlitLotNoY" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label46" runat="server" Text="Num Per Pack X"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label52" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblNumPerPackX" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label62" runat="server" Text="Grade X"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label63" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblGradeX" runat="server" Text=""></asp:Label></td>   
        </tr>
        
         <tr>
            <td class="style24"><asp:Label ID="Label53" runat="server" Text="Num Per Pack Y"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label56" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblNumPerPackY" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label66" runat="server" Text="Grade Y"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label67" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblGradeY" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label57" runat="server" Text="PC1 X"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label60" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblPC1X" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label70" runat="server" Text="Core Code X"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label71" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCoreCodeX" runat="server" Text=""></asp:Label></td> 
        </tr>
        
         <tr>
            <td class="style24"><asp:Label ID="Label61" runat="server" Text="PC1 Y"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label64" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblPC1Y" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label74" runat="server" Text="Core Code Y"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label75" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCoreCodeY" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label65" runat="server" Text="Length X"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label68" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblLengthX" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label78" runat="server" Text="Barcode X"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label79" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblBarcodeX" runat="server" Text=""></asp:Label></td>  
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label69" runat="server" Text="Length Y"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label72" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblLengthY" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label82" runat="server" Text="Barcode Y"></asp:Label>
                                <asp:Label ID="Label83" runat="server" Text=" : "></asp:Label></td>
            <td class="style13"></td>
            <td><asp:Label ID="lblBarcodeY" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label73" runat="server" Text="Thickness X"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label76" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"> <asp:Label ID="lblThicknessX" runat="server" Text=""></asp:Label></td>      
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label77" runat="server" Text="Thickness Y"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label80" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblThicknessY" runat="server" Text=""></asp:Label></td> 
            <td class="style22"><asp:Label ID="Label85" runat="server" Text="Default Printer"></asp:Label></td>
            <td class="style13"><asp:Label ID="Label86" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblDefaultPrinter" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
            <td class="style24"><asp:Label ID="Label20" runat="server" Text="Type X"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label41" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblTypeX" runat="server" Text=""></asp:Label></td> 
        </tr>
      
        <tr>
            <td class="style24"><asp:Label ID="Label81" runat="server" Text="Type Y"></asp:Label></td>
            <td class="style11"><asp:Label ID="Label84" runat="server" Text=" : "></asp:Label></td>
            <td class="style6"><asp:Label ID="lblTypeY" runat="server" Text=""></asp:Label></td> 
        </tr>
       
    </table>
    </asp:Panel>
    
 <asp:Panel ID="Cmodify" runat="server" Height="950px">
     <table class="detailinfo">
         <tr>
             <td class="style18">
                 <asp:Label ID="Label131" runat="server" Text="Company Code "></asp:Label></td>
             <td class="style11">
                  <asp:Label ID="Label132" runat="server" Text=" : "></asp:Label></td>
             <td class="style19">
                 <asp:Label ID="lbCompanyCode" runat="server"></asp:Label></td>
             <td class="style20"></td>
             <td class="style17">&nbsp;</td>
             <td></td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label23" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label24" runat="server" Text="Printer Name "></asp:Label></td>
             <td class="style11"><asp:Label ID="labelPrinterNm" runat="server" Text=" : "></asp:Label></td>
             <td class="style19">
                 <asp:TextBox ID="txtPrinterName" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfPrinterName" runat="server" 
                     ControlToValidate="txtPrinterName" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:Label ID="lbPrinterName" runat="server"></asp:Label></td>
             <td class="style20">
                 <asp:Label ID="Label29" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label30" runat="server" Text="Width X "></asp:Label></td>
             <td class="style17">
                 <asp:Label ID="lbWidthX" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtWidthX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfWidthX" runat="server" 
                     ControlToValidate="txtWidthX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reWidthX" runat="server" 
                     ControlToValidate="txtWidthX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
             <td class="style18">
                 <asp:Label ID="Label26" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label27" runat="server" Text="Text Font "></asp:Label>     
             </td>
             <td class="style11">
                 <asp:Label ID="lbTextFont" runat="server" Text=" : "></asp:Label></td>
             <td class="style19" >
                 <asp:TextBox ID="txtTextFont" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfTextFont" runat="server" 
                     ControlToValidate="txtTextFont" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label34" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label35" runat="server" Text="Width Y "></asp:Label>
             </td>
             <td class="style17">
                  <asp:Label ID="lbWidthY" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtWidthY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rftxtWidthY" runat="server" 
                     ControlToValidate="txtWidthY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reWidthY" runat="server" 
                     ControlToValidate="txtWidthY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label28" runat="server" ForeColor="Red" 
                    Text="* "></asp:Label>
                 <asp:Label ID="Label31" runat="server" 
                    Text="Text Font Size "></asp:Label>  
             </td>
             <td class="style11">
                   <asp:Label ID="lbTextFontSize" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19">
                 <asp:TextBox ID="txtTextFontSize" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfTextFontSize" runat="server" 
                     ControlToValidate="txtTextFontSize" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reTextFontSize" runat="server" 
                     ControlToValidate="txtTextFontSize" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label38" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label39" runat="server" Text="Length Header X "></asp:Label>   
             </td>
             <td class="style17">
                 <asp:Label ID="lbLengthHeaderX" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtLengthHeaderX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfLengthHeaderX" runat="server" 
                     ControlToValidate="txtLengthHeaderX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reLengthHeaderX" runat="server" 
                     ControlToValidate="txtLengthHeaderX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label33" runat="server" ForeColor="Red" 
                    Text="* "></asp:Label>
                 <asp:Label ID="Label36" runat="server" 
                    Text="Barcode Font"></asp:Label>     
             </td>
             <td class="style11">
                <asp:Label ID="lbBarcodeFont" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19" >
                 <asp:TextBox ID="txtBarcodeFont" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfBarcodeFont" runat="server" 
                     ControlToValidate="txtBarcodeFont" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label45" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label87" runat="server" Text="Length Header Y "></asp:Label>  
             </td>
             <td class="style17">
                <asp:Label ID="lbLengthHeaderY" runat="server" Text=" : "></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtLengthHeaderY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfLengthHeaderY" runat="server" 
                     ControlToValidate="txtLengthHeaderY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reLengthHeaderY" runat="server" 
                     ControlToValidate="txtLengthHeaderY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label32" runat="server" ForeColor="Red" 
                    Text="* "></asp:Label>
                 <asp:Label ID="Label42" runat="server" 
                    Text="Barcode Font Size"></asp:Label>  
             </td>
             <td class="style11">
                  <asp:Label ID="lbBarcodeFontSize" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19">
                 <asp:TextBox ID="txtBarcodeFontSize" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfBarcodeFontSize" runat="server" 
                     ControlToValidate="txtBarcodeFontSize" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reBarcodeFontSize" runat="server" 
                     ControlToValidate="txtBarcodeFontSize" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label47" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label48" runat="server" Text="Unit Weight X "></asp:Label>
             </td>
             <td class="style17">
                   <asp:Label ID="lbUnitWeightX" runat="server" Text=" : "></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtUnitWeightX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfUnitWeightX" runat="server" 
                     ControlToValidate="txtUnitWeightX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reUnitWeightX" runat="server" 
                     ControlToValidate="txtUnitWeightX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18" >
                 <asp:Label ID="Label40" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label43" runat="server" 
                Text="Pack Code X"></asp:Label> 
             </td>
             <td class="style11">
                 <asp:Label ID="lbPackCodeX" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19" >
                 <asp:TextBox ID="txtPackCodeX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfPackCodeX" runat="server" 
                     ControlToValidate="txtPackCodeX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rePackCodeX" runat="server" 
                     ControlToValidate="txtPackCodeX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label89" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label90" runat="server" Text="Unit Weight Y "></asp:Label>  
             </td>
             <td class="style17">
                 <asp:Label ID="lbUnitWeightY" runat="server" Text=" : "></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtUnitWeightY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfUnitWeightY" runat="server" 
                     ControlToValidate="txtUnitWeightY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reUnitWeightY" runat="server" 
                     ControlToValidate="txtUnitWeightY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18" >
                 <asp:Label ID="Label44" runat="server" ForeColor="Red" 
                Text="*"></asp:Label>
                 <asp:Label ID="Label49" runat="server" 
                Text="Pack Code Y"></asp:Label> 
             </td>
             <td class="style11">
                  <asp:Label ID="lbPackCodeY" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19" >
                 <asp:TextBox ID="txtPackCodeY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfPackCodeY" runat="server" 
                     ControlToValidate="txtPackCodeY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rePackCodeY" runat="server" 
                     ControlToValidate="txtPackCodeY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label93" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label94" runat="server" Text="Slit Lot No X "></asp:Label>   
             </td>
             <td class="style17">
                  <asp:Label ID="lbSlitLotNoX" runat="server" Text=" : "></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtSlitLotNoX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfSlitLotNoX" runat="server" 
                     ControlToValidate="txtSlitLotNoX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reSlitLotNoX" runat="server" 
                     ControlToValidate="txtSlitLotNoX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label88" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label91" runat="server" 
                Text="Num Per Pack X"></asp:Label>    
             </td>
             <td class="style11">
                 <asp:Label ID="lbNumPerPackX" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19">
                 <asp:TextBox ID="txtNumPerPackX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfNumPerPackX" runat="server" 
                     ControlToValidate="txtNumPerPackX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reNumPerPackX" runat="server" 
                     ControlToValidate="txtNumPerPackX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label97" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label98" runat="server" Text="Slit Lot No Y "></asp:Label>  
             </td>
             <td class="style17">
                  <asp:Label ID="lbSlitLotNoY" runat="server" Text=" : "></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtSlitLotNoY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfSlitLotNoY" runat="server" 
                     ControlToValidate="txtSlitLotNoY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reSlitLotNoY" runat="server" 
                     ControlToValidate="txtSlitLotNoY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label92" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label95" runat="server" 
                Text="Num Per Pack Y"></asp:Label>
             </td>
             <td class="style11">
                   <asp:Label ID="lbNumPerPackY" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19" >
                 <asp:TextBox ID="txtNumPerPackY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfNumPerPackY" runat="server" 
                     ControlToValidate="txtNumPerPackY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reNumPerPackY" runat="server" 
                     ControlToValidate="txtNumPerPackY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label101" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label102" runat="server" Text="Grade X "></asp:Label>   
             </td>
             <td class="style17">
                  <asp:Label ID="lbGradeX" runat="server" Text=" : "></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txtGradeX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rftGradeX" runat="server" 
                     ControlToValidate="txtGradeX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reGradeX" runat="server" 
                     ControlToValidate="txtGradeX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label96" runat="server" ForeColor="Red" 
                     Text="* "></asp:Label>
                 <asp:Label ID="Label99" runat="server" Text="PC1 X"></asp:Label>
             </td>
             <td class="style11">
                 <asp:Label ID="lbPC1X" runat="server" Text=" : "></asp:Label></td>
             <td class="style19" >
                 <asp:TextBox ID="txtPC1X" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfPC1X" runat="server" 
                     ControlToValidate="txtPC1X" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rePC1X" runat="server" 
                     ControlToValidate="txtPC1X" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label105" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label106" runat="server" Text="Grade Y "></asp:Label>  
             </td>
             <td class="style17">
                 <asp:Label ID="lbGradeY" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtGradeY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfGradeY" runat="server" 
                     ControlToValidate="txtGradeY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reGradeY" runat="server" 
                     ControlToValidate="txtGradeY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label100" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label103" runat="server" Text="PC1 Y"></asp:Label> 
             </td>
             <td class="style11">
                 <asp:Label ID="lbPC1Y" runat="server" Text=" : "></asp:Label></td>
             <td class="style19" >
                 <asp:TextBox ID="txtPC1Y" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfPC1Y" runat="server" 
                     ControlToValidate="txtPC1Y" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rePC1Y" runat="server" 
                     ControlToValidate="txtPC1Y" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label109" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label110" runat="server" Text="Core Code X "></asp:Label> 
             </td>
             <td class="style17">
                 <asp:Label ID="lbCoreCodeX" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtCoreCodeX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfCoreCodeX" runat="server" 
                     ControlToValidate="txtCoreCodeX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reCoreCodeX" runat="server" 
                     ControlToValidate="txtCoreCodeX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label104" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label107" runat="server" Text="Length X"></asp:Label> 
             </td>
             <td class="style11">
                 <asp:Label ID="lbLengthX" runat="server" Text=" : "></asp:Label></td>
             <td class="style19" >
                 <asp:TextBox ID="txtLengthX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfLengthX" runat="server" 
                     ControlToValidate="txtLengthX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reLengthX" runat="server" 
                     ControlToValidate="txtLengthX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label113" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label114" runat="server" Text="Core Code Y"></asp:Label>
             </td>
             <td class="style17">
                  <asp:Label ID="lbCoreCodeY" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtCoreCodeY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfCoreCodeY" runat="server" 
                     ControlToValidate="txtCoreCodeY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reCoreCodeY" runat="server" 
                     ControlToValidate="txtCoreCodeY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label108" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label111" runat="server" Text="Length Y"></asp:Label>
             </td>
             <td class="style11">
                   <asp:Label ID="lbLengthY" runat="server" Text=" : "></asp:Label></td>
             <td class="style19">
                 <asp:TextBox ID="txtLengthY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfLengthY" runat="server" 
                     ControlToValidate="txtLengthY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reLengthY" runat="server" 
                     ControlToValidate="txtLengthY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label117" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label118" runat="server" Text="Barcode X "></asp:Label>
             </td>
             <td class="style17">
                  <asp:Label ID="lbBarcodeX" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtBarcodeX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfBarcodeX" runat="server" 
                     ControlToValidate="txtBarcodeX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reBarcodeX" runat="server" 
                     ControlToValidate="txtBarcodeX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label112" runat="server" ForeColor="Red" 
                Text="* "></asp:Label>
                 <asp:Label ID="Label115" runat="server" 
                Text="Thickness X"></asp:Label>
             </td>
             <td class="style11">
                   <asp:Label ID="lbThicknessX" runat="server" Text=" : "></asp:Label></td>
             <td class="style19">
                 <asp:TextBox ID="txtThicknessX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfThicknessX" runat="server" 
                     ControlToValidate="txtThicknessX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reThicknessX" runat="server" 
                     ControlToValidate="txtThicknessX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label121" runat="server" ForeColor="Red" Text="* "></asp:Label>
                 <asp:Label ID="Label122" runat="server" Text="Barcode Y "></asp:Label>  
             </td>
             <td class="style17">
                <asp:Label ID="lbBarcodeY" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtBarcodeY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfBarcodeY" runat="server" 
                     ControlToValidate="txtBarcodeY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reBarcodeY" runat="server" 
                     ControlToValidate="txtBarcodeY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label116" runat="server" ForeColor="Red" 
                    Text="* "></asp:Label>
                 <asp:Label ID="Label119" runat="server" 
                    Text="Thickness Y"></asp:Label>
             </td>
             <td class="style11">
                 <asp:Label ID="lbThicknessY" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19">
                 <asp:TextBox ID="txtThicknessY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfThicknessY" runat="server" 
                     ControlToValidate="txtThicknessY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reThicknessY" runat="server" 
                     ControlToValidate="txtThicknessY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
         <tr>
             <td class="style18">
                 <asp:Label ID="Label120" runat="server" ForeColor="Red" 
                    Text="* "></asp:Label>
                 <asp:Label ID="Label123" runat="server" Text="Type X"></asp:Label>  
             </td>
             <td class="style11">
                 <asp:Label ID="lbTypeX" runat="server" Text=" : "></asp:Label>
             </td>
             <td class="style19">
                 <asp:TextBox ID="txtTypeX" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfTypeX" runat="server" 
                     ControlToValidate="txtTypeX" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reTypeX" runat="server" 
                     ControlToValidate="txtTypeX" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
             <td class="style20">
                 <asp:Label ID="Label124" runat="server" style="margin-left: 10px" 
                     Text="Default Printer"></asp:Label>
             </td>
             <td class="style17">
                  <asp:Label ID="Label127" runat="server" Text=" : "></asp:Label></td>
             <td>
                 <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" 
                     Text="Yes" OnCheckedChanged="RadioButton1_CheckedChanged" />
                 <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="true" 
                     Text="No" OnCheckedChanged="RadioButton2_CheckedChanged" />
             </td>
         </tr>
         <tr>
             <td class="style18" >
                 <asp:Label ID="Label125" runat="server" ForeColor="Red" 
                    Text="* "></asp:Label>
                 <asp:Label ID="Label126" runat="server" Text="Type Y "></asp:Label>   
             </td>
             <td class="style11">
                 <asp:Label ID="lbTypeY" runat="server" Text=" : "></asp:Label></td>
             <td class="style19">
                 <asp:TextBox ID="txtTypeY" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfTypeY" runat="server" 
                     ControlToValidate="txtTypeY" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="reTypeY" runat="server" 
                     ControlToValidate="txtTypeY" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
             </td>
         </tr>
     </table>
    
    </asp:Panel>
    
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"/>
    
</asp:Content>