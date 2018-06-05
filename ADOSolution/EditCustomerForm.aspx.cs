using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class EditCustomerForm : System.Web.UI.Page
    {
        public static List<CustomerCityStateClass> CustomerCityStateList = new List<CustomerCityStateClass>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                foreach (var data in CustomerWebForm.CustomerList)
                {
                    CustomerCityStateClass ccs = new CustomerCityStateClass
                    {
                        CountryId = data.CountryId,
                        Country = data.Country,
                        City = data.City,
                        CityId = data.CityId,
                        StateId = data.StateId,
                        State = data.State
                    };
                    CustomerCityStateList.Add(ccs);
                }
                BindCountry();
            }
        }

        public void BindCountry()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from Country", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        ddlCountry.DataSource = dataTable;
                        ddlCountry.DataValueField = "CountryID";
                        ddlCountry.DataTextField = "Country";
                        ddlCountry.DataBind();
                        foreach (var data in CustomerCityStateList)
                        {
                            ddlCountry.Items.Insert(0, data.Country);
                            ddlState.Items.Insert(0, data.State);
                            ddlCity.Items.Insert(0, data.City);
                            
                            break;
                        }
                        CustomerCityStateList.Clear();
                    }
                }
                connection.Close();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command;
                using (command = new SqlCommand("select * from State where CountryID='" + ddlCountry.SelectedValue + "'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        ddlState.DataSource = dataTable;
                        ddlState.DataValueField = "StateID";
                        ddlState.DataTextField = "State";
                        ddlState.DataBind();
                        ddlState.Items.Insert(0, "Select State");
                    }
                }
                connection.Close();
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command;
                using (command = new SqlCommand("select * from City where StateID='" + ddlState.SelectedValue + "'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        ddlCity.DataSource = dataTable;
                        ddlCity.DataValueField = "CityID";
                        ddlCity.DataTextField = "City";
                        ddlCity.DataBind();
                        ddlCity.Items.Insert(0, "Select City");
                    }
                }
                connection.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var status = chkStatus.Checked ? 1 : 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("update Customer set Name='" + txtName.Text + "',Address='" + txtAddress.Text + "',CityID='" + ddlCity.SelectedValue + "',IsActive='" + status + "' where CustomerID='" + txtID.Text + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Response.Redirect("CustomerWebForm.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerWebForm.aspx");
        }
        public class CustomerCityStateClass
        {
            public int CityId { get; set; }
            public string City { get; set; }
            public int CountryId { get; set; }
            public string Country { get; set; }
            public int StateId { get; set; }
            public string State { get; set; }
        }
    }
}