using System;
using System.Linq;
using Alps.Domain;
using Alps.Domain.AccountingMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AlpsContext _context;

        public UsersController(AlpsContext context)
        {
            _context = context;
        }

        // GET: api/Commodity
        [HttpGet]
        public IActionResult GetUsers()
        {
            return this.AlpsActionOk(_context.AlpsUsers.Include(p => p.Roles).Select(p => new UserListDto
            {
                ID = p.ID,
                IDName = p.IDName,
                IdentityNumber = p.IdentityNumber,
                MobilePhoneNumber = p.MobilePhoneNumber,
                Name = p.Name,
                Roles = p.GetRoles()
            }));
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            return this.AlpsActionOk(_context.AlpsUsers.Include(p => p.Roles).Select(p => new UserDetailDto
            {
                ID = p.ID,
                IDName = p.IDName,
                IdentityNumber = p.IdentityNumber,
                MobilePhoneNumber = p.MobilePhoneNumber,
                Name = p.Name,
                Roles = p.Roles.Select(k => new RoleDto { ID = k.ID, Name = k.Name })
            }).FirstOrDefault(p => p.ID == id));
        }
        [HttpGet("getroles")]
        public IActionResult GetRoles()
        {
            return this.AlpsActionOk(_context.AlpsRoles.Select(k => new RoleDto { ID = k.ID, Name = k.Name }));
        }
        [HttpGet("getrole/{id}")]
        public IActionResult GetRole(Guid id)
        {
            var role = _context.AlpsRoles.Find(id);
            if (role != null)
            {
                return this.AlpsActionOk(new RoleDto { ID = role.ID, Name = role.Name });
            }
            else
                return this.AlpsActionWarning("无此身份信息");
        }
        [HttpPost("saverole")]
        public IActionResult GetRole(RoleDto dto)
        {
            return this.AlpsActionOk(dto);
        }
    }
}