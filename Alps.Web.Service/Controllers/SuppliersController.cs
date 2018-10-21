using Alps.Domain;
using Alps.Domain.Common;
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
    [Route("api/Suppliers")]
    public class SuppliersController : Controller
    {
        private readonly AlpsContext _context;

        public SuppliersController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public IEnumerable<SupplierListDto> GetSupplier()
        {
            return from s in _context.Suppliers
                   from c in _context.Counties
                   where s.Address.CountyID == c.ID
                   select new SupplierListDto { ID = s.ID, Name = s.Name, Contact = "", Address = c.FullName + " " + s.Address.Street };
        }
        [HttpGet("getSupplierClasses")]
        public IEnumerable<SupplierClass> GetSupplierClasses()
        {
            return _context.SupplierClasses;
        }
        [HttpGet("getSupplierClass/{id}")]
        public async Task<IActionResult> GetSupplierClass([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplierClass = await _context.SupplierClasses.SingleOrDefaultAsync(m => m.ID == id);

            if (supplierClass == null)
            {
                return NotFound();
            }

            return this.AlpsActionOk(supplierClass);
        }
        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplier = await _context.Suppliers.SingleOrDefaultAsync(m => m.ID == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }
        [HttpPost("saveSupplierClass")]
        public async Task<IActionResult> SaveSupplierClass([FromBody] SupplierClass supplierClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (supplierClass.ID == Guid.Empty)
            {
                var newSupplierClass = SupplierClass.Create(supplierClass.Name);
                _context.SupplierClasses.Add(newSupplierClass);
            }
            else
            {
                var existSC = _context.SupplierClasses.Find(supplierClass.ID);
                existSC.Name = supplierClass.Name;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (supplierClass.ID != Guid.Empty && !_context.SupplierClasses.Any(e => e.ID == supplierClass.ID))
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
        // PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier([FromRoute] Guid id, [FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier.ID)
            {
                return BadRequest();
            }
            var existSupplier = _context.Suppliers.Find(id);
            existSupplier.Name = supplier.Name;
            existSupplier.Address = supplier.Address;
            //_context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/Suppliers
        [HttpPost]
        public async Task<IActionResult> PostSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", new { id = supplier.ID }, supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplier = await _context.Suppliers.SingleOrDefaultAsync(m => m.ID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(supplier);
        }
        private bool SupplierExists(Guid id)
        {
            return _context.Suppliers.Any(e => e.ID == id);
        }
    }
}
