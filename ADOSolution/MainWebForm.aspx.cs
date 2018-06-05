using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADOSolution
{
    public partial class MainWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCountry_Click(object sender, EventArgs e)
        {
            Response.Redirect("CountryForm.aspx");
        }

        protected void btnState_Click(object sender, EventArgs e)
        {
            Response.Redirect("StateForm.aspx");
        }

        protected void btnCity_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityForm.aspx");
        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerWebForm.aspx");
        }

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductForm.aspx");
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderWebForm.aspx");
        }

        protected void btnBill_Click(object sender, EventArgs e)
        {
            Response.Redirect("BillForm.aspx");
        }
    }
}