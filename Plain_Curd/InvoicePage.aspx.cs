using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace Plain_Curd.Admin
{
    public partial class InvoicePage : System.Web.UI.Page
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
                BindInvoiceData();
            }
        }
        public void BindInvoiceData()
        {
            List<OrderDetailsByOrderId> order = GetInvoiceData();
          
            var payment = "";
            foreach(var item in order)
            {
                lblInvoiceNumber.Text = item.InvoiceNumber;
                payment = item.PaymentMode.ToString();
               
            }
            if (payment == "1")
            {
                lblPaymentMethod.Text = "Online";
            }
            else
            {
                lblPaymentMethod.Text = "COD";
            }
            
            rptItems.DataSource = order;
            rptItems.DataBind();
            Label lblFinalTotal = (Label)rptItems.Controls[rptItems.Controls.Count - 1].FindControl("lblFinalTotal");
            lblFinalTotal.Text = order.Sum(o => o.Total).ToString();
        }
        public void BindUser()
        {
            UserDetails u = GetUserDetails();
            lblUserName.Text = u.FirstName +" " + u.LastName;
            lblPhone.Text = u.Phone;
            lblAddress.Text = u.Address;
            lblEmail.Text = u.UserEmail;
            lblOrderID.Text = Session["OrderNo"].ToString();
        }
        public List<OrderDetailsByOrderId> GetInvoiceData()
        {
            var od = new List<OrderDetailsByOrderId>(); 
            var orderid= Session["OrderNo"].ToString();
            using(var conn = new SqlConnection(Connection))
            {
                using (var cmd = new SqlCommand("GetInvoiceData",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderno", orderid);
                    conn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             var o = new OrderDetailsByOrderId
                            {
                                OrderNo = reader["OrderNo"].ToString(),
                                Name = reader["Name"].ToString(),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                                FinalTotal = Convert.ToDecimal(reader["FinalTotal"]),
                                 Total = Convert.ToDecimal(reader["Total"]),
                                PaymentMode=reader["PaymentMode"].ToString(),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                PricePerUnit = Convert.ToDecimal(reader["PricePerUnit"])
                            };
                            od.Add(o);
                        }
                        
                    }
                }
            }
            return od;
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


        public class OrderDetailsByOrderId
        {
            public string OrderNo { get; set; }
            public string Name { get; set; }
            public DateTime OrderDate { get; set; }
            public string InvoiceNumber { get; set; }
            public decimal FinalTotal { get; set; }
            public decimal  Total { get; set; }
            public string PaymentMode { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerUnit { get; set; }
        }
        public class UserDetails
        {
            public string UserEmail { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }

        }
    }
}