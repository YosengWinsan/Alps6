using Alps.Domain;
using Alps.Domain.Service;
using Alps.Domain.StockMgr;
using Alps.Web.Service.Extensions;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alps.Web.Service.Controllers
{
     [Authorize]
    [Produces("application/json")]
    [Route("api/StockOutVouchers")]
    public class StockOutVouchersController : Controller
    {
        private readonly AlpsContext _context;
        private readonly StockService _stockService;
        public StockOutVouchersController(AlpsContext context, StockService stockService)
        {
            this._context = context;
            this._stockService = stockService;
        }
        // GET: api/StockOutVouchers
        [HttpGet]
        public IEnumerable<StockOutVoucherListDto> GetStockOutVoucherListDto()
        {
            var result = _context.StockOutVouchers.Select(p =>
             new StockOutVoucherListDto
             {
                 ID = p.ID,
                 CreateTime = p.CreateTime,
                 Customer = p.Customer.Name,
                 Department = p.Department.Name,
                 TotalAuxiliaryQuantity = p.TotalAuxiliaryQuantity,
                 TotalQuantity = p.TotalQuantity,
                 Confirmer = p.Confirmer,
                 ConfirmTime = p.CreateTime,
                 TotalAmount = p.TotalAmount,
                 Status = p.Status.ToString("G")
             }).ToList();

            foreach (var d in result)
            {
                d.Status = EnumHelper.GetDisplayValue(typeof(StockOutVoucherStatus), d.Status);
            }
            return result;
        }

        // GET: api/StockOutVouchers/5
        private StockOutVoucherEditDto GetStockOutVoucherEditDto(Guid id)
        {
            return (from k in _context.StockOutVouchers
                    where k.ID == id
                    select new StockOutVoucherEditDto
                    {
                        DepartmentID = k.DepartmentID,
                        ID = k.ID,
                        CustomerID = k.CustomerID,
                        Status = (int)k.Status,
                        Items = from i in _context.StockOutVoucherItems
                        from sku in _context.ProductSkus
                        from p in _context.Positions
                                where i.StockOutVoucherID == k.ID && sku.ID==i.ProductSkuID && i.PositionID==p.ID
                                select new StockOutVoucherItemDto
                                {
                                    ID = i.ID,
                                    ProductSkuID = i.ProductSkuID,
                                    SerialNumber = i.SerialNumber,
                                    PositionID = i.PositionID,
                                    AuxiliaryQuantity = i.AuxiliaryQuantity,
                                    Quantity = i.Quantity,
                                    Price = i.Price,
                                    Position=p.Name,
                                    ProductSku=sku.FullName
                                }
                    }).FirstOrDefault();
        }
        // GET: api/StockInVouchers/5
        [HttpGet("detail/{id}")]
        public IActionResult Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rst = _context.StockOutVouchers.Select(k => new StockOutVoucherDetailDto
            {
              
                Department = k.Department.Name,
                ID = k.ID,
                Customer = k.Customer.Name,
                StatusValue = (int)k.Status,
                Status = EnumHelper.GetDisplayValue(k.Status),
                TotalAuxiliaryQuantity = k.TotalAuxiliaryQuantity,
                TotalQuantity = k.TotalQuantity,
                TotalAmount = k.TotalAmount,
                Items = k.Items.Join(_context.ProductSkus,p=>p.ProductSkuID,o=>o.ID,(p,o)=>new{item= p,ProductSkuName=o.FullName})
                .Join(_context.Positions,l=>l.item.PositionID,d=>d.ID,(l,d)=>new {Item=l.item,Position=d.Name,ProductSku=l.ProductSkuName})
                .Select(i => new StockOutVoucherDetailItemDto
                {
                    ID = i.Item.ID,
                    ProductSku = i.ProductSku,
                    SerialNumber = i.Item.SerialNumber,
                    Position = i.Position,
                    AuxiliaryQuantity = i.Item.AuxiliaryQuantity,
                    Quantity = i.Item.Quantity,
                    Price = i.Item.Price
                })
            }).FirstOrDefault(p => p.ID == id);
            return this.AlpsActionOk(rst);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(GetStockOutVoucherEditDto(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]StockOutVoucherEditDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var voucher = StockOutVoucher.Create(dto.DepartmentID, dto.CustomerID, this.User.Identity.Name);

            voucher.UpdateItems(dto.Items);
            _context.StockOutVouchers.Add(voucher);
            _context.SaveChanges();
            return Ok(GetStockOutVoucherEditDto(voucher.ID));
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]StockOutVoucherEditDto dto)
        {
            //_context.Database.Log = (s) => Console.WriteLine(s);
            if (!ModelState.IsValid || id != dto.ID)
            {
                return BadRequest();
            }

            var voucher = _context.StockOutVouchers.Include(p => p.Items).SingleOrDefault(p => p.ID == id);
            voucher.DepartmentID = dto.DepartmentID;
            voucher.CustomerID = dto.CustomerID;
            voucher.UpdateItems(dto.Items);
            _context.SaveChanges();
            return Ok(GetStockOutVoucherEditDto(id));
        }
        private StockOutVoucher GetStockOutVoucher(Guid id)
        {
            var voucher = _context.StockOutVouchers.Include(p => p.Items).FirstOrDefault(p => p.ID == id);
            if (voucher == null)
                throw new DomainException("无此单据");
            return voucher;
        }
        [HttpPost("submit/{id}")]
        public IActionResult Submit(Guid id)
        {
            try
            {
                var voucher = GetStockOutVoucher(id);
                _stockService.StockOut(voucher);
                _context.SaveChanges();
            }
            catch (DomainException de)
            {
                return this.AlpsActionError(de.Message);
            }
            return this.AlpsActionOk();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
