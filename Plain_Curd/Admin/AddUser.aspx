<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Plain_Curd.Admin.AddUser" %>

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
            $('#<%= txtFirstName.ClientID %>').val('');
            $('#<%= txtLastName.ClientID %>').val('');
            $('#<%= txtUserEmail.ClientID %>').val('');
            $('#<%= txtPhone.ClientID %>').val('');
            $('#<%= txtPassword.ClientID %>').val('');
            $('#<%= txtCPassword.ClientID %>').val('');
            $('#<%= txtAddress.ClientID %>').val('');
            $('#<%= ddlRole.ClientID %>').val('0');



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
    <div class="section login active">
        <h2 class="header">User List</h2>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="UserId" HeaderText="User ID" />
                    <asp:BoundField DataField="RoleId" HeaderText="Role ID" />
                    <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:BoundField DataField="State" HeaderText="State" />
                    <asp:BoundField DataField="Country" HeaderText="Country" />
                    <asp:BoundField DataField="UniqueKey" Visible="false" HeaderText="Unique Key" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("uniqueKey")+"+"+Eval("UserId")  %>' OnClick="btnEdit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("uniqueKey")+"+"+Eval("UserId") %>' OnClick="btnDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="section signup">
        <div class="contaier">
            <div class="header">
                <h2>Add User</h2>
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
                    <label for="exampleInputEmail1" style="padding: 10px;">Status *</label>
                    <asp:CheckBox ID="chkStatus" runat="server" Style="padding-left: 10px;" />
                </div>
                <div class="col">

                    <label for="exampleInputEmail1" style="padding: 10px;">Select Role *</label>
                    <asp:DropDownList ID="ddlRole" runat="server"></asp:DropDownList>
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
