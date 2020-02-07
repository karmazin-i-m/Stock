using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB.Repositories
{
    class MSSQLOrderRepository : IRepository<Order>
    {
        private readonly StockContext db;

        public MSSQLOrderRepository(StockContext db)
        {
            this.db = db;
        }

        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(int id)
        {
            Order order = Get(id);
            if (order != null)
                db.Orders.Remove(order);
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetList()
        {
            return db.Orders;
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
