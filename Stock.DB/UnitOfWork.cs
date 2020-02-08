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

        public UnitOfWork()
        {
            this.db = new StockContext();
            //Thread.Sleep(2000);
            StartDataInitialize();
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

        private void StartDataInitialize()
        {
            Product product1 = new Product() { ProductName = "Яблоко", Price = 21.3, Weight = 13.3 };
            Product product2 = new Product() { ProductName = "Арбуз", Price = 21.3, Weight = 1.4 };
            Product product3 = new Product() { ProductName = "Дыня", Price = 21.3, Weight = 12 };

            Product product4 = new Product() { ProductName = "Апельсин", Price = 21.3, Weight = 13.3 };
            Product product5 = new Product() { ProductName = "Мандарин", Price = 21.3, Weight = 13.3 };
            Product product6 = new Product() { ProductName = "Виноград", Price = 21.3, Weight = 13.3 };

            Product product7 = new Product() { ProductName = "Курица", Price = 21.3, Weight = 13.3 };
            Product product8 = new Product() { ProductName = "Свинина", Price = 21.3, Weight = 13.3 };
            Product product9 = new Product() { ProductName = "Говядина", Price = 21.3, Weight = 13.3 };

            Products.Create(product1);
            Products.Create(product2);
            Products.Create(product3);

            Products.Create(product4);
            Products.Create(product5);
            Products.Create(product6);

            Products.Create(product7);
            Products.Create(product8);
            Products.Create(product9);

            Order order1 = new Order() { Date = DateTime.Now, Products = new List<Product>() { product1, product2, product3 } };
            Order order2 = new Order() { Date = DateTime.Now, Products = new List<Product>() { product4, product5, product6 } };
            Order order3 = new Order() { Date = DateTime.Now, Products = new List<Product>() { product7, product8, product9 } };

            Orders.Create(order1);
            Orders.Create(order2);
            Orders.Create(order3);

            User user1 = new User() { Name = "Illya", Email = "illya@gmail.com", Login = "illya_ua", Password = "qwerty", Role = Role.Admin, Orders = new List<Order>() { order1, order2 } };
            User user2 = new User() { Name = "Lilia", Email = "lilia@gmail.com", Login = "lilia_ua", Password = "qwerty", Role = Role.User, Orders = new List<Order>() { order3 } };
            User user3 = new User() { Name = "Oleg", Email = "oleg@gmail.com", Login = "oleg_ua", Password = "qwerty", Role = Role.User };

            Users.Create(user1);
            Users.Create(user2);
            Users.Create(user3);

            this.Save();
        }
    }
}
