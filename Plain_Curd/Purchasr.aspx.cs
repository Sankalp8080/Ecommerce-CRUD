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
    public partial class Purchasr : System.Web.UI.Page
    {
        // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("/UserLoginRegister.aspx");
                }
                BindUser();
                lblTotalPrice.Text = Session["TotalPrice"].ToString();
            }
        }
        public void BindUser()
        {
            UserDetails u = GetUserDetails();
            txtFirstName.Text = u.FirstName;
            txtLastName.Text = u.LastName;
            txtPhone.Text = u.Phone;
            txtAddress.Text = u.Address;
            txtUserEmail.Text = u.UserEmail;

        }
        public UserDetails GetUserDetails()
        {
            UserDetails userdetails = null;
            var userid = Session["UserId"];
            using (var conn = new SqlConnection(Connection))
            {
                using (var cmd = new SqlCommand("getUserDetailsByid", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Connection.Open();
                    using (var result = cmd.ExecuteReader())
                    {
                        if (result != null)
                        {
                            while (result.Read())
                            {
                                userdetails = new UserDetails
                                {
                                    UserEmail = result["UserEmail"].ToString(),
                                    FirstName = result["FirstName"].ToString(),
                                    LastName = result["LastName"].ToString(),
                                    Phone = result["Phone"].ToString(),
                                    Address = result["Address"].ToString()
                                };
                            }

                        }
                    }

                }
            }
            return userdetails;
        }
        public class UserDetails
        {
            public string UserEmail { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            var orderId = "ORD" + DateTime.Now.ToString("ddMMyyyyHHmm") + Guid.NewGuid().ToString("N").Substring(0, 8); 
            var Inv = "INV" + DateTime.Now.ToString("ddMMyyyyHHmm") + Guid.NewGuid().ToString("N").Substring(0, 8); 
            //var payment = PaymentModeList.SelectedItem.Value.ToString() == null ? "-1" : PaymentModeList.SelectedItem.Value.ToString();
            if (PaymentModeList.SelectedItem!= null)
            {
                var payment = PaymentModeList.SelectedItem.Value.ToString();
            List<ProductDetailsbyid> cart = (List<ProductDetailsbyid>)Session["Cart"];
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                objSqlConnection.Open();
                foreach (var item in cart)
                {
                    using (SqlCommand cmd = new SqlCommand("placeOrder", objSqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", Session["UserId"]);
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@amount", Session["TotalPrice"]);
                        cmd.Parameters.AddWithValue("@usertype", Session["RoleID"]);
                        cmd.Parameters.AddWithValue("@productid", item.itemcode);
                        cmd.Parameters.AddWithValue("@quantity", item.quantity);
                        cmd.Parameters.AddWithValue("@productprice", item.Price);
                        cmd.Parameters.AddWithValue("@payment", payment);
                        cmd.Parameters.AddWithValue("@InvoiceNo", Inv);
                        var result = cmd.ExecuteScalar();
                        if (result.ToString() == "2")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Item Quantity is not available at this time');", true);
                            return;
                        }
                    }
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Order Placed Successfully');", true);
                Response.Redirect("~/Home.aspx");
            }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select payment method');", true);
            }
        }


    }
}