using Alps.Domain;
using Alps.Domain.ProductMgr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Alps.Web.Service.Controllers
{
  [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly AlpsContext _context;

        public ProductsController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Model.ProductListDto> GetProduct()
        {
            return _context.Products.Select(p=> new Model.ProductListDto {
              Catagory=p.Catagory.Name,
              Name=p.Name,
              FullName=p.FullName,
              FullDescription=p.FullDescription,
              ShortDescription=p.ShortDescription,
              EnableAuxiliaryQuantity=p.EnableAuxiliaryUnit,
              Deprecated=p.Deleted
        
            } );
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }
      var result = new Model.ProductEditDto()
      {
        ID = product.ID,
        Name = product.Name,
        FullName = product.FullName,
        ShortDescription = product.ShortDescription
      ,
        FullDescription = product.FullDescription,
        Deprecated = product.Deleted,
        EnableAuxiliaryQuantity=product.EnableAuxiliaryUnit
      };
            return Ok(result);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] Guid id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}