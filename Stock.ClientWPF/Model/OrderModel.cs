using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.Model
{
    class OrderModel
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        public UserModel User { get; set; }

        public OrderModel()
        {
            this.Products = new List<ProductModel>();
        }
    }
}
