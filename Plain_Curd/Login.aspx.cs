using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Plain_Curd
{
    public partial class Login : System.Web.UI.Page
    {
       // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void btn_SubmitClicked(object sender, EventArgs e)
        {
            var useremail = txtUserEmail.Text;
            var password = txtPassword.Text;
            if (!string.IsNullOrEmpty(useremail))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    using (var objSqlConnection = new SqlConnection(Connection))
                    {
                        using (SqlCommand cmd = new SqlCommand("loginCheck", objSqlConnection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserEmail", useremail);
                            cmd.Parameters.AddWithValue("@Password", password);

                           
                                objSqlConnection.Open();
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader != null)
                                    {
                                        if (reader.HasRows) {
                                            while (reader.Read())
                                            {
                                                Session["FirstName"] = reader["FirstName"].ToString();
                                                //var role = reader["RoleId"].ToString();
                                                int role = (int)reader["RoleId"];
                                                Session["RoleId"] = role;
                                                CheckRole(role);
                                            }
                                        }
                                        else
                                        {
                                            string script = "alert('Wrong email ID or password. Please try again.');";
                                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                        }

                                    }
                                    else
                                    {
                                        string script = "alert('Wrong email ID or password. Please try again.');";
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                    }
                                }
                              
                          
                        }
                    }
                }
                else
                {
                    string script = "alert('Please enter Password.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
            else
            {
                string script = "alert('Please enter Email Id.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }

        public void CheckRole(long role)
        {
            if (role != 0)
            {
                switch (role)
                {
                    case 1:
                        Response.Redirect(@"~\Admin\DashBoard.aspx");
                        break;
                    default:
                        Response.Redirect(@"~\Home.aspx");
                        break;
                }
            }
            else
            {
                string script = "alert('Please Register First');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
    }
}