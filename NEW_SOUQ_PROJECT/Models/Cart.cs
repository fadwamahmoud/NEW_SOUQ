using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEW_SOUQ_PROJECT.Models
{
    public class Cart
    {
        public Cart(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product
        {
            get;
            set;
        }

        public int Quantity { get; set; }


        
    }
}