using Microsoft.EntityFrameworkCore;
using Stock.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DB.Repositories
{
    class MSSQLUserRepository : IRepository<User>
    {
        private readonly StockContext db;
        public MSSQLUserRepository(StockContext db)
        {
            this.db = db;
        }
        public Int32 Create(User item)
        {
            db.Users.Add(item);
            return db.Users.FirstAsync((user) => user.Login == item.Login).Id;
        }

        public async Task<Int32> CreateAsync(User item)
        {
            await db.Users.AddAsync(item);
            return db.Users.FirstAsync((user) => user.Login == item.Login).Id;
        }

        public Boolean Delete(int id)
        {
            User user = Get(id);
            if (user == null)
                return false;
            db.Users.Remove(user);
            return true;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public async Task<User> GetAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public IEnumerable<User> GetList()
        {
            return db.Users;
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await db.Users.ToListAsync<User>();
        }

        public Boolean Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
