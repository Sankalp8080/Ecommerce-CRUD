<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="cartpage.aspx.cs" Inherits="Plain_Curd.cartpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
          .header {
            display: flex;
            position: relative;
            justify-content: center;
        }
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
            flex-wrap: wrap;
            justify-content: space-between;
        }

        .btn-cart {
            margin-left: 75px;
        }

        .borderimage {
            border: 1px solid;
        }
          .btn-sub {
            display: flex;
            position: relative;
            justify-content: center;
        }
          
    </style>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                alert("Please enter numeric values.");

                return false;
            }
            return true;
        }
        function isNumberKeyDecimal(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var inputValue = evt.target.value;
            if ((charCode >= 48 && charCode <= 57) || charCode === 46) {
                if (charCode === 46 && inputValue.indexOf('.') !== -1) {
                    alert("Only single decimal point is allowed.");
                    return false;

                }

                return true;
            }
            else {
                alert(" Please enter numeric values only.");

                return false;
            }
        }
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
                <h2>Cart</h2>
            </div>

    <div class="product-grid">
        <asp:Repeater ID="CartRepeater" runat="server">

            <ItemTemplate>
                <div class="product">
                    <asp:Image ID="ProductImage" runat="server" CssClass="borderimage" CommandArgument='<%# Eval("uniquekey")+"+"+Eval("itemcode") %>' ImageUrl='<%# "~/UploadImage/" + Eval("image") %>' />
                    Name:<h4><%# Eval("Name") %></h4>
                    Price:<p><%# Eval("price") %></p>
                    Item Code:   
                    <p><%# Eval("itemcode") %></p>
                    Select Quantity<asp:TextBox ID="txtQuantity" runat="server" Text="1" AutoPostBack="true" onkeypress="return isNumberKey(event)" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                     <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("uniquekey") %>' OnClick="btnRemove_Click" />
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
    <div class="row g-3">
        <div class="col  btn-sub">
        <asp:Button ID="btnClearCart" runat="server" Text="Clear Cart" OnClick="btnClearCart_Click" />
        </div>
    </div> 
    <div class="row g-3">
        <div class="col  btn-sub">
         Total Amount :  <asp:Label ID="lblTotalPrice" runat="server" Text=""></asp:Label>
        </div>
    </div> <div class="row g-3">
        <div class="col  btn-sub">
            <asp:Button runat="server" class="btn btn-dark" Text="Continue To Shipping" ID="btn_Continue" OnClick="btn_Continue_Click" /> &nbsp;
            <asp:Button runat="server" class="btn btn-warning" Text="       BUY       " ID="btn_Buy" OnClick="btn_Buy_Click" />
        </div>
    </div>
</asp:Content>
