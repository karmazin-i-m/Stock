using Stock.DB.Models;
using Stock.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Stock.DB
{
    public class UnitOfWork : IDisposable
    {
        private static readonly UnitOfWork instance = null;

        private readonly StockContext db;
        private IRepository<User> usersRepositories;
        private IRepository<Product> productsRepositories;
        private IRepository<Order> ordersRepositories;

        public IRepository<User> Users
        {
            get
            {
                if (usersRepositories == null)
                    usersRepositories = new MSSQLUserRepository(db);
                return usersRepositories;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (ordersRepositories == null)
                    ordersRepositories = new MSSQLOrderRepository(db);
                return ordersRepositories;
            }
        }
        public IRepository<Product> Products
        {
            get
            {
                if (productsRepositories == null)
                    productsRepositories = new MSSQLProductRepository(db);
                return productsRepositories;
            }
        }

        private UnitOfWork()
        {
            this.db = new StockContext();
        }

        public static UnitOfWork GetInstance()
        {
            if (instance == null)
                return new UnitOfWork();
            return instance;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
