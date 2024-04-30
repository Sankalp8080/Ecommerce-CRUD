<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Plain_Curd.Admin.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .section {
            display: none;
            padding: 20px;
            border: 2px solid #000000;
            margin: 0 auto;
            width: 80%;
            border-radius: 10px;
        }

        .active {
            display: block;
            align-content: center;
        }
        /* Styling for the buttons */
        .button {
            background-color: #fff;
            color: #000;
            padding: 10px 35px;
            cursor: pointer;
            border-radius: 5px;
            transition: background-color 0.3s ease;
            width: 120px;
            margin: 10px;
            border: 2px solid #000;
        }

            .button.active {
                background-color: #ff0000;
                color: #fff;
            }

        #button-container {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .header {
            display: flex;
            position: relative;
            justify-content: center;
        }

        .btn-sub {
            display: flex;
            position: relative;
            justify-content: center;
        }

        .login {
            padding-left: 10%;
            padding-right: 10%;
            justify-content: center;
        }
    </style>
    <script>
        function resetForm() {
            $('#<%= txtProductName.ClientID %>').val('');
            $('#<%= txtPrice.ClientID %>').val('');
            $('#<%= txtQuantity.ClientID %>').val('');
            $('#<%= txtWeight.ClientID %>').val('');
            $('#<%= txtWidth.ClientID %>').val('');
            $('#<%= txtHeight.ClientID %>').val('');
            $('#<%= txtColor.ClientID %>').val('');
            $('#<%= txtItemCode.ClientID %>').val('');
            $('#<%= productimage.ClientID %>').val('');



            return false;
        }
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
    <asp:HiddenField ID="hfuniquekey" runat="server" Value="0" />
    <asp:HiddenField ID="hfSlno" runat="server" Value="0" />
    <div id="button-container">
        <div class="button active" id="login-button">List</div>
        <div class="button" id="signup-button">Add</div>
    </div>
    <div class="section login active" runat="server" id="list">

        <h2 class="header">Product List</h2>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Slno" Visible="false" HeaderText="Product ID" />
                    <asp:BoundField DataField="Name" HeaderText="Product Name" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="weight" HeaderText="Weight" />
                    <asp:BoundField DataField="height" HeaderText="Height" />
                    <asp:BoundField DataField="width" HeaderText="Width" />
                    <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                        
                    <asp:TemplateField HeaderText="Product Image">
            <ItemTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/UploadImage/" + Eval("image") %>'  height="200" width="150"/>
            </ItemTemplate>
        </asp:TemplateField>
                    <asp:BoundField DataField="color" HeaderText="Color" />
                    <asp:BoundField DataField="itemcode" HeaderText="Item Code" />
                   
                    <asp:BoundField DataField="uniqueKey" Visible="false" HeaderText="Unique Key" />
                   <asp:TemplateField HeaderText="Edit">
            <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("uniqueKey")+"+"+Eval("Slno")  %>' OnClick="btnEdit_Click" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Delete">
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("uniqueKey")+"+"+Eval("Slno") %>' OnClick="btnDelete_Click" />
            </ItemTemplate>
        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="section signup" runat="server" id="signup">
        <div class="contaier">
            <div class="header">
                <h2>Add Product</h2>
            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Product Name *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtProductName" runat="server" AutoComplete="off" placeholder="Enter Product Name"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Price *</label>
                    <asp:TextBox MaxLength="10" Style="padding-left: 10px;" onkeypress="return isNumberKeyDecimal(event);" class="form-control" ID="txtPrice" runat="server" AutoComplete="off" placeholder="Enter Price"></asp:TextBox>

                </div>
            </div>
            <div class="row g-3">

                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Quantity  *</label>
                    <asp:TextBox MaxLength="5" Style="padding-left: 10px;" onkeypress="return isNumberKey(event)" class="form-control" ID="txtQuantity" runat="server" AutoComplete="off" placeholder="Enter Quantity"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Weight *</label>
                    <asp:TextBox MaxLength="10" Style="padding-left: 10px;" onkeypress="return isNumberKeyDecimal(event);" class="form-control" ID="txtWeight" runat="server" AutoComplete="off" placeholder="Enter Weight"></asp:TextBox>
                </div>

            </div>
            <div class="row g-3">

                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Width *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" onkeypress="return isNumberKeyDecimal(event);" class="form-control" ID="txtWidth" runat="server" AutoComplete="off" placeholder="Enter Width"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Height *</label>
                    <asp:TextBox MaxLength="10" Style="padding-left: 10px;" onkeypress="return isNumberKeyDecimal(event);" class="form-control" ID="txtHeight" runat="server" AutoComplete="off" placeholder="Enter Height "></asp:TextBox>
                </div>

            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Color  *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtColor" runat="server" AutoComplete="off" placeholder="Enter Color"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Item Code *</label>
                    <asp:TextBox MaxLength="10" Style="padding-left: 10px;" class="form-control" ID="txtItemCode" runat="server" AutoComplete="off" placeholder="Enter Item Code "></asp:TextBox>
                </div>
            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Upload Product Image *</label>

                    <asp:FileUpload ID="productimage" Style="padding-left: 10px;" class="form-control" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </div>
                <div class="col">
                    <asp:CheckBox ID="chkStatus" runat="server" Style="padding-left: 10px;" />
                    <label for="exampleInputEmail1" style="padding: 10px;">Status *</label>

                </div>
            </div>
            <div class="row g-3">
                <div class="col  btn-sub">
                    <asp:Button runat="server" class="btn btn-danger" Text="Register" ID="btn_Submit" OnClick="btn_Submit_Click" />
                </div>
                <div class="col  btn-sub">
                    <asp:Button runat="server" class="btn btn-secondary" Text="Reset" ID="btn_reset2" OnClientClick="resetForm(); return false;" />
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            // Initial state: Show login section, hide signup section
            $('.login').addClass('active');
            $('.signup').removeClass('active');


            $('#login-button').click(function () {
                $('.section').removeClass('active');
                $('.login').addClass('active');
                $('.button').removeClass('active');
                $(this).addClass('active');
            });
            $('#signup-button').click(function () {
                $('.section').removeClass('active');
                $('.signup').addClass('active');
                $('.button').removeClass('active');
                $(this).addClass('active');
            });
        });
    </script>
</asp:Content>
