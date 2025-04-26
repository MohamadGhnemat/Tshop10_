using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tshop.API.Data;
using Tshop.API.DTOs.Requests;
using Tshop.API.DTOs.Responses;
using Tshop.API.Models;
using Tshop.API.Services;

namespace Tshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
       
        private readonly ICategoryService categoryService = categoryService;

      

        [HttpGet("")]
        public IActionResult getAll()
        {
           var categories= categoryService.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute]int id)
        {
            var category = categoryService.Get(e=>e.Id == id);
            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }



        [HttpPost("")]

        public IActionResult Create([FromBody] CategoryRequest categoryRequest)
        {
          var categoryInDb =  categoryService.Add(categoryRequest.Adapt<Category>());
            return CreatedAtAction(nameof(getById), new { categoryInDb.Id }, categoryInDb);
        }
     
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id ,[FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = categoryService.Edit(id, categoryRequest.Adapt<Category>());
            if (!categoryInDb) return NotFound();
            return NoContent();
        }
     
        [HttpDelete("{id}")]

        public IActionResult Delete([FromRoute]int id) {
            var categoryInDb = categoryService.Remove(id);
            if(!categoryInDb) return NotFound();
            return NoContent();//204
        }
    }
}
