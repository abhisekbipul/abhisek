using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class RaiseTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateSupportStaffDropdown();
            }
        }

        private void PopulateSupportStaffDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            string query = "SELECT UserID, Username FROM Users WHERE Role = 'Support'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlAssignTo.DataSource = reader;
                ddlAssignTo.DataTextField = "Username";
                ddlAssignTo.DataValueField = "UserID";
                ddlAssignTo.DataBind();
            }

            ddlAssignTo.Items.Insert(0, new ListItem("Select a support staff", ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            int raisedBy = Convert.ToInt32(Session["UserID"]);
            int assignedTo = Convert.ToInt32(ddlAssignTo.SelectedValue);
            string imagePath = null;

            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    string uploadFolder = Server.MapPath("~/Uploads/");

                    // Ensure the Uploads folder exists
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    imagePath = Path.Combine(uploadFolder, filename);
                    FileUpload1.SaveAs(imagePath);

                    // Save the relative path
                    imagePath = "~/Uploads/" + filename;
                }
                catch (Exception ex)
                {
                    // Handle file upload exception
                    lblMessage.Text = "File upload failed: " + ex.Message;
                    return;
                }
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Tickets (Title, Description, RaisedBy, AssignedTo, CreatedDate, ImagePath) VALUES (@Title, @Description, @RaisedBy, @AssignedTo, @CreatedDate, @ImagePath)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@RaisedBy", raisedBy);
                command.Parameters.AddWithValue("@AssignedTo", assignedTo);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                command.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);

                conn.Open();
                command.ExecuteNonQuery();
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Ticket raised successfully.";
            ClearForm();
        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlAssignTo.SelectedIndex = 0;
        
        }
    }
}