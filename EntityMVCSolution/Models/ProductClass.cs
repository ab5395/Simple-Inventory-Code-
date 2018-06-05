using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityMVCSolution.Models
{
    public class ProductClass
    {
        public InventorySystemEntities Entites = new InventorySystemEntities();

        public List<Product> GetProductList()
        {
            return Entites.Products.Where(x => x.IsDeleted == false).ToList();
        }

        public List<Product> GetProductListById(int productid)
        {
            return Entites.Products.Where(x => x.ProductID == productid).ToList();
        }

        public void AddProduct(Product product)
        {
            Product pd = new Product()
            {
                Name = product.Name,
                Quantity = product.Quantity,
                SaleMRP = product.SaleMRP,
                IsActive = product.IsActive,
                IsDeleted = false
            };
            Entites.Products.Add(pd);
            Entites.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var data = Entites.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
            if (data != null)
            {
                data.Name = product.Name;
                data.Quantity = product.Quantity;
                data.SaleMRP = product.SaleMRP;
                data.IsActive = product.IsActive;
            }
            Entites.SaveChanges();
        }

        public void UpdateProductStatus(Product product)
        {
            var data = Entites.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
            if (data != null)
            {
                data.IsActive = product.IsActive;
            }
            Entites.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var data = Entites.Products.FirstOrDefault(x => x.ProductID == id);
            if (data != null)
            {
                data.IsActive = false;
                data.IsDeleted = true;
            }
            Entites.SaveChanges();
        }


    }

    public class EditProductClass
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float Mrp { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

    }
}