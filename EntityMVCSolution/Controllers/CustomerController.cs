using System.Collections.Generic;
using System.Web.Mvc;
using EntityMVCSolution.Models;

namespace EntityMVCSolution.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerClass Cc =new CustomerClass();
        public BasicClass Bc =new BasicClass();
        public static List<EditCustomerClass> EditCustomerList=new List<EditCustomerClass>();

        public JsonResult BindCityList(int id)
        {
            var state = Bc.GetCityListByStateId(id);
            return Json(new SelectList(state.ToArray(), "CityID", "City1"), JsonRequestBehavior.AllowGet);
        }

        // GET: Customer
        [HttpGet]
        public ActionResult CustomerForm()
        {
            foreach (var data in EditCustomerList)
            {
                ViewData["CustomerId"] = data.CustomerId;
                ViewData["Name"] = data.Name;
                ViewData["Address"] = data.Address;
                ViewData["CityId"] = data.CityId;
                ViewData["City"] = data.City;
                ViewData["StateId"] = data.StateId;
                ViewData["State"] = data.State;
                ViewData["CountryID"] = data.CountryId;
                ViewData["Country"] = data.Country;
                ViewData["Status"] = data.Status;

            }
            EditCustomerList.Clear();
            ViewBag.CountryList = Bc.GetCountryList();
            return View(Cc.GetCustomerList());
        }

        [HttpPost]
        public ActionResult CustomerForm(FormCollection fc)
        {
           // int cid=0;
            if (fc["btnInsert"] == "Insert")
            {
                //if (fc["txtCity"] != "")
                //{
                //    cid = int.Parse(fc["txtCity"]);
                //}
                Customer customer=new Customer()
                {
                    Name = fc["txtName"],
                    Address = fc["txtAddress"],
                    CityID = int.Parse(fc["txtCity"]),
                    //CityID = cid,
                    IsActive = bool.Parse(fc["txtStatus"])
                };
                Cc.AddCustomer(customer);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                Customer customer = new Customer()
                {
                    CustomerID = int.Parse(fc["txtCustomerID"]),
                    Name = fc["txtName"],
                    Address = fc["txtAddress"],
                    CityID = int.Parse(fc["txtCity"]),
                    IsActive = bool.Parse(fc["txtStatus"])
                };
                Cc.UpdateCustomer(customer);
            }
            return RedirectToAction("CustomerForm");
        }

        [HttpGet]
        public ActionResult EditCustomer(int customerId)
        {
            var result = Cc.GetCustomerListByCustomerId(customerId);
            foreach (var data in result)
            {
                EditCustomerClass edc=new EditCustomerClass()
                {
                    CustomerId = data.CustomerID,
                    Name = data.Name,
                    Address = data.Address,
                    CityId = data.City.CityID,
                    City = data.City.City1,
                    State = data.City.State.State1,
                    StateId = data.City.State.StateID,
                    Country = data.City.State.Country.Country1,
                    CountryId = data.City.State.Country.CountryID,
                    Status = bool.Parse(data.IsActive.ToString())
                };
                EditCustomerList.Add(edc);
            }
            return RedirectToAction("CustomerForm");
        }

        [HttpGet]
        public ActionResult DeleteCustomer(int customerId)
        {
            Cc.DeleteCustomer(customerId);
            return RedirectToAction("CustomerForm");
        }

        [HttpGet]
        public ActionResult UpdateCustomerStatus(int customerId,int status)
        {
            if (status == 0)
            {
                Customer data=new Customer()
                {
                    CustomerID = customerId,
                    IsActive = true
                };
                Cc.UpdateCustomerStatus(data);
            }
            else
            {
                Customer data = new Customer()
                {
                    CustomerID = customerId,
                    IsActive = false
                };
                Cc.UpdateCustomerStatus(data);
            }
            return RedirectToAction("CustomerForm");
        }
    }
}