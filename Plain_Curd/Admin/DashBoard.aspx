<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Plain_Curd.Admin.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 332px;
            margin-top: 175px;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
        }
        .auto-style1 td, .auto-style1 th {
            border: 1px solid #ddd;
            padding: 65px;
        }
        .auto-style1 tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        .auto-style1 tr:hover {
            background-color: #ddd;
        }
        .auto-style1 th {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #808080;
            color: white;
        }
        .center-text {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="center-text">DashBoard</h2>
    <table class="auto-style1">
        <tr>
            <th>Total Users</th>
            <th>Total Products</th>
            <th>Total Orders</th>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="lblUser" Text=""></asp:Label></td>
            <td><asp:Label runat="server" ID="lblProduct" Text=""></asp:Label></td>
            <td><asp:Label runat="server" ID="lblOrder" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>
