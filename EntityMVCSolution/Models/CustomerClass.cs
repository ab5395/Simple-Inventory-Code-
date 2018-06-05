using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityMVCSolution.Models
{
    public class CustomerClass
    {
        public InventorySystemEntities Entites = new InventorySystemEntities();


        //CustomerEntities
        public List<Customer> GetCustomerList()
        {
            return Entites.Customers.Where(x => x.IsDeleted == false).ToList();
        }

        public List<Customer> GetCustomerListByCustomerId(int customerid)
        {
            return Entites.Customers.Where(x => x.CustomerID == customerid).ToList();
        }

        public void AddCustomer(Customer customer)
        {
            Customer cus = new Customer()
            {
                Name = customer.Name,
                Address = customer.Address,
                CityID = customer.CityID,
                IsActive = customer.IsActive,
                IsDeleted = false
            };
            Entites.Customers.Add(cus);
            Entites.SaveChanges();
        }

        public string GetAddress(int customerId)
        {
            var data = Entites.Customers.Single(x => x.CustomerID == customerId);
            return data.Address;
        }

        public string GetCountry(int customerId)
        {
            var data = Entites.Customers.Single(x => x.CustomerID == customerId);
            return data.City.State.Country.Country1;
        }

        public string GetCity(int customerId)
        {
            var data = Entites.Customers.Single(x => x.CustomerID == customerId);
            return data.City.City1;
        }

        public string GetState(int customerId)
        {
            var data = Entites.Customers.Single(x => x.CustomerID == customerId);
            return data.City.State.State1;
        }

        public void UpdateCustomer(Customer customer)
        {
            Customer data = Entites.Customers.FirstOrDefault(x => x.CustomerID == customer.CustomerID);
            if (data != null)
            {
                data.Name = customer.Name;
                data.Address = customer.Address;
                data.CityID = customer.CityID;
                data.IsActive = customer.IsActive;
            }
            Entites.SaveChanges();
        }

        public void UpdateCustomerStatus(Customer customer)
        {
            var data = Entites.Customers.FirstOrDefault(x => x.CustomerID == customer.CustomerID);
            if (data != null)
            {
                data.IsActive = customer.IsActive;
            }
            Entites.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var data = Entites.Customers.FirstOrDefault(x => x.CustomerID == id);
            if (data != null)
            {
                data.IsActive = false;
                data.IsDeleted = true;
            }
            Entites.SaveChanges();
        }


    }

    public class EditCustomerClass
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public bool Status { get; set; }

    }
}