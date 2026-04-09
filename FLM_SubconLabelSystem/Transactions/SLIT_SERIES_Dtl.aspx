<%@ Page Language="C#" MasterPageFile="~/master/Main.master" EnableEventValidation="true"
    AutoEventWireup="true" CodeFile="SLIT_SERIES_Dtl.aspx.cs" Inherits="Transaction_SlitSeries_Dtl"
    Title="SlitSeries_Dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

  <link href="http://code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css"/>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <style type="text/css">
            .detailinfo
            {
                margin-right: 128px;
                width: 1084px;
                height: 19px;
            }
            .style16
            {
                width: 235px;
                height: 23px;
            }
            .style17
            {
                height: 23px;
            }
            .style18
            {
                height: 23px;
                width: 3px;
            }
            .style19
            {
                width: 3px;
            }
            .style20
            {
                height: 23px;
                width: 134px;
            }
            .style21
            {
                width: 134px;
            }
            .style22
            {
                height: 23px;
                width: 10px;
            }
            .style23
            {
                width: 10px;
            }
            .style24
            {
                height: 23px;
                width: 158px;
            }
            .style25
            {
                width: 158px;
            }
            .style26
            {
                width: 158px;
                height: 34px;
            }
            .style27
            {
                width: 10px;
                height: 34px;
            }
            .style28
            {
                height: 34px;
            }
            
            .sameline
             {
                 margin-left: 10px;
             }
            .style29
            {
                height: 25px;
            }
    </style>

    <script type="text/javascript" language="javascript">
            $(function () {
               $(".txtDate").datepicker({
                  showOn: 'button',
                    onSelect: function(date) {
             
             },
                  buttonImageOnly: true,
                  buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                  numberOfMonths: 1,
                  dateFormat: 'yymm'
                });
          
            });
           
    </script>
    
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
        
        function popupwindow(pc2mother,lblpc2mother) {
        
            var refno = document.getElementById('<%=ddlRefNo.ClientID%>').value
            var _str_ProdLine = document.getElementById('<%=ddlProdLine.ClientID%>').value
            var _str_PC1Mother = document.getElementById('<%=ddlPC1Mother.ClientID%>').value  
            var _str_buttonName = '<%=Button2.ClientID%>'       
            var _str_hdn_PC2_Mother = '<%=hdn_PC2_Mother.ClientID%>'    
            var _str_hdn_Unit_Weight_Mother = '<%=hdn_Unit_Weight_Mother.ClientID%>'  
            
            document.getElementById('<%=ddlPC1Customer.ClientID%>').selectedIndex = 0;
            document.getElementById('<%=txtPC2Customer.ClientID%>').value = "";
            document.getElementById('<%=txtUnitWeightCustomer.ClientID%>').value = "";
            document.getElementById('<%=txtLotNo.ClientID%>').value = "";
            document.getElementById('<%=hdn_PC2_Customer.ClientID%>').value = "";
            document.getElementById('<%=hdn_UnitWeightCustomer.ClientID%>').value = "";
            
            window.open('<%= ResolveUrl("../PopUp/PP_PC2.aspx") %>?itm1=' + pc2mother + '&itm2=' + lblpc2mother + '&itm3=' + refno + '&itm4=' + _str_ProdLine + '&itm5=' + _str_PC1Mother + '&itm6=' + _str_buttonName + '&itm7=' + _str_hdn_PC2_Mother + '&itm8=' + _str_hdn_Unit_Weight_Mother, 'PopUp', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=800,height=600');
        }
        
        function popupwindow1(pc2cust,lblpc2cust) { 
        
            var refno = document.getElementById('<%=ddlRefNo.ClientID%>').value 
            var _str_ProdLine = document.getElementById('<%=ddlProdLine.ClientID%>').value
            var _str_PC1Mother = document.getElementById('<%=ddlPC1Mother.ClientID%>').value
            var _str_PC2Mother = document.getElementById('<%=txtPC2Mother.ClientID%>').value
            var _str_PC1Customer = document.getElementById('<%=ddlPC1Customer.ClientID%>').value  
            var _str_buttonName = '<%=Button2.ClientID%>' 
            var _str_hdn_PC2_Customer = '<%=hdn_PC2_Customer.ClientID%>'  
            var _str_hdn_UnitWeightCustomer = '<%=hdn_UnitWeightCustomer.ClientID%>'  
            
            window.open('<%= ResolveUrl("../PopUp/PP_PC2_Cust.aspx") %>?itm1=' + pc2cust + '&itm2=' + lblpc2cust + '&itm3=' + refno + '&itm4=' + _str_ProdLine + '&itm5=' + _str_PC1Mother + '&itm6=' + _str_PC2Mother + '&itm7=' + _str_PC1Customer  + '&itm8=' + _str_buttonName + '&itm9=' + _str_hdn_PC2_Customer + '&itm10=' + _str_hdn_UnitWeightCustomer, 'PopUp', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=800,height=600');
        }

            function showDialogue(){
                 var control = '<%=inpHide.ClientID%>'; 
                 document.getElementById(control).value = "1";  
                 document.getElementById('<%=Button1.ClientID%>').click(); 
            }
        
            function disableEnterKey(e){
                 if (e.keyCode == 13) 
                 {
                    document.getElementById('<%=txtNoOfSlit.ClientID%>').focus();
                 }
                 var key;
                 if (window.event){ key = window.event.keyCode;}
                 else {key = e.which};

                 return (key != 13);
            }
                       
            function rbValidation(oSrouce, args){
                var matrixRB = document.getElementById('<%= rdMatrix.ClientID %>');
                var txtPosition = document.getElementById('<%= rdPos.ClientID %>');
                
                if(matrixRB.checked){
                    if(txtPosition.value=="" || txtPosition.value==null)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
            }
            
           function rbValidation2(oSrouce, args){
                var matrixRB = document.getElementById('<%= rdMatrix.ClientID %>');
                var txtPosition = document.getElementById('<%= rdPos.ClientID %>');
                
                if(matrixRB.checked){
                    if(txtPosition.value == 0)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
            }
            
          function rbValidation3(oSrouce, args){
              var matrixRB = document.getElementById('<%= rdMatrix.ClientID %>'); 
              var seqRB = document.getElementById('<%= rdSeq.ClientID %>');
              var oddRB = document.getElementById('<%= rdOdd.ClientID %>');
              var evenRB = document.getElementById('<%= rdEven.ClientID %>');
          
              if(matrixRB.checked || seqRB.checked || oddRB.checked || evenRB.checked){
                  args.IsValid = true;
              }else{
                  args.IsValid = false;
              } 
          } 
            
          function isNumberKey(evt)
          {
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;    
             return true;
          }
           
        </script>
        
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Error ID="UCError" runat="server" ValidationGroup="Group1" />
    <asp:Panel ID="Cdisplay" runat="server">
        <table class="detailinfo">
        <tr>
                <td class="sameline" style="width:24%;">
                    Company Code
                </td>
                <td class="style18">
                    <asp:Label ID="Label7" runat="server" Text=":"></asp:Label>
                </td>
                <td class="style17">
                    <asp:Label ID="lblCompCode" runat="server" Text=""></asp:Label>
                </td>
        </tr>
       
        <tr>
            <td class="style20">Ref.No:</td>
            <td class="style18">
            <asp:Label ID="Label20" runat="server" Text=":"></asp:Label>
            </td>
            <td><asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></td> 
        </tr>
        
        <tr>
                <td class="style21">
                    Planning Year Month
                </td>
                <td class="style19">
                    <asp:Label ID="Label8" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPlanYrMth" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21">
                    Production Line
                </td>
                <td class="style19">
                    <asp:Label ID="Label9" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProdLine" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21">
                    PC1 Mother
                </td>
                <td class="style19">
                    <asp:Label ID="Label10" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPC1Mother" runat="server" Text=""></asp:Label>
                </td>
        </tr>  
        <tr>
                <td class="style21">
                    PC2 Mother
                </td>
                <td class="style19">
                    <asp:Label ID="Label11" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPC2Mother" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style20">
                    Unit Weight Mother
                </td>
                <td class="style18">
                    <asp:Label ID="Label12" runat="server" Text=":"></asp:Label>
                </td>
                <td class="style16">
                    <asp:Label ID="lblUnitWeightMthr" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21">
                    Lot No
                </td>
                <td class="style19">
                    <asp:Label ID="Label16" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblLotNo" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        <table style="background:#ddeeff;padding:20px 20px 20px 0;width:100%;">
        <tr>
                <td class="style20" style="width:255px;">
                    PC1 Customer
                </td>
                <td class="style18" style="width:18px;">
                    <asp:Label ID="Label13" runat="server" Text=":"></asp:Label>
                </td>
                <td class="style17">
                    <asp:Label ID="lblPC1Customer" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21" style="width:255px;">
                    PC2 Customer
                </td>
                <td class="style19" style="width:18px;">
                    <asp:Label ID="Label14" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPC2Customer" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21" style="width:255px;">
                    Unit Weight Customer
                </td>
                <td class="style19" style="width:18px;">
                    <asp:Label ID="Label15" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUnitWeightCust" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21" style="width:255px;">
                    Number Of Slit
                </td>
                <td class="style19" style="width:18px;">
                    <asp:Label ID="Label17" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNumOfSlit" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21" style="width:255px;">
                    Type Of Slit
                </td>
                <td class="style19" style="width:18px;">
                    <asp:Label ID="Label18" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTypeOfSlit" runat="server" Text=""></asp:Label>
                </td>
        </tr>
        
        <tr>
                <td class="style21" style="width:255px;">
                    Lot Slit Status
                </td>
                <td class="style19" style="width:18px;">
                    <asp:Label ID="Label19" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblLotSlitStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            </table>
        </table>
    </asp:Panel>
      
    <asp:Panel ID="Cmodify" runat="server">
   
        <asp:Label ID="LabelEdit" runat="server" ForeColor="Red"></asp:Label>
      
        <table style="width: 100%;">
            <tr>
                <td class="style24">
                    &nbsp;&nbsp;Company Code
                </td>
                <td class="style22">
                    <asp:Label ID="Label66" runat="server" Text=":"></asp:Label>
                </td>
                <td class="style17">
                    <asp:TextBox ID="txtCompanyCode" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            
             <tr>
                <td class="style24">
                <asp:Label ID="Label77" runat="server" CssClass="styler" ForeColor="Red" Text="*"/> Ref.No:</td>
                <td class="style22">
                <asp:Label ID="Label60" runat="server" Text=":"></asp:Label>
                </td>
                <td class="style17">
                    <asp:DropDownList ID="ddlRefNo" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlRefNo_Changed" Width="155px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfRefNo" runat="server" 
                        ControlToValidate="ddlRefNo" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>    
            <tr>
                <td class="style25">
                    <asp:Label ID="Label1" runat="server" CssClass="styler" ForeColor="Red" Text="*" /> Planning
                    Year Month:
                </td>
                <td class="style23">
                    <asp:Label ID="Label21" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDate" style="width:75px;" runat="server" CssClass="txtDate"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rftxtyeardate" runat="server" ControlToValidate="txtDate"
                        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style25">
                    <asp:Label ID="Label2" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                    Production Line
                </td>
                <td class="style23">
                    <asp:Label ID="Label22" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProdLine" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlProdLine_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfddlProdLine" runat="server" ControlToValidate="ddlProdLine"
                        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style25">
                    <asp:Label ID="Label3" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                    PC1 Mother
                </td>
                <td class="style23">
                    <asp:Label ID="Label23" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPC1Mother" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPC1Mother_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfddlPC1Mother" runat="server" ControlToValidate="ddlPC1Mother"
                        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style26">
                     <asp:Label ID="Label33" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                     PC2 Mother
                </td>
                <td class="style27">
                    <asp:Label ID="Label24" runat="server" Text=":"></asp:Label>
                </td>
                <td class="style28">
                    <asp:TextBox ID="txtPC2Mother" style="width:20%;" runat="server"  
                        CssClass="pc2mother" ReadOnly="True"></asp:TextBox>    
                    <input id="btnPC2Mother" runat="server" onclick="popupwindow('pc2mother','lblpc2mother', 'txtRefno2');"
                        type="button" value="..." />
                </td>
            </tr>
            <tr>
                <td class="style25">
                     &nbsp;&nbsp;Unit Weight Mother
                </td>
                <td class="style23">
                    <asp:Label ID="Label25" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                 <asp:TextBox ID="txtUnitWeightMother" runat="server" CssClass="lblpc2mother" 
                        ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style25">
                    <asp:Label ID="Label5" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                    Lot No
                </td>
                <td class="style23">
                    <asp:Label ID="Label29" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLotNo" runat="server" onkeypress="return disableEnterKey(event);"></asp:TextBox>
                    <asp:Label ID="lbLotNo" runat="server" Text=""></asp:Label>
                     <asp:RequiredFieldValidator ID="rfLotNo" runat="server" 
                    ControlToValidate="txtLotNo" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <table style="background:#ddeeff;padding:20px 20px 20px 0;width:100%;">
            <tr>
                <td class="style25">
                    <asp:Label ID="Label4" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                    PC1 Customer
                </td>
                <td class="style23">
                    <asp:Label ID="Label26" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPC1Customer" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPC1Customer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfddlPC1Customer" runat="server" ControlToValidate="ddlPC1Customer"
                        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style25">
                     <asp:Label ID="Label35" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                     PC2 Customer
                </td>
                <td class="style23">
                    <asp:Label ID="Label27" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPC2Customer" style="width:20%" runat="server" 
                        CssClass="pc2cust" ReadOnly="True"></asp:TextBox>
                    <input id="btnPC2Customer" runat="server" onclick="popupwindow1('pc2cust','lblpc2cust');"
                        type="button" value="..." />
                </td>
            </tr>
            <tr>
                <td class="style25">
                     &nbsp;&nbsp;Unit Weight Customer
                </td>
                <td class="style23">
                    <asp:Label ID="Label28" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                   <asp:TextBox ID="txtUnitWeightCustomer" runat="server" CssClass="lblpc2cust" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style25">
                    <asp:Label ID="Label6" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                    Number Of Slit
                </td>
                <td class="style23">
                    <asp:Label ID="Label30" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNoOfSlit" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfNoOfSlit" runat="server" ControlToValidate="txtNoOfSlit"
                        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtNoOfSlit" ErrorMessage="Number Of Slit cannot be zero." Operator="NotEqual" Type="Integer" ></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style25">
                     <asp:Label ID="Label34" runat="server" CssClass="styler" ForeColor="Red" Text="*" />
                     Type Of Slit
                </td>
                <td class="style23">
                    <asp:Label ID="Label31" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style29">
                                <asp:RadioButton ID="rdSeq" runat="server" GroupName="rdBox" Text="Sequence Eg.(1,2,3)"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                <asp:RadioButton ID="rdOdd" runat="server" GroupName="rdBox" Text="Odd Eg.(1,3,5)"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                <asp:RadioButton ID="rdEven" runat="server" GroupName="rdBox" Text="Even Eg.(2,4,6)"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                                <asp:RadioButton ID="rdMatrix" runat="server" GroupName="rdBox" Text="Matrix - Position: "/><asp:TextBox ID="rdPos" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"  style="width: 20px" /> Increment: <asp:TextBox ID="rdInc" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"  style="width: 20px" />
                                <asp:CustomValidator ID="rfrdPos" ClientValidationFunction="rbValidation" runat="server" ErrorMessage="Matrix Position Cannot be Empty!" Display="None" ValidationGroup="Group1"></asp:CustomValidator>
                                <asp:CustomValidator ID="rfrdPos2" ClientValidationFunction="rbValidation2" runat="server" ErrorMessage="Matrix Position Cannot be Zero!" Display="None" ValidationGroup="Group1"></asp:CustomValidator>
                                <asp:CustomValidator ID="rfrdcv" ClientValidationFunction="rbValidation3" runat="server" ErrorMessage="Type of Slit Cannot be Empty!" Display="None" ValidationGroup="Group1"></asp:CustomValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td class="style25">
                    <asp:Label ID="lbtxtLotSlitStatus" runat="server" CssClass="styler" Text="&nbsp;&nbsp;Lot Slit Status"></asp:Label>
                </td>
                <td class="style23">
                <asp:Label ID="Label32" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <input id="inpHide" runat="server" type="hidden" />
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Style="display: none;" Text="hide" />
                    <asp:Label ID="lbLotSlitStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
           </table>
        </table>
        <div style="display: none">
            <asp:TextBox ID="txtPC2MotherID" runat="server" CssClass="idpc2mother"></asp:TextBox>
            <asp:TextBox ID="txtPC2CustomerID" runat="server" CssClass="lblpc2cust"></asp:TextBox>
        </div>
        
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" style="display:none;" />
        <asp:HiddenField ID="hdn_PC2_Mother" runat="server" />
        <asp:HiddenField ID="hdn_Unit_Weight_Mother" runat="server" />
        <asp:HiddenField ID="hdn_PC2_Customer" runat="server" />
        <asp:HiddenField ID="hdn_PC1_Customer" runat="server" />
        <asp:HiddenField ID="hdn_UnitWeightCustomer" runat="server" />
        
    </asp:Panel>
    
    <control:Controller ID="UCAction" DateTimeFormat="dd/MM/yyyy hh:mm:ss tt" ValidationGroup="Group1"
        runat="server" AuditTrailDisplayType="FUll" />
       
     <script type="text/javascript">
             var radios = $("input:radio");
             radios.click(function() {
             radios.removeProp("checked");
             radios.removeAttr("checked").prop("checked", false);
             $(this).attr("checked", "checked").prop("checked", true);
             return true;
        });
     </script>   
</asp:Content>