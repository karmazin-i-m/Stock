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
        /// <summary>
        /// Добавляет продукт в Базу
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody]Product product)
        {
            await products.CreateAsync(product);
            await db.SaveAsync();
            return Ok();
        }
        /// <summary>
        /// Возвращает продукт по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(Int32 id)
        {
            Product product = await products.GetAsync(id);
            return Json(product);
        }
        /// <summary>
        /// ВОзвращает коллекцию всех продуктов
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet()]
        public async Task<IEnumerable<Product>> GetProductsListAsync()
        {
            return await products.GetListAsync();
        }
        /// <summary>
        /// Обновляет данные о продукте
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult UpdateProduct([FromBody]Product product)
        {
            products.Update(product);
            db.Save();
            return Ok();
        }
        /// <summary>
        /// Удаляет продукт из базы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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