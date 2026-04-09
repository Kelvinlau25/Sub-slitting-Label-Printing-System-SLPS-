<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeBehind="PP_PC2_Cust.aspx.cs" Inherits="PopUp_PP_PC2_Cust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function passvalue(pc2cust, lblpc2cust) { 
     
            window.opener.$('.<%= this.pc2cust %>').val(pc2cust);
            window.opener.$('.<%= this.lblpc2cust %>').val(lblpc2cust);
            window.opener.document.getElementById('<%= this.str_hdn_PC2Customer %>').value = pc2cust;
            window.opener.document.getElementById('<%= this.str_hdn_UnitWeightCustomer %>').value = lblpc2cust;
            
            window.opener.document.getElementById('<%= this.str_BtnName %>').click();
            
            window.close();
        }
    </script>
    
    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />

    
    <table width="100%">
    <tr>
        <td>
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_PC2" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                  
                        <asp:TemplateField HeaderStyle-Width="30%" HeaderText="Product Code 2 Customer" HeaderStyle-HorizontalAlign="Center" SortExpression="ID_MM_PC2">
                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                 <a href="#" onclick="javascript:passvalue('<%#Eval("PC2")%>','<%#Eval("UNIT_WEIGHT")%>');"> <%#Eval("PC2")%> </a>
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