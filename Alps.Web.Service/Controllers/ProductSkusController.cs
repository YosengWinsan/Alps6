using Alps.Domain;
using Alps.Domain.ProductMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Alps.Web.Service.Controllers
{
     [Authorize]
    [Produces("application/json")]
    [Route("api/ProductSkus")]
    public class ProductSkusController : Controller
    {
        private readonly AlpsContext _context;

        public ProductSkusController(AlpsContext context)
        {
            _context = context;
        }

        [HttpGet("GetListByProduct/{id}")]
        public ActionResult GetProductSkuByProduct(Guid id)
        {
            return Ok(_context.ProductSkus.Where(p => p.ProductID == id));
        }

        // GET: api/ProductSkus
        [HttpGet]
        public IEnumerable<ProductSku> GetProductSku()
        {
            return _context.ProductSkus;
        }

        // GET: api/ProductSkus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductSku([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productSku = await _context.ProductSkus.Select(p => new ProductskuEditDto
            {
                Name = p.Name,
                ID = p.ID,
                ProductID = p.ProductID,
                Product = p.Product.Name,
                Description = p.Description,
                Code = p.Code,
                ListPrice = p.ListPrice,
                Vendable = p.Vendable,
                CommodityName = p.CommodityName,
                PreSellAuxiliaryQuantity = p.PreSellAuxiliaryQuantity,
                PreSellQuantity = p.PreSellQuantity
            }).SingleOrDefaultAsync(m => m.ID == id);

            if (productSku == null)
            {
                return NotFound();
            }

            return this.AlpsActionOk(productSku);
        }

        // PUT: api/ProductSkus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSku([FromRoute] Guid id, [FromBody] ProductskuEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.ID)
            {
                return BadRequest();
            }
            var productsku = _context.ProductSkus.Find(id);
            if (productsku == null)
                return BadRequest();
            productsku.Name = dto.Name;
            productsku.Description = dto.Description;
            productsku.Code = dto.Code;
            productsku.Vendable = dto.Vendable;
            productsku.ListPrice = dto.ListPrice;
            productsku.CommodityName = dto.CommodityName;
            productsku.PreSellAuxiliaryQuantity = dto.PreSellAuxiliaryQuantity;
            productsku.PreSellQuantity = dto.PreSellQuantity;
            productsku.ProductID = dto.ProductID;
            productsku.FullName = dto.Name;
            //var p = _context.Products.Find(dto.ProductID);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSkuExists(id))
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

        // POST: api/ProductSkus
        [HttpPost]
        public async Task<IActionResult> PostProductSku([FromBody]ProductskuEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Product p = _context.Products.Find(dto.ProductID);
            var productSku = ProductSku.Create(dto.ProductID, dto.Name, dto.Description, dto.Code, dto.Vendable,
            dto.CommodityName, dto.ListPrice, dto.QuantityRate, dto.PreSellQuantity, dto.PreSellAuxiliaryQuantity);

            _context.ProductSkus.Add(productSku);
            await _context.SaveChangesAsync();

            return this.AlpsActionOk();
        }

        // DELETE: api/ProductSkus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSku([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productSku = await _context.ProductSkus.SingleOrDefaultAsync(m => m.ID == id);
            productSku.MarkAsDeleted();
            if (productSku == null)
            {
                return NotFound();
            }

            //_context.ProductSkus.Remove(productSku);
            await _context.SaveChangesAsync();

            return Ok(productSku);
        }

        private bool ProductSkuExists(Guid id)
        {
            return _context.ProductSkus.Any(e => e.ID == id);
        }
    }
}
