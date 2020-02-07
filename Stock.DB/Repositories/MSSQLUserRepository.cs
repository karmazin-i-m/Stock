using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB.Repositories
{
    class MSSQLUserRepository : IRepository<User>
    {
        private readonly StockContext db;
        public MSSQLUserRepository(StockContext db)
        {
            this.db = db;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = Get(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetList()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
