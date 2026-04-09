<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeFile="MM_PC2.aspx.cs" Inherits="MasterMaint_MM_PC2" title="MM_PC2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    <table width="100%">
    <tr>
        <td>
            <asp:GridView ID="grdResult" DataKeyNames="ID_MM_PC2" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Width="60%" HeaderText="PC2" HeaderStyle-HorizontalAlign="Left" SortExpression="PC2">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a target="_self" href="<%# GetUrl(EnumAction.View, Eval("ID_MM_PC2").ToString()) %>"><%# Eval("PC2") %></a>
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Grade" HeaderStyle-HorizontalAlign="Left" SortExpression="GRADE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("GRADE") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Packing Code" HeaderStyle-HorizontalAlign="Left" SortExpression="PACK_CODE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("PACK_CODE") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Core Code" HeaderStyle-HorizontalAlign="Left" SortExpression="CORE_CODE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("CORE_CODE") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Unit Weight" HeaderStyle-HorizontalAlign="Left" SortExpression="UNIT_WEIGHT">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <%# Eval("UNIT_WEIGHT") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Width="6%" HeaderText="Record Type" DataField="REC_TYPE">
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