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

            return this.AlpsActionOk(_context.ProductSkus.Where(p => p.Vendable).Select(p => new CommodityListDto
            {
                ID = p.ID,
                CommodityName = p.CommodityName,
                ListPrice = p.ListPrice,
                QuantityRate = p.QuantityRate,
                PreSellQuantity = p.PreSellQuantity,
                PreSellAuxiliaryQuantity = p.PreSellAuxiliaryQuantity,
                StockQuantity = p.StockQuantity,
                StockAuxiliaryQuantity = p.StockAuxiliaryQuantity,
                OrderedAuxiliaryQuantity = p.OrderedAuxiliaryQuantity,
                OrderedQuantity = p.OrderedQuantity,
                SellableQuantity = p.SellableQuantity,
                SellableAuxiliaryQuantity = p.SellableAuxiliaryQuantity
            }));
        }
        [HttpGet("getCommoditiesByCatagoryID/{id}")]
        public IActionResult getCommoditiesByCatagoryID(Guid id)
        {
            return this.AlpsActionOk(_context.ProductSkus.Where(p => p.Vendable && p.Product.CatagoryID==id).Select(p => new CommodityListDto
            {
                ID = p.ID,
                CommodityName = p.CommodityName,
                ListPrice = p.ListPrice,
                QuantityRate = p.QuantityRate,
                PreSellQuantity = p.PreSellQuantity,
                PreSellAuxiliaryQuantity = p.PreSellAuxiliaryQuantity,
                StockQuantity = p.StockQuantity,
                StockAuxiliaryQuantity = p.StockAuxiliaryQuantity,
                OrderedAuxiliaryQuantity = p.OrderedAuxiliaryQuantity,
                OrderedQuantity = p.OrderedQuantity,
                SellableQuantity = p.SellableQuantity,
                SellableAuxiliaryQuantity = p.SellableAuxiliaryQuantity
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
            CommodityEditDto dto = new CommodityEditDto
            {
                ID = commodity.ID,
                Name = commodity.Name,
                Description = commodity.Description,
                ProductSkuID = commodity.ProductSkuID,
                AuxiliaryQuantity = commodity.PreSellAuxiliaryQuantity,
                Quantity = commodity.PreSellQuantity,
                ListPrice = commodity.ListPrice
            };
            return this.AlpsActionOk(dto);
        }

        // PUT: api/Commodity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommodity([FromRoute] Guid id, [FromBody] CommodityEditDto commodity)
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
            existCommodity.PreSellQuantity = commodity.Quantity;
            existCommodity.PreSellAuxiliaryQuantity = commodity.AuxiliaryQuantity;

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
        public async Task<IActionResult> PostCommodity([FromBody] CommodityEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Commodity c = Commodity.Create(dto.ProductSkuID, dto.Name, dto.Description, dto.ListPrice, dto.Quantity, dto.AuxiliaryQuantity);
            _context.Commodities.Add(c);
            await _context.SaveChangesAsync();

            return this.AlpsActionOk();
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