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
    public partial class InvoiceGenerate : System.Web.UI.Page
    { //public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            BindInvoiceList();

               
                if (Session["RoleId"] == null)
                {
                    Response.Redirect(@"~\Login.aspx");
                }
            }
        }

        public void BindInvoiceList()
        {
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("getInvoiceList", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    objSqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    objSqlConnection.Close();
                    ddlRole.DataSource = dt;
                    ddlRole.DataValueField = "Slno";
                    ddlRole.DataTextField = "InvoiceNumber";
                    ddlRole.DataBind();
                    var ls = new ListItem("Select Invoice Number", "0");
                    ddlRole.Items.Insert(0,ls);

                }
            }
        }

        protected void btn_invoice_Click(object sender, EventArgs e)
        {
            var index = ddlRole.SelectedItem.Value;
            if(index !="0" )
            {
                using(var sqlconn = new SqlConnection(Connection))
                {
                    using (var cmd = new SqlCommand("generateInvoice", sqlconn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@invoice",Convert.ToInt32( index));
                        cmd.Parameters.AddWithValue("@userid", Session["RoleId"]);
                        cmd.Connection.Open();
                        var result = cmd.ExecuteScalar();
                        if (Convert.ToInt32(result) == 1)
                        {
                           
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invoice Genrated Succefully');", true);
                            Response.Redirect(@"~\Admin\DashBoard.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error while genrating invoice');", true);
                        }
                    }
                }
            }
            else
            {
                string script = "alert('Please Select Invoice for Generating invoice);";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
    }
}