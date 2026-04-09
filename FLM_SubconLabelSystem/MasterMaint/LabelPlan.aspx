<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeBehind="LabelPlan.aspx.cs" Inherits="MasterMaint_LabelPlan" title="Label_Plan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">

        var gvID = '<%= grdResult.ClientID %>';

        $(document).ready(function () {
            $(('#' + gvID + ' tr')).each(function (i, row) {
                if (typeof $(this).find('td').eq(14).text() !== "undefined") {             
                    if ($.trim($(this).find('td').eq(14).text()) == "Deleted" ){
                        $(this).find("input[type=checkbox]").attr("hidden","true");
                    }                                
                }
            });                            
        });
        
        function reload_page(){
 
            window.location.reload();
        
        }
        
         function redirect(){
            
            var re = '<%= Session["ChkAll"] %>';
            var chk = 0;
            
            $('#' + gvID + ' tr input[type="checkbox"]').each(
                 function() { 
                     if (this.checked){
                     chk = 1;
                     }
             });
            
            if (chk=='1'){
            if (re=='1')
            {
                window.open("ExportAll.aspx");
            }
            else
            {
                window.open("Export.aspx");
            }
            }
        }

</script>
    
<style type="text/css">
    
    .style5
    {
        height: 110px;
    }
    
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    
    <div style="display:none">
     <control:Header ID="UCHeader" runat="server" />
    </div>
 
   <table width="100%">
       <tr><td style="color:Red">Note: Click Export button and Save  to D:\subconlabelprinter</td></tr>
       <tr><td style="background-color:#91B3DD" >&nbsp;</td></tr>
       <tr>
           <td> 
                <asp:GridView ID="grdResult" DataKeyNames="SLIT_LOT_NO" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" OnRowCreated="grdResult_RowCreated" OnRowDataBound="grdResult_RowDataBound" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                    
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Ref No" HeaderStyle-HorizontalAlign="Left"
                            SortExpression="REFNO">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("REFNO")%>
                            </ItemTemplate>
                       </asp:TemplateField>
                        
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Company Code" HeaderStyle-HorizontalAlign="Left" SortExpression="COMPANYCODE">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <%#Eval("COMPANYCODE")%>
                            </ItemTemplate>
                       </asp:TemplateField>
                        
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Production Line" HeaderStyle-HorizontalAlign="Left" SortExpression="PRODLINE_NO">
                            <HeaderStyle HorizontalAlign="Left" Width="6%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("PRODLINE_NO")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                    
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="PC1 Mother" HeaderStyle-HorizontalAlign="Left" SortExpression="PC1_MOTHER">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                 <%#Eval("PC1_MOTHER")%></a>
                            </ItemTemplate>
                       </asp:TemplateField> 
                      
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="PC2 Mother" HeaderStyle-HorizontalAlign="Left" SortExpression="PC2_MOTHER">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("PC2_MOTHER")%>
                            </ItemTemplate>
                       </asp:TemplateField>
                         
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Unit Weight" HeaderStyle-HorizontalAlign="Left" SortExpression="M_UNITWEIGHT">
                            <HeaderStyle HorizontalAlign="Left" Width="6%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("M_UNITWEIGHT")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                        
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Lot No" HeaderStyle-HorizontalAlign="Left" SortExpression="LOTNO">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("LOTNO")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                                  
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="PC1 Customer" HeaderStyle-HorizontalAlign="Left" SortExpression="PC1_CUST">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                 <%#Eval("PC1_CUST")%></a>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="PC2 Customer" HeaderStyle-HorizontalAlign="Left" SortExpression="PC2_CUST">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                 <%#Eval("PC2_CUST")%></a>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Unit Weight" HeaderStyle-HorizontalAlign="Left" SortExpression="C_UNITWEIGHT">
                            <HeaderStyle HorizontalAlign="Left" Width="6%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("C_UNITWEIGHT")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                                         
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Slit Lot No" HeaderStyle-HorizontalAlign="Left" SortExpression="SLIT_LOT_NO">
                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a id="Slit_Lot_No" target="_self" href="<%# GetUrl(EnumAction.View, Eval("SLIT_LOT_NO").ToString()) %>"><%#Eval("SLIT_LOT_NO")%></a>
                            </ItemTemplate>
                       </asp:TemplateField>
                            
                       <asp:BoundField HeaderStyle-Width="8%" HeaderText="SlitLot" DataField="SLIT_LOT_NO" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide"  > 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                       </asp:BoundField>
                 
                       <asp:BoundField HeaderStyle-Width="6%" HeaderText="Record Type" DataField="REC_TYPE"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                       </asp:BoundField>
                                      
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Print Status" HeaderStyle-HorizontalAlign="Left" SortExpression="STATUS">
                            <HeaderStyle HorizontalAlign="Left" Width="6%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("STATUS")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                      
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Print" HeaderStyle-HorizontalAlign="Left" SortExpression="">
                            <HeaderStyle HorizontalAlign="Left" Width="6%"></HeaderStyle>
                            <HeaderTemplate><asp:CheckBox ID="cbSelectAll" runat="server" Text="Print" OnCheckedChanged="PrintChkBoxAll_CheckedChanged" AutoPostBack="True"/></HeaderTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:CheckBox ID="PrintChkBox" runat="server" OnCheckedChanged="PrintChkBox_CheckedChanged" AutoPostBack="True"></asp:CheckBox>   
                            </ItemTemplate>
                       </asp:TemplateField> 
                          
                    </Columns>
                    
                    <EmptyDataRowStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
                    <EmptyDataTemplate>Record Not Found</EmptyDataTemplate>
                    
                <PagerStyle HorizontalAlign="Right" />
                <HeaderStyle CssClass="title_bar"></HeaderStyle>
                </asp:GridView> 
            </td>
        </tr>
   </table>
    
   <control:Footer ID="UCFooter" runat="server" /> 

   <table width="100%">
    <tr>
     <td width="10%" class="style5"></td>
     <td width="10%" class="style5">&nbsp;</td>
     <td width="10%" class="style5"></td>
     <td width="10%" class="style5"></td>
     <td width="10%" class="style5"></td>
     <td width="10%" class="style5"></td>
     <td width="10%" class="style5"></td>
     <td width="10%" class="style5"></td>
     <td width="15%" class="style5"><asp:Button ID="btnExport" style="float: right;" runat="server" Text="Export" BorderStyle="Solid" OnClientClick="redirect()" OnClick="btnExport_Click" /></td>
     <td width="5%" class="style5"></td>
    </tr>
  </table>
  
</asp:Content>