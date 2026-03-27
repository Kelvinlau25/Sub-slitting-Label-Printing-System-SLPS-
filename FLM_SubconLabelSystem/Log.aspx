<%@ Page Title="" Language="VB"  AutoEventWireup="false" CodeFile="Log.aspx.vb" Inherits="Log" %>

<%@ Register src="App_Module/Title.ascx" tagname="Title" tagprefix="uc1" %>
<%@ Register src="App_Module/GridFooter.ascx" tagname="GridFooter" tagprefix="uc2" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="<% Response.Write(ResolveUrl("~/css/Detail.css")) %>" rel="stylesheet" type="text/css" />
    <link href="<% Response.Write(ResolveUrl("~/css/PenGroup.css")) %>" rel="stylesheet" type="text/css" />
    <link href="<% Response.Write(ResolveUrl("~/css/prodSystem.css")) %>" rel="stylesheet" type="text/css" />
    <link href="<% Response.Write(ResolveUrl("~/css/stylesheetmsdn.css")) %>" rel="stylesheet" type="text/css" />
    <link href="<% Response.Write(ResolveUrl("~/css/ui-lightness/DatePicker.css")) %>" rel="stylesheet" type="text/css" />
    <link href="<% Response.Write(ResolveUrl("~/css/ui-lightness/jquery-ui-1.8.19.custom.css")) %>" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<uc1:Title ID="UCTitle" runat="server" Audit="true" />
    <div id="divDesc" runat="server"></div><br />
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="grdResult" RowStyle-CssClass="content" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="False">
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="content"></RowStyle>
                        <Columns>           
                            <asp:BoundField HeaderStyle-Width="20%" HeaderText="Field" DataField="FIELD_NAME"> 
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="content" VerticalAlign="Middle" />
                            </asp:BoundField>  
                            <asp:BoundField HeaderStyle-Width="20%" HeaderText="Old Value" DataField="B4_UPDATE"> 
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="content" VerticalAlign="Middle" />
                            </asp:BoundField>  
                            <asp:BoundField HeaderStyle-Width="20%" HeaderText="New Value" DataField="AF_UPDATE"> 
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="content" VerticalAlign="Middle" />
                            </asp:BoundField>  
                            <asp:BoundField HeaderStyle-Width="20%" HeaderText="User" DataField="UPDATED_BY"> 
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="content" VerticalAlign="Middle" />
                            </asp:BoundField>  
                            <asp:BoundField HeaderText="Time" DataField="UPDATED_DATE" DataFormatString="{0:dd/MM/yyyy h:mm:ss tt}"> 
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="content" VerticalAlign="Middle" />
                            </asp:BoundField>  
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" />
                    <HeaderStyle CssClass="title_bar"></HeaderStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <uc2:GridFooter ID="UCFooter" runat="server" Audit="true" />
    <div style="width:100%;text-align:center">
        <asp:Button ID="btnclose" runat="server" OnClientClick="self.close ()" Text="Close" />
    </div>

    </div>
    </form>
</body>
</html>
