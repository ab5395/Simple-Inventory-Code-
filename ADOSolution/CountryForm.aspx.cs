using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class CountryForm : System.Web.UI.Page
    {
        public static List<CountryClass> CountryList=new List<CountryClass>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                foreach (var data in CountryList)
                {
                    txtCountryID.Text = data.CountryId;
                    txtCountry.Text = data.Country;
                }
                CountryList.Clear();
            }
        }

        public void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select * from Country", connection);
                SqlDataAdapter adp = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                connection.Close();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into Country(Country) values('" + txtCountry.Text + "')",
                    connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            BindGrid();
            Response.Redirect("~/CountryForm.aspx");
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var gridView1DataKey = GridView1.DataKeys[e.NewEditIndex];
            if (gridView1DataKey != null)
            {
                string id = gridView1DataKey.Value.ToString();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("select * from Country where CountryID='" + id + "'", connection);
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        /*txtCountryID.Text = dt.Rows[0]["CountryID"].ToString();
                        txtCountry.Text = dt.Rows[0]["Country"].ToString();*/
                        CountryClass cc = new CountryClass()
                        {
                            CountryId = dt.Rows[0]["CountryID"].ToString(),
                            Country = dt.Rows[0]["Country"].ToString()
                        };
                        CountryList.Add(cc);   
                    }
                    connection.Close();
                    Response.Redirect("CountryForm.aspx");
                }

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Update Country set Country='" + txtCountry.Text + "' where CountryID='" + txtCountryID.Text + "'",
                    connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            BindGrid();
            Response.Redirect("~/CountryForm.aspx");
        }

        public class CountryClass
        {
            public string CountryId { get; set; }
            public string Country { get; set; }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainWebForm.aspx");
        }
    }
}