<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeBehind="PP_PC2SUB.aspx.cs" Inherits="PopUp_PP_PC2SUB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function passvalue(pc2mother, lblpc2mother) { 
     
            window.opener.$('.<%= this.pc2mother %>').val(pc2mother);
            window.opener.$('.<%= this.lblpc2mother %>').val(lblpc2mother);
            if ('<%= this.pc2mother %>'=='pc2mother') {
            window.opener.$('.btnpc2').click();}
            else 
            {
            window.opener.$('.btnpc2child').click();
            }
            
            window.close();
        }
    </script>
    
    <control:Main ID="UCTitle" runat="server" />
    <h3><asp:Label ID="lblTittle" runat="server"></asp:Label></h3>
    <control:Search ID="UCSearch" runat="server" />

    
    <table width="100%">
    <tr>
        <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_PC2" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                  
                        <asp:TemplateField HeaderStyle-Width="30%" HeaderText="Product Code 2" HeaderStyle-HorizontalAlign="Center" SortExpression="ID_MM_PC2">
                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                 <a href="#" onclick="javascript:passvalue('<%#Eval("PC2")%>','<%#Eval("ID_MM_PC2")%>')"><%#Eval("PC2")%> </a>
                            </ItemTemplate>
                        </asp:TemplateField>                                          
                        
                    </Columns>
                    <EmptyDataRowStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
                    <EmptyDataTemplate>Record Not Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Right" />
                <HeaderStyle CssClass="title_bar" BackColor="#91B3DD"></HeaderStyle>
            </asp:GridView>
            </td>
            </tr>
    </table>
    
    <control:Footer ID="UCFooter" runat="server" />
    
</asp:Content>