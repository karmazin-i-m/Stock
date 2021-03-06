﻿using Stock.DB.Models;
using Stock.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.DB.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly StockContext db;
        private IRepository<User> usersRepositories;
        private IRepository<Product> productsRepositories;
        private IRepository<Order> ordersRepositories;
        private static readonly Object _lock = new object();

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

        public UnitOfWork()
        {
            this.db = new StockContext();
        }

        public Boolean Save()
        {
            lock(_lock)
                db.SaveChanges();
            return true;
        }
        public async Task<bool> SaveAsync()
        {
            await db.SaveChangesAsync();
            return true;
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
