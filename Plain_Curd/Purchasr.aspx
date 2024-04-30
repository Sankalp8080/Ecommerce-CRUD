<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="Purchasr.aspx.cs" Inherits="Plain_Curd.Purchasr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .section {
            display: none;
            padding: 20px;
            border: 2px solid #000000;
            margin: 0 auto;
            width: 50%;
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

        .button-container {
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


            // Clear input fields in the signup section
            $('#<%= txtFirstName.ClientID %>').val('');
            $('#<%= txtLastName.ClientID %>').val('');
            $('#<%= txtUserEmail.ClientID %>').val('');
            $('#<%= txtPhone.ClientID %>').val('');
            $('#<%= txtAddress.ClientID %>').val('');


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
    <div class="section signup active">
        <div class="contaier" runat="server" id="signup">
            <div class="header">
                <h2>User Registration Page</h2>
            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">First Name *</label>
                    <asp:TextBox MaxLength="50" ReadOnly="true" Style="padding-left: 10px;" class="form-control" ID="txtFirstName" runat="server" AutoComplete="off" placeholder="Enter First Name"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Last Name *</label>
                    <asp:TextBox MaxLength="50" ReadOnly="true" Style="padding-left: 10px;" class="form-control" ID="txtLastName" runat="server" AutoComplete="off" placeholder="Enter Last Name"></asp:TextBox>

                </div>
            </div>
            <div class="row g-3">

                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Eamil ID *</label>
                    <asp:TextBox MaxLength="50" ReadOnly="true" Style="padding-left: 10px;" class="form-control" ID="txtUserEmail" runat="server" AutoComplete="off" placeholder="Enter Email"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Phone number *</label>
                    <asp:TextBox MaxLength="10" ReadOnly="true" Style="padding-left: 10px;" onkeypress="return isNumberKey(event)" class="form-control" ID="txtPhone" runat="server" AutoComplete="off" placeholder="Enter Phone number"></asp:TextBox>
                </div>

            </div>

            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Address *</label>
                    <asp:TextBox MaxLength="50" ReadOnly="true" Style="padding-left: 10px;" class="form-control" ID="txtAddress" TextMode="multiline" runat="server" AutoComplete="off" placeholder="Enter Address"></asp:TextBox>
                </div>
            </div>
            <div class="row g-3" style="padding-top: 10px;">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding: 10px;">Payment Mode *</label>
                </div>
                <div class="col">
                    <asp:RadioButtonList ID="PaymentModeList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">Online</asp:ListItem>
                        <asp:ListItem Value="0">COD</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row g-3">
                <div class="col  btn-sub">
                    <asp:Button runat="server" class="btn btn-warning" Text="Place Order" ID="btn_Submit" OnClick="btn_Submit_Click" />
                </div>

            </div>
            <div class="row g-3">
                <div class="col  btn-sub">
                    <label for="exampleInputEmail1" style="padding: 10px;">Total Amount :</label>
                 <asp:Label ID="lblTotalPrice" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
