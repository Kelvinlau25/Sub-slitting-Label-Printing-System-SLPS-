<%@ Page Language="VB" MasterPageFile="~/master/Main.master"  AutoEventWireup="false" CodeFile="SLIT_SERIES.aspx.vb" Inherits="Transactions_SlitSeries" title="Slit_Series" EnableEventValidation = "false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    <table width="100%">
    <tr>
        <td>
            <asp:GridView ID="grdResult" DataKeyNames="ID_PC2_LOTNO" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                    
                        <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Ref. No" HeaderStyle-HorizontalAlign="Left" SortExpression="REFNO">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("REFNO")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Company Code" HeaderStyle-HorizontalAlign="Left" SortExpression="COMPANYCODE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                 <%#Eval("COMPANYCODE")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                        <asp:TemplateField HeaderStyle-Width="8%" HeaderText="PC1 Customer" HeaderStyle-HorizontalAlign="Left" SortExpression="PC1_CUST">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                 <%#Eval("PC1_CUST")%></a>
                            </ItemTemplate>
                        </asp:TemplateField> 
                      
                        <asp:TemplateField HeaderStyle-Width="16%" HeaderText="PC2 Mother" HeaderStyle-HorizontalAlign="Left" SortExpression="PC2_MOTHER">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("PC2_MOTHER")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Lot No" HeaderStyle-HorizontalAlign="Left" SortExpression="LOTNO">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a target="_self" href="<%# MyBase.GetUrl(EnumAction.View, Eval("ID_PC2_LOTNO")) %>"><%#Eval("LOTNO")%></a>
                            </ItemTemplate>
                        </asp:TemplateField> 
                                        
                        <asp:TemplateField HeaderStyle-Width="13%" HeaderText="PC2 Customer" HeaderStyle-HorizontalAlign="Left" SortExpression="PC2_CUST">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>                        
                                <%#Eval("PC2_CUST")%>
                            </ItemTemplate>
                        </asp:TemplateField>  
                                
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Unit Weight" HeaderStyle-HorizontalAlign="Left" SortExpression="UNIT_WEIGHT">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("UNIT_WEIGHT")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Line" HeaderStyle-HorizontalAlign="Left" SortExpression="PRODLINE_NO">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("PRODLINE_NO")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                       
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Planning Year Month" HeaderStyle-HorizontalAlign="Left" SortExpression="PLAN_YEAR_MONTH">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("PLAN_YEAR_MONTH")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                        
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Type Of Slit" HeaderStyle-HorizontalAlign="Left" SortExpression="TYPE_OF_SLIT">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("TYPE_OF_SLIT")%>
                            </ItemTemplate>
                      </asp:TemplateField> 
                      
                      <asp:TemplateField HeaderStyle-Width="3%" HeaderText="No. Of Slit" HeaderStyle-HorizontalAlign="Left" SortExpression="NO_OF_SLIT">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("NO_OF_SLIT")%>
                            </ItemTemplate>
                      </asp:TemplateField> 
                        
                     <asp:BoundField HeaderStyle-Width="6%" HeaderText="Record Type" DataField="REC_TYPE"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                     </asp:BoundField>
                     
                     <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Lot Slitting Status" HeaderStyle-HorizontalAlign="Left" SortExpression="STATUS">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# IIF(Eval("STATUS").ToString="Create","<a href=" & Eval("Create_URL") & ">Create</a>",Eval("STATUS") )%>'></asp:Label> 
                            </ItemTemplate>
                     </asp:TemplateField> 
                        
                     <asp:BoundField HeaderStyle-Width="6%" HeaderText="ID_PC2_LOTNO" DataField="ID_PC2_LOTNO" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                     </asp:BoundField>
                     
                     <asp:BoundField HeaderStyle-Width="6%" HeaderText="Status" DataField="STATUS" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                     </asp:BoundField>
                        
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
    <asp:HiddenField ID="hdn_LotID" runat="server" />
   
</asp:Content>




