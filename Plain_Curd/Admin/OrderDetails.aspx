<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Plain_Curd.Admin.OrderDetails" %>

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
           
            $('#<%= DropDownList1.ClientID %>').val('0');



            return false;
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
        <h2 class="header">Order List</h2>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="SlNo" HeaderText="SlNo" />
                    <asp:BoundField DataField="OrderNo" HeaderText="OrderNo" />
                    <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" />
                    <asp:BoundField DataField="UserID" HeaderText="UserID" />
                    <asp:BoundField DataField="UserType" HeaderText="UserType" />
                    <asp:BoundField DataField="payment" HeaderText="Payment Mode" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                     
                   
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("SlNo")+"+"+Eval("OrderNo")  %>' onclick="btnEdit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                  <%--  <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("SlNo")+"+"+Eval("OrderNo") %>'   />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
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
                    <label for="exampleInputEmail1" style="padding-left: 10px;">Order Number </label>
                    <asp:TextBox MaxLength="50" Style="padding-left: 10px;" class="form-control" ID="txtOrderNo" runat="server" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col">

                    <label for="exampleInputEmail1" style="padding: 10px;">Select Status *</label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Deliver" Value="1"></asp:ListItem>

                    </asp:DropDownList>
                </div>
            </div>
            
            <div class="row g-3">
                <div class="col  btn-sub">
                    <asp:Button runat="server" class="btn btn-danger" Text="Register" ID="btn_Submit"  OnClick="btn_Submit_Click"/>
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
