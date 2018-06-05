using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class CityForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountry();
                BindGrid();
            }
        }

        public void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select ci.CityID,ci.City,s.StateID,s.State,c.Country from State s,Country c,City ci where s.CountryID=c.CountryID and ci.StateID=s.StateID", connection);
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

        [WebMethod]
        public static List<StateList> BindState(string cid)
        {
            List<StateList> stateList = new List<StateList>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from State where CountryID='" + cid + "'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            stateList = (from DataRow dr in dataTable.Rows
                                         select new StateList()
                                         {
                                             StateId = Int32.Parse(dr["StateID"].ToString()),
                                             State = dr["State"].ToString()
                                         }).ToList();
                        }
                    }
                    connection.Close();
                }
            }
            return stateList;
        }

        [WebMethod]
        public static bool AddCity(string city, int sid)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("insert into City(City,StateID) values('" + city + "','" + sid + "')", connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return true;
        }
        

        public class StateList
        {
            public int StateId { get; set; }
            public string State { get; set; }
        }




        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var gridView1DataKey = GridView1.DataKeys[e.RowIndex];
            if (gridView1DataKey != null)
            {
                string id = gridView1DataKey.Value.ToString();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("delete from City where CityID='" + id + "'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            BindGrid();
            Response.Redirect("~/CityForm.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainWebForm.aspx");
        }
    }
}