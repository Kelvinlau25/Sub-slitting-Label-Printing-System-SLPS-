<%@ Page Language="C#" MasterPageFile="~/master/Main.master" enableEventValidation="true" AutoEventWireup="true" CodeFile="MM_USER_Dtl.aspx.cs" Inherits="MasterMaint_MM_USER_Dtl" title="MM_USER_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

        <style type="text/css">
            .detailinfo
            {
                width: 685px;
            }
            .style3
            {
                width: 180px;
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
                    
                  
            $(document).on('click', ':not(form)[data-confirm]', function(e){
                if(!confirm($(this).data('confirm'))){
                    e.stopImmediatePropagation();
                    e.preventDefault();
                }
            });
            
            function ValidateStringLength(source, arguments)
               {
                    var slen = arguments.Value.length;
                    // alert(arguments.Value + '\n' + slen);
                    if (slen < 8){
                        arguments.IsValid = false;
                    } else {
                        arguments.IsValid = true;
                    }
               }
        </script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Error ID="UCError" runat="server" ValidationGroup="Group1"/>
    
    <asp:Panel ID="Cdisplay" runat="server">
    <table class="detailinfo">
         <tr>
            <td class="style3"><asp:Label ID="Label11" runat="server" Text="Company Name "></asp:Label></td>
            <td><asp:Label ID="Label12" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblCompName" runat="server" Text=""></asp:Label></td>   
        </tr>
        <tr>
            <td class="style3"><asp:Label ID="Label1" runat="server" Text="Name "></asp:Label></td>
            <td><asp:Label ID="Label2" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>   
        </tr>
        <tr>
            <td class="style3"><asp:Label ID="Label3" runat="server" Text="UserID "></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblUserID" runat="server" Text=""></asp:Label></td> 
        </tr>
        <tr>
            <td class="style3"><asp:Label ID="Label5" runat="server" Text="Department "></asp:Label></td>
            <td><asp:Label ID="Label6" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label></td> 
        </tr>
        <tr>
            <td class="style3"><asp:Label ID="Label7" runat="server" Text="Email Address "></asp:Label></td>
            <td><asp:Label ID="Label8" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td> 
        </tr>
        <tr>
            <td class="style3"><asp:Label ID="Label9" runat="server" Text="Level "></asp:Label></td>
            <td><asp:Label ID="Label10" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblLevel" runat="server" Text=""/></td> 
        </tr>
        <tr>
            <td class="style3"><asp:Label ID="Label35" runat="server" Text="Account Status "></asp:Label></td>
            <td><asp:Label ID="Label36" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblaccstats" runat="server" Text=""/></td>
        </tr>       
    </table>
    </asp:Panel>
    
 <asp:Panel ID="Cmodify" runat="server">
    <table class="detailinfo">
        
         <tr>
            <td><asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td style="width:180px;"><asp:Label ID="Label14" runat="server" Text="Company Name "></asp:Label></td>
            <td><asp:Label ID="Label33" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbCompName" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlCompName" runat="server" Width="205" AutoPostBack="true" OnSelectedIndexChanged="ddlCompName_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfCompName" runat="server" ControlToValidate="ddlCompName" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>  
         </tr>
         <tr>
            <td><asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label16" runat="server" Text="Name "></asp:Label></td>
            <td><asp:Label ID="Label17" runat="server" Text=" : "></asp:Label></td>
            <td><asp:TextBox ID="txtName" runat="server" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfName" runat="server" ControlToValidate="txtName" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>  
         </tr>
         <tr>
            <td><asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label19" runat="server" Text="User ID "></asp:Label></td>
            <td><asp:Label ID="Label20" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lbUserID" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtUserID" runat="server" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfUserID" runat="server" ControlToValidate="txtUserID" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
         </tr>
         <tr>
            <td><asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label22" runat="server" Text="Department "></asp:Label></td>
            <td><asp:Label ID="Label23" runat="server" Text=" : "></asp:Label></td>
            <td><asp:TextBox ID="txtDepartment" runat="server" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfDepartment" runat="server" ControlToValidate="txtDepartment" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>   
         </tr>
         <tr>
            <td><asp:Label ID="Label24" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label25" runat="server" Text="Email Address "></asp:Label></td>
            <td><asp:Label ID="Label26" runat="server" Text=" : "></asp:Label></td>
            <td><asp:TextBox ID="txtEmail" runat="server" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfEmail2" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Group1" Display="None"></asp:RegularExpressionValidator></td>   
         </tr>
         <tr>
            <td></td>
            <td></td>
            <td></td>
            <td><asp:RadioButton ID="RBLevel1" runat="server" Text="System Administrator" AutoPostBack="true" OnCheckedChanged="RBLevel1_CheckedChanged" />
                <asp:TextBox ID="TextBox1" runat="server" style="display:none"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfRB" runat="server" ControlToValidate="TextBox1" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>
         </tr>
         <tr>
            <td><asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label28" runat="server" Text="Level "></asp:Label></td>
            <td><asp:Label ID="Label29" runat="server" Text=" : "></asp:Label></td>
            <td><asp:RadioButton ID="RBLevel2" runat="server" Text="User" AutoPostBack="true" OnCheckedChanged="RBLevel2_CheckedChanged"/></td>  
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td><asp:RadioButton ID="RBLevel3" runat="server" Text="Vendor" AutoPostBack="true" OnCheckedChanged="RBLevel3_CheckedChanged" /></td> 
        </tr>             
        <tr>
            <td><asp:Label ID="Label34" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td><asp:Label ID="Label37" runat="server" Text="Account Status "></asp:Label></td>
            <td><asp:Label ID="Label38" runat="server" Text=" : "></asp:Label></td>
            <td><asp:Label ID="lblAccStats2" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlAccStats" runat="server" Width="205" AutoPostBack="false">
                    <asp:ListItem Enabled="true" Text="Normal" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Locked" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFddlaccstats" runat="server" ControlToValidate="ddlAccStats" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator></td>  
        </tr>
        <tr id="tblpassword" runat="server" class="detailinfo">
            <td>
                <asp:Label ID="Label30" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td style="width:183px;"><asp:Label ID="Label31" runat="server" Text="Password "></asp:Label></td>
            <td><asp:Label ID="Label32" runat="server" Text=" : "></asp:Label></td>
            <td>
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200px" MaxLength="15" style="margin-left: 4px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rePassword" runat="server" ControlToValidate="txtPassword" ValidationGroup="Group1" Display="None"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfPassword" runat="server" ControlToValidate="txtPassword" ValidationGroup="Group1" Display="None"></asp:RequiredFieldValidator>
            </td>  
        </tr>
    </table>
    </asp:Panel>
    
    <br />
    
    <table>
    <tr>
        <td>
        <asp:LinkButton ID="resetlnk" runat="server" data-confirm="Are you sure you want to reset the password?" OnClick="resetlnk_Click">Reset Password</asp:LinkButton>
        </td>
    </tr>
    </table>    
    
    <table>
    <tr>
        <td>
         <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1" runat="server" AuditTrailDisplayType="FUll"
             OnDisplayMode="UCAction_DisplayMode"
             OnModifyMode="UCAction_ModifyMode"
             OnAddAction="UCAction_AddAction"
             OnEditAction="UCAction_AddAction"
             OnAddResetAction="UCAction_AddResetAction"
             OnEditResetAction="UCAction_AddResetAction"
             OnDeleteAction="UCAction_DeleteAction"/>
        </td>
    </tr>
    </table>
   
</asp:Content>