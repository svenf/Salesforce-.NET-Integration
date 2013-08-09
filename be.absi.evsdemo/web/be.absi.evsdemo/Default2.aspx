<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false">
        <Columns>
        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
        <asp:TemplateField HeaderText="Header 1">
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="-1">Select</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Header 2">
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="-1">Select</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Header 3">
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="-1">Select</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
            <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row"
                    onclick="ButtonAdd_Click" />
            </FooterTemplate>
        </asp:TemplateField>
        </Columns>
        </asp:gridview>
    </div>
    </form>
</body>
</html>
