using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB.Models
{
    public class Order
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public User User { get; set; }

        public Order()
        {
            this.Products = new List<Product>();
        }
    }
}
