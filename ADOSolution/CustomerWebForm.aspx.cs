using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class CustomerWebForm : System.Web.UI.Page
    {
        public static List<CustomerClass> CustomerList = new List<CustomerClass>();
        public static List<CustomerCityStateClass> CustomerCityStateList = new List<CustomerCityStateClass>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (var data in CustomerList)
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
                    txtID.Text = data.CId.ToString();
                    txtName.Text = data.Name;
                    txtAddress.Text = data.Address;
                    chkStatus.Checked = data.Status == "True";
                }
                BindCountry();
                BindGrid();
            }
        }

        public void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select c.CustomerID,c.Name,c.Address,ci.City,c.IsActive from Customer c,City ci where c.CityID=ci.CityID and IsDeleted='False'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        GridView1.DataSource = dataTable;
                        GridView1.DataBind();
                    }
                }
                connection.Close();
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
                        if (CustomerCityStateList.Count == 0)
                        {
                            ddlCountry.Items.Insert(0, "Select Country");
                        }
                        else
                        {
                            foreach (var data in CustomerCityStateList)
                            {
                                ddlCountry.Items.Insert(0, data.Country);
                                ddlState.Items.Insert(0, data.State);
                                ddlCity.Items.Insert(0, data.City);
                                break;
                            }
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

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            var status = chkStatus.Checked ? 1 : 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("insert into Customer(Name,Address,CityID,IsActive,IsDeleted) values('" + txtName.Text + "','" + txtAddress.Text + "','" + ddlCity.SelectedValue + "','" + status + "','0')", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            BindGrid();
            Response.Redirect("CustomerWebForm.aspx");
        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CustomerList.Clear();
            int id = 0;
            var gridView1DataKey = GridView1.DataKeys[e.NewEditIndex];
            if (gridView1DataKey != null)
            {
                id = Int32.Parse(gridView1DataKey.Value.ToString());
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command;
                using (command = new SqlCommand("select c.CustomerID,c.Name,c.Address,c.IsActive,ci.CityID,ci.City,s.StateID,s.State,co.CountryID,co.Country from Customer c,City ci,Country co,State s where CustomerID='" + id + "' and ci.CityID=c.CityID and ci.StateID=s.StateID and s.CountryID=co.CountryID", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        foreach (DataRow data in dataTable.Rows)
                        {
                            CustomerClass cc = new CustomerClass
                            {
                                CId = id,
                                Name = data["Name"].ToString(),
                                Address = data["Address"].ToString(),
                                CityId = Int32.Parse(data["CityID"].ToString()),
                                City = data["City"].ToString(),
                                StateId = Int32.Parse(data["StateID"].ToString()),
                                State = data["State"].ToString(),
                                CountryId = Int32.Parse(data["CountryID"].ToString()),
                                Country = data["Country"].ToString(),
                                Status = data["IsActive"].ToString()
                            };
                            CustomerList.Add(cc);
                        }
                    }
                }
                connection.Close();
            }
            //Response.Redirect("EditCustomerForm.aspx");
            Response.Redirect("CustomerWebForm.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Response.Write(ddlCountry.SelectedValue + Environment.NewLine + ddlState.SelectedValue + Environment.NewLine + ddlCity.SelectedValue);
            foreach (var data in CustomerList)
            {
                var status = chkStatus.Checked ? 1 : 0;
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
                {
                    connection.Open();
                    if ((ddlCountry.SelectedValue == data.Country) && (ddlState.SelectedValue == data.State) &&
                        (ddlCity.SelectedValue == data.City))
                    {
                        using (SqlCommand command =new SqlCommand("update Customer set Name='" + txtName.Text + "',Address='" + txtAddress.Text + "',IsActive='" + status + "' where CustomerID='" + txtID.Text + "'", connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand command =new SqlCommand("update Customer set Name='" + txtName.Text + "',Address='" + txtAddress.Text +"',CityID='" + ddlCity.SelectedValue + "',IsActive='" + status +"' where CustomerID='" + txtID.Text + "'", connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }

                CustomerList.Clear();
                Response.Redirect("CustomerWebForm.aspx");
            }
        }

        public class CustomerClass
        {
            public int CId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int CityId { get; set; }
            public string City { get; set; }
            public int CountryId { get; set; }
            public string Country { get; set; }
            public int StateId { get; set; }
            public string State { get; set; }
            public string Status { get; set; }
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CustomerList.Clear();
            int id = 0;
            var gridView1DataKey = GridView1.DataKeys[e.RowIndex];
            if (gridView1DataKey != null)
            {
                id = Int32.Parse(gridView1DataKey.Value.ToString());
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("update Customer set IsDeleted='1' where CustomerID='" + id + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Response.Redirect("~/CustomerWebForm.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainWebForm.aspx");
        }
    }
}