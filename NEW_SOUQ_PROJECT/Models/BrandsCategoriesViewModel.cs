using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;
namespace NEW_SOUQ_PROJECT.Models
{
    public class BrandsCategoriesViewModel
    {
        public List<Category> categories { set; get; }
        public List<Brand> brands { get; set; }
        public IPagedList<Product> products { get; set; }

    }
}