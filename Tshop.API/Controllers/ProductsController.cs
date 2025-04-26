using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tshop.API.Data;
using Tshop.API.DTOs.Requests;
using Tshop.API.DTOs.Responses;
using Tshop.API.Models;
using Tshop.API.Services;

namespace Tshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ApplicationDbContext dbcontext) : ControllerBase
    {
        private readonly ApplicationDbContext _context = dbcontext;

        
        [HttpGet("")]
        public IActionResult GetAll()
        {
           var products = _context.Products.ToList();
            if(products is null)
            {
                return NotFound();
            }
            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id) {
            var products = _context.Products.Find(id);
            if (products is null) { 
            return NotFound();
            }
            return Ok(products.Adapt<ProductResponse>());

        }
        [HttpPost("")]
        public IActionResult Create([FromForm] ProductRequest productRequest)
        {
            var file = productRequest.mainImg;
            var product = productRequest.Adapt<Product>();
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

               var filePath= Path.Combine(Directory.GetCurrentDirectory(),"images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.mainImg = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetById),new {id=product.Id},product);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product= _context.Products.Find(id);
            if (product is  null) return NotFound();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", product.mainImg);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
           
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();//204
        }

    }
}
