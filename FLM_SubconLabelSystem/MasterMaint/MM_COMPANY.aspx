<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeBehind="MM_COMPANY.aspx.cs" Inherits="MasterMaint_MM_COMPANY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    <table width="100%">
    <tr>
        <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_COMPANY" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Company Code" HeaderStyle-HorizontalAlign="Left" SortExpression="COMPANYCODE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <a target="_self" href="<%# GetUrl(EnumAction.View, Eval("ID_MM_COMPANY").ToString()) %>"><%# Eval("COMPANYCODE") %></a>
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="20%" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" SortExpression="COMPANYNAME">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <%# Eval("COMPANYNAME").ToString().ToUpper() %>
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="6%" HeaderText="Slit Code" HeaderStyle-HorizontalAlign="Left" SortExpression="SLIT_CODE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("SLIT_CODE") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Address" HeaderStyle-HorizontalAlign="Left" SortExpression="ADDRESS">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("ADDRESS").ToString().ToUpper() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Telephone" HeaderStyle-HorizontalAlign="Left" SortExpression="TELEPHONE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("TELEPHONE") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="12%" HeaderText="Email" HeaderStyle-HorizontalAlign="Left" SortExpression="EMAIL">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%# Eval("EMAIL") %>
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