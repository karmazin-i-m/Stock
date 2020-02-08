using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DB.Repositories
{
    class MSSQLProductRepository : IRepository<Product>
    {
        private readonly StockContext db;

        public MSSQLProductRepository(StockContext db)
        {
            this.db = db;
        }
        public Int32 Create(Product item)
        {
            db.Products.Add(item);
            return db.Products.Find(item).Id;
        }

        public async Task<int> CreateAsync(Product item)
        {
            await db.Products.AddAsync(item);
            Product newItem = await db.Products.FindAsync(item);
            return newItem.Id;
        }

        public Boolean Delete(int id)
        {
            Product product = Get(id);
            if (product == null)
                return false;
            db.Products.Remove(product);
            return true;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public async Task<Product> GetAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public IEnumerable<Product> GetList()
        {
            return db.Products;
        }

        public async Task<IEnumerable<Product>> GetListAsync()
        {
            return await db.Products.ToListAsync<Product>();
        }

        public Boolean Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
