using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alps.Domain;
using Alps.Domain.LoanMgr;
using Microsoft.AspNetCore.Authorization;
using Alps.Web.Service.Model;

namespace Alps.Web.Service.Controllers
{
    [Authorize]
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

            var lender = await _context.Lenders.Select(p => new LenderEditDto
            {
                ID = p.ID,
                IDNumber = p.IDNumber,
                MobilePhoneNumber = p.MobilePhoneNumber,
                Memo = p.Memo,
                Name = p.Name,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate,
                Invalid = p.Invalid,
                InvalidDate = p.InvalidDate.Value
            }).FirstOrDefaultAsync(p => p.ID == id);
            if (lender == null)
            {
                return NotFound();
            }

            return this.AlpsActionOk(lender);
        }

        // PUT: api/Lenders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLender([FromRoute] Guid id, [FromBody] LenderEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.ID)
            {
                return BadRequest();
            }
            var lender = _context.Lenders.Find(id);
            lender.Name = dto.Name;
            lender.IDNumber = dto.IDNumber;
            lender.MobilePhoneNumber = dto.MobilePhoneNumber;
            lender.Memo = dto.Memo;

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

            return this.AlpsActionOk();
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
        [HttpPost("invalidate/{id}")]
        public async Task<IActionResult> Invalidate([FromRoute]Guid id)
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
            lender.Invalidate();

            //_context.Lenders.Remove(lender);
            await _context.SaveChangesAsync();

            return this.AlpsActionOk();
        }
        // [HttpPost("importlender")]
        // public async Task<IActionResult> ImportLender([FromBody]string list)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var lenderList = list.Split(",");
        //     foreach (string lender in lenderList)
        //     {
        //         if (lender != string.Empty)
        //             _context.Lenders.Add(Lender.Create(lender, "Import", "Import"));
        //     }
        //     await _context.SaveChangesAsync();
        //     return this.AlpsActionOk();
        // }
    }
}