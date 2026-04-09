<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeFile="MM_USER.aspx.cs" Inherits="MasterMaint_MM_USER" title="MM_USER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    <table width="100%">
    <tr>
         <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_USERID" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="UserID" HeaderStyle-HorizontalAlign="Left" SortExpression="USERID">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a target="_self" href="<%# GetUrl(Library.Root.Control.Base.EnumAction.View, Eval("ID_MM_USERID").ToString()) %>"><%#Eval("USERID")%></a>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderStyle-Width="18%" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" SortExpression="NAME">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("NAME")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Department" HeaderStyle-HorizontalAlign="Left" SortExpression="DEPARTMENT">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("DEPARTMENT")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Email Address" HeaderStyle-HorizontalAlign="Left" SortExpression="EMAIL">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("EMAIL")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Level" HeaderStyle-HorizontalAlign="Left" SortExpression="ULEVEL">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("ULEVEL")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderStyle-Width="9%" HeaderText="Company Code" HeaderStyle-HorizontalAlign="Left" SortExpression="COMPANYCODE">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("COMPANYCODE")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField HeaderStyle-Width="7%" HeaderText="Record Type" DataField="REC_TYPE"> 
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