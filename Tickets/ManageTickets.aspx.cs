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
    public partial class ManageTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTickets();
                BindAssignees();
            }
        }

        private void BindTickets()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT t.TicketID, t.Title, t.Description, t.Status, t.CreatedDate, 
                                 t.ImagePath, 
                                 (SELECT Username FROM Users WHERE UserID = t.RaisedBy) AS RaisedByUsername
                                 FROM Tickets t
                                 WHERE t.Status <> 'Resolved'";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                gvTickets.DataSource = dt;
                gvTickets.DataBind();
            }
        }

        private void BindAssignees()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Username FROM Users WHERE Role = 'Support'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    foreach (GridViewRow row in gvTickets.Rows)
                    {
                        DropDownList ddlAssignee = (DropDownList)row.FindControl("ddlAssignee");
                        ddlAssignee.Items.Add(new ListItem(reader["Username"].ToString(), reader["UserID"].ToString()));
                    }
                }
                reader.Close();
            }
        }

        protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Assign")
            {
                int ticketID = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                DropDownList ddlAssignee = (DropDownList)row.FindControl("ddlAssignee");

                if (ddlAssignee != null && !string.IsNullOrEmpty(ddlAssignee.SelectedValue))
                {
                    int assigneeID = Convert.ToInt32(ddlAssignee.SelectedValue);
                    UpdateTicketAssignment(ticketID, assigneeID);
                }
            }
            else if (e.CommandName == "Resolve")
            {
                int ticketID = Convert.ToInt32(e.CommandArgument);
                ResolveTicket(ticketID);
            }
        }

        private void UpdateTicketAssignment(int ticketID, int assigneeID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tickets SET AssignedTo = @AssignedTo, Status = 'Assigned' WHERE TicketID = @TicketID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AssignedTo", assigneeID);
                cmd.Parameters.AddWithValue("@TicketID", ticketID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindTickets();
        }

        private void ResolveTicket(int ticketID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tickets SET Status = 'Resolved', ResolvedDate = GETDATE() WHERE TicketID = @TicketID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TicketID", ticketID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindTickets();
        }

    }
}