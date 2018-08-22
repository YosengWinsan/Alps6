
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alps.Domain;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Alps.Web.Service.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AlpsContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AlpsContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto dto)
        {
            if (dto.Username == "1" && dto.Password == "1")
            {
                var claims = new[]
                    {
                   new Claim(ClaimTypes.Name, dto.Username),
                   new Claim("username",dto.Username)
               };
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                    iss: The issuer of the token，token 是给谁的  发送者
                    audience: 接收的
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                var token = new JwtSecurityToken(
                    issuer: "jwttest",
                    audience: "jwttest",
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