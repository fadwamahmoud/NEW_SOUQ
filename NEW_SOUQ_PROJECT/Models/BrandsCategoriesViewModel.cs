using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEW_SOUQ_PROJECT.Models
{
    public class BrandsCategoriesViewModel
    {
        public List<Category> categories { set; get; }
        public List<Brand> brands { get; set; }
        public List<Product> products { get; set; }

    }
}