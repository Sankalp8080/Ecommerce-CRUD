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
    public partial class OrderHistory : System.Web.UI.Page
    {
        // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                List<Order> orderList = getOrderHistory();
                if (orderList != null)
                {
                    OrderRepeater.DataSource = orderList;
                    OrderRepeater.DataBind();
                }
            }
            else
            {
                Response.Redirect("/UserLoginRegister.aspx");
            }
        }

        public class GetUserOrderHistory
        {
            public string Name { get; set; }
            public string OrderNo { get; set; }
            public long ProductID { get; set; }
            public int Quantity { get; set; }
            public decimal Amount { get; set; }
            public string image { get; set; }
            public string Status { get; set; }

        }
        public class Order
        {
            public string OrderID { get; set; }
            public string Status { get; set; }
            public List<GetUserOrderHistory> Items { get; set; }
        }

        public List<Order> getOrderHistory()
        {
            List<Order> orders = new List<Order>();
            var userid = Session["UserId"];
            using (var conn = new SqlConnection(Connection))
            {
                using (var cmd = new SqlCommand("orderHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        Order currentOrder = null;
                        while (reader.Read())
                        {
                            var orderNo = reader["OrderNo"].ToString();
                            var Status = reader["Status"].ToString();
                            if (currentOrder == null || currentOrder.OrderID != orderNo)
                            {
                                currentOrder = new Order { OrderID = orderNo, Status=Status, Items = new List<GetUserOrderHistory>() };
                                orders.Add(currentOrder);
                            }
                            GetUserOrderHistory orderHistory = new GetUserOrderHistory
                            {
                                OrderNo = orderNo,
                                ProductID = Convert.ToInt64(reader["ProductID"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                image = reader["image"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                            currentOrder.Items.Add(orderHistory);
                        }
                    }
                }
            }
            return orders;
        }


        protected void btn_invoice_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            Session["OrderNo"] = "";
            Session["OrderNo"] = btn.CommandArgument;
            Response.Redirect(@"/InvoicePage.aspx");
        }
    }
}