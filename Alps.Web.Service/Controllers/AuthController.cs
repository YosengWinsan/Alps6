
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Alps.Domain;
using Alps.Domain.SecurityMgr;
using Alps.Web.Service.Auth;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Alps.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AlpsContext _context;
        private readonly AlpsJwtOption _jwtOption;
        public AuthController(AlpsContext context, AlpsJwtOption jwtOption)
        {
            this._context = context;
            this._jwtOption = jwtOption;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto dto)
        {
            AlpsUser user = _context.AlpsUsers.Include(p => p.RoleUsers).ThenInclude(p => p.Role).FirstOrDefault(p => p.IDName == dto.Username && p.Password == dto.Password);
            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim("idName",user.IDName),
                    new Claim("name",user.Name)
                                };
                foreach (var roleUser in user.RoleUsers)
                {
                    claims.Add(new Claim("role", roleUser.Role.Name));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _jwtOption.Issuer,
                    audience: _jwtOption.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                return this.AlpsActionOk(new
                {
                    result = true,
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return this.AlpsActionOk(new { result = false, message = "密码有错" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(_context.AlpsUsers.Count(p=>p.IDName==dto.Username)>0)
            {
                return this.AlpsActionWarning("已有相同用户名了，请更改用户名");
            }
            AlpsUser newUser = AlpsUser.Create(dto.Username, dto.Password, dto.RealName, dto.IdentityNumber, dto.MobilePhoneNumber);
            newUser.AddRole(_context.AlpsRoles.FirstOrDefault(p => p.Name == "User"));
            _context.AlpsUsers.Add(newUser);
            if (_context.SaveChanges() ==2)
            {
                return this.AlpsActionOk();
            }
            else
            {
                return this.AlpsActionWarning("身份信息有误，注册失败");

            }
        }
    }
}