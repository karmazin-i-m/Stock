using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB.Repositories
{
    class MSSQLProductRepository : IRepository<Product>
    {
        private readonly StockContext db;

        public MSSQLProductRepository(StockContext db)
        {
            this.db = db;
        }
        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = Get(id);
            if (product != null)
                db.Products.Remove(product);
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetList()
        {
            return db.Products;
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
