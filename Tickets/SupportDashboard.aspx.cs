using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets
{
    public partial class SupportDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSupportTickets();
            }
        }

        private void LoadSupportTickets()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TicketID, Title, Description, Status, RaisedBy, CreatedDate FROM Tickets WHERE AssignedTo = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvSupportTickets.DataSource = dt;
                gvSupportTickets.DataBind();
            }
        }

        protected void gvSupportTickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int ticketId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");

                string newStatus = ddlStatus.SelectedValue;
                UpdateTicketStatus(ticketId, newStatus);
                LoadSupportTickets();
            }
        }

        private void UpdateTicketStatus(int ticketId, string newStatus)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tickets SET Status = @Status, ResolvedDate = CASE WHEN @Status = 'Resolved' THEN GETDATE() ELSE NULL END WHERE TicketID = @TicketID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@TicketID", ticketId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}