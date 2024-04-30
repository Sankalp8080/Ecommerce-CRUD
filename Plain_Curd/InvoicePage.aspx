<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="InvoicePage.aspx.cs" Inherits="Plain_Curd.Admin.InvoicePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
    .invoice {
        width: 80%;
        margin-top: 20px;
        margin-left:10%;
        padding: 20px;
        border: 1px solid #ddd;
        box-shadow: 0 0 10px rgba(0, 0, 0, .15);
        font-size: 16px;
        line-height: 24px;
        font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
        color: #555;
    }

    .invoice h2 {
        text-align: center;
        margin-bottom: 20px;
    }

    .invoice table {
        width: 100%;
        line-height: inherit;
        text-align: left;
    }

    .invoice table td, .invoice table th {
        padding: 12px;
        border: 1px solid #eee;
    }

    .invoice table th {
        background: #eee;
        text-align: center;
    }
    .fontstyle{
        font-weight:bold;
        font-size:16px;
    }
    .innerfont{
         font-weight:normal;
          font-size:16px;
    }
    .fontstyle2{


    }
/* Fontstyle */
#form1 div .fontstyle:nth-child(7){
 position:relative;
 top:-200px;
}

/* Fontstyle */
#form1 div .fontstyle:nth-child(8){
 position:relative;
 top:-200px;
}

/* Fontstyle */
#form1 div .invoice .fontstyle:nth-child(8){
 left:65% !important;
}

/* Invoice */
#form1 div .invoice{
 transform:translatex(0px) translatey(0px);
}

/* Fontstyle */
#form1 div .invoice .fontstyle:nth-child(7){
 left:65% !important;
}
</style>
    <script>
        function printPartOfPage(elementId) {
            var printContent = document.getElementById(elementId);
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="invoice" id="invoice">
        <h2>Invoice</h2>
        <p class="fontstyle">User Name: <asp:Label ID="lblUserName" CssClass="innerfont" runat="server"></asp:Label></p>
        <p  class="fontstyle">Address: <asp:Label ID="lblAddress" CssClass="innerfont" runat="server"></asp:Label></p>
        <p class="fontstyle">Phone: <asp:Label ID="lblPhone" CssClass="innerfont" runat="server"></asp:Label></p>
        <p class="fontstyle">Email: <asp:Label ID="lblEmail" CssClass="innerfont" runat="server"></asp:Label></p>
        <p class="fontstyle">Payment Method: <asp:Label ID="lblPaymentMethod" CssClass="innerfont" runat="server"></asp:Label></p>
        <p class="fontstyle fontstyle2 ">Order ID: <asp:Label ID="lblOrderID" CssClass="innerfont" runat="server"></asp:Label></p>
        <p class="fontstyle fontstyle2">Invoice Number: <asp:Label ID="lblInvoiceNumber" CssClass="innerfont" runat="server"></asp:Label></p>

        <asp:Repeater ID="rptItems" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Product Name</th>
                        <th>Per Unit Price</th>
                        <th>Quantity</th>
                        <th>Total</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><%# Eval("Name") %></td>
                        <td><%# Eval("PricePerUnit") %></td>
                        <td><%# Eval("Quantity") %></td>
                        <td><%# Eval("Total") %></td>
                    </tr>
               
            </ItemTemplate>
            <FooterTemplate>
                <ItemTemplate>
                 <tr>
                    <td colspan="4">
                     <p class="fontstyle">Final Total: <asp:Label ID="lblFinalTotal" runat="server"></asp:Label></p>
                    </td>
                </tr>
                </table>
                    </ItemTemplate>
            </FooterTemplate>
        </asp:Repeater>
       <input type="button" value="Print" onclick="JavaScript:printPartOfPage('invoice');"/>  
    </div>
</asp:Content>

