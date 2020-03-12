using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEW_SOUQ_PROJECT.Models
{
    public class UserRoleViewModel
    {
       public List<SelectListItem> roles { get; set; }
       public ApplicationUser User { get; set; }
    }
}