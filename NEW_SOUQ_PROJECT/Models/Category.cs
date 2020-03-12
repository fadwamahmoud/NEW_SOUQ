using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEW_SOUQ_PROJECT.Models
{
    public class Category
    {
        public Category()
        {
            this.Brands = new HashSet<Brand>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Brand> Brands { get; set; }
    }
}