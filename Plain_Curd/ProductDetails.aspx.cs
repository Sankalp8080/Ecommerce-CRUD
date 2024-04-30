using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Plain_Curd
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Session["uniquekey"] == null && Session["itemcode"] == null)
                {
                    Response.Redirect("ProductPage.aspx");
                }
                BindProduct();
            }

        }
      
       
        public ProductDetailsbyid GetproductDetails()
        {
            ProductDetailsbyid productdetails = null;
            var uniquekey = Session["uniquekey"];
            var itemcode = Session["itemcode"];
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("usergetProductDetailsbyId", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@uniquekey", uniquekey);
                    cmd.Connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productdetails = new ProductDetailsbyid
                            {
                                Slno = Convert.ToInt32(reader["Slno"]),
                                uniquekey = new Guid(reader["uniquekey"].ToString()),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                weight = Convert.ToDecimal(reader["weight"]),
                                width = Convert.ToDecimal(reader["width"]),
                                height = Convert.ToDecimal(reader["height"]),
                                quantity = Convert.ToInt64(reader["quantity"]),
                                image = reader["image"].ToString(),
                                color = reader["color"].ToString(),
                                itemcode = reader["itemcode"].ToString()
                            };
                        }
                    }
                }
            }
            return productdetails;
        }

        public void BindProduct()
        {
            ProductDetailsbyid p = GetproductDetails();
            lblName.Text = p.Name;
            lblColor.Text = p.color;
            lblItemCode.Text = p.itemcode;
            lblHeight.Text =p.height.ToString();
            lblWeight.Text = p.weight.ToString();
            lblWidth.Text = p.width.ToString();
            lblprice.Text = p.Price.ToString();
            Image1.ImageUrl = "~/UploadImage/" + p.image.ToString();
            hfslno.Value = p.Slno.ToString();
            hfuniquekey.Value = p.uniquekey.ToString();

        }

        protected void btn_cart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            string[] args = btn.CommandArgument.Split('+');

            var uniquekey = hfuniquekey.Value;
            var itemcode = hfslno.Value;
            AddToCart(uniquekey, itemcode);
        }

        public ProductDetailsbyid GetProductById(string uniquekey, string itemcode)
        {
            ProductDetailsbyid product = null;

            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("usergetProductDetailsbyId", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@uniquekey", uniquekey);
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product = new ProductDetailsbyid
                            {
                                Slno = Convert.ToInt32(reader["Slno"]),
                                uniquekey = new Guid(reader["uniquekey"].ToString()),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                weight = Convert.ToDecimal(reader["weight"]),
                                width = Convert.ToDecimal(reader["width"]),
                                height = Convert.ToDecimal(reader["height"]),
                                quantity = Convert.ToInt64(reader["quantity"]),
                                image = reader["image"].ToString(),
                                color = reader["color"].ToString(),
                                itemcode = reader["itemcode"].ToString()
                            };
                        }
                    }
                }
            }

            return product;
        }
        public void AddToCart(string uniquekey, string itemcode)
        {
            ProductDetailsbyid product = GetProductById(uniquekey, itemcode);
            product.quantity = Convert.ToInt64(txtQuantity.Text);

            if (Session["Cart"] == null)
            {

                List<ProductDetailsbyid> cart = new List<ProductDetailsbyid>();
                cart.Add(product);
                Session["Cart"] = cart;
            }
            else
            {

                List<ProductDetailsbyid> cart = (List<ProductDetailsbyid>)Session["Cart"];


                ProductDetailsbyid existingProduct = cart.FirstOrDefault(p => p.uniquekey == product.uniquekey && p.itemcode == product.itemcode);
                if (existingProduct != null)
                {

                    cart.Remove(existingProduct);
                }


                cart.Add(product);


                Session["Cart"] = cart;
            }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            decimal price = Convert.ToDecimal(lblprice.Text);
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal Totalprice = price * quantity;
            lblTotalPrice.Text = Totalprice.ToString();
        }
            protected void btn_Buy_Click(object sender, EventArgs e)
        {
            var uniquekey = hfuniquekey.Value;
            var itemcode = hfslno.Value;
            AddToCart(uniquekey, itemcode);

            Session["TotalPrice"] = lblTotalPrice.Text;
            Response.Redirect("/Purchasr.aspx");
        }
    }
}