using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class InvoiceForm : System.Web.UI.Page
    {
        public static List<CancelBillClass> CancelBillList = new List<CancelBillClass>();
        public static List<CancelBillDetailClass> CancelBillDetailList = new List<CancelBillDetailClass>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string order = null;
                order = Session["Order"].ToString();
                if (order == "Already Canceled")
                {
                    btnCancelOrder.Visible = false;
                }
                else if (order == "Not Canceled")
                {
                    btnCancelOrder.Visible = true;
                }
                float total = 0;
                foreach (var data in ConfirmOrderForm.BillList)
                {
                    txtBillNo.Text = data.BillId.ToString();
                    txtName.Text = data.CustomerName;
                    txtAddress.Text = data.Address;
                    txtCity.Text = data.City;
                    txtState.Text = data.State;
                    txtCountry.Text = data.Country;
                    txtShippingcharge.Text = data.ShippingCharge.ToString(CultureInfo.InvariantCulture);
                    txtTotalCharge.Text = data.GrandTotal.ToString(CultureInfo.InvariantCulture);

                    CancelBillClass cbc = new CancelBillClass()
                    {
                        BillId = data.BillId
                    };

                    CancelBillList.Add(cbc);

                }
                foreach (var data in OrderWebForm.BillDetailList)
                {
                    total = total + data.Total;
                    CancelBillDetailClass cbdc = new CancelBillDetailClass()
                    {
                        ProductId = data.ProductId,
                        Quantity = data.Quantity
                    };
                    CancelBillDetailList.Add(cbdc);
                }
                txtTotalProductCharge.Text = total.ToString(CultureInfo.InvariantCulture);

                GridView1.DataSource = OrderWebForm.BillDetailList;
                GridView1.DataBind();

                OrderWebForm.BillDetailList.Clear();
                ConfirmOrderForm.BillList.Clear();
            }

        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            int bid = 0;
            foreach (var data in CancelBillList)
            {
                bid = data.BillId;
            }

            using (
                SqlConnection connection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["InventoryConnectionString"].ConnectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTransaction;
                try
                {

                    command.CommandText = "update Bill set IsCanceled='1' where BillID='" + bid + "'";
                    command.ExecuteNonQuery();

                    foreach (var data in CancelBillDetailList)
                    {
                        var pid = data.ProductId;
                        var quantity = data.Quantity;

                        command.CommandText = "select Quantity from Product where ProductID='" + pid + "'";
                        int dbquantity = int.Parse(command.ExecuteScalar().ToString());

                        quantity = quantity + dbquantity;

                        command.CommandText = "update Product set Quantity='" + quantity + "' where ProductID='" + pid +
                                              "'";
                        command.ExecuteNonQuery();

                    }

                    sqlTransaction.Commit();
                    Response.Redirect("~/BillForm.aspx");
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
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            OrderWebForm.BillDetailList.Clear();
            ConfirmOrderForm.BillList.Clear();
            CancelBillDetailList.Clear();
            CancelBillList.Clear();
            Response.Redirect("BillForm.aspx");
        }
    }
}