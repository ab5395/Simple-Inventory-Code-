using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class ConfirmOrderForm : System.Web.UI.Page
    {
        public static List<BillClass> BillList=new List<BillClass>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomer();
                float t = 0;
                foreach (var data in OrderWebForm.BillDetailList)
                {
                    t = t + data.Total;
                }
                txtTotal.Text = t.ToString(CultureInfo.InvariantCulture);
                GridView1.DataSource = OrderWebForm.BillDetailList;
                GridView1.DataBind();
            }
        }

        public void BindCustomer()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from Customer where IsDeleted='False'and IsActive='True'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        ddlCustomer.DataSource = dataTable;
                        ddlCustomer.DataValueField = "CustomerID";
                        ddlCustomer.DataTextField = "Name";
                        ddlCustomer.DataBind();
                        ddlCustomer.Items.Insert(0, "Select Customer");
                    }
                }
                connection.Close();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = 0;
            var gridView1DataKey = GridView1.DataKeys[e.RowIndex];
            if (gridView1DataKey != null)
            {
                id = Convert.ToInt32(gridView1DataKey.Value.ToString());
            }
            var selectlist1 = OrderWebForm.BillDetailList.FirstOrDefault(x => x.ProductId == Convert.ToInt32(id));
            OrderWebForm.BillDetailList.Remove(selectlist1);
            Response.Redirect("ConfirmOrderForm.aspx");
        }

        protected void btnBill_Click(object sender, EventArgs e)
        {
            // ReSharper disable once TooWideLocalVariableScope
            int bid;
            if (ddlCustomer.SelectedValue != "Select Customer")
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
                {
                    connection.Open();
                    SqlTransaction sqlTransaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = sqlTransaction;
                    try
                    {

                        txtGrandTotal.Text = (float.Parse(txtTotal.Text) + float.Parse(txtShippingCharge.Text)).ToString(CultureInfo.InvariantCulture);

                       // Query Type Transaction
                        command.CommandText = "INSERT INTO Bill(CustomerID,BillDate,ShippingCharge,GrandTotal,IsCanceled) OUTPUT INSERTED.BillID VALUES ('"+ddlCustomer.SelectedValue+"','"+ DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "','"+txtShippingCharge.Text+"','"+ txtGrandTotal.Text+"','0') ";
                        bid = Int32.Parse(command.ExecuteScalar().ToString());
                        //Response.Write(command.ExecuteScalar().ToString());

                        BillClass bc = new BillClass
                        {
                            BillId = bid,
                            CustomerName = ddlCustomer.SelectedItem.ToString(),
                            Address = txtAddress.Text,
                            City = txtCity.Text,
                            State = txtState.Text,
                            Country = txtCountry.Text,
                            CustomerId = int.Parse(ddlCustomer.SelectedValue),
                            GrandTotal = float.Parse(txtGrandTotal.Text),
                            ShippingCharge = float.Parse(txtShippingCharge.Text)
                        };
                        BillList.Add(bc);

                        foreach (var data in OrderWebForm.BillDetailList)
                        {
                            command.CommandText = "select Quantity from Product where ProductID='"+data.ProductId+"'";
                            var quantity = int.Parse(command.ExecuteScalar().ToString());

                            quantity = quantity - data.Quantity;

                            command.CommandText = "update Product set Quantity='"+quantity+"' where ProductID='"+data.ProductId+"'";
                            command.ExecuteNonQuery();
                            
                            command.CommandText = "insert into BillDetail(BillID,ProductID,Quantity,Price,DiscountPercentage,DiscountValue,ProductTotal) values('"+bid+"','"+data.ProductId+"','"+data.Quantity+"','"+data.Price+"','"+data.DisPercentage+"','"+data.DisValue+"','"+ data.Total+ "')";
                            command.ExecuteNonQuery();
                        }
                        sqlTransaction.Commit();
                        Session["Order"] = "Not Canceled";
                        Response.Redirect("~/InvoiceForm.aspx");
                        Thread.ResetAbort();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "Thread was being aborted.")
                        {
                            sqlTransaction.Rollback();
                        }
                        Response.Write(ex);
                    }
                    connection.Close();
                }//if bracket
            }
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue != "Select Customer")
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select c.Address,ci.City,co.Country,s.State from Customer c,City ci,State s,Country co where CustomerID='" + ddlCustomer.SelectedValue + "' and c.CityID=ci.CityID and ci.StateID=s.StateID and s.CountryID=co.CountryID", connection))
                    {
                        using (SqlDataAdapter adp = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adp.Fill(dataTable);
                            ddlCustomer.DataSource = dataTable;
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                txtAddress.Text = dr["Address"].ToString();
                                txtCountry.Text = dr["Country"].ToString();
                                txtState.Text = dr["State"].ToString();
                                txtCity.Text = dr["City"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }

       

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            OrderWebForm.BillDetailList.Clear();
            BillList.Clear();
            Response.Redirect("OrderWebForm.aspx");
        }
    }
}