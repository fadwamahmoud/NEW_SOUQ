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
        public ActionResult Single_Product(int? id)
        {
            List<Category> categories = ctx.Categories.ToList();
            List<Brand> brands = ctx.Brands.ToList();
            ViewBag.brands = brands;
            ViewBag.categories = categories;

            if (id!= null)
            {
                Product product = ctx.Products.SingleOrDefault(x => x.Id == id);
                return View(product);
            }
            else
            {
                return RedirectToAction("Products_Listing");
            }

            
        }
        //essraa
        [HttpGet]
        public ActionResult Create_Product()
        {
            CreateProductViewModel provm = new CreateProductViewModel();

            provm.categories = ctx.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            provm.brands = ctx.Brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();

            return View(provm);
        }


        [HttpPost]
        public ActionResult Create_Product(Product product)
        {
            if (ModelState.IsValid)
            {

                ctx.Products.Add(product);
                ctx.SaveChanges();

            }
            ModelState.Clear();
            return RedirectToAction("Index");
            
        }

        public ActionResult Products_Listing(int? id)
        {
            BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel();
            bcvm.categories = ctx.Categories.ToList();
            bcvm.brands = ctx.Brands.ToList();
            if(id != null)
            {
                bcvm.products = ctx.Products.Where(p => p.FK_CategoryId == id).ToList();
                ViewBag.title = ctx.Categories.Find(id).Name;

            }
            else
            {
                bcvm.products = ctx.Products.ToList();
                ViewBag.title = "All Products";
            }

            
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