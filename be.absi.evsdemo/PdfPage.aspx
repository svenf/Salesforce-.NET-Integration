<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PdfPage.aspx.cs" Inherits="pdf_PdfPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>
        <img src="servlet.jpg" alt="" /></h1>
    <table>
        <tr>
            <td>
                <b>Company Adress</b>
            </td>
            <td>
                16 Rue Bois St Jean
            </td>
            <td>&nbsp</td>
            <td>&nbsp</td>
            <td>
                <b>Created Date</b>
            </td> 
            <td>
                <%= this.quote.CreatedDate %>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>  
            <td>
                Seraing, 4102
            </td>   
            <td>&nbsp</td>
            <td>&nbsp</td>
            <td>
                <b>Quote Number</b>
            </td> 
            <td>
                <%= this.quote.QuoteNumber %>
            </td>          
        </tr>
        <tr>
            <td>
                
            </td>  
            <td>
                Belgium
            </td> 
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>             
            </td>   
            
        </tr>
        <tr>
            <td>
                <b>Prepared By</b>
            </td>
            <td>
                <%= this.user.Name %>
            </td>
            <td>&nbsp</td>
            <td>&nbsp</td>
            <td>
                <b>Contact Name</b>
            </td>
            <td>
                <%= this.quote.Contact.Name %>
            </td>
        </tr>
        <tr>
            <td>
                <b>E-mail</b>
           </td>
           <td>
                <%= this.user.Email %>
           </td>
           <td>&nbsp</td>
           <td>&nbsp</td>
           <td>
                <b>Email</b>
           </td>
           <td>
                <%= this.quote.Contact.Email%>
           </td>
        </tr>
        <tr>
            <td>
                <b>Bill To Name</b>
            </td>
            <td>
                <%= this.quote.BillingName %>                
           </td>
           <td>&nbsp</td>
           <td>&nbsp</td>
           <td>
                <b>Ship To Name</b>
            </td>
            <td>
                <%= this.quote.ShippingName %>
           </td>
        </tr>
        <tr>
            <td>
                <b>Bill To</b>
            </td>
            <td>
                <%= this.quote.BillingStreet %>

           </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <%= this.quote.BillingCity + ", " + this.quote.BillingPostalCode %>

           </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>               
                <%= this.quote.BillingCountry %>

           </td>
        </tr>
    </table>
    <br /> <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <th style="width:200px;">Product</th>
            <th style="width:150px;">Unit Price</th>            
            <th style="width:150px;">Quantity</th>
            <th style="width:150px;">TotalPrice</th>
        </tr>
        <asp:Repeater runat="server" ID="rptQuoteLineItems">
        <ItemTemplate>
            <tr>
                <td style="width:200px;text-align:center;">
                    <%# Eval("pricebookentry.Name")%>
                </td>
                <td style="width:150px;text-align:center;">
                    <%# Eval("unitPrice")%>
                </td>                
                <td style="width:150px;text-align:center;">
                    <%# Eval("quantity")%>
                </td>
                <td style="width:150px;text-align:center;">
                    <%# Eval("totalPrice")%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
</body>
</html>
