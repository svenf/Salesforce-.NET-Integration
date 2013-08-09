<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="salesforceForm" runat="server">
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        EVS Demo
                    </h1>
                </div>            
                <div class="clear hideSkiplink">               
                </div>
            </div>
            <div class="main">
                <div id="info" runat="server">
                    <h2>Create Quote for opportunity:  <%= this.opportunity.Name %></h2>

                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td> 
                                <asp:Label ID="Label1" Text="Quote Name" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtQuoteName" CssClass="LeftValueColumn"/>   
                                <asp:RequiredFieldValidator ErrorMessage="A name is required." ControlToValidate="txtQuoteName"
                                    runat="server" />     
                            </td>
                        </tr>                        
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccountName" Text="Account Name" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAccountName" CssClass="LeftValueColumn"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccountBillingstreet" Text="Account Billing Street" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAccountBillingstreet" CssClass="LeftValueColumn"/>
                            </td>
                            <td>
                                <asp:Label ID="lblAccountBillingstate" Text="Account Billing State" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAccountBillingState" CssClass="LeftValueColumn"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccountBillingcity" Text="Account Billing City" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAccountBillingCity" CssClass="LeftValueColumn"/>
                            </td>
                            <td>
                                <asp:Label ID="lblAccountBillingcountry" Text="Account Billing Country" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAccountBillingcountry" CssClass="LeftValueColumn"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccountBillingpostalcode" Text="Account Billing Postal Code" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAccountBillingpostalcode" CssClass="LeftValueColumn"/>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblContactName" Text="Contact Name" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlContacts" CssClass="LeftValueColumn"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" Text="Select a pricebook" runat="server" CssClass="LeftLabelColumn"/>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlPricebooks" AutoPostBack="true" OnSelectedIndexChanged="ddlPriceBooks_SelectedIndexChanged" CssClass="LeftValueColumn"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <h3>Quote Line Items</h3> <br />

                    <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false" onrowcreated="gvQuoteLineItems_RowCreated" OnRowDataBound="gvQuoteLineItems_RowDataBound" >
                        <Columns>                        
                            <asp:TemplateField HeaderText="Product Name">
                                <ItemTemplate>                                
                                    <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Product Reference">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtProductReference" runat="server"></asp:TextBox>--%>
                                    <asp:Label ID="lblProductReference" Text="" runat="server" />
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUnitPrice" runat="server"></asp:TextBox>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />                                
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnRemove" runat="server" onclick="lbtnRemove_Click">Remove</asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAddQuoteLineItem" runat="server" Text="Add New Quote Line Item" onclick="btnAddQuoteLineItem_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:gridview>
                    <br />                    
                    <!--<asp:CheckBox Text="Create PDF?" runat="server" ID="cbxPdf" />-->
                    <!--<br />-->
                    <br />
                    <asp:Button Text="Save Quote" runat="server" onclick="btnAddQuote_Click"/>
                </div>

                <asp:Label ID="lblAccountResult" runat="server"></asp:Label>
            </div>
            <div class="clear"></div>
        </div>
        <div class="footer"></div>
    </form>
</body>
</html>