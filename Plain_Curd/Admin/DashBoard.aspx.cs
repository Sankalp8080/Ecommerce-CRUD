using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Plain_Curd.Admin
{
    public partial class DashBoard : System.Web.UI.Page
    {
        // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindData();
                if (Session["RoleId"] == null)
                {
                    Response.Redirect(@"~\Login.aspx");
                }
            }
        }

        public class Data
        {
            public int TotalUser { get; set; }
            public int TotalOrder { get; set; }
            public int TotalProduct { get; set; }
        }

        public void BindData()
        {
            var d = GetData();
            lblOrder.Text = d.TotalOrder.ToString();
            lblProduct.Text = d.TotalProduct.ToString();
            lblUser.Text = d.TotalUser.ToString();
        }
        public Data GetData()
        {
            Data data = new Data();
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("GetDashBoardData", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.TotalUser = Convert.ToInt32(reader["TotalUser"]);
                        }

                        if (reader.NextResult() && reader.Read())
                        {
                            data.TotalOrder = Convert.ToInt32(reader["TotalOrder"]);
                        }

                        if (reader.NextResult() && reader.Read())
                        {
                            data.TotalProduct = Convert.ToInt32(reader["TotalProduct"]);
                        }
                    }
                }
            }
            return data;
        }
        
    }
}