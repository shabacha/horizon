using horizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace horizon.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Product product = new Product();
            ViewBag.ListProduct = product.List();
            return View();
        }

        public ActionResult AddProduct()
        {
            ViewBag.Titre = "Ajout article";
            return View();
        }
        public ActionResult UpdateProduct(int id)
        {
            Product product = new Product();
            product.Affiche(id);
            ViewBag.Titre = "Modification article";
            return View("AddProduct", product);
        }

        public ActionResult DetailProduct(int id)
        {
            Product product = new Product();
            product.Affiche(id);
            return View(product);
        }

        public ActionResult Save(Product product)
        {
            if (product.Id != 0)
            {
                product.Update();
            }
            else
            {
                product.Add();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Product product = new Product();
            ViewBag.msg = product.Delete(id);
            return RedirectToAction("Index");
        }
    }
}