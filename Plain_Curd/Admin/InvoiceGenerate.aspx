<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="InvoiceGenerate.aspx.cs" Inherits="Plain_Curd.Admin.InvoiceGenerate" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section login active">
        <h2 class="header">Invoice Generation</h2>
        <div>
            <div class="row g-3">

                <div class="col" style="margin-left: 35%; margin-top: 20px;">

                    <label for="exampleInputEmail1" style="padding: 10px;">Select Order Number: *</label>
                    <asp:DropDownList ID="ddlRole" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row g-3">
                <div class="col  btn-sub">
                    <asp:Button runat="server" class="btn btn-danger" Text="Generate" ID="btn_invoice" OnClick="btn_invoice_Click"   />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
