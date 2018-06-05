using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Services;
using EntityMVCSolution.Models;

namespace EntityMVCSolution.Controllers
{
    public class BasicController : Controller
    {
        public BasicClass Bc = new BasicClass();
        public static List<CityClass> CityClassList = new List<CityClass>();
        public static List<StateClass> StateClassList = new List<StateClass>();
        public static List<CountryClass> CountryClassList = new List<CountryClass>();

        public JsonResult BindStateList(int id)
        {
            var state = Bc.GetStateListByCountryId(id);
            //var state = from s in db.States
            //            where s.CountryID == id
            //            select s;

            return Json(new SelectList(state.ToArray(), "StateID", "State1"), JsonRequestBehavior.AllowGet);
        }
        
        //CityForm
        [HttpGet]
        public ActionResult CityForm()
        {
            foreach (var data in CityClassList)
            {
                ViewData["CityID"] = data.CityId;
                ViewData["City"] = data.City;
            }
            CityClassList.Clear();
            ViewBag.CountryList = Bc.GetCountryList();
            return View(Bc.GetCityList());
        }

        [HttpPost]
        public ActionResult CityForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                City city = new City()
                {
                    City1 = fc["txtCity"],
                    StateID = int.Parse(fc["txtState"])
                };
                Bc.AddCity(city);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                City city = new City()
                {
                    CityID = int.Parse(fc["txtCityID"]),
                    City1 = fc["txtCity"]
                };
                Bc.UpdateCity(city);
            }
            return RedirectToAction("CityForm");
        }

        [HttpGet]
        public ActionResult EditCity(int cityId)
        {
            foreach (var data in Bc.GetCityListByCityId(cityId))
            {
                CityClass cc = new CityClass()
                {
                    CityId = data.CityID,
                    City = data.City1
                };
                CityClassList.Add(cc);
            }
            return RedirectToAction("CityForm");
        }


        //StateForm
        [HttpGet]
        public ActionResult StateForm()
        {
            foreach (var data in StateClassList)
            {
                ViewData["StateID"] = data.StateId;
                ViewData["State"] = data.State;
                ViewData["CountryID"] = data.CountryId;
                ViewData["Country"] = data.Country;
            }
            StateClassList.Clear();
            ViewBag.CountryList = Bc.GetCountryList();
            return View(Bc.GetStateList());
        }

        [HttpPost]
        public ActionResult StateForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                State state=new State()
                {
                    State1 = fc["txtState"],
                    CountryID = int.Parse(fc["txtCountry"])
                };
                Bc.AddState(state);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                State state = new State()
                {
                    StateID = int.Parse(fc["txtStateID"]),
                    State1 = fc["txtState"],
                    CountryID = int.Parse(fc["txtCountry"])
                };
                Bc.UpdateState(state);
            }
            return RedirectToAction("StateForm");
        }

        [HttpGet]
        public ActionResult EditState(int stateId)
        {
            foreach (var data in Bc.GetStateListByStateId(stateId))
            {
                StateClass sc=new StateClass()
                {
                    StateId = data.StateID,
                    State = data.State1,
                    CountryId = data.Country.CountryID,
                    Country = data.Country.Country1
                };
                StateClassList.Add(sc);
            }
            return RedirectToAction("StateForm");
        }

        //CountryForm
        [HttpGet]
        public ActionResult CountryForm()
        {
            foreach (var data in CountryClassList)
            {
                ViewData["CountryID"] = data.CountryId;
                ViewData["Country"] = data.Country;
            }
            CountryClassList.Clear();
            return View(Bc.GetCountryList());
        }

        [HttpPost]
        public ActionResult CountryForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                Country country=new Country()
                {
                    Country1 = fc["txtCountry"]
                };
                Bc.AddCountry(country);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                Country country = new Country()
                {
                    CountryID = int.Parse(fc["txtCountryID"]),
                    Country1 = fc["txtCountry"]
                };
                Bc.UpdateCountry(country);
            }
            return RedirectToAction("CountryForm");
        }

        [HttpGet]
        public ActionResult EditCountry(int countryId)
        {
            foreach (var data in Bc.GetCountryListByCountryId(countryId))
            {
                CountryClass cc=new CountryClass()
                {
                    CountryId = data.CountryID,
                    Country =data.Country1
                };
                CountryClassList.Add(cc);
            }
            return RedirectToAction("CountryForm");
        }
    }
}