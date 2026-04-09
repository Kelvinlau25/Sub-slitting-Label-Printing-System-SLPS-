<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeFile="MM_PRODLINE.aspx.cs" Inherits="MasterMaint_MM_PRODLINE" title="MM_PRODLINE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    <table width="100%">
    <tr>
        <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_PRODLINE" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Line" HeaderStyle-HorizontalAlign="Left" SortExpression="PRODLINE_NO">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a target="_self" href="<%# GetUrl(EnumAction.View, Eval("ID_MM_PRODLINE").ToString()) %>"><%#Eval("PRODLINE_NO")%></a>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderStyle-Width="20%" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" SortExpression="DESCRIPTION">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("DESCRIPTION")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Created By" HeaderStyle-HorizontalAlign="Left" SortExpression="CREATED_BY">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Created_By")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="17%" HeaderText="Created Date" HeaderStyle-HorizontalAlign="Left" SortExpression="CREATED_DATE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Created_Date")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Updated By" HeaderStyle-HorizontalAlign="Left" SortExpression="UPDATED_BY">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Updated_By")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderStyle-Width="17%" HeaderText="Updated Date" HeaderStyle-HorizontalAlign="Left" SortExpression="UPDATED_DATE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Updated_Date")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField HeaderStyle-Width="8%" HeaderText="Record Type" DataField="REC_TYPE"> 
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
  
</asp:Content>