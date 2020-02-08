using Stock.DB.Models;
using Stock.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.DB.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
