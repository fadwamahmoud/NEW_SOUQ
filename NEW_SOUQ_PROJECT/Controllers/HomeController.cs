using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NEW_SOUQ_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NEW_SOUQ_PROJECT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            
            BrandsCategoriesViewModel bcvm = new BrandsCategoriesViewModel()
            {
                categories = ctx.Categories.ToList(),
                brands = ctx.Brands.ToList()
            };
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole user = new IdentityRole("User");
            IdentityRole seller = new IdentityRole("Seller");
            //creates object of role manager which has the create role function

            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            roleManager.Create(admin);
            roleManager.Create(user);
            roleManager.Create(seller);
            //assign views to roles
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            //ApplicationUser adminUser = userManager.FindByEmail("admin@h.com");
            //ApplicationUser userUser = userManager.FindByEmail("user@h.com");
            //ApplicationUser sellerUser = userManager.FindByEmail("seller@h.com");
            //userManager.AddToRole(adminUser.Id, "Admin");
            //adminUser.RoleName = "Admin";
            //userManager.AddToRole(userUser.Id, "User");
            //adminUser.RoleName = "User";
            //userManager.AddToRole(sellerUser.Id, "Seller");
            //adminUser.RoleName = "Seller";
            return View(bcvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        
        public ActionResult SellerPage()
        {
            string userId = User.Identity.GetUserId();
            if (User.IsInRole("Seller")) {
                List<Product> products = ctx.Products.Where(x => x.FK_UserId == userId).ToList();
                return View(products);

            }
            else
            {
                return RedirectToAction("Index");
            }

            
        }
        public ActionResult SellerRequest(string userId)
        {

            Request request = new Request {
                UserId = userId,
                Name="seller",
                Type="seller"


            };
            ctx.Requests.Add(request);
            ctx.SaveChanges();
            ViewBag.Message = "Your request has been sent.";

            return View();
        }


        public ActionResult UserPage()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<ActionResult> AdminPage()
        {
            if (User.IsInRole("Admin"))
            {
                AdminViewModel avm = new AdminViewModel();
                avm.categories = ctx.Categories.ToList();
                avm.brands = ctx.Brands.ToList();
                List<ApplicationUser> list = ctx.Users.ToList();
                foreach (var item in list)
                {
                    using (
                    var userManager =
                        new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                    {
                        var rolesForUser = await userManager.GetRolesAsync(item.Id);

                        ApplicationUser User = ctx.Users.Find(item.Id);
                        //ViewBag.rolesList = rolesForUser;
                        //User.RoleId = rolesForUser.First();

                    }

                }
                List<Request> Requests = ctx.Requests.ToList();
                avm.Requests = Requests;
                avm.Users = list;
                return PartialView("AdminPage", avm);

            }
            else
            {
                return RedirectToAction("Index");
            }
           

        }
        
        public ActionResult EditRole(string id)
        {
            ApplicationUser user = ctx.Users.SingleOrDefault(x => x.Id == id);
            UserRoleViewModel urvm = new UserRoleViewModel
            {
                roles = ctx.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }
                ).ToList(),
                User = user
                //assign user
            };
            //ViewBag.rolesList = urvm.roles; 
            
            return PartialView("Modal_Partial",urvm);
            //return Json(urvm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditRole(ApplicationUser user)
        {

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            userManager.RemoveFromRole(user.Id, "Admin");
            userManager.RemoveFromRole(user.Id, "User");
            userManager.RemoveFromRole(user.Id, "Seller");
            userManager.AddToRole(user.Id, user.RoleId);
            ApplicationUser AppUser = ctx.Users.Find(user.Id);
            AppUser.RoleId = user.RoleId;


            ctx.SaveChanges();


            return RedirectToAction("AdminPage");
        }

        //when user clicks on request a new brand or category
        //in future might need product id
        public ActionResult RequestNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RequestNew(Request request)
        {
            Request newRequest = new Request
            {
                UserId = User.Identity.GetUserId(),
                Name = request.Name,
                Type = request.Type


            };
            ctx.Requests.Add(newRequest);
            ctx.SaveChanges();

            return View("SellerPage");
        }
        public ActionResult ApproveRequest(int id)
        {
            Request request = ctx.Requests.Find(id);
            if (request.Type == "Category")
            {
                Category category = new Category()
                {
                    Name = request.Name
                    
                };
                ctx.Categories.Add(category);
                ctx.Requests.Remove(request);
                ctx.SaveChanges();
                
            }
            else 
            {
                Brand brand = new Brand()
                {
                    Name = request.Name
                };
                ctx.Brands.Add(brand);
            }

            return RedirectToAction("AdminPage");

        }
        public ActionResult DenyRequest(int id)
        {
            Request r = ctx.Requests.Find(id);
            ctx.Requests.Remove(r);
            ctx.SaveChanges();
            
            return new EmptyResult();
        }

        
        



    }
}