using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alps.Domain;
using Alps.Domain.SaleMgr;
using Alps.Web.Service.Model;
namespace Alps.Web.Service.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommoditiesController : ControllerBase
    {
        private readonly AlpsContext _context;

        public CommoditiesController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Commodity
        [HttpGet]
        public IActionResult GetCommodities()
        {

            return this.AlpsActionOk(_context.Commodities.Select(p => new CommodityListDto
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                ListPrice = p.ListPrice,
                AuxiliaryQuantity = p.AuxiliayQuantity,
                ProductSkuID = p.ProductSkuID
            }));
        }

        // GET: api/Commodity/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommodity([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commodity = await _context.Commodities.FindAsync(id);

            if (commodity == null)
            {
                return this.AlpsActionError("无此商品");
                //return NotFound();
            }

            return this.AlpsActionOk(commodity);
        }

        // PUT: api/Commodity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommodity([FromRoute] Guid id, [FromBody] Commodity commodity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commodity.ID)
            {
                return BadRequest();
            }

            var existCommodity = _context.Commodities.Find(id);
            if (existCommodity == null)
                return this.AlpsActionError("此商品不存在");
            existCommodity.ProductSkuID = commodity.ProductSkuID;
            existCommodity.ListPrice = commodity.ListPrice;
            existCommodity.Name = commodity.Name;
            existCommodity.Description = commodity.Description;
            existCommodity.Quantity = commodity.Quantity;
            existCommodity.AuxiliayQuantity = commodity.AuxiliayQuantity;
 
            //_context.Entry(commodity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CommodityExists(id))
                {
                    //return NotFound();
                    return this.AlpsActionError("无此商品");
                }
                else
                {
                    return this.AlpsActionError(ex.Message);
                    //return this.AlpsActionError();
                    //throw;
                }
            }

            return this.AlpsActionOk();
        }

        // POST: api/Commodity
        [HttpPost]
        public async Task<IActionResult> PostCommodity([FromBody] Commodity commodity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Commodities.Add(commodity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommodity", new { id = commodity.ID }, commodity);
        }

        // DELETE: api/Commodity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommodity([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commodity = await _context.Commodities.FindAsync(id);
            if (commodity == null)
            {
                return NotFound();
            }

            _context.Commodities.Remove(commodity);
            await _context.SaveChangesAsync();

            return Ok(commodity);
        }

        private bool CommodityExists(Guid id)
        {
            return _context.Commodities.Any(e => e.ID == id);
        }
    }
}