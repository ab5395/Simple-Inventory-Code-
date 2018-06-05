using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADOSolution
{
    public partial class BillForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        public void BindGrid()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select b.BillID,c.Name,b.BillDate,b.GrandTotal,b.ShippingCharge,b.IsCanceled from Bill b,Customer c where b.CustomerID=c.CustomerID", connection))
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

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            

            int id = 0;
            var gridView1DataKey = GridView1.DataKeys[e.RowIndex];
            if (gridView1DataKey != null)
            {
                id = Int32.Parse(gridView1DataKey.Value.ToString());
            }
            int bid = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select b.BillID,c.Name,c.CustomerID,c.Address,b.BillDate,b.GrandTotal,b.ShippingCharge,b.IsCanceled,ci.City,s.State,co.Country from Bill b,Customer c,City ci,Country co,State s where b.CustomerID=c.CustomerID and b.BillID='" + id + "' and c.CityID=ci.CityID and ci.StateID=s.StateID and s.CountryID=co.CountryID", connection);
                SqlDataAdapter adp = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adp.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    BillClass bc = new BillClass()
                    {
                        BillId = int.Parse(dr["BillID"].ToString()),
                        CustomerId = int.Parse(dr["CustomerID"].ToString()),
                        CustomerName = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        City = dr["City"].ToString(),
                        Country = dr["Country"].ToString(),
                        State = dr["State"].ToString(),
                        ShippingCharge = float.Parse(dr["ShippingCharge"].ToString()),
                        GrandTotal = float.Parse(dr["GrandTotal"].ToString())
                    };
                    ConfirmOrderForm.BillList.Add(bc);
                    bid = (int)dr["BillID"];
                    if (dr["IsCanceled"].ToString() == "True")
                    {
                        Session["Order"] = "Already Canceled";
                    }
                    else
                    {
                        Session["Order"] = "Not Canceled";
                    }
                    break;
                }

                SqlCommand command1 = new SqlCommand("select p.ProductID,p.Name,b.Quantity,b.Price,b.DiscountPercentage,b.DiscountValue,b.ProductTotal from BillDetail b,Product p where b.BillID='" + bid + "' and b.ProductID=p.ProductID", connection);
                SqlDataAdapter adp1 = new SqlDataAdapter(command1);
                DataTable dataTable1 = new DataTable();
                adp1.Fill(dataTable1);
                foreach (DataRow dr in dataTable1.Rows)
                {
                    BillDetailClass bdc = new BillDetailClass()
                    {
                        ProductId = int.Parse(dr["ProductID"].ToString()),
                        ProductName = dr["Name"].ToString(),
                        Quantity = int.Parse(dr["Quantity"].ToString()),
                        Price = float.Parse(dr["Price"].ToString()),
                        DisPercentage = float.Parse(dr["DiscountPercentage"].ToString()),
                        DisValue = float.Parse(dr["DiscountValue"].ToString()),
                        Total = float.Parse(dr["ProductTotal"].ToString())
                    };
                    OrderWebForm.BillDetailList.Add(bdc);
                }
                connection.Close();

                Response.Redirect("~/InvoiceForm.aspx");
            }


        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainWebForm.aspx");
        }
    }
}