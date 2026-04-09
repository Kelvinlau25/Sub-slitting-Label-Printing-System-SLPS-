<%@ Page Language="C#" Async="true" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeFile="PRINT_ALIGN_INIT.aspx.cs" Inherits="MasterMaint_PRINT_ALIGN_INIT" title="PRINT_ALIGN_INIT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    <control:Header ID="UCHeader" runat="server" />
    
    <table width="100%">
    <tr>
        <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_Print_Align_Init" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                      <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Company Code" HeaderStyle-HorizontalAlign="Left" SortExpression="CompanyCode">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("CompanyCode")%>
                            </ItemTemplate>
                      </asp:TemplateField> 
                      <asp:TemplateField HeaderStyle-Width="18%" HeaderText="Printer Name" HeaderStyle-HorizontalAlign="Left" SortExpression="Printer_Name">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a target="_self" href="<%# GetUrl(Library.Root.Control.Base.EnumAction.View, Eval("ID_Print_Align_Init").ToString()) %>"><%#Eval("Printer_Name")%></a>
                            </ItemTemplate>
                      </asp:TemplateField> 
                      <asp:TemplateField HeaderStyle-Width="12%" HeaderText="Created By" HeaderStyle-HorizontalAlign="Left" SortExpression="Created_By">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Created_By")%>
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderStyle-Width="18%" HeaderText="Created Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Created_Date">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Created_Date")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderStyle-Width="12%" HeaderText="Updated By" HeaderStyle-HorizontalAlign="Left" SortExpression="Updated_By">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Updated_By")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderStyle-Width="18%" HeaderText="Updated Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Updated_Date">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Updated_Date")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Default" HeaderStyle-HorizontalAlign="Left" SortExpression="Default_Printer">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("Default_Printer")%>
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
    <table style="width: 636px">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Text1">First Time Setup :</asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
            
                <asp:Label ID="Label2" runat="server" Text="Text2">1. For first time installation, please download</asp:Label>
               &nbsp;<asp:LinkButton ID="herebutton" runat="server" OnClick="herebutton_Click">here</asp:LinkButton> 
            &nbsp;and settings <asp:LinkButton ID="hereButton1" runat="server" OnClick="hereButton1_Click">here</asp:LinkButton> 
        
            </td>
           
        </tr>
        
        <tr>
            <td>
                
                <asp:Label ID="Label3" runat="server" Text="Text4">2. Go to directory D:\ and create a folder named 'subconlabelprinter'</asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
            
                <asp:Label ID="Label4" runat="server" Text="Text5">3. Unzip the file and move all the downloaded files to D:\subconlabelprinter</asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
            
                4. Create shortcut for executable (*.exe) file </td>
        </tr>
        
        <tr>
            <td>
            
                5. Move the shortcut icon to C:\ProgramData\Microsoft\Windows\Start 
                Menu\Programs\Startup</td>
        </tr>
        
        <tr>
            <td>
            
                6. Double click the icon to start and your printer is ready to print</td>
        </tr>
    </table>
    
  
</asp:Content>