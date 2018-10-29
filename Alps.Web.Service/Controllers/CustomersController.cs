using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alps.Domain;
using Alps.Domain.Common;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;

namespace Alps.Web.Service.Controllers
{
     [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AlpsContext _context;

        public CustomersController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customerList = from c in _context.Customers
                               from a in _context.Counties
                               where a.ID == c.Address.CountyID
                               select new CustomerListDto { Name = c.Name, ID = c.ID, Contact = c.Contact, Address = a.FullName + " " + c.Address.Street };
            return this.AlpsActionOk(customerList);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] Guid id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.ID)
            {
                return BadRequest();
            }
            var existCustomer = _context.Customers.Find(id);
            existCustomer.Name = customer.Name;
            existCustomer.Contact = customer.Contact;
            existCustomer.Address = customer.Address;
            //_context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.ID }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}