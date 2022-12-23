using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ClothStore.Models;

namespace ClothStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Nut them san pham. Chuc nang them san pham.
        public ActionResult Create()
        {
            return View();
        }

       public ActionResult ViewList()
        {
            return View();
        }


       
       // Chuc nang hien thi san pham bang JSON.
       public JsonResult DisplayList()
        {
            var listofproduct = new List<ClothProduct>();
            using (var i = new ClothesEntities())
            {
                listofproduct = i.ClothProducts.Where(x => x.Status >= 0).ToList();
            }
            return Json(listofproduct, JsonRequestBehavior.AllowGet);
        }

      [HttpPost]
      public JsonResult CreateProduct(int ?id, string productname, string productsize, string producttype, int productprice)
      {
            var product1 = new ClothProduct();
            
            using (var i = new ClothesEntities())
            {
                
               // Chuc nang sua thong tin.

                // Chuc nang them moi.
                if(id > 0)
                {
                    var producttemp = i.ClothProducts.FirstOrDefault(x => x.ProductId == id);
                    producttemp.ProductName = productname;
                    producttemp.ProductPrice = productprice;
                    producttemp.ProductSize = productsize;
                    producttemp.ProductType = producttype;
                    producttemp.Updateddate = DateTime.Now;

                    
                    i.SaveChanges();
                }
                else
                {
                    product1.ProductName = productname;
                    product1.ProductPrice = productprice;
                    product1.ProductSize = productsize;
                    product1.ProductType = producttype;
                    product1.Status = 0;
                    product1.Updateddate = DateTime.Now;
                    product1.Createddate = DateTime.Now;
                    i.ClothProducts.Add(product1);
                    i.SaveChanges();
                }
                    
                    
               
            }
            return Json("thanh cong", JsonRequestBehavior.AllowGet);
            
           
      }
      
      public JsonResult GetIdProduct(int ?id)
        {
            var product = new ClothProduct();
            using(var i = new ClothesEntities())
            {
                product = i.ClothProducts.FirstOrDefault(x => x.ProductId == id);
            }
            return Json(product, JsonRequestBehavior.AllowGet);
        }
      
      public JsonResult DeleteProduct(int ?id)
        {
            var product = new ClothProduct();
            using (var i = new ClothesEntities())
            {
                product = i.ClothProducts.FirstOrDefault(x => x.ProductId == id);
                product.Status = -1;
                i.SaveChanges();
            }
            return Json(product, JsonRequestBehavior.AllowGet);
        }
     

    }
}