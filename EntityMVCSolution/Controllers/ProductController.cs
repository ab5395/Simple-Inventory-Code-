using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityMVCSolution.Models;

namespace EntityMVCSolution.Controllers
{
    public class ProductController : Controller
    {
        public ProductClass Pc=new ProductClass();
        public static List<EditProductClass> EditProductList=new List<EditProductClass>();
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            foreach (var data in EditProductList)
            {
                ViewData["ProductID"] = data.ProductId.ToString();
                ViewData["Quantity"] = data.Quantity.ToString();
                ViewData["SaleMRP"] = data.Mrp.ToString(CultureInfo.InvariantCulture);
                ViewData["Status"] = data.Status;
                ViewData["Name"] = data.Name;
            }
            EditProductList.Clear();
            return View(Pc.GetProductList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                Product product=new Product()
                {
                    Name = fc["txtName"],
                    Quantity = int.Parse(fc["txtQuantity"]),
                    SaleMRP = float.Parse(fc["txtSaleMRP"]),
                    IsActive = bool.Parse(fc["txtStatus"])
                };
                Pc.AddProduct(product);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                Product product = new Product()
                {
                    ProductID = int.Parse(fc["txtProductID"]),
                    Name = fc["txtName"],
                    Quantity = int.Parse(fc["txtQuantity"]),
                    SaleMRP = float.Parse(fc["txtSaleMRP"]),
                    IsActive = bool.Parse(fc["txtStatus"])
                };
                Pc.UpdateProduct(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            foreach (var data in Pc.GetProductListById(productId))
            {
                EditProductClass epc=new EditProductClass()
                {
                    ProductId = data.ProductID,
                    Quantity = int.Parse(data.Quantity.ToString()),
                    Mrp = float.Parse(data.SaleMRP.ToString()),
                    Name = data.Name,
                    Status = data.IsActive.ToString()
                };
                EditProductList.Add(epc);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateProductStatus(int productId, int status)
        {
            if (status == 0)
            {
                Product data = new Product()
                {
                    ProductID = productId,
                    IsActive = true
                };
                Pc.UpdateProductStatus(data);
            }
            else
            {
                Product data = new Product()
                {
                    ProductID = productId,
                    IsActive = false
                };
                Pc.UpdateProductStatus(data);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int productId)
        {
            Pc.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}