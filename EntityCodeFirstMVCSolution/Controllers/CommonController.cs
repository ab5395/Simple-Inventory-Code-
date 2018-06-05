using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityCodeFirstMVCSolution.Models;

namespace EntityCodeFirstMVCSolution.Controllers
{
    public class CommonController : Controller
    {
        public SimpleDomain Sp = new SimpleDomain();
        public static List<EditCountryCityStateClass> EditList = new List<EditCountryCityStateClass>();
        public static List<CityClass> CityClassList = new List<CityClass>();

        [HttpGet]
        public ActionResult ValidationForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidationForm([Bind(Exclude = "Id")]SampleValidation productToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            // TODO: Add insert logic here
            return RedirectToAction("ValidationForm");
        }

        [HttpGet]
        public ActionResult StudentFormValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentFormValidation([Bind(Exclude = "StudentId")]StudentClass sc)
        {
            if (!ModelState.IsValid)
                return View();

            // TODO: Add insert logic here
            return RedirectToAction("StudentFormValidation");
        }

        //Country
        [HttpGet]
        public ActionResult CountryForm()
        {
            foreach (var data in EditList)
            {
                ViewData["CountryId"] = data.CountryId;
                ViewData["Country"] = data.CountryName;
            }
            EditList.Clear();
            return View(Sp.GetCountryList());
        }

        [HttpPost]
        public ActionResult CountryForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                Country country = new Country()
                {
                    CountryName = fc["txtCountry"]
                };
                Sp.AddCountry(country);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                Country country = new Country()
                {
                    CountryId = int.Parse(fc["txtID"]),
                    CountryName = fc["txtCountry"]
                };
                Sp.UpdateCountry(country);
            }
            return RedirectToAction("CountryForm");
        }

        [HttpGet]
        public ActionResult EditCountry(int id)
        {
            var result = Sp.GetCountryList().Where(x => x.CountryId == id);
            foreach (var data in result)
            {
                EditCountryCityStateClass ec = new EditCountryCityStateClass()
                {
                    CountryId = data.CountryId,
                    CountryName = data.CountryName
                };
                EditList.Add(ec);
            }
            return RedirectToAction("CountryForm");
        }


        //State
        [HttpGet]
        public ActionResult StateForm()
        {
            int cid = 0;
            foreach (var data in EditList)
            {
                cid = data.CountryId;
                ViewData["StateID"] = data.StateId;
                ViewData["State"] = data.StateName;
            }
            var countryname = Sp.GetCountryByid(cid);
            ViewBag.CountryList = EditList.Count != 0 ? new SelectList(Sp.GetCountryList(), "CountryId", "CountryName", countryname.CountryId) : new SelectList(Sp.GetCountryList(), "CountryId", "CountryName");
            EditList.Clear();
            return View(Sp.GetStateList());
        }

        [HttpPost]
        public ActionResult StateForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                State state = new State()
                {
                    StateName = fc["txtState"],
                    CountryId = int.Parse(fc["CountryList"])
                };
                Sp.AddState(state);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                State state = new State()
                {
                    StateId = int.Parse(fc["txtID"]),
                    StateName = fc["txtState"],
                    CountryId = int.Parse(fc["CountryList"])
                };
                Sp.UpdateState(state);
            }
            return RedirectToAction("StateForm");
        }

        [HttpGet]
        public ActionResult EditState(int id)
        {
            var result = Sp.GetStateList().Where(x => x.StateId == id);
            foreach (var data in result)
            {
                int cid = data.CountryId;
                var countryname = Sp.GetCountryByid(cid);
                EditCountryCityStateClass ec = new EditCountryCityStateClass()
                {
                    CountryId = data.CountryId,
                    CountryName = countryname.CountryName,
                    StateId = data.StateId,
                    StateName = data.StateName
                };
                EditList.Add(ec);
            }
            return RedirectToAction("StateForm");
        }

        public JsonResult StateList(int id)
        {
            var state = Sp.GetStateList().Where(x => x.CountryId == id);
            return Json(new SelectList(state.ToArray(), "StateId", "StateName"), JsonRequestBehavior.AllowGet);
        }

        //City
        [HttpGet]
        public ActionResult CityForm()
        {
            int cid = 0, sid = 0;
            foreach (var data in EditList)
            {
                cid = data.CountryId;
                sid = data.StateId;
                ViewData["CityId"] = data.CityId;
                ViewData["City"] = data.CityName;
            }
            var countryname = Sp.GetCountryByid(cid);
            var state = Sp.GetStateByid(sid);
            if (EditList.Count != 0)
            {
                ViewBag.CountryList = new SelectList(Sp.GetCountryList(), "CountryId", "CountryName", countryname.CountryId);
                ViewBag.StateList = new SelectList(Sp.GetStateList().Where(x=>x.CountryId==cid), "StateId", "StateName", state.StateId);
            }
            else
            {
                ViewBag.CountryList = new SelectList(Sp.GetCountryList(), "CountryId", "CountryName");
                ViewBag.StateList = new SelectList("Select State"); 
            }
            EditList.Clear();
            return View(Sp.GetCityClassList());
        }

        [HttpPost]
        public ActionResult CityForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                City city = new City()
                {
                    CityName = fc["txtCity"],
                    StateId = int.Parse(fc["StateList"])
                };
                Sp.AddCity(city);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                City city = new City()
                {
                    CityId = int.Parse(fc["txtID"]),
                    CityName = fc["txtCity"],
                    StateId = int.Parse(fc["StateList"])
                };
                Sp.UpdateCity(city);
            }
            return RedirectToAction("CityForm");
        }

        [HttpGet]
        public ActionResult EditCity(int id)
        {
            var result = Sp.GetCityList().Where(x => x.CityId == id);
            foreach (var data in result)
            {
                int sid = data.StateId;
                var state = Sp.GetStateByid(sid);
                EditCountryCityStateClass ec = new EditCountryCityStateClass()
                {
                    CityId = data.CityId,
                    CityName = data.CityName,
                    StateId = data.StateId,
                    CountryId = state.CountryId
                };
                EditList.Add(ec);
            }
            return RedirectToAction("CityForm");
        }

    }
}