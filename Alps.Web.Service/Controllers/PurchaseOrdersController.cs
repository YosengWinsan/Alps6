using Alps.Domain;
using Alps.Domain.PurchaseMgr;
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
  [Route("api/PurchaseOrders")]
  public class PurchaseOrdersController : Controller
  {
    private AlpsContext _context = null;
    public PurchaseOrdersController(AlpsContext context)
    {
      this._context = context;
    }
    
    [HttpGet("{id}")]
    public ActionResult GetEditModel(Guid id)
    {
      if(id==Guid.Empty)
      {
        return NotFound();
      }
      PurchaseOrderEditModel model = new PurchaseOrderEditModel();
      PurchaseOrder order= _context.PurchaseOrders.Find(id);
      if(order!=null)
      {
        model.Order = new PurchaseOrderForEditDto() { ID = order.ID, Supplier = order.Supplier.Name, SupplierID = order.SupplierID };
        model.Order.Items = order.Items.Select(p => new PurchaseOrderItemForEditDto() {ID=p.ID, SKUID = p.ProductSkuInfo.SkuID, Price = p.Price, Amount = p.Amount, Quantity = p.Quantity });
      }
      model.SupplierOptions = _context.Suppliers.Select(p => new AlpsSelectorItemDto() { Value = p.ID, DisplayValue = p.Name });
      return Ok(model);
    }
    // GET: api/PurchaseOrders
    [HttpGet]
    public IQueryable<PurchaseOrderListDto> Get()
    {
      
      return _context.PurchaseOrders.Select(p => new PurchaseOrderListDto
      {
        ID = p.ID,
        Supplier = p.Supplier.Name,
        CreateTime = p.OrderTime,
        Confirmer = p.Creater,
        Creater = p.Creater,
        ConfirmTime = p.OrderTime,
        TotalQuantity = p.TotalQuantity
      }).AsQueryable();
      //var result = Json(context.PurchaseOrders.ToList());
      //return result;
    }

    // GET: api/PurchaseOrders/5

    public async Task<ActionResult> Get(Guid id)
    {
      PurchaseOrder purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

      if (purchaseOrder == null)
      {
        return NotFound();
      }
      PurchaseOrderForEditDto purchaseOrderEditModel = new PurchaseOrderForEditDto
      {
        ID = purchaseOrder.ID,
        Supplier = purchaseOrder.Supplier.Name,
        Items = purchaseOrder.Items.Select(p => new PurchaseOrderItemForEditDto {ID=p.ID, SKUID = p.ProductSkuInfo.SkuID, Price = p.Price, Amount = p.Amount, Quantity = p.Quantity })
      };
      return Ok(purchaseOrderEditModel);
    }

    // POST: api/PurchaseOrders
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]PurchaseOrderForEditDto dto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      PurchaseOrder order = PurchaseOrder.Create(dto.SupplierID, dto.Creater);
      
      _context.PurchaseOrders.Add(order);
      await _context.SaveChangesAsync();
      return Ok(order);      
    }

    // PUT: api/PurchaseOrders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody]PurchaseOrderForEditDto dto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != dto.ID)
      {
        return BadRequest();
      }
      PurchaseOrder order = _context.PurchaseOrders.Find(dto.ID);
      if (order == null)
        return BadRequest();
      _context.Entry(order).CurrentValues.SetValues(dto);
      var items = new List<PurchaseOrderItem>();
      foreach(var item in dto.Items)
      {
        items.Add(new PurchaseOrderItem
        {
          ID = item.ID,
          Price = item.Price,
          Quantity = item.Quantity,
          Amount = item.Price * item.Quantity          ,
          ProductSkuInfo = new Domain.ProductMgr.ProductSkuInfo { SkuID = item.SKUID }
        });
        
      }
      order.UpdateItem(items);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PurchaseOrderExists(id))
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

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
    private bool PurchaseOrderExists(Guid id)
    {
      return _context.Products.Any(e => e.ID == id);
    }
  }
}
