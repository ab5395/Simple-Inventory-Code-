using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace EntityMVCSolution.Models
{
    public class OrderClass
    {
        public InventorySystemEntities Entities = new InventorySystemEntities();
        public double GetPrice(int productid)
        {
            var price = Entities.Products.Single(x => x.ProductID == productid);
            return double.Parse(price.SaleMRP.ToString());
        }
        

        public int Quantity(int productid)
        {
            var price = Entities.Products.Single(x => x.ProductID == productid);
            return int.Parse(price.Quantity.ToString());
        }

        public string GetName(int productid)
        {
            var price = Entities.Products.Single(x => x.ProductID == productid);
            return price.Name;
        }

        public string SetBill(Bill bill, List<BillDetailClass> billList)
        {
            int bid = 0;
            DbContextTransaction transaction = Entities.Database.BeginTransaction();
            try
            {
                Bill bl = new Bill()
                {
                    CustomerID = bill.CustomerID,
                    BillDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")),
                    ShippingCharge = bill.ShippingCharge,
                    GrandTotal = bill.GrandTotal,
                    IsCanceled = false
                };
                Entities.Bills.Add(bl);
                Entities.SaveChanges();
                bid = bl.BillID;

                foreach (var data in billList)
                {
                    var dbquantity = Entities.Products.Single(x => x.ProductID == data.ProductId);
                    int quantity = 0;
                    if (dbquantity.Quantity != null)
                    {
                        quantity = dbquantity.Quantity.Value - data.Quantity;
                    }

                    var product = Entities.Products.FirstOrDefault(x => x.ProductID == data.ProductId);
                    if (product != null)
                    {
                        product.Quantity = quantity;
                    }
                    Entities.SaveChanges();


                    BillDetail bd = new BillDetail()
                    {
                        BillID = bid,
                        ProductID = data.ProductId,
                        Quantity = data.Quantity,
                        Price = data.Price,
                        DiscountPercentage = data.DisPercentage,
                        DiscountValue = data.DisValue,
                        ProductTotal = data.Total
                    };
                    Entities.BillDetails.Add(bd);
                    Entities.SaveChanges();
                }
                transaction.Commit();
                return bid.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
        }

        public string CancelBill(int billid)
        {
            DbContextTransaction transaction = Entities.Database.BeginTransaction();
            try
            {
                var bill = Entities.Bills.FirstOrDefault(x => x.BillID == billid);
                if (bill != null)
                {
                    bill.IsCanceled = true;
                }
                Entities.SaveChanges();

                foreach (var data in GetBillDetailListbyBillId(billid))
                {
                    var product = Entities.Products.FirstOrDefault(x => x.ProductID == data.ProductID);
                    if (product != null) product.Quantity = data.Quantity + product.Quantity;
                    Entities.SaveChanges();
                }

                transaction.Commit();
                return "True";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
        }

        public List<Bill> GetBillList()
        {
            return Entities.Bills.ToList();
        }

        public List<Bill> GetBillListbyBillId(int bid)
        {
            return Entities.Bills.Where(x => x.BillID == bid).ToList();
        }

        public List<BillDetail> GetBillDetailListbyBillId(int bid)
        {
            return Entities.BillDetails.Where(x => x.BillID == bid).ToList();
        }


    }

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
        public float Total { get; set; }
        public string Status { get; set; }
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

    public class ChangeQuantityClass
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float DisPercentage { get; set; }
    }
}