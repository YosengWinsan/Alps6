using System;
using System.Linq;
using Alps.Domain;
using Alps.Domain.SecurityMgr;
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
            return this.AlpsActionOk(_context.AlpsRoles.Select(k => new RoleDto { ID = k.ID, Name = k.Name ,Description=k.Description}));
        }
        [HttpGet("getrole/{id}")]
        public IActionResult GetRole(Guid id)
        {
            var role = _context.AlpsRoles.Find(id);
            if (role != null)
            {
                return this.AlpsActionOk(new RoleDto { ID = role.ID, Name = role.Name,Timestamp=role.Timestamp ,Description=role.Description});
            }
            else
                return this.AlpsActionWarning("无此身份信息");
        }
        [HttpPost("saverole")]
        public IActionResult SaveRole(RoleDto dto)
        {
            if (dto.ID == Guid.Empty)
            {
                var role = AlpsRole.Create(dto.Name, dto.Description);
                _context.AlpsRoles.Add(role);
            }
            else
            {

                AlpsRole role = _context.AlpsRoles.Find(dto.ID);
                if (role == null)
                    return this.AlpsActionWarning("身份信息不存在");
                else
                {
                    if (!role.Timestamp.SequenceEqual(dto.Timestamp))
                        return this.AlpsActionWarning("身份信息已改变");
                    else
                    {
                        role.Name = dto.Name;
                        role.Description = dto.Description;
                    }
                }
            }
            try
            {
                _context.SaveChanges();
                return this.AlpsActionOk();
            }
            catch { return this.AlpsActionWarning("保存失败"); }


        }
    }
}