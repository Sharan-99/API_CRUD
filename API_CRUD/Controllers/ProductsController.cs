using API_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> repo;
        public ProductsController(IRepository<Product> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(statusCode:200, type: typeof(IReadOnlyCollection<Product>))]
        public IActionResult GetProducts()
        {
            var products = repo.Get();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(statusCode: 200, type: typeof(Product))]
        [ProducesResponseType(statusCode:404)]
        public IActionResult GetProducts(int id)
        {
            var products = repo.Get(p => p.Id == id);
            var product = products.FirstOrDefault();
            if (product == null)
                return NotFound();
            return Ok(product);

        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> AddProducts(Product prod)
        {
            repo.Add(prod);
            int rowsAffected = await repo.SaveAsync();
            if (rowsAffected == 1)
                return CreatedAtAction("GetProducts", new { id = prod.Id });
               
            return BadRequest("Add failed");
        }

        [HttpPut]
        [ProducesResponseType(statusCode: 200, type: typeof(Product))]
        [ProducesResponseType(statusCode:400)]
        public async Task<IActionResult> UpdateProducts(Product prod)
        {
            repo.Update(prod);
            int rowsAffected = await repo.SaveAsync();
            if (rowsAffected == 1)
                return Ok(prod);
            return BadRequest("Update failed");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode:404)]
        [ProducesResponseType(statusCode:204)]
        [ProducesResponseType(statusCode:400)]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = repo.Get(p => p.Id == id);
            var product = products.FirstOrDefault();
            if (product == null)
                return NotFound();

            repo.Delete(product);
            int rowsAffected = await repo.SaveAsync();
            if (rowsAffected == 1)
                return NoContent();
            return BadRequest("Delete failed");
        }
    }
}
