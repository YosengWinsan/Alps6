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
using Alps.Web.Service.Extensions;

namespace Alps.Web.Service.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrdersController : ControllerBase
    {
        private readonly AlpsContext _context;

        public SaleOrdersController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/SaleOrders
        [HttpGet]
        public IActionResult GetSaleOrders()
        {
            return this.AlpsActionOk(_context.SaleOrders.Select(p => new SaleOrderListDto
            {
                ID = p.ID,
                OrderTime = p.OrderTime,
                Customer = p.Customer.Name,
                TotalAmount = p.TotalAmount,
                TotalAuxiliaryQuantity = p.TotalAuxiliaryQuantity,
                TotalQuantity = p.TotalQuantity,
                Status = EnumHelper.GetDisplayValue(p.Status)
            }));
        }

        // GET: api/SaleOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleOrder = await _context.SaleOrders.Select(p => new SaleOrderEditDto
            {
                ID = p.ID,
                CustomerID = p.CustomerID,
                Items = p.Items.Select(l => new SaleOrderItemEditDto
                {
                    ID = l.ID,
                    CommodityID = l.CommodityID,
                    Commodity = l.Commodity.Name,
                    Quantity = l.Quantity,
                    AuxiliaryQuantity = l.AuxiliaryQuantity,
                    Price = l.Price
                })
            }).FirstOrDefaultAsync(p => p.ID == id);

            if (saleOrder == null)
            {
                return NotFound();
            }

            return Ok(saleOrder);
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleOrder = await _context.SaleOrders.Select(p => new SaleOrderDetailDto
            {
                ID = p.ID,
                Customer = p.Customer.Name,
                Status = EnumHelper.GetDisplayValue(p.Status),
                StatusValue = (int)p.Status,
                TotalQuantity = p.TotalQuantity,
                TotalAuxiliaryQuantity = p.TotalAuxiliaryQuantity,
                TotalAmount = p.TotalAmount,
                Items = p.Items.Select(l => new SaleOrderItemEditDto
                {
                    ID = l.ID,
                    CommodityID = l.CommodityID,
                    Commodity = l.Commodity.Name,
                    Quantity = l.Quantity,
                    AuxiliaryQuantity = l.AuxiliaryQuantity,
                    Price = l.Price
                })
            }).FirstOrDefaultAsync(p => p.ID == id);

            if (saleOrder == null)
            {
                return NotFound();
            }

            return this.AlpsActionOk(saleOrder);
        }
        // PUT: api/SaleOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleOrder([FromRoute] Guid id, [FromBody] SaleOrder saleOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saleOrder.ID)
            {
                return BadRequest();
            }

            _context.Entry(saleOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleOrderExists(id))
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

        // POST: api/SaleOrders
        [HttpPost]
        public async Task<IActionResult> PostSaleOrder([FromBody] SaleOrder saleOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SaleOrders.Add(saleOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleOrder", new { id = saleOrder.ID }, saleOrder);
        }

        // DELETE: api/SaleOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleOrder = await _context.SaleOrders.FindAsync(id);
            if (saleOrder == null)
            {
                return NotFound();
            }

            _context.SaleOrders.Remove(saleOrder);
            await _context.SaveChangesAsync();

            return Ok(saleOrder);
        }
        [HttpPost("Submit/{id}")]
        public async Task<IActionResult> Submit([FromRoute] Guid id)
        {
            var saleOrderVoucher =await _context.SaleOrders.FindAsync(id);
            if (saleOrderVoucher.Status != SaleOrderStatus.UnConfirm)
            {
                return this.AlpsActionWarning("单据已提交");
            }
            saleOrderVoucher.Status = SaleOrderStatus.Confirm;

            await _context.SaveChangesAsync();
            return this.AlpsActionOk();


        }
        private bool SaleOrderExists(Guid id)
        {
            return _context.SaleOrders.Any(e => e.ID == id);
        }
    }
}