using System;
using System.Collections.Generic;
using System.Linq;
using Alps.Domain;
using Alps.Domain.SecurityMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AlpsContext _context;
        private readonly IActionDescriptorCollectionProvider _actionProvider;

        public UsersController(AlpsContext context, IActionDescriptorCollectionProvider actionProvider)
        {
            _context = context;
            _actionProvider = actionProvider;
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
            return this.AlpsActionOk(_context.AlpsRoles.Select(k => new RoleDto { ID = k.ID, Name = k.Name, Description = k.Description }));
        }
        [HttpGet("getrole/{id}")]
        public IActionResult GetRole(Guid id)
        {
            var role = _context.AlpsRoles.Find(id);
            if (role != null)
            {
                return this.AlpsActionOk(new RoleDto { ID = role.ID, Name = role.Name, Timestamp = role.Timestamp, Description = role.Description });
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
        [HttpPost("updateresources", Name = "更新资源")]
        public IActionResult UpdateResources()
        {
            var query = this._actionProvider.ActionDescriptors.Items.Select(p => new
            {
                Controller = p.RouteValues["Controller"],
                Action = p.RouteValues["Action"],
                Name = p.AttributeRouteInfo.Name == null ? string.Empty : p.AttributeRouteInfo.Name
            }).GroupBy(p => new { p.Controller, p.Action, p.Name })
               .Select(p => new AlpsResource { Controller = p.Key.Controller, Action = p.Key.Action, Name = p.Key.Name, UpdateTime = DateTimeOffset.Now });
            var existResource = _context.AlpsResources.ToList();
            var deletedR = _context.AlpsResources.Where(p => !query.Any(k => k.Controller == p.Controller && k.Action == p.Action && p.Name == k.Name));

            var insertedR = query.Where(p => !_context.AlpsResources.Any(k => k.Controller == p.Controller && k.Action == p.Action && p.Name == k.Name));
            _context.AlpsResources.RemoveRange(deletedR);
            _context.AlpsResources.AddRange(insertedR);
            _context.SaveChanges();
            return this.AlpsActionOk();
        }

        [HttpGet("getresources", Name = "获取资源")]
        public IActionResult GetResources()
        {
            return this.AlpsActionOk(_context.AlpsResources);
        }
        [HttpGet("getPermissions", Name = "获取权限")]
        public IActionResult GetPermissions()
        {
            // var query=from r in _context.AlpsResources
            //             from role in _context.AlpsRoles
            //             select new {ResourceID=r.ID,RoleID=role.ID} into list
            //             join p in  _context.Permissions on new {list.RoleID,list.ResourceID} equals new {p.RoleID,p.ResourceID}
            //             select new { list.ResourceID,list.RoleID,}
            
            return this.AlpsActionOk(_context.Permissions.Select(p=>new {p.ResourceID,p.RoleID}));
        }
        [HttpPost("savepermissions", Name = "更新权限")]
        public IActionResult SavePermissions( List<PermissionDto> dtos)
        {
            var pList=_context.Permissions;
            var newPermissions=dtos.Where(dto=>!pList.Any(p=>p.ResourceID== dto.ResourceID&& p.RoleID==dto.RoleID));
            var deletedPermissions=pList.Where(p=>!dtos.Any(dto=>p.ResourceID== dto.ResourceID&& p.RoleID==dto.RoleID));
            _context.Permissions.RemoveRange(deletedPermissions);
            foreach( var p in newPermissions)
            {
                _context.Permissions.Add(Permission.Create(p.ResourceID,p.RoleID));
            }
            _context.SaveChanges();
            return this.AlpsActionOk();
        }
    }
}