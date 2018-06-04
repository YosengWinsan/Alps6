using Alps.Domain;
using Alps.Domain.Service;
using Alps.Domain.StockMgr;
using Alps.Web.Service.Extensions;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alps.Web.Service.Controllers
{
  [Produces("application/json")]
    [Route("api/StockOutVouchers")]
    public class StockOutVouchersController : Controller
    {
    private readonly AlpsContext _context;
    private readonly StockService _stockService;
    public StockOutVouchersController(AlpsContext context,StockService stockService)
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
         Destination = p.Destination.Name,
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
                DestinationID = k.DestinationID,
                Status = (int)k.Status,
                Items = from i in _context.StockOutVoucherItems
                        where i.StockOutVoucherID == k.ID
                        select new StockOutVoucherItemDto
                        {
                          ID = i.ID,
                          ProductSkuID = i.ProductSkuID,
                          SerialNumber = i.SerialNumber,
                          PositionID = i.PositionID,
                          AuxiliaryQuantity = i.AuxiliaryQuantity,
                          Quantity = i.Quantity,
                          Price = i.Price
                        }
              }).FirstOrDefault();
    }
    // GET: api/StockInVouchers/5
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
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
      var voucher = StockOutVoucher.Create(dto.DepartmentID, dto.DestinationID, this.User.Identity.Name);

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
      voucher.DestinationID = dto.DestinationID;
      voucher.UpdateItems(dto.Items);
      _context.SaveChanges();
      return Ok(GetStockOutVoucherEditDto(id));
    }
    private StockOutVoucher GetStockOutVoucher(Guid id)
    {
      var voucher= _context.StockOutVouchers.Include(p => p.Items).FirstOrDefault(p=>p.ID==id);
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
      catch(DomainException de)
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
