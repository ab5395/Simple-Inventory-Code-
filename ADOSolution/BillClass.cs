using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOSolution
{
    public class BillClass
    {
        public int BillId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public float ShippingCharge { get; set; }
        public float GrandTotal { get; set; }
    }

    public class BillDetailClass
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float DisPercentage { get; set; }
        public float DisValue { get; set; }
        public float Total { get; set; }
    }

    public class CancelBillClass
    {
        public int BillId { get; set; }
    }

    public class CancelBillDetailClass
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    /*SqlTransaction tra;
            cn.Open();
            tra = cn.BeginTransaction();
            SqlCommand cmd = new SqlCommand("INSERT INTO bill(cid) OUTPUT INSERTED.bid VALUES (@cid) ", cn,tra);
            cmd.Parameters.AddWithValue("@cid", Session["cid"]);

            int a=(int)cmd.ExecuteScalar();
*/
}
 