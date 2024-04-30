using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Plain_Curd.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        //  public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGrid();
                if (Session["RoleId"] == null)
                {
                    Response.Redirect(@"~\Login.aspx");
                }
            }
        }
        private void BindGrid()
        {

            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("getproductList"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    GridView1.DataSource = cmd.ExecuteReader();
                    GridView1.DataBind();
                    con.Close();
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split('+');

            var uniquekey = args[0];
            var Slno = args[1];
           var data= GetProductDetails(uniquekey, Convert.ToInt32(Slno));
            if (data != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddProductTab", "$(function() { $('.section').removeClass('active'); $('.signup').addClass('active');$('.button').removeClass('active');$(this).addClass('active');});", true);
                txtProductName.Text = data.name;
            txtPrice.Text = data.price.ToString();
            txtWeight.Text = data.weight.ToString();
            txtHeight.Text = data.height.ToString();
            txtWidth.Text = data.width.ToString();
            txtColor.Text = data.color;
            txtItemCode.Text = data.itemcode;
            txtQuantity.Text = data.quantity.ToString();
                hfSlno.Value = data.slno.ToString();
                hfuniquekey.Value = data.uniquekey.ToString();
                btn_Submit.Text = "Update";
            }
        }
        public ProductDetailsByID GetProductDetails(string uniquekey, int Slno)
        {
            ProductDetailsByID productdata = null;
            var unique = uniquekey;
            var slno = Slno;
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("GetProductByID", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uniquekey", unique);
                    cmd.Parameters.AddWithValue("@slno", slno);
                    cmd.Connection.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productdata = new ProductDetailsByID {
                                name = reader["Name"].ToString(),
                                price = Convert.ToDecimal(reader["Price"]),
                                weight = Convert.ToDecimal(reader["weight"]),
                                height = Convert.ToDecimal(reader["height"]),
                                width = Convert.ToDecimal(reader["width"]),
                                quantity = Convert.ToInt64(reader["quantity"]),
                                image = reader["image"].ToString(),
                                color = reader["color"].ToString(),
                                itemcode = reader["itemcode"].ToString(),
                                uniquekey = reader["uniquekey"].ToString(),
                                slno = Convert.ToInt32(reader["Slno"])

                            };
                        }
                    }
                }
            }
            return productdata;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split('+');

            var uniquekey = args[0];
            var Slno = args[1];
            var userid = Session["RoleId"];
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("deleteProductByID", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uniquekey", uniquekey);
                    cmd.Parameters.AddWithValue("@slno", Slno);
                    cmd.Parameters.Add(new SqlParameter("@userid", Convert.ToInt32(userid)));
                    cmd.Connection.Open();
                    var result = cmd.ExecuteScalar();
                    switch (result)
                    {
                        case 200:
                            string script1 = "alert('Product Succsefully deleted');";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script1, true);
                            Response.Redirect(@"~\Admin\DashBoard.aspx");
                            break;
                        case 400:
                            string script11 = "alert('Error while deleting Product ');";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script11, true);
                            Response.Redirect(@"~\Admin\Product.aspx");
                            break;
                        default:
                            string script2 = "alert('Error');";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script2, true);
                            Response.Redirect(@"~\Admin\Product.aspx");
                            break;

                    }
                }
            }
                    // Your code to delete the item with the given uniqueKey
                }
        public class ProductDetailsByID
        {
            public int slno { get; set; }
            public string uniquekey { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
            public decimal weight { get; set; }
            public decimal height { get; set; }
            public decimal width { get; set; }
            public long quantity { get; set; }
            public string image { get; set; }
            public string color { get; set; }
            public string itemcode { get; set; }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            var productname = txtProductName.Text;
            var price = txtPrice.Text;
            var weight = txtWeight.Text;
            var width = txtWidth.Text;
            var height = txtHeight.Text;
            var quantity = txtQuantity.Text;
            var color = txtColor.Text;
            var itemcode = txtItemCode.Text;
            var userid = Session["RoleId"];
            var uniquekey = hfuniquekey.Value == "0" ? Guid.NewGuid() : new Guid(hfuniquekey.Value);
            var slno = hfSlno.Value == "0" ? 0 : Convert.ToInt32(hfSlno.Value);
            var path = "";
            if (productimage.HasFile)
                productimage.SaveAs(HttpContext.Current.Request.PhysicalApplicationPath + "UploadImage/" + productimage.FileName);
            path = productimage.FileName;

            if (!string.IsNullOrEmpty(productname))
            {
                if (!string.IsNullOrEmpty(price) && Convert.ToDecimal(price) > 0)
                {
                    if (!string.IsNullOrEmpty(weight) && Convert.ToDecimal(weight) > 0)
                    {
                        if (!string.IsNullOrEmpty(width) && Convert.ToDecimal(width) > 0)
                        {
                            if (!string.IsNullOrEmpty(height) && Convert.ToDecimal(height) > 0)
                            {
                                if (!string.IsNullOrEmpty(quantity) && Convert.ToInt16(quantity) > 0)
                                {
                                    if (!string.IsNullOrEmpty(color))
                                    {
                                        if (!string.IsNullOrEmpty(itemcode))
                                        {
                                            
                                                using (var objSqlConnection = new SqlConnection(Connection))
                                                {
                                                    using (SqlCommand cmd = new SqlCommand("ProductAddUpdate", objSqlConnection))
                                                    {
                                                        cmd.CommandType = CommandType.StoredProcedure;
                                                        cmd.Parameters.Add(new SqlParameter("@slno", slno));
                                                        cmd.Parameters.Add(new SqlParameter("@uniquekey", uniquekey));
                                                        cmd.Parameters.Add(new SqlParameter("@name", productname));
                                                        cmd.Parameters.Add(new SqlParameter("@price", Convert.ToDecimal(price)));
                                                        cmd.Parameters.Add(new SqlParameter("@weight", Convert.ToDecimal(weight)));
                                                        cmd.Parameters.Add(new SqlParameter("@height", Convert.ToDecimal(height)));
                                                        cmd.Parameters.Add(new SqlParameter("@width", Convert.ToDecimal(width)));
                                                        cmd.Parameters.Add(new SqlParameter("@quantity", Convert.ToInt64(quantity)));
                                                        cmd.Parameters.Add(new SqlParameter("@image", path));
                                                        cmd.Parameters.Add(new SqlParameter("@color", color));
                                                        cmd.Parameters.Add(new SqlParameter("@itemcode", itemcode));
                                                        cmd.Parameters.Add(new SqlParameter("@useid", Convert.ToInt32(userid)));
                                                        objSqlConnection.Open();
                                                        var result = cmd.ExecuteScalar();
                                                        switch (result)
                                                        {
                                                            case 200:
                                                                string script = "alert('Product Updated Successfully);";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                                                Response.Redirect(@"~\Admin\DashBoard.aspx");
                                                                break;
                                                            case 201:
                                                                string script1 = "alert('Product Added Succsefully');";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script1, true);
                                                                Response.Redirect(@"~\Admin\DashBoard.aspx");
                                                                break;
                                                            case 401:
                                                                string script11 = "alert('Error while adding Product or item code already exists');";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script11, true);
                                                                Response.Redirect(@"~\Admin\Product.aspx");
                                                                break;
                                                            default:
                                                                string script2 = "alert('Already exits);";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script2, true);
                                                                Response.Redirect(@"~\Admin\Product.aspx");
                                                                break;

                                                        }
                                                    }
                                                }
                                            
                                           
                                           
                                        }
                                        else
                                        {
                                            string script = "alert('Please Enter Item Code');";
                                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                        }
                                    }
                                    else
                                    {
                                        string script = "alert('Please Enter Color Name');";
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                    }
                                }
                                else
                                {
                                    string script = "alert('Please Enter Quantity');";
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                }
                            }
                            else
                            {
                                string script = "alert('Please Enter Height');";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                            }
                        }
                        else
                        {
                            string script = "alert('Please Enter Width');";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                        }
                    }
                    else
                    {
                        string script = "alert('Please Enter Weight');";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    }
                }
                else
                {
                    string script = "alert('Please Enter Price');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
            else
            {
                string script = "alert('Please Enter Product Name');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
    }
}