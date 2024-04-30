using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Plain_Curd
{
    public partial class UserLoginRegister : System.Web.UI.Page
    {
        // public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["FirstName"] != null)
                {
                    buttoncontainer.Style.Add("Display", "none");
                    login.Style.Add("Display", "none");
                    signout.Style.Add("Display", "block");
                    signup.Style.Add("Display", "none");

                }
                else
                {
                    signout.Style.Add("Display", "none");
                }
            }
        }
        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/Home.aspx");
        }
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            var useremail = txtUserID.Text;
            var password = txtUserPassword.Text;
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
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            Session["FirstName"] = reader["FirstName"].ToString();
                                            //var role = reader["RoleId"].ToString();
                                            int UserId = (int)reader["UserId"];
                                            Session["UserId"] = UserId;
                                            Session["RoleID"] = reader["RoleID"];
                                            CheckRole(UserId);
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
        public void CheckRole(long UserId)
        {
            if (UserId != 0)
            {
                switch (UserId)
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
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var password = txtPassword.Text;
            var cpassword = txtCPassword.Text;
            var email = txtUserEmail.Text;
            var phone = txtPhone.Text;
            var address = txtAddress.Text;
            var term = chkTerm.Checked == false ? "False" : "True";
            var uniquekey = hfuniquekey.Value == "0" ? Guid.NewGuid():new Guid( hfuniquekey.Value);

            if (!string.IsNullOrEmpty(firstName))
            {
                if (!string.IsNullOrEmpty(lastName))
                {
                    if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(cpassword))
                    {
                        if (password == cpassword)
                        {
                            if (!string.IsNullOrEmpty(email))
                            {

                                if (!string.IsNullOrEmpty(address))
                                {
                                    using (var objSqlConnection = new SqlConnection(Connection))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("UserRegister", objSqlConnection))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            cmd.Parameters.AddWithValue("@firstName", firstName);
                                            cmd.Parameters.AddWithValue("@lastName", lastName);
                                            cmd.Parameters.AddWithValue("@email", email);
                                            cmd.Parameters.AddWithValue("@cpassword", cpassword);
                                            cmd.Parameters.AddWithValue("@phone", phone);
                                            cmd.Parameters.AddWithValue("@address", address);
                                            cmd.Parameters.AddWithValue("@uniquekey", uniquekey);
                                            objSqlConnection.Open();
                                            var result = cmd.ExecuteScalar();
                                            switch(result)
                                            {
                                                case 0:
                                                    string script = "alert('Email Id or Phone number already exists!!);";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                                    break;
                                                case 1:
                                                    string script1 = "alert('Succsefully Register');";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script1, true);
                                                    Response.Redirect(@"~\Home.aspx");
                                                    break;
                                                default:
                                                    string script2 = "alert('Error while adding new user);";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script2, true);
                                                    break;

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string script = "alert('Please Enter Address');";
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                                }
                            }
                            else
                            {
                                string script = "alert('Please Enter Email Id');";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                            }
                        }
                        else
                        {
                            string script = "alert('Password does not match');";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                        }

                    }
                    else
                    {
                        string script = "alert('Please Enter Password');";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    }
                }
                else
                {
                    string script = "alert('Please Enter Last Name');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
            else
            {
                string script = "alert('Please Enter First Name');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txtUserID.Text = string.Empty;
            txtUserPassword.Text = string.Empty;
        }
        protected void btn_reset2_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUserEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCPassword.Text = string.Empty;
            txtAddress.Text = string.Empty;
            chkTerm.Checked = false;
        }
    }
}