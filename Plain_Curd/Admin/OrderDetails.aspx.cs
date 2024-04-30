using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Plain_Curd.Admin
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        //public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderList();
                if (Session["RoleId"] == null)
                {
                    Response.Redirect(@"~\Login.aspx");
                }
            }
        }
        public void BindOrderList()
        {

            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("GetOrderData"))
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
        public class OrderDEtails
        {
            public int SlNo { get; set; }
            public string OrderNo { get; set; }
        }

        public OrderDEtails GetOrderDetails(int slno)
        {
            OrderDEtails order = null;
            var Slno = slno;
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("OrderDEtailsbyid", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@slno", slno);
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            order = new OrderDEtails
                            {
                                SlNo= Convert.ToInt32(reader["SlNo"]),
                                OrderNo = reader["OrderNo"].ToString()
                            };
                            }
                    }
                }
            }
            return order;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split('+');

            var slno = args[0];
            var orderno = args[1];
            var data = GetOrderDetails(Convert.ToInt32(slno));
            if (data != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddProductTab", "$(function() { $('.section').removeClass('active'); $('.signup').addClass('active');$('.button').removeClass('active');$(this).addClass('active');});", true);
                txtOrderNo.Text = data.OrderNo;
                hfSlno.Value = data.SlNo.ToString();
                btn_Submit.Text = "Update";
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            var orderNo = txtOrderNo.Text;
            var slno = Convert.ToInt32(hfSlno.Value);
            var userid = Session["RoleId"];
            var status = DropDownList1.SelectedItem.Text;

            if(DropDownList1.SelectedValue != "0")
            {
                using (var objSqlConnection = new SqlConnection(Connection))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateOrderStaus", objSqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@slno", slno);
                        cmd.Parameters.AddWithValue("@orderno", orderNo);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Connection.Open();
                        var result = cmd.ExecuteScalar();
                        switch (result)
                        {
                            case 200:
                                string script1 = "alert('Order Status updates Succsefully ');";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script1, true);
                                Response.Redirect(@"~\Admin\DashBoard.aspx");
                                break;
                            case 400:
                                string script11 = "alert('Error while updating order status ');";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script11, true);
                                Response.Redirect(@"~\Admin\OrderDetails.aspx");
                                break;
                            default:
                                string script2 = "alert('Error');";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script2, true);
                                Response.Redirect(@"~\Admin\Product.aspx");
                                break;

                        }
                    }
                }
            }
            else
            {
                string script = "alert('Please Select Status);";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }

        }
    }
}