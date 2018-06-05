using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class OrderWebForm : System.Web.UI.Page
    {
        public static List<BillDetailClass> BillDetailList = new List<BillDetailClass>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = BillDetailList;
                GridView1.DataBind();
                BindProduct();
            }
        }

        public void BindProduct()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from Product where IsDeleted='False'and IsActive='True'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        ddlProduct.DataSource = dataTable;
                        ddlProduct.DataValueField = "ProductID";
                        ddlProduct.DataTextField = "Name";
                        ddlProduct.DataBind();
                        ddlProduct.Items.Insert(0, "Select Product");
                    }
                }
                connection.Close();
            }
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue != "Select Product")
            {
                var selectlist = BillDetailList.Where(x => x.ProductId == Convert.ToInt32(ddlProduct.SelectedValue)).ToList();
                if (selectlist.Count != 0)
                {
                    foreach (var data in selectlist)
                    {
                        //txtQuantity.Text = data.Quantity.ToString();
                        txtPrice.Text = data.Price.ToString(CultureInfo.InvariantCulture);
                        txtDiscount.Text = data.DisPercentage.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    //Response.Write(ddlProduct.SelectedValue);
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("select * from Product where ProductID='" + ddlProduct.SelectedValue + "'", connection))
                        {
                            using (SqlDataAdapter adp = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adp.Fill(dataTable);
                                ddlProduct.DataSource = dataTable;
                                foreach (DataRow data in dataTable.Rows)
                                {
                                    txtPrice.Text = data["SaleMRP"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            int q, q2;
            float disper = 0, disval = 0, totalprice;
            //q = q2 = q3 = q4 = 0;
            var selectlist = BillDetailList.Where(x => x.ProductId == Convert.ToInt32(ddlProduct.SelectedValue)).ToList();
            if (selectlist.Count == 0)
            {
                q = Int32.Parse(txtQuantity.Text);
                q2 = Quantity(Convert.ToInt32(ddlProduct.SelectedValue));
                if (q <= q2)
                {
                    totalprice = float.Parse(txtPrice.Text) * q;
                    if (txtDiscount.Text != "")
                    {
                        disper = float.Parse(txtDiscount.Text);
                        disval = disper * totalprice / 100;
                    }


                    BillDetailClass bdc = new BillDetailClass
                    {
                        ProductId = Convert.ToInt32(ddlProduct.SelectedValue),
                        ProductName = ddlProduct.SelectedItem.ToString(),
                        Quantity = Convert.ToInt32(txtQuantity.Text),
                        Price = float.Parse(txtPrice.Text),
                        DisPercentage = disper,
                        DisValue = disval,
                        Total = totalprice - disval
                    };
                    BillDetailList.Add(bdc);
                    Response.Redirect("OrderWebForm.aspx");
                }
                else
                {
                    Response.Write("Please enter quntity <=" + q2);
                }
            }
            else
            {
                foreach (var data in selectlist)
                {
                    q = data.Quantity;
                    var q3 = Quantity(data.ProductId);
                    q2 = Int32.Parse(txtQuantity.Text);
                    var q4 = q3 - q;
                    if (q2 <= q4)
                    {

                        totalprice = float.Parse(txtPrice.Text) * (data.Quantity + q2);
                        if (txtDiscount.Text != "")
                        {
                            disper = float.Parse(txtDiscount.Text);
                            disval = disper * totalprice / 100;
                        }

                        BillDetailClass bdc = new BillDetailClass
                        {
                            ProductId = data.ProductId,
                            ProductName = data.ProductName,
                            Quantity = data.Quantity + q2,
                            Price = float.Parse(txtPrice.Text),
                            DisPercentage = float.Parse(txtDiscount.Text),
                            DisValue = disval,
                            Total = totalprice - disval
                        };
                        var selectlist1 = BillDetailList.FirstOrDefault(x => x.ProductId == Convert.ToInt32(ddlProduct.SelectedValue));
                        BillDetailList.Remove(selectlist1);
                        BillDetailList.Add(bdc);
                        Response.Redirect("OrderWebForm.aspx");
                    }
                    else
                    {
                        Response.Write("Please enter quntity <=" + q4);
                    }
                }
            }
        }

        public int Quantity(int pid)
        {
            int quantity = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from Product where ProductID='" + pid + "'", connection))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adp.Fill(dataTable);
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            quantity = Int32.Parse(dr["Quantity"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            return quantity;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = 0;
            var gridView1DataKey = GridView1.DataKeys[e.RowIndex];
            if (gridView1DataKey != null)
            {
                id = Convert.ToInt32(gridView1DataKey.Value.ToString());
            }
            var selectlist1 = BillDetailList.FirstOrDefault(x => x.ProductId == Convert.ToInt32(id));
            BillDetailList.Remove(selectlist1);
            Response.Redirect("OrderWebForm.aspx");
        }

        protected void btnConfirm_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ConfirmOrderForm.aspx");
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            BillDetailList.Clear();
            Response.Redirect("OrderWebForm.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            BillDetailList.Clear();
            Response.Redirect("MainWebForm.aspx");
        }
    }
}