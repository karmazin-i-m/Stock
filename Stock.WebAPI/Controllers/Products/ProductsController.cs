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
using Stock.DB.Services;

namespace Stock.WebAPI.Controllers.Products
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork db;
        private readonly IRepository<Product> products;

        public ProductsController(IUnitOfWork db)
        {
            this.db = db;
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
        public IActionResult GetProduct(Int32 id)
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

        [Authorize]
        [HttpPut]
        public IActionResult UpdateProduct([FromBody]Product product)
        {
            products.Update(product);
            db.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult UpdateProduct(Int32 id)
        {
            products.Delete(id);
            db.Save();
            return Ok();
        }
    }
}   