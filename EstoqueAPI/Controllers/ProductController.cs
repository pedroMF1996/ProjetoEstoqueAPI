using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EstoqueAPI.Models;
using EstoqueAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Service service;
        public ProductController([FromServices] IService repository)
        {
            service = (Service)repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await service.FindAllAsync(new Product());
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await service.FindByIdAsync(new Product(), id);
            return product;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory( int id)
        {
            var product = await service.FindProductsByCategoryId(id);
            return product;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post ([FromBody] Product model)
        {
            try
            {
                service.Add(model);

                if (await service.SaveChangesAsync())
                {
                    return Created($"/api/products/{model.Id}", model);
                }

                return BadRequest();
            }
            catch (System.Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou \n" + err.Message + " \n" + err.InnerException);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Put
            (
                [FromBody] Product model,
                int id
            )
        {
            try
            {
                var product = await service.FindByIdAsync(new Product(), id);

                if (product == null)
                {
                    return NotFound();
                }

                model.Id = id;

                service.Update(model);

                if (await service.SaveChangesAsync())
                {
                    product = await service.FindByIdAsync(new Product(), id); 
                    return Created($"/api/product/{model.Id}", product);
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var product = await service.FindByIdAsync(new Product(), productId);

                if (product == null) return NotFound();

                service.Delete(product);

                if (await service.SaveChangesAsync())
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }
    }
}
