<%@ Page Language="C#" MasterPageFile="~/master/Main.master" AutoEventWireup="false" CodeBehind="SUBSLIT_REQ_.aspx.cs" Inherits="Transactions_SubSlitRequest" title="Sub_Slitting_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link href="<%= ResolveUrl("~/css/jquery-ui.css") %>" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .ui-datepicker-trigger {
            display: inline;
        }
        .style2 {
            width: 130px;
        }
        .style3 {
            width: 10px;
        }
        .style4 {
            width: 280px;
        }
        .style12 {
            height: 30px;
        }
        .bin {
            background-image: url("paper.gif");
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#txtRefNo").change(function () {
                console.log("in");
                var x = $(this).val();
                if (x.includes("&")) {
                    alert("The character & cannot be used in Ref No. Please replace it with a different value.");
                }
            });
        });

        $(function () {
            $(".txtDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });
            $(".txtETD").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".txtETA").datepicker("option", "minDate", selected);
                }
            });
            $(".txtETA").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '<%= ResolveUrl("~/image/icon_cal.png") %>',
                numberOfMonths: 1,
                minDate: 0,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".txtETD").datepicker("option", selected);
                }
            });

            $(".no_type").keydown(function (evt) {
                return false;
            });
        });

        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=grdList.ClientID %>');
            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }

        function ValMotherQty() {
            var x = document.getElementById('<%=txtQty.ClientID%>').value;
            if (parseInt(x.value) == '0') {
                alert("Please Enter Value More than 0");
                x.value = '';
            }
        }

        function ValChildQty() {
            var x = document.getElementById("txtQTYC");
            if (parseInt(x.value) == '0') {
                alert("Please Enter Value More than 0");
                x.value = '';
            }
        }

        function showDialogue(v) {
            var control = '<%=inpHide.ClientID%>';
            document.getElementById("dialog-msg").innerHTML = "Do you want to proceed to save for Sub Slitting Waste is " + v + " value?";
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 170,
                width: 400,
                modal: true,
                buttons: {
                    "Yes": function () {
                        document.getElementById(control).value = "1";
                        $(this).dialog("close");
                        document.getElementById('<%=Button1.ClientID%>').click();
                    },
                    "No": function () {
                        document.getElementById(control).value = "0";
                        $(this).dialog("close");
                    }
                }
            });
        }

        function popupwindow(pc2mother, lblpc2mother) {
            window.open('<%= ResolveUrl("../PopUp/PP_PC2SUB.aspx") %>?itm1=' + pc2mother + '&itm2=' + lblpc2mother + '&itm3=', 'PopUp', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=800,height=600');
        }
    </script>

    <script type="text/javascript">
        $(function () {
            var lbl = eval('[<%= string.Join(", ", lbl) %>]');
            $(".prod").autocomplete({
                source: lbl,
                response: function (event, ui) {
                    if (!ui.content.length) {
                        var noResult = { value: "", label: "No results found" };
                        ui.content.push(noResult);
                        return false;
                    }
                }
            });

            var lbl2 = eval('[<%= string.Join(", ", lbl2) %>]');
            $(".pc1").autocomplete({
                source: lbl2,
                response: function (event, ui) {
                    if (!ui.content.length) {
                        var noResult = { value: "", label: "No results found" };
                        ui.content.push(noResult);
                        return false;
                    }
                }
            });

            var lbl3 = eval('[<%= string.Join(", ", lbl3) %>]');
            $(".pc2").autocomplete({
                source: lbl3,
                select: function (event, ui) {
                    event.preventDefault();
                    var selected = ui.item.value;
                    $('.pc2').val(selected);
                    document.getElementById('<%=Button2.ClientID%>').click();
                }
            });

            var lblchild = eval('[<%= string.Join(", ", lblchild) %>]');
            $(".pcchild1").autocomplete({
                source: lblchild,
                response: function (event, ui) {
                    if (!ui.content.length) {
                        var noResult = { value: "", label: "No results found" };
                        ui.content.push(noResult);
                        return false;
                    }
                }
            });

            var lblchild2 = eval('[<%= string.Join(", ", lblchild2) %>]');
            $(".pcchild2").autocomplete({
                source: lblchild2,
                select: function (event, ui) {
                    event.preventDefault();
                    var selected = ui.item.value;
                    $(this).val(selected);
                    document.getElementById('<%=Button3.ClientID%>').click();
                }
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="dialog-confirm" title="" style="display:none;">
        <table>
            <tr>
                <td>
                    <img src='<%= ResolveUrl("~/image/warning-sign.png") %>' style="width:50px; height:50px;" />
                </td>
                <td>
                    <span id="dialog-msg"></span>
                </td>
            </tr>
        </table>
    </div>

    <table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td><h3>Sub-Slitting Request Form - Add</h3></td>
        </tr>
    </table>
    <br />

    <input id="inpHide" runat="server" type="hidden" />
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display:none" />
    <asp:Button ID="Button3" runat="server" Text="Button" Style="display:none" />

    <table width="100%" style="height:100%;" border="0">
        <tr>
            <td class="style2"><asp:Label ID="Label1" runat="server" Text="To"></asp:Label></td>
            <td class="style3"><asp:Label ID="Label2" runat="server" Text=":"></asp:Label></td>
            <td class="style4"><asp:DropDownList ID="ddlCompCode" runat="server"></asp:DropDownList></td>
            <td class="style2"><asp:Label ID="Label3" runat="server" Text="Ref No"></asp:Label></td>
            <td class="style3"><asp:Label ID="Label4" runat="server" Text=":"></asp:Label></td>
            <td class="style4">
                <asp:TextBox ID="txtRefNo" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtSubReqId" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Department"></asp:Label></td>
            <td><asp:Label ID="Label6" runat="server" Text=":"></asp:Label></td>
            <td><asp:Label ID="lblDept" runat="server"></asp:Label></td>
            <td><asp:Label ID="Label7" runat="server" Text="Date"></asp:Label></td>
            <td><asp:Label ID="Label8" runat="server" Text=":"></asp:Label></td>
            <td><asp:TextBox ID="txtDate" runat="server" CssClass="txtDate"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label9" runat="server" Text="By"></asp:Label></td>
            <td><asp:Label ID="Label10" runat="server" Text=":"></asp:Label></td>
            <td><asp:Label ID="lblBy" runat="server"></asp:Label></td>
            <td><asp:Label ID="Label11" runat="server" Text="Rev"></asp:Label></td>
            <td><asp:Label ID="Label12" runat="server" Text=":"></asp:Label></td>
            <td><asp:Label ID="lblRev" runat="server" Text="0"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label13" runat="server" Text="Requestor Status"></asp:Label></td>
            <td><asp:Label ID="Label14" runat="server" Text=":"></asp:Label></td>
            <td><asp:Label ID="lblReqStat" runat="server" Text="New"></asp:Label></td>
            <td><asp:Label ID="Label15" runat="server" Text="Vendor Status"></asp:Label></td>
            <td><asp:Label ID="Label16" runat="server" Text=":"></asp:Label></td>
            <td><asp:Label ID="lblVenStat" runat="server" Text="N/A"></asp:Label></td>
        </tr>
    </table>

    <br />

    <table width="100%" style="height:100%;" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td><asp:Label ID="Label20" runat="server" Text="SUB-SLIT MOTHER ROLL DESCRIPTION"></asp:Label></td>
        </tr>
    </table>

    <table border="1" style="width:100%; border-collapse:collapse; border-color:Black;">
        <tr style="text-align:center; background-color:#bfd4ea;">
            <td style="width:2%">LINE</td>
            <td style="width:3%">PC1</td>
            <td style="width:10%">PC2</td>
            <td style="width:2%">QTY (ROLL)</td>
            <td style="width:2%">UNIT WEIGHT (KG)</td>
            <td style="width:2%">TOTAL WEIGHT (KG)</td>
            <td style="width:2%">SUB-SLIT WASTE (KG)</td>
            <td style="width:5%">ETD PFR</td>
            <td style="width:5%">ETA SUB-SLIT CONTRACTOR</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtSeqMother" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="ddlProdLine" runat="server" CssClass="prod" style="width:94%;"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="ddlPC1" runat="server" CssClass="pc1" style="width:94%;"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="ddlPC2" runat="server" CssClass="pc2mother no_type" Width="85%"></asp:TextBox>
                <input id="btnPC2Mother" runat="server" onclick="popupwindow('pc2mother','lblpc2mother');" type="button" value="..." />
                <asp:Button ID="btnpc2" runat="server" Text="btnddlpc2" CssClass="btnpc2" Style="display:none" />
            </td>
            <td>
                <asp:TextBox ID="txtQty" runat="server" AutoPostBack="true" style="width:90%"></asp:TextBox>
            </td>
            <td style="text-align:right">
                <asp:Label ID="lblUnitWeight" runat="server"></asp:Label>
            </td>
            <td style="text-align:right">
                <asp:Label ID="lblTotWeight" runat="server"></asp:Label>
            </td>
            <td style="text-align:right">
                <asp:Label ID="lblSubSlit" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtETD" runat="server" CssClass="txtETD" style="width:80%"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtETA" runat="server" CssClass="txtETA" style="width:80%"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table width="90%" style="height:100%;" border="0">
        <tr>
            <td width="10%" align="center"><asp:LinkButton ID="btnNext" runat="server">Next</asp:LinkButton></td>
            <td width="10%" align="center"><asp:LinkButton ID="btnReset" runat="server">Reset</asp:LinkButton></td>
            <td width="10%"></td>
            <td width="10%"></td>
            <td width="10%"></td>
            <td width="10%"></td>
            <td width="10%"></td>
            <td width="15%" align="center"></td>
            <td width="15%" align="center"></td>
        </tr>
    </table>
    <br />

    <asp:Panel ID="pnlChild" runat="server">
        SUB-SLIT CUSTOMER ROLL DESCRIPTION
        <br />
        <asp:Button ID="btnpc2child" runat="server" Text="btnddlpc2child" CssClass="btnpc2child" Style="display:none" />
        <asp:GridView ID="grdChild" runat="server" AutoGenerateColumns="False" Width="100%"
            OnRowDataBound="OnRowDataBound" OnRowDeleting="OnRowDeleting">
            <PagerSettings Visible="False" />
            <Columns>
                <asp:ButtonField ButtonType="Link" CommandName="DELETE" HeaderText="DELETE"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%"
                    Text="&lt;img src='../image/delete.gif' /&gt;">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>

                <asp:TemplateField HeaderText="PC1" ItemStyle-Width="4%">
                    <ItemTemplate>
                        <asp:TextBox ID="ddlPC1Child" runat="server" CssClass="pcchild1" Width="95%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PC2" ItemStyle-Width="11%">
                    <ItemTemplate>
                        <asp:TextBox ID="ddlPC2Child" runat="server"
                            CssClass='<%# (Container.DataItemIndex + 1).ToString() + " no_type" %>'
                            Width="85%"></asp:TextBox>
                        <input id="btnPC2Mother" runat="server"
                            onclick='<%# "popupwindow(\"" + (Container.DataItemIndex + 1).ToString() + "\",\"lblpc2mother\");" %>'
                            type="button" value="..." />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="QTY (ROLL)" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQTYC" runat="server" AutoPostBack="true" Width="95%"
                            OnTextChanged="txtQTYC_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UNIT WEIGHT (KG)" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitWeightC" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TOTAL WEIGHT (KG)" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <asp:Label ID="lblTotWeightC" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="REMARK" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemarkC" runat="server" Width="97%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle Font-Bold="true" ForeColor="Red" HorizontalAlign="Center" VerticalAlign="Middle" />
            <EmptyDataTemplate>Record Not Found</EmptyDataTemplate>
            <PagerStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="title_bar" />
        </asp:GridView>

        <table border="0" style="height:100%;" width="90%">
            <tr>
                <td align="center" width="20%">
                    <asp:LinkButton ID="btnAddRow" runat="server">Add Row</asp:LinkButton>
                </td>
                <td align="center" width="20%">
                    <asp:LinkButton ID="btnSave" runat="server">Save</asp:LinkButton>
                </td>
                <td width="8%"></td>
                <td align="center" width="16%"></td>
                <td align="center" width="16%"></td>
                <td width="8%"></td>
                <td width="8%"></td>
            </tr>
        </table>
    </asp:Panel>
    <br />

    <asp:Panel ID="pnlList" runat="server">
        <table width="90%" style="height:100%;" border="0">
            <tr>
                <td width="10%">&nbsp;</td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="15%"></td>
                <td width="15%"></td>
            </tr>
            <tr><td colspan="9">&nbsp;</td></tr>
        </table>

        <asp:GridView ID="grdList" DataKeyNames="REFNO" EnableViewState="false"
            HeaderStyle-CssClass="title_bar" Width="100%"
            OnRowDataBound="grdList_OnRowDataBound" ShowFooter="True"
            runat="server" AutoGenerateColumns="False"
            AllowSorting="True" AllowPaging="False" style="margin-bottom:0px">
            <Columns>
                <asp:TemplateField ItemStyle-Width="2%">
                    <ItemTemplate>
                        <asp:RadioButton ID="RadioButton1" runat="server"
                            Visible='<%# Eval("CHK").ToString() == "1" %>'
                            OnCheckedChanged="RadioButton1_CheckedChanged"
                            AutoPostBack="true" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("PC2_MOTHER") %>' />
                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("PC1_MOTHER") %>' />
                        <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("PRODLINE_NO") %>' />
                        <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("SEQ") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="2%" />
                </asp:TemplateField>

                <asp:BoundField HeaderText="PRODUCTION LINE" DataField="PRODLINE_NO" ItemStyle-Width="10%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC1 MOTHER" DataField="PC1_MOTHER" ItemStyle-Width="10%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC2 MOTHER" DataField="PC2_MOTHER" ItemStyle-Width="10%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QTY (ROLL)" DataField="QTY" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UNIT WEIGHT (KG)" DataField="M_WEIGHT" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TOTAL WEIGHT (KG)" DataField="M_TOTAL_WEIGHT" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC1 CUSTOMER" DataField="PC1_CUST" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PC2 CUSTOMER" DataField="PC2_CUST" ItemStyle-Width="10%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="QTY (ROLL)" DataField="C_QTY" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UNIT WEIGHT (KG)" DataField="C_WEIGHT" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TOTAL WEIGHT (KG)" DataField="C_TOTAL_WEIGHT" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="SUB-SLIT WASTE (KG)" DataField="SUBSLIT_WASTE" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ETD PFR" DataField="ETD" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ETA SUBSLIT CONTRACTOR" DataField="ETA" ItemStyle-Width="5%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="REMARK" DataField="REMARK" ItemStyle-Width="8%">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="SEQ" DataField="SEQ"
                    HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide">
                    <FooterStyle CssClass="hide" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CHK" DataField="CHK"
                    HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" FooterStyle-CssClass="hide">
                    <FooterStyle CssClass="hide" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
            </Columns>
            <EmptyDataRowStyle VerticalAlign="Middle" HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
            <EmptyDataTemplate>Record Not Found</EmptyDataTemplate>
            <PagerStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="title_bar" />
        </asp:GridView>

        <table width="90%" style="height:100%;" border="0">
            <tr>
                <td width="10%" align="center">
                    <asp:LinkButton ID="btnEdit" runat="server">Edit</asp:LinkButton>
                </td>
                <td width="10%" align="center">
                    <asp:LinkButton ID="btnDelete" runat="server"
                        OnClientClick="return confirm('Are you sure to delete the selected PC2 Mother/Child records?');">Delete</asp:LinkButton>
                </td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="10%"></td>
                <td width="15%"></td>
                <td width="15%"></td>
            </tr>
            <tr><td colspan="9">&nbsp;</td></tr>
        </table>

        <table id="SSRTotal" width="100%" style="height:100%;" border="0" runat="server">
            <tr>
                <td width="2%"></td>
                <td width="9%"></td>
                <td width="10%"></td>
                <td width="12%">Total</td>
                <td width="4.5%" align="center"><asp:Label ID="lblMQty" runat="server" Text="0"></asp:Label></td>
                <td width="5%"></td>
                <td width="4.5%" align="right"><asp:Label ID="lblMTotalWeight" runat="server" Text="0"></asp:Label></td>
                <td width="6%"></td>
                <td width="9.5%"></td>
                <td width="6%" align="center"><asp:Label ID="lblCQty" runat="server" Text="0"></asp:Label></td>
                <td width="5%"></td>
                <td width="5%" align="center"><asp:Label ID="lblCTotalWeight" runat="server" Text="0"></asp:Label></td>
                <td width="5%" align="center"><asp:Label ID="lblSubSlitWaste" runat="server" Text="0"></asp:Label></td>
                <td width="5.5%"></td>
                <td width="7.5%"></td>
                <td width="7.5%"></td>
            </tr>
        </table>
    </asp:Panel>

    <table width="90%" style="height:100%;" border="0">
        <tr>
            <td width="10%" class="style12" align="center"></td>
            <td width="10%" class="style12" align="center"></td>
            <td width="10%" class="style12"></td>
            <td width="10%" class="style12"></td>
            <td width="10%" class="style12"></td>
            <td width="10%" class="style12"></td>
            <td width="10%" class="style12"></td>
            <td width="15%" align="center" class="style12">
                <asp:Button ID="Submit_Button" runat="server" Text="Submit" Width="116px" />
            </td>
            <td width="15%" align="center" class="style12">
                <asp:Button ID="Cancel_Button" runat="server" Text="Cancel" Width="116px" />
            </td>
        </tr>
        <tr><td colspan="9">&nbsp;</td></tr>
    </table>

</asp:Content>