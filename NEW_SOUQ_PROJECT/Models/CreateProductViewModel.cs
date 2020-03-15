using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEW_SOUQ_PROJECT.Models
{
    public class CreateProductViewModel
    {
        public List<SelectListItem> categories { set; get; }
        public List<SelectListItem> brands { get; set; }
        public Product product { get; set; }

    }
}