using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueAPI.Data;
using EstoqueAPI.Models;
using EstoqueAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly Service service;
        public CategoryController([FromServices] IService service)
        {
            this.service = (Service)service;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await service.FindAllAsync(new Category()); 
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await service.FindByIdAsync(new Category(), id);
            return category;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post
            (
                [FromBody] Category model
            )
        {
            try
            {
                service.Add(model);

                if (await service.SaveChangesAsync())
                {
                    return Created($"/api/category/{model.Id}", model);
                }

                return BadRequest();
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou " + err.Message + " " + err.InnerException);
            }
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Put(int categoryId, [FromBody] Category model)
        {
            try
            {

                var category = await service.FindByIdAsync(new Category(), categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                model.Id = categoryId;

                service.Update(model);

                if (await service.SaveChangesAsync())
                {
                    category = await service.FindByIdAsync(new Category(), categoryId);
                    return Created($"/api/aluno/{model.Id}", category);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            try
            {
                var category = await service.FindByIdAsync(new Category(), categoryId);

                if (category == null) return NotFound();

                service.Delete(category);

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
