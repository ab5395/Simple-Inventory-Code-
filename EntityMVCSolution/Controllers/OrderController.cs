using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EntityMVCSolution.Models;
using PagedList;

namespace EntityMVCSolution.Controllers
{
    public class OrderController : Controller
    {
        public static List<BillDetailClass> BillDetailList = new List<BillDetailClass>();
        public static List<BillClass> BillClassList = new List<BillClass>();
        public static List<ChangeQuantityClass> ChangeQuantityList = new List<ChangeQuantityClass>();
        public ProductClass Pc = new ProductClass();
        public OrderClass Oc = new OrderClass();
        public CustomerClass Cc = new CustomerClass();

        [HttpPost]
        public JsonResult BindPrice(string id)
        {
            var v = Oc.GetPrice(int.Parse(id));
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        // GET: Order
        public ActionResult OrderForm()
        {
            if (Session["quantityerror"] != null)
            {
                ViewData["error"] = Session["quantityerror"];
            }
            foreach (var data in ChangeQuantityList)
            {
                ViewData["ProductID"] = data.ProductId;
                ViewData["Product"] = data.ProductName;
                ViewData["Quantity"] = data.Quantity;
                ViewData["Price"] = data.Price;
                ViewData["Discount"] = data.DisPercentage;

            }
            ChangeQuantityList.Clear();
            ViewBag.ProductList = Pc.GetProductList().Where(x => x.IsActive == true);
            return View(BillDetailList);
        }

        [HttpPost]
        public ActionResult OrderForm(FormCollection fc)
        {
            if (fc["btnNewOrder"] == "Add New Order")
            {
                var selectlist = new List<BillDetailClass>();
                int q, q2;
                float disper = 0, disval = 0, totalprice;
                if (fc["txtProduct"] != null)
                {
                    //q = q2 = q3 = q4 = 0;
                    selectlist = BillDetailList.Where(x => x.ProductId == int.Parse(fc["txtProduct"])).ToList();
                }
                if (selectlist.Count == 0)
                {
                    q = int.Parse(fc["txtQuantity"]);
                    q2 = Oc.Quantity(Convert.ToInt32(int.Parse(fc["txtProduct"])));
                    if (q <= q2)
                    {
                        totalprice = float.Parse(fc["txtPrice"]) * q;
                        if (fc["txtDiscount"] != "")
                        {
                            disper = float.Parse(fc["txtDiscount"]);
                            disval = disper * totalprice / 100;
                        }


                        BillDetailClass bdc = new BillDetailClass
                        {
                            ProductId = Convert.ToInt32(fc["txtProduct"]),
                            ProductName = Oc.GetName(Convert.ToInt32(fc["txtProduct"])),
                            Quantity = int.Parse(fc["txtQuantity"]),
                            Price = float.Parse(fc["txtPrice"]),
                            DisPercentage = disper,
                            DisValue = disval,
                            Total = totalprice - disval
                        };
                        BillDetailList.Add(bdc);
                    }
                    else
                    {
                        ChangeQuantityClass cqc = new ChangeQuantityClass()
                        {
                            ProductId = Convert.ToInt32(fc["txtProduct"]),
                            ProductName = Oc.GetName(Convert.ToInt32(fc["txtProduct"])),
                            Quantity = int.Parse(fc["txtQuantity"]),
                            Price = float.Parse(fc["txtPrice"]),
                            DisPercentage = disper
                        };
                        ChangeQuantityList.Add(cqc);
                        Session["quantityerror"] = "Please enter quntity <=" + q2;
                    }
                }
                else
                {
                    foreach (var data in selectlist)
                    {
                        q = data.Quantity;
                        var q3 = Oc.Quantity(data.ProductId);
                        q2 = int.Parse(fc["txtQuantity"]);
                        var q4 = q3 - q;
                        if (q2 <= q4)
                        {
                            totalprice = float.Parse(fc["txtPrice"]) * (data.Quantity + q2);
                            if (fc["txtDiscount"] != "")
                            {
                                disper = float.Parse(fc["txtDiscount"]);
                                disval = disper * totalprice / 100;
                            }

                            BillDetailClass bdc = new BillDetailClass
                            {
                                ProductId = data.ProductId,
                                ProductName = data.ProductName,
                                Quantity = data.Quantity + q2,
                                Price = float.Parse(fc["txtPrice"]),
                                DisPercentage = float.Parse(disper.ToString(CultureInfo.InvariantCulture)),
                                DisValue = disval,
                                Total = totalprice - disval
                            };
                            var selectlist1 = BillDetailList.FirstOrDefault(x => x.ProductId == Convert.ToInt32(fc["txtProduct"]));
                            BillDetailList.Remove(selectlist1);
                            BillDetailList.Add(bdc);
                        }
                        else
                        {
                            ChangeQuantityClass cqc = new ChangeQuantityClass()
                            {
                                ProductId = Convert.ToInt32(fc["txtProduct"]),
                                ProductName = Oc.GetName(Convert.ToInt32(fc["txtProduct"])),
                                Quantity = int.Parse(fc["txtQuantity"]),
                                Price = float.Parse(fc["txtPrice"]),
                                DisPercentage = disper
                            };
                            ChangeQuantityList.Add(cqc);
                            Session["quantityerror"] = "Please enter quntity <=" + q4;
                        }
                    }
                }
            }
            return RedirectToAction("OrderForm");
        }

        [HttpGet]
        public ActionResult ChangeQuantity(int productId)
        {
            foreach (var data in BillDetailList.Where(x => x.ProductId == productId))
            {
                ChangeQuantityClass cqc = new ChangeQuantityClass()
                {
                    ProductId = data.ProductId,
                    ProductName = data.ProductName,
                    Quantity = data.Quantity,
                    Price = data.Price,
                    DisPercentage = data.DisPercentage
                };
                ChangeQuantityList.Add(cqc);
            }
            return RedirectToAction("OrderForm");
        }

        [HttpGet]
        public ActionResult DeleteQuantity(int productId)
        {
            var data = BillDetailList.FirstOrDefault(x => x.ProductId == productId);
            BillDetailList.Remove(data);
            return RedirectToAction("OrderForm");
        }

        [HttpGet]
        public ActionResult ConfirmOrder()
        {
            return RedirectToAction("ConfirmOrderForm");
        }

        [HttpGet]
        public ActionResult CancelOrder()
        {
            BillDetailList.Clear();
            BillClassList.Clear();
            ChangeQuantityList.Clear();
            return RedirectToAction("OrderForm");
        }


        //Confirm Order 

        [HttpPost]
        public JsonResult GetAddress(string id)
        {
            var v = Cc.GetAddress(int.Parse(id));
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCity(string id)
        {
            var v = Cc.GetCity(int.Parse(id));
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetState(string id)
        {
            var v = Cc.GetState(int.Parse(id));
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCountry(string id)
        {
            var v = Cc.GetCountry(int.Parse(id));
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConfirmOrderForm()
        {
            float total = 0;
            foreach (var data in BillDetailList)
            {
                total = total + data.Total;
            }
            ViewData["Total"] = total;
            ViewBag.CustomerList = Cc.GetCustomerList().Where(x => x.IsActive == true);
            return View(BillDetailList);
        }

        [HttpPost]
        public ActionResult ConfirmOrderForm(FormCollection fc)
        {
            if (fc["btnGenerateBill"] == "Generate Bill")
            {
                int cid = int.Parse(fc["txtCustomer"]);
                float total = float.Parse(fc["txtTotal"]);
                float shippingcharge = float.Parse(fc["txtShippingCharge"]);
                float grandtotal = float.Parse(fc["txtGrandTotal"]);

                Bill bill = new Bill()
                {
                    CustomerID = cid,
                    ShippingCharge = shippingcharge,
                    //BillDate = Convert.ToDateTime(date),
                    GrandTotal = grandtotal
                };
                string bid = Oc.SetBill(bill, BillDetailList);
                var result = Oc.GetBillListbyBillId(int.Parse(bid));
                foreach (var data in result)
                {
                    BillClass bc = new BillClass();

                    bc.BillId = data.BillID;
                    bc.CustomerId = int.Parse(data.CustomerID.ToString());
                    foreach (var data1 in Cc.GetCustomerListByCustomerId(int.Parse(data.CustomerID.ToString())))
                    {
                        bc.CustomerName = data1.Name;
                        bc.Address = data1.Address;
                        bc.Country = data1.City.State.Country.Country1;
                        bc.State = data1.City.State.State1;
                        bc.City = data1.City.City1;
                    }
                    bc.ShippingCharge = float.Parse(data.ShippingCharge.ToString());
                    bc.GrandTotal = float.Parse(data.GrandTotal.ToString());
                    bc.Status = data.IsCanceled.ToString();
                    BillClassList.Add(bc);
                }
            }
            return RedirectToAction("InvoiceForm");
        }

        [HttpGet]
        public ActionResult ConfirmDeleteQuantity(int productId)
        {
            var data = BillDetailList.FirstOrDefault(x => x.ProductId == productId);
            BillDetailList.Remove(data);
            return RedirectToAction("ConfirmOrderForm");
        }

        [HttpGet]
        public ActionResult OrderDelete()
        {
            BillDetailList.Clear();
            return RedirectToAction("OrderForm");
        }

        [HttpGet]
        public ActionResult BillList(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            BillDetailList.Clear();
            BillClassList.Clear();
            ChangeQuantityList.Clear();
            return View(Oc.GetBillList().ToPagedList(pageNumber,pageSize));
        }

        public ActionResult BillDetail(int billid)
        {
            foreach (var data in Oc.GetBillListbyBillId(billid))
            {
                BillClass bc = new BillClass()
                {
                    BillId = data.BillID,
                    CustomerId = data.Customer.CustomerID,
                    CustomerName = data.Customer.Name,
                    Address = data.Customer.Address,
                    Country = data.Customer.City.State.Country.Country1,
                    State = data.Customer.City.State.State1,
                    City = data.Customer.City.City1,
                    ShippingCharge = float.Parse(data.ShippingCharge.ToString()),
                    GrandTotal = float.Parse(data.GrandTotal.ToString()),
                    Status = data.IsCanceled.ToString()
                };
                BillClassList.Add(bc);
            }
            return RedirectToAction("InvoiceForm");
        }

        public ActionResult InvoiceForm()
        {
            int bid = 0;
            double total = 0;
            foreach (var data in BillClassList)
            {
                bid = data.BillId;
                ViewData["BillID"] = data.BillId;
                ViewData["Name"] = data.CustomerName;
                ViewData["Address"] = data.Address;
                ViewData["City"] = data.City;
                ViewData["Country"] = data.Country;
                ViewData["State"] = data.State;
                ViewData["ShippingCharge"] = data.ShippingCharge;
                ViewData["GrandTotal"] = data.GrandTotal;
                ViewData["Status"] = data.Status;
            }
            foreach (var data in Oc.GetBillDetailListbyBillId(bid))
            {
                total = total + double.Parse(data.ProductTotal.ToString());
            }
            ViewData["Total"] = total;
            return View(Oc.GetBillDetailListbyBillId(bid));
        }

        [HttpGet]
        public ActionResult CancelBillOrder(string billid)
        {
            Oc.CancelBill(int.Parse(billid));
            return RedirectToAction("BillList");
        }
    }
}