using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.Model
{
    class ProductModel
    {
        public static IEnumerable<ProductModel> Products { get; set; }

        public Int32 Id { get; set; }
        public String ProductName { get; set; }
        public Double Weight { get; set; }
        public Double Price { get; set; }
        public OrderModel Order { get; set; }
    }
}
