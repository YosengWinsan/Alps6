using System;
using System.Threading.Tasks;
using Alps.Domain;
using Alps.Domain.ProductMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly AlpsContext _context;
        public PositionsController(AlpsContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult GetPositions()
        {
            return this.AlpsActionOk(_context.Positions);
        }
        [HttpGet("{id}")]
        public IActionResult GetPositions(Guid id)
        {

            return this.AlpsActionOk(_context.Positions.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> PostPosition([FromBody]Position dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Position p = Position.Create(dto.Name, dto.Number, dto.Warehouse);
            _context.Positions.Add(p);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(Guid id,[FromBody]Position dto)
        {
            if (!ModelState.IsValid || id!=dto.ID)
            {
                return BadRequest(ModelState);
            }

            Position p = _context.Positions.Find(dto.ID);
            if (p == null)
                return BadRequest();
            p.Name = dto.Name;
            p.Number = dto.Number;
            p.Warehouse = dto.Warehouse;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return this.AlpsActionOk();
        }
    }
}