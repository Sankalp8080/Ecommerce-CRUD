using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Plain_Curd
{
    public partial class cartpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cart"] != null)
                {

                    List<ProductDetailsbyid> cart = (List<ProductDetailsbyid>)Session["Cart"];
                    
                    CartRepeater.DataSource = cart;
                    decimal totalPrice = cart.Sum(p => p.Price * 1);
                    
                    lblTotalPrice.Text = totalPrice.ToString();
                    CartRepeater.DataBind();
                }
                else
                {
                    Response.Redirect("/ProductPage.aspx");
                }
            }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            List<ProductDetailsbyid> cart = (List<ProductDetailsbyid>)Session["Cart"];
            CartRepeater.DataSource = cart;

            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            Label lblPrice = (Label)item.FindControl("lblPrice");
            decimal price = cart.Sum(p => p.Price);
            
            int quantity = Int32.Parse(txtQuantity.Text);
            foreach(var items in cart)
            {
                items.quantity = quantity;
                
            }
         
            decimal total = 1;
            if (quantity > 1)
            {

                total = price * quantity;
            }
            else
            {
                total = price * 1;
                cart[0].quantity = 1;
            }
            lblTotalPrice.Text = total.ToString();
            Session["TotalPrice"] = lblTotalPrice.Text;

        }


        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);

            string uniquekey = btn.CommandArgument;

            List<ProductDetailsbyid> cart = Session["Cart"] as List<ProductDetailsbyid>;
            ProductDetailsbyid productToRemove = cart.FirstOrDefault(p => p.uniquekey.ToString() == uniquekey);
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
            if (productToRemove != null)
            {

                cart.Remove(productToRemove);


                Session["Cart"] = cart;

                CartRepeater.DataSource = cart;
               foreach(var items in cart)
                {
                    items.quantity= Convert.ToInt64(txtQuantity.Text);
                }
                    //cart[0].quantity = Convert.ToInt64(txtQuantity.Text);
            
                CartRepeater.DataBind();
                decimal totalPrice = cart.Sum(p => p.Price * Int32.Parse((txtQuantity).Text));
                lblTotalPrice.Text = totalPrice.ToString();
                Session["TotalPrice"] = lblTotalPrice.Text;

            }
        }

        protected void btnClearCart_Click(object sender, EventArgs e)
        {
            Session["Cart"] = null;
            lblTotalPrice.Text = "0";
            CartRepeater.DataSource = null;
            CartRepeater.DataBind();
        }

        protected void btn_Continue_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ProductPage.aspx");
        }

        protected void btn_Buy_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("/UserLoginRegister.aspx");
            }
            else
            {
                Session["TotalPrice"] = lblTotalPrice.Text;
                Response.Redirect("/Purchasr.aspx");

            }
        }
    }
}