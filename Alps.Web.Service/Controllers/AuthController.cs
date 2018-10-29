
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Alps.Domain;
using Alps.Domain.AccountingMgr;
using Alps.Web.Service.Auth;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
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
            AlpsUser user = _context.AlpsUsers.Include(p => p.Roles).FirstOrDefault(p => p.IDName == dto.Username && p.Password == dto.Password);
            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim("idName",user.IDName),
                    new Claim("name",user.Name)
                                };
                foreach(var role in user.Roles)
                {
                    claims.Add(new Claim("role",role.Name));
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

    }
}