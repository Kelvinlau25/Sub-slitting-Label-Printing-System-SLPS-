<%@ Page Language="C#" MasterPageFile="~/master/Main.master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="SSR_SEARCH_Dtl.aspx.cs" Inherits="Transactions_SSR_SEARCH_Dtl" title="SSR_SEARCH_Dtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css" >
            html,body {
	            margin:0;
	            padding:0;
	            height:100%;
	             }
            #ChangedPassword
            {
    	        font-size:11px;
            }
            .style2
            {
                width: 130px;
            }
            .style3
            {
                width: 10px;
            }
            .style4
            {
                width: 280px;
            }
            .style10
            {
                width: 8%;
            }
            .style11
            {
                width: 6%;
            }
    </style>
    <script type="text/javascript" language="javascript">
    
            $(function () {
               $(".txtDate").datepicker({
                  showOn: 'button',
                  buttonImageOnly: true,
                  buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                  numberOfMonths: 1,
                  dateFormat: 'dd/MM/yyyy'
                });
                $(".txtETD").datepicker({
                  showOn: 'button',
                  buttonImageOnly: true,
                  buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                  numberOfMonths: 1,
                  dateFormat: 'dd/MM/yyyy',
                  onSelect: function(selected) {
                  $(".txtETA").datepicker("option","minDate", selected)
                }
                });
                $(".txtETA").datepicker({
                  showOn: 'button',
                  buttonImageOnly: true,
                  buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                  numberOfMonths: 1,
                  minDate: 0,
                  dateFormat: 'dd/MM/yyyy',
                onSelect: function(selected) {
                   $(".txtETD").datepicker("option", selected)
                }
                });
            });
            
            function checkRadioBtn(id) {
                var gv = document.getElementById('<%=grdList.ClientID %>');

                for (var i = 1; i < gv.rows.length; i++) {
                    var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                    // Check if the id not same
                    if (radioBtn[0].id != id.id) {
                        radioBtn[0].checked = false;
                    }
                }
            };
            
            function pop_up_confirm(){

                if(confirm("Are you sure you want to create a new revision?")){
                
                    window.location.href = "SUBSLIT_REQ_.aspx";
                     }
            
               }; 
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td><h3>Sub-Slitting Request -
            <asp:Label ID="lbltittle" runat="server" ></asp:Label>
            </h3></td>
    </tr>
    </table>
    
    <br/>
    
    <table width="100%" style="height:100%;" border="0">
    <tr>
        <td class="style2"><asp:Label ID="Label1" runat="server" Text="To"></asp:Label></td>
        <td class="style3"><asp:Label ID="Label2" runat="server" Text=":"></asp:Label></td>
        <td class="style4"><asp:Label ID="lblCompCode" runat="server"></asp:Label></td>
        <td class="style2"><asp:Label ID="Label3" runat="server" Text="Ref No"></asp:Label></td>
        <td class="style3"><asp:Label ID="Label4" runat="server" Text=":"></asp:Label></td>
        <td class="style4"><asp:Label ID="lblRefNo" runat="server" ></asp:Label></td>
    </tr>
    <tr>
        <td><asp:Label ID="Label5" runat="server" Text="Department"></asp:Label></td>
        <td><asp:Label ID="Label6" runat="server" Text=":"></asp:Label></td>
        <td><asp:Label ID="lblDept" runat="server"></asp:Label></td>
        <td><asp:Label ID="Label7" runat="server" Text="Date"></asp:Label></td>
        <td><asp:Label ID="Label8" runat="server" Text=":"></asp:Label></td>
        <td><asp:Label ID="lblDate" runat="server" ></asp:Label></td>
    </tr>
    <tr>
        <td><asp:Label ID="Label9" runat="server" Text="By"></asp:Label></td>
        <td><asp:Label ID="Label10" runat="server" Text=":"></asp:Label></td>
        <td><asp:Label ID="lblBy" runat="server"></asp:Label></td>
        <td><asp:Label ID="Label11" runat="server" Text="Rev"></asp:Label></td>
        <td><asp:Label ID="Label12" runat="server" Text=":"></asp:Label></td>
        <td><asp:Label ID="lblRev" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td><asp:Label ID="Label13" runat="server" Text="Requestor Status"></asp:Label></td>
        <td><asp:Label ID="Label14" runat="server" Text=":"></asp:Label></td>
        <td><asp:DropDownList ID="ddlReqStat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReqStat_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label ID="lblrequest" runat="server"></asp:Label></td>
        <td><asp:Label ID="Label15" runat="server" Text="Vendor Status"></asp:Label></td>
        <td><asp:Label ID="Label16" runat="server" Text=":"></asp:Label></td>
        <td><asp:DropDownList ID="ddlVenStat" runat="server"></asp:DropDownList>
            <asp:Label ID="lblvendor" runat="server"></asp:Label></td>
    </tr>
   
    </table>
    
    <br/>
    
    <table width="100%">
    <tr><td style="background-color:#91B3DD" >&nbsp;</td></tr>
      </table>
      
         <br/>    
         
    <asp:Panel ID="pnlList" runat="server">

        <asp:GridView ID="grdList" DataKeyNames="REFNO" EnableViewState="false" 
            HeaderStyle-CssClass="title_bar" Width="100%" OnRowDataBound="grdList_OnRowDataBound" ShowFooter="True"
        runat="server" AutoGenerateColumns="False" AllowSorting="True" 
            AllowPaging="False" style="margin-bottom: 0px">
                     
            <Columns>
            
                <asp:BoundField HeaderText="PRODUCTION LINE" DataField="PRODLINE_NO">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC1 MOTHER" DataField="PC1_MOTHER">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC2 MOTHER" DataField="PC2_MOTHER">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QTY (ROLL)" DataField="QTY">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UNIT WEIGHT (KG)" DataField="M_WEIGHT">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TOTAL WEIGHT (KG)" DataField="M_TOTAL_WEIGHT">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC1 CUSTOMER" DataField="PC1_CUST">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC2 CUSTOMER" DataField="PC2_CUST">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QTY (ROLL)" DataField="C_QTY">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UNIT WEIGHT (KG)" DataField="C_WEIGHT">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TOTAL WEIGHT (KG)" DataField="C_TOTAL_WEIGHT">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="SUB-SLIT WASTE (KG)" DataField="SUBSLIT_WASTE">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ETD PFR" DataField="ETD">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ETA SUBSLIT CONTRACTOR" DataField="ETA">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="REMARK" DataField="REMARK">
                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:BoundField>
   
            </Columns>
            
            <EmptyDataRowStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
            <EmptyDataTemplate>Record Not Found</EmptyDataTemplate>
            <PagerStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="title_bar"></HeaderStyle>
        </asp:GridView>
        <br />
        <table id="SSRTotal" width="100%" style="height:100%;" border="0" runat="server" >
                <tr>
                <td width="2%"></td>
                <td width="5%"></td>
                <td width="5%"></td>
                <td width="5%">Total</td>
                <td width="6.5%" align="center"><asp:Label ID="lblMQty"  runat="server" Text="0"></asp:Label></td>
                <td width="5%"></td>
                <td align="center" class="style11"><asp:Label ID="lblMTotalWeight"  runat="server" Text="0"></asp:Label></td>
                <td class="style10"></td>
                <td width="7.5%"></td>
                <td width="3%" align="left"><asp:Label ID="lblCQty"  runat="server" Text="0"></asp:Label></td>
                <td width="5%"></td>
                <td width="5%" align="center"><asp:Label ID="lblCTotalWeight"  runat="server" Text="0"></asp:Label></td>
                <td width="6%" align="right"><asp:Label ID="lblSubSlitWaste"  runat="server" Text="0"></asp:Label></td>
                <td width="5.5%"></td>
                <td width="7.5%"></td>
                <td width="7.5%"></td>
                </tr>
     </table>
     <br />
     <br />
    </asp:Panel>
    
    <table id="viewonly" width="90%" style="height:100%;" border="0" >
            <tr>
                <td width="10%">&nbsp;</td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%" align="center">
                     <asp:Button ID="UpdStat_Button" runat="server" Text="Update Status" Width="99px" OnClick="UpdStat_Button_Click" /></td>
                <td width="10%" align="center">
                    <asp:Button ID="NewRev_Button" runat="server" Text="New Revision" Width="99px" 
                        OnClientClick="return confirm('Are you sure you want to create a new revision?');"
                        OnClick="NewRev_Button_Click" /></td>
                <td width="10%" align="center">
                    <asp:Button ID="Export_Button" runat="server" Text="Export" Width="99px" OnClick="Export_Button_Click" /></td>
                <td width="10%" align="center"> 
                    <asp:Button ID="Cancel_Button" runat="server" Text="Cancel" Width="99px" OnClick="Cancel_Button_Click" /></td>
            </tr>
        </table>

<asp:Panel ID="pninfo" runat="server">
<div class="createdpanel">
    <table ID="pninfo1" class="loginfo">
        <tr>
            <td class="label">Created By</td>
            <td class="value">
                <asp:Label ID="lblcreatedby" runat="server" Text="[Created By]"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">Created Date</td>
            <td class="value"><asp:Label ID="lblcreateddate" runat="server" 
                    Text="[Created Date]"></asp:Label></td>
        </tr>
        <tr>
            <td class="label">Updated By</td>
            <td class="value"><asp:Label ID="lblupdatedby" runat="server" Text="[Updated By]"></asp:Label></td>
        </tr>
        <tr>
            <td class="label">Updated Date</td>
            <td class="value"><asp:Label ID="lblupdateddate" runat="server" 
                    Text="[Updated Date]"></asp:Label></td>
        </tr>
    </table>
</div>
</asp:Panel>

<asp:Panel ID="pnconfirmation" runat="server">
    <table ID="pnconfirmation1" class="deleteInfo" width="100%">
        <tr>
            <td class="label">Delete the record ?</td>
            <td class="value">
                <asp:RadioButton ID="rbyes" Text="Yes" GroupName="confirm" runat="server" />
                <asp:RadioButton ID="rbno" Text="No" GroupName="confirm" Checked="True" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
    </table>
</asp:Panel>

<div class="clear"></div>
<div class="ControllerAdditionalBtn" style="float:left;"></div>
<div style="float:left;">
<asp:Button ID="btnSubmit" CssClass="control" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
</div>
       
</asp:Content>