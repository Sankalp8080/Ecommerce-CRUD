<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="UserLoginRegister.aspx.cs" Inherits="Plain_Curd.UserLoginRegister" %>

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
            // Clear input fields in the login section
            $('#<%= txtUserID.ClientID %>').val('');
            $('#<%= txtUserPassword.ClientID %>').val('');

            // Clear input fields in the signup section
            $('#<%= txtFirstName.ClientID %>').val('');
            $('#<%= txtLastName.ClientID %>').val('');
            $('#<%= txtUserEmail.ClientID %>').val('');
            $('#<%= txtPhone.ClientID %>').val('');
            $('#<%= txtPassword.ClientID %>').val('');
            $('#<%= txtCPassword.ClientID %>').val('');
            $('#<%= txtAddress.ClientID %>').val('');

            // Uncheck the checkbox for terms and conditions if needed
            $('#<%= chkTerm.ClientID %>').prop('checked', false);

            // Return false to prevent default form submission
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
    <div id="buttoncontainer" class="button-container" runat="server">
        <div class="button active" id="login-button">Login</div>
        <div class="button" id="signup-button">Signup</div>
    </div>
    <div class="section login active">
        <div class="login" runat="server" id="login">
            <div class="header">
                <h2>User Login Page</h2>
            </div>

            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Eamil ID Or Phone No *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtUserID" runat="server" AutoComplete="off" placeholder="Enter Email Id or Phone no"></asp:TextBox>
                </div>
            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Passowrd *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtUserPassword" TextMode="password" runat="server" AutoComplete="off" placeholder="Enter Password"></asp:TextBox>
                </div>
            </div>

            <div class="row g-3">
                <div class="col  btn-sub" style="padding-top: 15px;">
                    <asp:Button runat="server" class="btn btn-danger" Text="Login" ID="btn_Login" OnClick="btn_Login_Click" />
                </div>
                <div class="col  btn-sub" style="padding-top: 15px;">
                    <asp:Button runat="server" class="btn btn-secondary" Text="Reset" ID="Button1" OnClientClick="resetForm(); return false;" />
                </div>
            </div>
        </div>
    </div>
    <div class="section signup">
        <div class="contaier" runat="server" id="signup">
            <div class="header">
                <h2>User Registration Page</h2>
            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">First Name *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtFirstName" runat="server" AutoComplete="off" placeholder="Enter First Name"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Last Name *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtLastName" runat="server" AutoComplete="off" placeholder="Enter Last Name"></asp:TextBox>

                </div>
            </div>
            <div class="row g-3">

                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Eamil ID *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtUserEmail" runat="server" AutoComplete="off" placeholder="Enter Email"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Phone number *</label>
                    <asp:TextBox MaxLength="10" Style="padding-left: 10px;" onkeypress="return isNumberKey(event)" class="form-control" ID="txtPhone" runat="server" AutoComplete="off" placeholder="Enter Phone number"></asp:TextBox>
                </div>

            </div>
            <div class="row g-3">

                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Passowrd *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtPassword" TextMode="password" runat="server" AutoComplete="off" placeholder="Enter Password"></asp:TextBox>
                </div>
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Confirm Password *</label>
                    <asp:TextBox MaxLength="10" Style="padding-left: 10px;" class="form-control" ID="txtCPassword" TextMode="password" runat="server" AutoComplete="off" placeholder="Enter Confirm Password"></asp:TextBox>
                </div>

            </div>
            <div class="row g-3">
                <div class="col">
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Address *</label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtAddress" TextMode="multiline" runat="server" AutoComplete="off" placeholder="Enter Address"></asp:TextBox>
                </div>
            </div>
            <div class="row g-3">
                <div class="col">
                    <asp:CheckBox ID="chkTerm" runat="server" Style="padding-left: 10px;" />
                    <label for="exampleInputEmail1" style="padding: 10px;">Term and Condition *</label>

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
    <div class="section" id="signout" runat="server">
        <div class="row g-3">
            <div class="col  btn-sub" style="padding-top: 15px;">
                <asp:Button runat="server" class="btn btn-danger" Text="SignOut" ID="Button2" OnClick="btn_logout_Click" />
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


