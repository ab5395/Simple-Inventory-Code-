using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityMVCSolution.Models
{
    public class BasicClass
    {
        public InventorySystemEntities Entities = new InventorySystemEntities();


        //City Entity
        public List<City> GetCityList()
        {
            return Entities.Cities.ToList();
        }

        public List<City> GetCityListByCityId(int cityId)
        {
            return Entities.Cities.Where(x => x.CityID == cityId).ToList();
        }

        public List<City> GetCityListByStateId(int stateId)
        {
            return Entities.Cities.Where(x => x.StateID == stateId).ToList();
        }

        public void AddCity(City city)
        {
            City objCity = new City()
            {
                City1 = city.City1,
                StateID = city.StateID
            };
            Entities.Cities.Add(objCity);
            Entities.SaveChanges();
        }

        public void DeleteCity(int cid)
        {
            var data = Entities.Cities.FirstOrDefault(x => x.CityID == cid);
            Entities.Cities.Remove(data);
            Entities.SaveChanges();
        }

        public void UpdateCity(City city)
        {
            var data = Entities.Cities.FirstOrDefault(x => x.CityID == city.CityID);
            if (data != null) data.City1 = city.City1;
            Entities.SaveChanges();
        }


        //State Entity

        public List<State> GetStateList()
        {
            return Entities.States.ToList();
        }

        public List<State> GetStateListByStateId(int stateId)
        {
            return Entities.States.Where(x => x.StateID == stateId).ToList();
        }

        public List<State> GetStateListByCountryId(int countryId)
        {
            return Entities.States.Where(x => x.CountryID == countryId).ToList();
        }

        public void AddState(State state)
        {
            State objState = new State()
            {
                State1 = state.State1,
                CountryID = state.CountryID
            };
            Entities.States.Add(objState);
            Entities.SaveChanges();
        }

        public void DeleteState(int sid)
        {
            var data = Entities.States.FirstOrDefault(x => x.StateID == sid);
            Entities.States.Remove(data);
            Entities.SaveChanges();
        }

        public void UpdateState(State state)
        {
            var data = Entities.States.FirstOrDefault(x => x.StateID == state.StateID);
            if (data != null) data.State1 = state.State1;
            Entities.SaveChanges();
        }


        //Country Entity

        public List<Country> GetCountryList()
        {
            return Entities.Countries.ToList();
        }

        public List<Country> GetCountryListByCountryId(int countryId)
        {
            return Entities.Countries.Where(x => x.CountryID == countryId).ToList();
        }
        
        public void AddCountry(Country country)
        {
            Country objCountry = new Country()
            {
                Country1 = country.Country1
            };
            Entities.Countries.Add(objCountry);
            Entities.SaveChanges();
        }

        public void DeleteCountry(int cid)
        {
            var data = Entities.Countries.FirstOrDefault(x => x.CountryID == cid);
            Entities.Countries.Remove(data);
            Entities.SaveChanges();
        }

        public void UpdateCountry(Country country)
        {
            var data = Entities.Countries.FirstOrDefault(x => x.CountryID == country.CountryID);
            if (data != null) data.Country1 = country.Country1;
            Entities.SaveChanges();
        }
    }

    public class CityClass
    {
        public int CityId { get; set; }
        public string City { get; set; }
    }

    public class StateClass
    {
        public int StateId { get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
    }

    public class CountryClass
    {
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}