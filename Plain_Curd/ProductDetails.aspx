<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="Plain_Curd.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabledesign{
            margin:30px;
        }
        .auto-style3 {
            width: 793px;
        }
        .auto-style4 {
            width: 150px;
        }
        .auto-style5 {
            width: 320px;
        }
        .auto-style6 {
            width: 44px;
            height: 39px;
        }
        .auto-style7 {
            width: 42px;
            height: 40px;
        }
    </style>
     
<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
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
    <asp:HiddenField ID="hfuniquekey" runat="server" Value="0" />
    <asp:HiddenField ID="hfslno" runat="server" Value="0" />
  <div class="tabledesign">  
    <table class="w-100 table" border="0">
        <tr>
            <td colspan="2" rowspan="11"><asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/UploadImage/" + Eval("image") %>'  height="780" width="480"/></td>
            
        </tr>
        <tr>
            <td class="auto-style4">Name</td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblName" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style4">Price</td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblprice" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style4">Dimenstions </td>
            <td class="auto-style3">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Height</td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblHeight" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style4">Width</td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblWidth" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style4">Weight</td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblWeight" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style4">Color</td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblColor" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style4">Item Code </td>
            <td class="auto-style3"><asp:Label runat="server" ID="lblItemCode" Text=""></asp:Label></td>
        </tr>
       <tr>
    <td class="auto-style4">Quantity</td>
    <td class="auto-style3"> 
        <asp:TextBox MaxLength="50" Style="padding-left: 10px;" AutoPostBack="true" cssclass="form-control" OnTextChanged="txtQuantity_TextChanged" onkeypress="return isNumberKey(event)" ID="txtQuantity" Text="1" runat="server" AutoComplete="off"></asp:TextBox>
        <input type="button" value="+" onclick="incrementValue(); updateTotalPrice();" class="auto-style7" />
        <input type="button" value="-" onclick="decrementValue(); updateTotalPrice();" class="auto-style6" />
    </td>
</tr>
<tr>
    <td class="auto-style4">Total Price </td>
    <td class="auto-style3"><asp:Label runat="server" ID="lblTotalPrice" Text=""></asp:Label></td>
</tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style4"><asp:Button runat="server" class="btn btn-dark" Text="Add To Cart" ID="btn_cart" onclick="btn_cart_Click" CommandArgument='<%#Eval("Slno")+"+"+Eval("uniquekey") %>'/></td>
            <td class="auto-style3"><asp:Button runat="server" class="btn btn-warning" Text="BUY" ID="btn_Buy" OnClick="btn_Buy_Click" /></td>
        </tr>
    </table>
      </div>
    
<script type="text/javascript">
    
    function incrementValue() {
        var value = parseInt(document.getElementById('<%= txtQuantity.ClientID %>').value, 10);
        value = isNaN(value) ? 0 : value;
        value++;
        document.getElementById('<%= txtQuantity.ClientID %>').value = value;
    }
    function updateTotalPrice() {
        __doPostBack('<%= txtQuantity.ClientID %>', '');
    }
    function decrementValue() {
        var value = parseInt(document.getElementById('<%= txtQuantity.ClientID %>').value, 10);
        value = isNaN(value) ? 0 : value;
        value < 1 ? value = 1 : '';
        value--;
        document.getElementById('<%= txtQuantity.ClientID %>').value = value;
    }
</script>

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</asp:Content>
