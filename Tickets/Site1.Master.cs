using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Tickets.Login;

namespace Tickets
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public User CurrentUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetNavigationVisibility();
            }
        }

        private void SetNavigationVisibility()
        {
            if (Session["Role"] != null)
            {
                string role = Session["Role"].ToString();

                PlaceHolder employeeNav = FindControl("EmployeeNav") as PlaceHolder;
                PlaceHolder supportNav = FindControl("SupportNav") as PlaceHolder;
                PlaceHolder hrNav = FindControl("HRNav") as PlaceHolder;

                if (employeeNav != null && supportNav != null && hrNav != null)
                {
                    if (role == "Employee")
                    {
                        employeeNav.Visible = true;
                        supportNav.Visible = false;
                        hrNav.Visible = false;
                    }
                    else if (role == "Support")
                    {
                        employeeNav.Visible = false;
                        supportNav.Visible = true;
                        hrNav.Visible = false;
                    }
                    else if (role == "HR")
                    {
                        employeeNav.Visible = false;
                        supportNav.Visible = false;
                        hrNav.Visible = true;
                    }
                    else
                    {
                        employeeNav.Visible = false;
                        supportNav.Visible = false;
                        hrNav.Visible = false;
                    }
                }
            }
            else
            {
                // Handle case when session data is null
                PlaceHolder employeeNav = FindControl("EmployeeNav") as PlaceHolder;
                PlaceHolder supportNav = FindControl("SupportNav") as PlaceHolder;
                PlaceHolder hrNav = FindControl("HRNav") as PlaceHolder;

                if (employeeNav != null && supportNav != null && hrNav != null)
                {
                    employeeNav.Visible = false;
                    supportNav.Visible = false;
                    hrNav.Visible = false;
                }
            }
        }
    }
}