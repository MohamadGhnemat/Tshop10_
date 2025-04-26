using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tshop.API.Data;
using Tshop.API.DTOs.Requests;
using Tshop.API.Models;

namespace Tshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        ApplicationDbContext _context;
        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult getAll()
        {
            var brands= _context.Brands.ToList();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id ) {
            var brand = _context.Brands.Find(id);
            return brand == null ? NotFound() :   Ok(brand);
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] Brand brand)
        {
           _context.Brands.Add(brand);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById),new {brand.Id},brand);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return NoContent();//204

            
        }
    }
}
