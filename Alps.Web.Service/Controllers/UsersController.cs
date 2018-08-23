using System;
using System.Linq;
using Alps.Domain;
using Alps.Domain.AccountingMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.controllers
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
            return this.AlpsActionOk(_context.AlpsUsers.Include(p => p.Roles).Select(p=>new UserDetailDto{
                ID = p.ID,
                IDName = p.IDName,
                IdentityNumber = p.IdentityNumber,
                MobilePhoneNumber = p.MobilePhoneNumber,
                Name = p.Name,Roles=p.Roles.Select(k=>new RoleDto{ID=k.ID,Name=k.Name})
            }).FirstOrDefault(p => p.ID == id));
        }

    }
}