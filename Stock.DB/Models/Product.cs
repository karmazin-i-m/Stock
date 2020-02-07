using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB.Models
{
    public class Product
    {
        public Int32 Id { get; set; }
        public String ProductName { get; set; }
        public Double Weight { get; set; }
        public Double Price { get; set; }
        public Order Order { get; set; }
    }
}
