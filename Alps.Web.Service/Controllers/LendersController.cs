using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alps.Domain;
using Alps.Domain.LoanMgr;

namespace Alps.Web.Service.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LendersController : ControllerBase
    {
        private readonly AlpsContext _context;

        public LendersController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Lenders
        [HttpGet]
        public IEnumerable<Lender> GetLenders()
        {
            return _context.Lenders;
        }

        // GET: api/Lenders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLender([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lender = await _context.Lenders.FindAsync(id);

            if (lender == null)
            {
                return NotFound();
            }

            return Ok(lender);
        }

        // PUT: api/Lenders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLender([FromRoute] Guid id, [FromBody] Lender lender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lender.ID)
            {
                return BadRequest();
            }

            _context.Entry(lender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LenderExists(id))
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

        // POST: api/Lenders
        [HttpPost]
        public async Task<IActionResult> PostLender([FromBody] Lender lender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Lenders.Add(lender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLender", new { id = lender.ID }, lender);
        }

        // DELETE: api/Lenders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLender([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lender = await _context.Lenders.FindAsync(id);
            if (lender == null)
            {
                return NotFound();
            }

            _context.Lenders.Remove(lender);
            await _context.SaveChangesAsync();

            return Ok(lender);
        }

        private bool LenderExists(Guid id)
        {
            return _context.Lenders.Any(e => e.ID == id);
        }
    }
}