using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddProductAsync([FromBody]Product product)
        {
            await products.CreateAsync(product);
            await db.SaveAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(Int32 id)
        {
            Product product = await products.GetAsync(id);
            return Json(product);
        }

        [Authorize]
        [HttpGet()]
        public async Task<IEnumerable<Product>> GetProductsListAsync()
        {
            return await products.GetListAsync();
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
        public IActionResult DeleteProduct(Int32 id)
        {
            products.Delete(id);
            db.Save();
            return Ok();
        }
    }
}