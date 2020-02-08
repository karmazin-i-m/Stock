using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.DB;
using Stock.DB.Models;
using Stock.DB.Repositories;

namespace Stock.WebAPI.Controllers.Products
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly UnitOfWork db;
        private readonly IRepository<Product> products;

        public ProductsController()
        {
            this.db = UnitOfWork.GetInstance();
            this.products = db.Products;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct([FromBody]Product product)
        {
            products.Create(product);
            db.Save();
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            Product product = products.Get(id);
            return Json(product);
        }

        [Authorize]
        [HttpGet()]
        public IEnumerable<Product> GetProductsList()
        {
            return products.GetList();
        }
    }
}   