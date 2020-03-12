using NEW_SOUQ_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEW_SOUQ_PROJECT.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: Product
        public ActionResult Single_Product()
        {

            return View();
        }
        public ActionResult Create_Product()
        {

            return View();
        }
        public ActionResult Products_Listing(int id)
        {
            BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel();
            bcvm.categories = ctx.Categories.ToList();
            bcvm.brands = ctx.Brands.ToList();
            bcvm.products = ctx.Products.Where(p => p.FK_CategoryId == id).ToList();

            ViewBag.title = ctx.Categories.Find(id).Name;
            ViewBag.count = bcvm.products.Count();
            return View(bcvm);
        }
        public ActionResult Products_Listing_Brand(int id)
        {

            BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel();
            bcvm.categories = ctx.Categories.ToList();
            bcvm.brands = ctx.Brands.ToList();
            bcvm.products = ctx.Products.Where(p => p.FK_BrandId == id).ToList();

            ViewBag.count = bcvm.products.Count();
            ViewBag.title = ctx.Brands.Find(id).Name;

            return View("Products_Listing",bcvm);
        }
    }
}