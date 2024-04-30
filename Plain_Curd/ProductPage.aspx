<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="Plain_Curd.ProductPage" %>

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
            flex-wrap: wrap;
            justify-content: space-between;
        }

        .btn-cart {
            margin-left: 75px;
        }

        .borderimage {
            border: 1px solid;
            height:360px;
            width:270px;
        }

        .header {
            display: flex;
            position: relative;
            justify-content: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h2>Products</h2>
    </div>

    <div class="product-grid">
        <asp:Repeater ID="ProductRepeater" runat="server">
            <ItemTemplate>
                <div class="product">
                    <asp:ImageButton ID="ProductImage" runat="server" CssClass="borderimage" CommandArgument='<%# Eval("uniquekey")+"+"+Eval("itemcode") %>' ImageUrl='<%# "~/UploadImage/" + Eval("image") %>' OnClick="ProductImage_Click" />
                    Name:<h4><%# Eval("Name") %></h4>
                    Price:<p><%# Eval("price") %></p>
                    Item Code:   
                    <p><%# Eval("itemcode") %></p>
                    <asp:Label runat="server" Visible="false" Text='<%# Eval("uniquekey") %>' ID="lbluniquekey"></asp:Label>
                    <asp:Button runat="server" class="btn btn-dark btn-cart" Text="Add To Cart" ID="btnAddToCart" CommandArgument='<%# Eval("uniquekey")+"+"+Eval("itemcode") %>' OnClick="btnAddToCart_Click" />
                </div>
            </ItemTemplate>
        </asp:Repeater>


    </div>
</asp:Content>
