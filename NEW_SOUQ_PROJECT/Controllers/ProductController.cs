using NEW_SOUQ_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList.Mvc;
using PagedList;

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
        [Authorize(Roles ="Seller")]
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
        public ActionResult Create_Product(Product product,HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.Image = "/Content/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Content/Images/"), fileName);
                ImageFile.SaveAs(fileName);
                product.FK_UserId = User.Identity.GetUserId();

                ctx.Products.Add(product);
                ctx.SaveChanges();

            }
            ModelState.Clear();
            return RedirectToAction("SellerPage","Home",null);
            
        }

        
        //public ActionResult Search(string search, int? i)
        //{
        //    if (search != null)
        //    {
        //        BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel();
        //        bcvm.categories = ctx.Categories.ToList();
        //        bcvm.brands = ctx.Brands.ToList();
        //        bcvm.products = ctx.Products.Where(p => p.Name.Contains(search)).ToList().ToPagedList(i ?? 1, 10);
        //        ViewBag.title = search;
        //        ViewBag.count = bcvm.products.Count();


        //        return View("Products_Listing", bcvm);

        //    }
        //    else{
        //        return RedirectToAction("Products_Listing");
        //    }


        //}
        public ActionResult Products_Listing(string search, int? id, int? i)
        {
            BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel();
            bcvm.categories = ctx.Categories.ToList();
            bcvm.brands = ctx.Brands.ToList();
            if (search != null)
            {
                
                bcvm.products = ctx.Products.Where(p => p.Name.Contains(search)).ToList().ToPagedList(i ?? 1, 10);
                ViewBag.title = search;
                ViewBag.count = bcvm.products.Count();
                

            }
            else
            {
                if (id != null)
                {

                    bcvm.products = ctx.Products.Where(p => p.FK_CategoryId == id).ToList().ToPagedList(i ?? 1, 10);
                    ViewBag.title = ctx.Categories.Find(id).Name;

                }
                else
                {
                    bcvm.products = ctx.Products.ToList().ToPagedList(i ?? 1, 10);
                    ViewBag.title = "All Products";
                }

            }
            


            ViewBag.count = bcvm.products.Count();
            return View(bcvm);
        }
        public ActionResult Products_Listing_Brand(int? id, int? i)
        {

            BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel();
            bcvm.categories = ctx.Categories.ToList();
            bcvm.brands = ctx.Brands.ToList();
            if (id != null)
            {
                ViewBag.brand = true;
                bcvm.products = ctx.Products.Where(p => p.FK_BrandId == id).ToList().ToPagedList(i ?? 1, 10);
                ViewBag.title = ctx.Brands.Find(id).Name;

            }
            else
            {
                bcvm.products = ctx.Products.ToList().ToPagedList(i ?? 1, 10);
                ViewBag.title = "All Products";
            }


            ViewBag.count = bcvm.products.Count();
            return View("Products_Listing",bcvm);
        }

        //menna
        private string strCart = "Cart";
        // GET: Cart
        public ActionResult Index_Shop()
        {


            return View();
        }

        public ActionResult orderNow(int id)
        {

            if (Session["cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart(ctx.Products.Find(id), 1));
                Session["cart"] = cart;
            }

            else
            {
                List<Cart> cart = (List<Cart>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Cart(ctx.Products.Find(id), 1));
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index_Shop");
        }

        public ActionResult removeone(int id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index_Shop");
        }

        private int isExist(int id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }
        public ActionResult Edit_Product(int id)
        {
            Product product = ctx.Products.SingleOrDefault(x => x.Id == id);
            

            return PartialView("Edit_Price_Partial", product);
            //return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit_Product(Product product)

        {
            if (ModelState.IsValid)
            {
                Product newproduct = ctx.Products.FirstOrDefault(x => x.Id == product.Id);
                newproduct.Price = product.Price;
                newproduct.PriceAfterSale = product.PriceAfterSale;

            }
            

            ctx.SaveChanges();
            

            return RedirectToAction("AdminPage");
        }
        public ActionResult Delete_Product(int Id)
        { //u sure popup
            try
            {
                Product p = ctx.Products.Find(Id);
                ctx.Products.Remove(p);

                ctx.SaveChanges();
                //return RedirectToAction("SellerPage", "Home", null);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet); ;
            }

            
        }

    }
}