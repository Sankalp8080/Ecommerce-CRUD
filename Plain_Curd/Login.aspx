<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Plain_Curd.Login" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <title>Login-Page</title>
   <style>
        body {
     display: flex;
     align-items: center;
     justify-content: center;
     height: 100vh;
     margin: 0;
 }

 .form-container {
     border: 1px solid #ccc;
     border-radius: 10px;
     padding: 20px;
     max-width: 400px;
     width: 100%;
     box-shadow: 0 0 10px rgba(0, 0, 0, 0.4);
 }

 .center-btn {
     display: flex;
     justify-content: center;
     margin-top: 15px;
     
 }
    </style>

</head>
<body>
 <form runat="server" class="form-container">
    <div class="form-group">
        <label for="exampleInputEmail1">Email address</label>
        <asp:TextBox MaxLength="50" class="form-control" ID="txtUserEmail" runat="server" AutoComplete="off" placeholder="Enter email"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="exampleInputPassword1">Password</label>
        <asp:TextBox MaxLength="50" class="form-control" ID="txtPassword" runat="server" AutoComplete="off" placeholder="Enter password" TextMode="Password"></asp:TextBox>
    </div>
    <div class="center-btn">
        <asp:Button runat="server" class="btn btn-danger" Text="Submit" ID="btn_Submit" onclick="btn_SubmitClicked"/>
    </div>
</form>
    
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</body>
</html>
