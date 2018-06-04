using Alps.Domain;
using Alps.Domain.ProductMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Controllers
{
  [Produces("application/json")]
  [Route("api/Stocks")]
  public class StocksController : Controller
  {
    private readonly AlpsContext _context;

    public StocksController(AlpsContext context)
    {
      _context = context;
    }

    // GET: api/Stocks
    [HttpGet]
    public IEnumerable<StockListDto> GetProductStock()
    {
      var query = from stock in _context.ProductStocks
                  from sku in _context.ProductSkus
                  from p in _context.Positions
                  where stock.ProductSkuID == sku.ID && p.ID == stock.PositionID
                  select new StockListDto
                  {
                    Name = sku.Name,
                    AuxiliaryQuantity = stock.AuxiliaryQuantity,
                    Quantity = stock.Quantity,
                    Warehouse = p.Name,
                    Owner = stock.Owner.Name,
                    SerialNumber=stock.SerialNumber
                  };
      return query;
    }

    // GET: api/Stocks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductStock([FromRoute] Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var productStock = await _context.ProductStocks.SingleOrDefaultAsync(m => m.ID == id);

      if (productStock == null)
      {
        return NotFound();
      }

      return Ok(productStock);
    }

    // PUT: api/Stocks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductStock([FromRoute] Guid id, [FromBody] ProductStock productStock)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != productStock.ID)
      {
        return BadRequest();
      }

      _context.Entry(productStock).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductStockExists(id))
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

    // POST: api/Stocks
    [HttpPost]
    public async Task<IActionResult> PostProductStock([FromBody] ProductStock productStock)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.ProductStocks.Add(productStock);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProductStock", new { id = productStock.ID }, productStock);
    }

    // DELETE: api/Stocks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductStock([FromRoute] Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var productStock = await _context.ProductStocks.SingleOrDefaultAsync(m => m.ID == id);
      if (productStock == null)
      {
        return NotFound();
      }

      _context.ProductStocks.Remove(productStock);
      await _context.SaveChangesAsync();

      return Ok(productStock);
    }

    private bool ProductStockExists(Guid id)
    {
      return _context.ProductStocks.Any(e => e.ID == id);
    }
  }
}
