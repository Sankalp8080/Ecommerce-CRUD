using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Plain_Curd.Admin
{
    public partial class AddUser : System.Web.UI.Page
    {
        //public string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string Connection = ConfigurationManager.ConnectionStrings["ERPDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindRole();
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
                using (SqlCommand cmd = new SqlCommand("getUserList"))
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
            var data = GetUserDetails(uniquekey, Convert.ToInt32(Slno));
            if (data != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddProductTab", "$(function() { $('.section').removeClass('active'); $('.signup').addClass('active');$('.button').removeClass('active');$(this).addClass('active');});", true);
                txtFirstName.Text = data.FirstName;
                txtLastName.Text = data.LastName;
                txtUserEmail.Text = data.UserEmail;
                txtPassword.Text = data.Password;
                txtCPassword.Text = data.Password;

                txtPhone.Text = data.Phone;
                txtAddress.Text = data.Address;
                ddlRole.SelectedValue = data.RoleId.ToString() ;
              
                hfSlno.Value = data.UserId.ToString();
                hfuniquekey.Value = data.UniqueKey.ToString();
                btn_Submit.Text = "Update";
            }
        }
        public UserDetailsByID GetUserDetails(string uniquekey, int Slno)
        {
            UserDetailsByID productdata = null;
            var unique = uniquekey;
            var slno = Slno;
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("GetusetNameByID", objSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@unique", unique);
                    cmd.Parameters.AddWithValue("@userid", slno);
                    cmd.Connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productdata = new UserDetailsByID
                            {
                                UserEmail = reader["UserEmail"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Address = reader["Address"].ToString(),
                                Password = reader["Password"].ToString(),
                                UniqueKey = reader["UniqueKey"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                RoleId = Convert.ToInt32(reader["RoleId"])

                            };
                        }
                    }
                }
            }
            return productdata;
        }
        public void BindRole()
        {
            using (var objSqlConnection = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("BindRole", objSqlConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    objSqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    objSqlConnection.Close();
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "RoleName";
                    ddlRole.DataValueField = "Slno";
                    ddlRole.DataBind();
                    ListItem lt = new ListItem("Select Role", "0");
                    ddlRole.Items.Insert(0, lt);
                }
            }
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
                using (SqlCommand cmd = new SqlCommand("deleteUserByID", objSqlConnection))
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
        public class UserDetailsByID
        {
            public int UserId { get; set; }
            public int RoleId { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string UniqueKey { get; set; }

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
            var status = chkStatus.Checked == false ? "False" : "True";
            var uniquekey = hfuniquekey.Value == "0" ? Guid.NewGuid() : new Guid(hfuniquekey.Value);
            var role = ddlRole.SelectedItem.Value;
            var createrID = Session["RoleId"];
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
                                    if (role.ToString() != "0")
                                    {
                                        using (var objSqlConnection = new SqlConnection(Connection))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("adminUserRegister", objSqlConnection))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                cmd.Parameters.AddWithValue("@firstName", firstName);
                                                cmd.Parameters.AddWithValue("@lastName", lastName);
                                                cmd.Parameters.AddWithValue("@email", email);
                                                cmd.Parameters.AddWithValue("@cpassword", cpassword);
                                                cmd.Parameters.AddWithValue("@phone", phone);
                                                cmd.Parameters.AddWithValue("@address", address);
                                                cmd.Parameters.AddWithValue("@status", status);
                                                cmd.Parameters.AddWithValue("@role", role);
                                                cmd.Parameters.AddWithValue("@userid", createrID);
                                                cmd.Parameters.AddWithValue("@uniquekey", uniquekey);
                                                objSqlConnection.Open();
                                                var result = cmd.ExecuteScalar();
                                                switch (result)
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
                                        string script = "alert('Please Select Role);";
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
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

    }
}