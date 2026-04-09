<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeBehind="MM_PC1.aspx.cs" Inherits="MasterMaint_MM_PC1" title="MM_PC1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    <table width="100%">
    <tr>
        <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_PC1" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Width="10%" HeaderText="PC1" HeaderStyle-HorizontalAlign="Left" SortExpression="PC1">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <a target="_self" href="<%# GetUrl(EnumAction.View, Eval("ID_MM_PC1").ToString()) %>"><%# Eval("PC1") %></a>
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="60%" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" SortExpression="DESCRIPTION">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <%# Eval("DESCRIPTION").ToString().ToUpper() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Width="15%" HeaderText="Record Type" DataField="REC_TYPE">
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