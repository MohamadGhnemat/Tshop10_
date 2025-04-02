using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tshop.API.Data;
using Tshop.API.Models;

namespace Tshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult getAll()
        {
           var categories= _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute]int id)
        {
            var category = _context.Categories.Find(id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost("")]

        public IActionResult Create([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new { category.Id }, category);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete([FromRoute]int id) { 
        var category = _context.Categories.Find(id);
            if(category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();//204
        }
    }
}
