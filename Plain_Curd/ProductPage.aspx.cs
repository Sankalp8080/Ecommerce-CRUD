using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Plain_Curd
{
    public partial class ProductPage : System.Web.UI.Page
    {   // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetProduct();
            }
        }
        

        public void GetProduct()
        {
            List<ProductDetails> productDetails = BindProduct();
            ProductRepeater.DataSource = productDetails;
            ProductRepeater.DataBind();
        }

        protected void ProductImage_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)(sender);
            string[] args = btn.CommandArgument.Split('+');

            var uniquekey = args[0];
            var itemcode = args[1];
            Session["uniquekey"] = uniquekey;
            Session["itemcode"] = itemcode;
            Response.Redirect("~/ProductDetails.aspx");
        }
        public List<ProductDetails> BindProduct()
        {
            var product = new List<ProductDetails>();
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("userGetProduct", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductDetails productDetails = new ProductDetails
                            {
                                Name = reader["Name"].ToString(),
                                Price = decimal.Parse(reader["Price"].ToString()),
                                ItemCode = reader["itemcode"].ToString(),
                                UniqueKey = reader["uniquekey"].ToString(),
                                image=reader["image"].ToString()
                            };
                            product.Add(productDetails);
                        }
                    }
                }
            }
            return product;
        }
     
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            string[] args = btn.CommandArgument.Split('+');

            var uniquekey = args[0];
            var itemcode = args[1];
            AddToCart(uniquekey, itemcode);
        }

        public void AddToCart(string uniquekey, string itemcode)
        {
            ProductDetailsbyid product = GetProductById(uniquekey, itemcode);

           
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
    }
}