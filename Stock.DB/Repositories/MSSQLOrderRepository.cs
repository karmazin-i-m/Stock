using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DB.Repositories
{
    class MSSQLOrderRepository : IRepository<Order>
    {
        private readonly StockContext db;

        public MSSQLOrderRepository(StockContext db)
        {
            this.db = db;
        }

        public Int32 Create(Order item)
        {
            db.Orders.Add(item);
            return db.Orders.Find(item).Id;
        }

        public async Task<int> CreateAsync(Order item)
        {
            await db.Orders.AddAsync(item);
            return db.Orders.FindAsync(item).Id;
        }

        public Boolean Delete(int id)
        {
            Order order = Get(id);
            if (order == null)
                return false;
            db.Orders.Remove(order);
            return true;
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public async Task<Order> GetAsync(int id)
        {
            return await db.Orders.FindAsync(id);
        }

        public IEnumerable<Order> GetList()
        {
            return db.Orders;
        }

        public async Task<IEnumerable<Order>> GetListAsync()
        {
            return await db.Orders.ToListAsync<Order>();
        }

        public Boolean Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
