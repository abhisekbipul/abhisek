using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Authenticate user
            User user = AuthenticateUser(username, password);
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["Role"] = user.Role;

                // Redirect based on role
                switch (user.Role)
                {
                    case "Employee":
                        Response.Redirect("EmployeeDashboard.aspx");
                        break;
                    case "Support":
                        Response.Redirect("SupportDashboard.aspx");
                        break;
                    case "HR":
                        Response.Redirect("HRDashboard.aspx");
                        break;
                }
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private User AuthenticateUser(string username, string password)
        {
            // Replace with your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            User user = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Role FROM Users WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = reader.GetInt32(0),
                        Role = reader.GetString(1)
                    };
                }
            }

            return user;
        }

        public class User
        {
            public int UserID { get; set; }
            public string Role { get; set; }
        }

    }
}