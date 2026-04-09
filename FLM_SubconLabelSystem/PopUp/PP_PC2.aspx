<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeFile="PP_PC2.aspx.cs" Inherits="PopUp_PP_PC2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function passvalue(pc2mother, lblpc2mother) {  
     
            window.opener.$('.<%= this.pc2mother %>').val(pc2mother);
            window.opener.$('.<%= this.lblpc2mother %>').val(lblpc2mother);
            window.opener.document.getElementById('<%= this.str_hdn_PC2_Mother %>').value = pc2mother;
            window.opener.document.getElementById('<%= this.str_hdn_Unit_Weight_Mother %>').value = lblpc2mother;
            
            window.opener.document.getElementById('<%= this.str_BtnName %>').click();
         
            window.close();
        }
    </script>

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />

    
             <asp:GridView ID="grdResult" DataKeyNames="ID_MM_PC2" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                <PagerSettings Visible="False" />
                    <Columns>
                  
                        <asp:TemplateField HeaderStyle-Width="30%" HeaderText="Product Code 2 Mother" HeaderStyle-HorizontalAlign="Center" SortExpression="ID_MM_PC2">

                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                 <a href="#" onclick="javascript:passvalue('<%#Eval("PC2")%>','<%#Eval("UNIT_WEIGHT")%>')"><%#Eval("PC2")%> </a>
                            </ItemTemplate>
                        </asp:TemplateField>                                          
                     
                    </Columns>
                    <EmptyDataRowStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
                    <EmptyDataTemplate>Record Not Found</EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Right" />
                <HeaderStyle CssClass="title_bar" BackColor="#91B3DD"></HeaderStyle>
            </asp:GridView>    
    
    <control:Footer ID="UCFooter" runat="server" />
    
</asp:Content>
