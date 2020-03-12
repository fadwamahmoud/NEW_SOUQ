using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEW_SOUQ_PROJECT.Models
{
    public class AdminViewModel
    {
        public List<ApplicationUser> Users = new List<ApplicationUser>();
        public List<Request> Requests = new List<Request>();
        public List<Category> categories { set; get; }
        public List<Brand> brands { get; set; }

    }
}