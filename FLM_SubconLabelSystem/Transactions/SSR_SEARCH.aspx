<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="true" CodeBehind="SSR_SEARCH.aspx.cs" Inherits="Transactions_SUBSLITREQSEARCH" title="SUBSLITREQSEARCH" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">

    function pop_up_confirm(){

        if(confirm("Are you sure you want to create a new revision?")){
        
            window.location.href = "/Transactions/SUBSLIT_REQ_.aspx";
        }
    
    }

</script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <control:Main ID="UCTitle" runat="server" />
    <control:Search ID="UCSearch" runat="server" />
    
    <div style="display:none">
    <control:Header ID="UCHeader" runat="server" />
    </div>
    
    <table width="100%">
       <tr><td align="right" style="color:blue"><asp:LinkButton ID="Add_Button" runat="server" OnClick="Add_Button_Click">Add</asp:LinkButton></td></tr>
    <tr>
        <td>
            <asp:GridView ID="grdResult" DataKeyNames="ID_SUBSLIT_REQUEST" EnableViewState="false" HeaderStyle-CssClass="title_bar" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"
                OnRowDataBound="grdResult_RowDataBound" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                <PagerSettings Visible="False" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Width="12%" HeaderText="Company Code" HeaderStyle-HorizontalAlign="Left" SortExpression="COMPANYTO">
                            <HeaderStyle HorizontalAlign="Left" Width="18%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("COMPANYTO")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                       
                       <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Ref No" HeaderStyle-HorizontalAlign="Left" SortExpression="REFNO">
                            <HeaderStyle HorizontalAlign="Left" Width="11%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <a href="#" ><%#Eval("REFNO")%></a>
                            </ItemTemplate>
                       </asp:TemplateField>
                        
                       <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Revision" HeaderStyle-HorizontalAlign="Left" SortExpression="REVISIONCOUNT">
                            <HeaderStyle HorizontalAlign="Left" Width="11%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("REVISIONCOUNT")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                        
                       <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Requestor Status" HeaderStyle-HorizontalAlign="Left" SortExpression="REQUEST_STATUS">
                            <HeaderStyle HorizontalAlign="Left" Width="11%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("REQUEST_STATUS")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                        
                       <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Vendor Status" HeaderStyle-HorizontalAlign="Left" SortExpression="VENDOR_STATUS">
                            <HeaderStyle HorizontalAlign="Left" Width="11%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("VENDOR_STATUS")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                        
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Request Date" HeaderStyle-HorizontalAlign="Left" SortExpression="DATEREQ">
                            <HeaderStyle HorizontalAlign="Left" Width="11%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#Eval("DATEREQ")%>
                            </ItemTemplate>
                       </asp:TemplateField> 
                        
                       <asp:TemplateField HeaderStyle-Width="8%" HeaderText="Updated Date" HeaderStyle-HorizontalAlign="Left" SortExpression="UPDATED_DATE">
                            <HeaderStyle HorizontalAlign="Left" Width="11%"></HeaderStyle>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "UPDATED_DATE", "{0:dd/MM/yyyy hh:mm:ss tt}")%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="Record Type" DataField="REC_TYPE"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                        <asp:BoundField HeaderStyle-Width="6%" HeaderText="hRefno" DataField="REFNO" 
                            HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                        <asp:BoundField HeaderStyle-Width="6%" HeaderText="hID_SSR" DataField="ID_SUBSLIT_REQUEST" 
                            HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide"> 
                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                        
                        <asp:BoundField HeaderStyle-Width="6%" HeaderText="hReq_Status" DataField="REQUEST_STATUS" 
                            HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide"> 
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