using Alps.Domain;
//using Microsoft.EntityFrameworkCore;
using Alps.Domain.Common;
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
    [Route("api/Units")]
    public class UnitsController : Controller
    {
        private readonly AlpsContext _context;

        public UnitsController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Unitsqqwinsan

        [HttpGet]
        public IEnumerable<Unit> GetUnit()
        {
            return _context.Units;
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = await _context.Units.SingleOrDefaultAsync(m => m.ID == id);

            if (unit == null)
            {
                return NotFound();
            }

            return Ok(unit);
        }

        // PUT: api/Units/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit([FromRoute] Guid id, [FromBody] Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unit.ID)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Units
        [HttpPost]
        public async Task<IActionResult> PostUnit([FromBody] Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.ID }, unit);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unit = await _context.Units.SingleOrDefaultAsync(m => m.ID == id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return Ok(unit);
        }

        private bool UnitExists(Guid id)
        {
            return _context.Units.Any(e => e.ID == id);
        }
    }
}
