using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alps.Domain;
using Alps.Web.Service.Model;
using Alps.Web.Service.Extensions;
using Alps.Domain.StockMgr;
using Alps.Domain.Service;

namespace Alps.Web.Service.Controllers
{
  [Produces("application/json")]
  [Route("api/StockInVouchers")]
  public class StockInVouchersController : Controller
  {
    private readonly AlpsContext _context;
    private readonly StockService _stockService;
    public StockInVouchersController(AlpsContext context,StockService stockService)
    {
      this._context = context;
      this._stockService = stockService;
      //this._context.Database.Log = (s) => Console.WriteLine(s);
    }
    // GET: api/StockInVouchers
    [HttpGet]
    public IEnumerable<StockInVoucherListDto> GetStockInVoucherListDto()
    {
      var result = _context.StockInVouchers.Select(p =>
       new StockInVoucherListDto
       {
         ID = p.ID,
         CreateTime = p.CreateTime,
         Source = p.Source.Name,
         Department = p.Department.Name,
         TotalAuxiliaryQuantity = p.TotalAuxiliaryQuantity,
         TotalQuantity = p.TotalQuantity,
         Confirmer = p.Confirmer,
         ConfirmTime = p.CreateTime,
         TotalAmount=p.TotalAmount,
         Status = p.Status.ToString("G")
       }).ToList();

      foreach (var d in result)
      {
        d.Status = EnumHelper.GetDisplayValue(typeof(StockInVoucherStatus), d.Status);
      }
      return result;
    }
    private StockInVoucherEditDto GetStockInVoucherEditDto(Guid id)
    {
      return (from k in _context.StockInVouchers
              where k.ID == id
              select new StockInVoucherEditDto
              {
                DepartmentID = k.DepartmentID,
                ID = k.ID,
                SourceID = k.SourceID,
                Status = (int)k.Status,
                Items = from i in _context.StockInVoucherItems
                from sku in _context.ProductSkus
                from p in _context.Positions
                        where i.StockInVoucherID == k.ID && i.ProductSkuID==sku.ID&& i.PositionID==p.ID
                        select new StockInVoucherItemDto
                        {
                          ID = i.ID,
                          ProductSkuID = i.ProductSkuID,
                          ProductSku=sku.Name,
                          SerialNumber = i.SerialNumber,
                          PositionID = i.PositionID,
                          Position=p.Name,
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
      return this.AlpsActionOk(GetStockInVoucherEditDto(id));
    }
[HttpGet("detail/{id}")]
public IActionResult Detail(Guid id)
{
  if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var detail=(from k in _context.StockInVouchers
              where k.ID == id
              select new StockInVoucherDetailDto
              {
                Department = k.Department.Name,
                ID = k.ID,
                Source = k.Source.Name,
                StatusValue = (int)k.Status,
                Status=EnumHelper.GetDisplayValue(typeof(StockInVoucherStatus), k.Status.ToString("G")),
                TotalAmount=k.TotalAmount ,
                TotalAuxiliaryQuantity=k.TotalAuxiliaryQuantity,
                TotalQuantity=k.TotalQuantity,
                Items = from i in _context.StockInVoucherItems
                from sku in _context.ProductSkus
                from p in _context.Positions
                        where i.StockInVoucherID == k.ID && i.ProductSkuID==sku.ID&& i.PositionID==p.ID
                        select new StockInVoucherItemDto
                        {
                          ID = i.ID,
                          ProductSkuID = i.ProductSkuID,
                          ProductSku=sku.Name,
                          SerialNumber = i.SerialNumber,
                          PositionID = i.PositionID,
                          Position=p.Name,
                          AuxiliaryQuantity = i.AuxiliaryQuantity,
                          Quantity = i.Quantity,
                          Price = i.Price
                        }
              }).FirstOrDefault();
      return this.AlpsActionOk(detail);
}
    // POST: api/StockInVouchers
    [HttpPost]
    public IActionResult Post([FromBody]StockInVoucherEditDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();
      var voucher = StockInVoucher.Create(dto.SourceID, dto.DepartmentID, this.User.Identity.Name);
     
      voucher.UpdateItems(dto.Items);
      _context.StockInVouchers.Add(voucher);
      _context.SaveChanges();
      return this.AlpsActionOk();
    }

    // PUT: api/StockInVouchers/5
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody]StockInVoucherEditDto dto)
    {
      //_context.Database.Log = (s) => Console.WriteLine(s);
      if (!ModelState.IsValid || id != dto.ID)
      {
        return BadRequest();
      }
      
      var voucher = _context.StockInVouchers.Include(p=>p.Items).SingleOrDefault(p=>p.ID==id);
      voucher.DepartmentID = dto.DepartmentID;
      voucher.SourceID = dto.SourceID;
      voucher.UpdateItems(dto.Items);
      _context.SaveChanges();
      return this.AlpsActionOk();
      //return Ok(GetStockInVoucherEditDto(id));
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      if (!ModelState.IsValid)

        return BadRequest();
      var voucher = _context.StockInVouchers.Find(id);
      if (voucher == null)
        return BadRequest();
      _context.StockInVouchers.Remove(voucher);
      _context.SaveChanges();
      return this.AlpsActionOk();// Ok(new { ActionDone = true });
    }
    [HttpPost("Submit/{id}")]
    public IActionResult Submit(Guid id)
    {
      var stockInVoucher = GetStockInVoucher(id);
      if(stockInVoucher.Status!=StockInVoucherStatus.Unsubmit)
      {
        return this.AlpsActionWarning("单据已提交");
      }
      _stockService.StockIn(stockInVoucher);
      _context.SaveChanges();
      return this.AlpsActionOk(_context.ProductStocks.Count());
    }
    private StockInVoucher GetStockInVoucher(Guid id)
    {
      return _context.StockInVouchers.Include(p => p.Items).FirstOrDefault(p => p.ID == id);
    }
  }
}
