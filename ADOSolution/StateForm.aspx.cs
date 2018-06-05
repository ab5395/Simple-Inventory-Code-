using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class StateForm : System.Web.UI.Page
    {
        public static List<StateClass> StateList = new List<StateClass>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                BindCountry();
                foreach (var data in StateList)
                {
                    txtStateID.Text = data.Stateid;
                    txtState.Text = data.State;
                }
                StateList.Clear();
            }
        }

        public void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select s.StateID,s.State,c.Country from State s,Country c where s.CountryID=c.CountryID", connection);
                SqlDataAdapter adp = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
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
                        txtCountry.DataSource = dataTable;
                        txtCountry.DataValueField = "CountryID";
                        txtCountry.DataTextField = "Country";
                        txtCountry.DataBind();
                        txtCountry.Items.Insert(0, "Select Country");
                    }
                }
                connection.Close();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into State(State,CountryID) values('" + txtState.Text + "','" + txtCountry.Text + "')",
                    connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            BindGrid();
            Response.Redirect("~/StateForm.aspx");
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
                    SqlCommand command = new SqlCommand("select * from State where StateID='" + id + "'", connection);
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        /*txtStateID.Text = dt.Rows[0]["StateID"].ToString();
                        txtState.Text = dt.Rows[0]["State"].ToString();*/
                        StateClass sc = new StateClass()
                        {
                            Stateid = dt.Rows[0]["StateID"].ToString(),
                            State = dt.Rows[0]["State"].ToString()
                        };
                        StateList.Add(sc);
                    }
                    connection.Close();
                    Response.Redirect("~/StateForm.aspx");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Update State set State='" + txtState.Text + "' where StateID='" + txtStateID.Text + "'",
                    connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            BindGrid();
            Response.Redirect("~/StateForm.aspx");
        }

        public class StateClass
        {
            public string Stateid { get; set; }
            public string State { get; set; }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainWebForm.aspx");
        }
    }


}