using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEW_SOUQ_PROJECT.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}