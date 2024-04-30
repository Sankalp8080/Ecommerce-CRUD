<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="Plain_Curd.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
    .product {
        border: 1px solid black;
        padding: 20px;
        margin: 20px;
        box-sizing: border-box;
        flex: 1 0 10%;
        max-width: 21%;
    }

    .product-grid {
        display: flex;
        flex-direction: column;
        border :1px solid;
    }

    .btn-cart {
        margin-left: 75px;
    }

    .borderimage {
        border: 1px solid;
         height:360px;
            width:270px;
    }
    .btn-sub {
        
        position: relative;
        justify-content: end;
        padding-top:100%;
    }
    .header {
        display: flex;
        position: relative;
        justify-content: center;
    }
     .productd{
           border: 1px solid black;
           display:flex;
           width:100%;
           
     }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
                <h2>Order History</h2>
            </div>
     <div class="product-grid">
   
        <asp:Repeater ID="OrderRepeater" runat="server">
    <ItemTemplate>
        <div class="productd">
        Order ID: <%# Eval("OrderID") %>
            Status :<%#Eval("Status") %>
        <asp:Repeater ID="CartRepeater" runat="server" DataSource='<%# Eval("Items") %>'>
            <ItemTemplate>
                <div class="product">
                    <asp:Image ID="ProductImage" runat="server" CssClass="borderimage" CommandArgument='<%#  Eval("ProductID") %>' ImageUrl='<%# "~/UploadImage/" + Eval("image") %>' />
                    Order No :<p><%# Eval("OrderNo") %></p>
                    Name:<p><%# Eval("Name") %></p> 
                    Quantity:<p><%# Eval("quantity") %></p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="row g-3">
            <div class="col  btn-sub">
                <asp:Button runat="server" class="btn btn-dark" Text="Download Invoice" CommandArgument='<%#  Eval("OrderID") %>' ID="btn_invoice" OnClick="btn_invoice_Click" />  
            </div>
        </div></div>
    </ItemTemplate>
</asp:Repeater>

    </div>
</asp:Content>
