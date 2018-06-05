using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class ProductForm : System.Web.UI.Page
    {
        public static List<ProductClass> ProductList = new List<ProductClass>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                foreach (var data in ProductList)
                {
                    txtName.Text = data.Name;
                    txtID.Text = data.PId.ToString();
                    txtQuantity.Text = data.Quantity;
                    txtSalingMRP.Text = data.Mrp;
                    chkStatus.Checked = data.Status == "True";
                }
            }
        }

        public void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select ProductID,Name,Quantity,SaleMRP,IsActive from Product where IsDeleted='False'", connection))
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

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ProductList.Clear();
            var status = chkStatus.Checked ? 1 : 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("insert into Product(Name,Quantity,SaleMRP,IsActive,IsDeleted) values('" + txtName.Text + "','" + txtQuantity.Text + "','" + txtSalingMRP.Text + "','" + status + "','0')", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            BindGrid();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ProductList.Clear();
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
                using (command = new SqlCommand("select * from Product where ProductID='" + id + "'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        foreach (DataRow data in dataTable.Rows)
                        {
                            ProductClass cc = new ProductClass
                            {
                                PId = id,
                                Name = data["Name"].ToString(),
                                Quantity = data["Quantity"].ToString(),
                                Mrp = data["SaleMRP"].ToString(),
                                Status = data["IsActive"].ToString()
                            };
                            ProductList.Add(cc);
                        }
                    }
                }
                connection.Close();
            }
            Response.Redirect("~/ProductForm.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ProductList.Clear();
            int id = 0;
            var gridView1DataKey = GridView1.DataKeys[e.RowIndex];
            if (gridView1DataKey != null)
            {
                id = Int32.Parse(gridView1DataKey.Value.ToString());
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("update Product set IsDeleted='1' where ProductID='" + id + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Response.Redirect("~/ProductForm.aspx");
        }

        public class ProductClass
        {
            public int PId { get; set; }
            public string Name { get; set; }
            public string Quantity { get; set; }
            public string Mrp { get; set; }
            public string Status { get; set; }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ProductList.Clear();
            var status = chkStatus.Checked ? 1 : 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("update Product set Name='" + txtName.Text + "',Quantity='" + txtQuantity.Text + "',SaleMRP='" + txtSalingMRP.Text + "',IsActive='" + status + "' where ProductID='" + txtID.Text + "'", connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Response.Redirect("~/ProductForm.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainWebForm.aspx");
        }
    }

}